# Gameplay Mechanics — Reverse-Engineered Reference

> **Purpose:** Every legacy mechanic reverse-engineered from source, with strengths, bugs and extensibility notes.
> **Owner:** Franco Fusaro · **Status:** Baseline (as-built) · **Last Updated:** 2026-07-10
> **Related:** [CodeReview](CodeReview.md), [../../design/gameplay/Movement](../../design/gameplay/Movement.md), [../../production/Backlog](../../production/Backlog.md)

Every mechanic below is reconstructed from source. Citations use
`Script.cs:Method`. Physics is Rigidbody2D-based; collision detection is done
almost entirely via `Collider2D.IsTouchingLayers(LayerMask)` and trigger
callbacks.

## Layers & Tags (foundation for every mechanic)

From `ProjectSettings/TagManager.asset`:

| Layer | Used by |
|-------|---------|
| `Ground` | Jump ground-check (`Player.PlayerIsOnGround`) |
| `Ladder` | Climb detection (`Player.PlayerIsTouchingLadder`) |
| `Enemy` | Death (`Player.Die`) |
| `Hazards` | Death (`Player.Die`) |
| `Water`, `Background`, `Player`, `Interactibles` | Sorting/organization |

Sorting layers: Default, Background, Ladders, Enemies, Hazards, Interactibles, Player, Foreground.

**No user tags are defined** (`tags: []`) — everything is layer-driven.

---

## 1. Horizontal Movement (Walk)

- **Purpose:** Move the cat left/right.
- **Implementation:** `Player.MoveHorizontally()` reads `CrossPlatformInputManager.GetAxis("Horizontal")` (−1..+1) and sets `rigidBody.velocity = (throw * walkSpeed, velocity.y)`. `walkSpeed = 3`.
- **Animation:** `Player.ChangeWalkingAnimationState()` sets Animator bool `Walking` when `|velocity.x| > Epsilon`.
- **Sprite facing:** `Player.FlipSprite()` sets `localScale.x = Sign(velocity.x)` when moving.
- **Scripts:** `Player.cs`. **Prefabs:** `Player.prefab` (Rigidbody2D, Animator, CapsuleCollider2D, BoxCollider2D).
- **Strengths:** Simple, deterministic, framerate-independent (velocity-based).
- **Weaknesses:** Directly overwrites velocity every frame → **no acceleration/deceleration, no air control tuning, instant stop** (arcadey but stiff). Ignores `GetAxisRaw`, so smoothing depends on input axis gravity settings.
- **Possible bugs:** Setting velocity.x directly fights moving-platform parenting; on a platform the player inherits parent transform while also self-driving velocity.
- **Extensibility:** Add `acceleration`/`maxSpeed` lerp; expose an air-control multiplier.

## 2. Jump

- **Purpose:** Single jump from the ground.
- **Implementation:** `Player.Jump()` — early-returns unless `PlayerIsOnGround()` (feet `BoxCollider2D.IsTouchingLayers("Ground")`), then on `GetButtonDown("Jump")` adds `(0, jumpSpeed)` to velocity. `jumpSpeed = 5`.
- **Weaknesses / bugs:**
  - **`velocity += (0, jumpSpeed)`** adds to current vertical velocity → jump height varies with residual y-velocity (e.g. jumping the instant you land vs. standing still). Most implementations *set* velocity.y.
  - **No coyote time** — you cannot jump a frame after walking off a ledge.
  - **No jump buffering** — a press just before landing is dropped.
  - **No variable jump height** — holding vs. tapping is identical.
  - Ground check keys only on the `Ground` layer, so you **cannot jump off enemies or moving platforms** unless those are also on `Ground`.
- **Extensibility:** Introduce coyote-time timer, jump buffer, and a shorter-hop-on-release. High player-feel payoff.

## 3. Ladder Climbing

- **Purpose:** Vertical traversal on ladders.
- **Implementation:** `Player.ClimbLadder()`:
  - If **not** touching `Ladder` layer (feet collider): restore `gravityScale`, clear `Climbing` animation, return.
  - Else `MoveVertically()` sets `velocity.y = throw * climbSpeed` (`climbSpeed = 3`), sets Animator `Climbing` bool to `PlayerHasVerticalSpeed()`, and **zeroes gravity** while on the ladder.
- **Weaknesses / bugs:**
  - Detection uses the **feet** BoxCollider, so climbing engages/disengages based only on where the feet are; you can slide off the top awkwardly.
  - Gravity is set to 0 every frame while touching → if you hold nothing, you **hang motionless** on the ladder (no slow slide), which reads oddly.
  - The `Climbing` animation only plays while `velocity.y != 0`; standing still on a ladder shows idle mid-air.
- **Extensibility:** Snap X to ladder center; add climb-idle pose; allow jump-off-ladder.

## 4. Death / Damage / Respawn

- **Purpose:** Kill the cat on contact with enemies/hazards; consume a life.
- **Implementation:** `Player.Die()` — if **body** CapsuleCollider `IsTouchingLayers("Enemy","Hazards")` and `isAlive`:
  - `isAlive = false`, `velocity = deathKick` (`(15, 20)`), trigger Animator `Dying`, disable body collider, start `ProcessDeath()` coroutine.
  - `ProcessDeath()` waits `SecondsToReloadOnDeath = 1s` (realtime) then calls `FindObjectOfType<GameSession>().ProcessPlayerDeath()`.
- **Session handling:** `GameSession.ProcessPlayerDeath()` — if `playerLives > 1`, `TakeLife()` + `LevelLoader.RestartLevel()`; else `LoadMainMenu()` + `Destroy(gameObject)`.
- **Health model:** **One-hit kill.** "Health" is really *lives* (default 3), tracked in `GameSession.playerLives`; there is **no per-hit HP**. A `heart.png` sprite exists but no HP system uses it.
- **Weaknesses / bugs:**
  - Only the **body** collider triggers death; the **feet** box stays enabled → potential residual collisions.
  - `WaitForSecondsRealtime` ignores `Time.timeScale`, deliberately so death still resolves during slow-mo — but it means death during the level-exit slow-mo behaves inconsistently.
  - No i-frames, no knockback on non-lethal hits (there are none).
  - Death is detected in `Update` via `IsTouchingLayers`; a fast pass-through between frames could be missed **(ASSUMPTION — tunnelling depends on collider sizes/speed)**.
- **Extensibility:** Introduce real HP + hearts UI (art already exists), i-frames, hazard-specific responses.

## 5. Coins / Collectibles / Score

- **Purpose:** Reward exploration; drive score.
- **Implementation:** `CoinPickup.OnTriggerEnter2D` — if the other collider **`is CapsuleCollider2D`** (i.e. the player's body): play `coinPickUpSFX` via `AudioSource.PlayClipAtPoint` at the player position, `FindObjectOfType<GameSession>().AddToScore(scoreValue)` (`scoreValue = 50`), destroy the coin.
- **Animation:** `Coin.controller` + `CoinSpin.anim` (idle spin).
- **Weaknesses / bugs:**
  - **Player identity is inferred by collider type** (`is CapsuleCollider2D`) — any object with a CapsuleCollider2D would collect the coin. Fragile.
  - `PlayClipAtPoint` spawns a throwaway AudioSource each pickup (minor GC/allocation; volume not tied to master volume setting).
  - Collected coins are destroyed with **no persistence** → they respawn on level restart (consistent with score not persisting either).
- **Extensibility:** Tag/interface-based player check; pooled pickup SFX; persistent collected-coin state.

## 6. Level Exit / Progression

- **Purpose:** Advance to the next level.
- **Implementation:** `LevelExit.OnTriggerEnter2D` → `FindObjectOfType<LevelLoader>().LoadNextLevel()`. `LevelLoader.NextLevel()` sets `Time.timeScale = LevelExitSlowMotionFactor (0.2)`, waits `LevelLoadDelay = 2s` realtime, restores timescale, then loads `currentSceneIndex + 1`, destroying the `ScenePersist`.
- **Weaknesses / bugs:**
  - **Any** collider entering the exit triggers it (no player check) — an enemy or platform could advance the level.
  - `NextLevel()` calls `Destroy(FindObjectOfType<ScenePersist>().gameObject)` then immediately `FindObjectOfType<ScenePersist>().gameObject.SetActive(false)` — the **second `FindObjectOfType` can return null** (object just destroyed) → potential `NullReferenceException`. Same double-call anti-pattern in `LoadMainMenu()`.
  - Loading by `index + 1` past the last level would load out of range; relies on `Success` being the next index after Level 4.
- **Extensibility:** Player-only check; a proper `SceneFlowManager`; guard the ScenePersist teardown.

## 7. Enemy AI (Patrol Rat)

- **Purpose:** Simple back-and-forth ground enemy that kills on contact.
- **Implementation:** `EnemyMovement.Update()` drives `velocity.x = ±moveSpeed` (`moveSpeed = 1`) based on `transform.localScale.x` sign. `OnTriggerExit2D` — when the enemy's trigger exits a `Tilemap` collider, `FlipSprite()` reverses `localScale.x` (turns around at platform edges). **Prefab:** `EnemyRat.prefab`, on the `Enemy` layer.
- **Strengths:** Cheap, no pathfinding, self-contained edge detection.
- **Weaknesses / bugs:**
  - Turnaround depends on **exiting a Tilemap trigger** — needs an edge trigger setup; behavior is sensitive to collider geometry.
  - `FlipSprite` uses `Sign(-velocity.x)`, coupling facing to velocity while `Update` derives velocity from facing → works but is circular/fragile.
  - No player detection, no attack, no death (enemies can't be defeated — no stomp mechanic).
- **Extensibility:** Add stomp-to-kill (check player feet vs. enemy top), raycast edge/wall detection, chase behavior, an enemy base class.

## 8. Moving Platforms

- **Purpose:** Carry the player across gaps.
- **Implementation:**
  - `MovingPlatform.cs` moves `platform` toward `waypoints[i]` via `Vector2.MoveTowards` at `speed = 0.5`; advances waypoint index at arrival, loops back to 0. `move` bool gates motion. Destroys itself if `waypointIndex` exceeds the list (guard path).
  - `Platform.cs` (on the rideable surface, trigger): on `OnTriggerStay2D` **re-parents the rider** (`otherCollider.transform.parent = transform`) so it moves with the platform; clears parent on exit. If `startMovingOnPlayerCollsion`, first contact sets the parent `MovingPlatform.move = true`.
- **Weaknesses / bugs:**
  - **Re-parenting the player** changes its transform hierarchy every frame of contact — can introduce scale/jitter issues and interacts badly with `Player.FlipSprite` (which sets `localScale`), potentially inheriting platform scale.
  - `OnTriggerStay2D` re-parents **any** collider, not just the player (enemies, coins could get parented).
  - `Vector2.kEpsilon` arrival threshold with `MoveTowards` is fine, but at `speed 0.5` the platform is slow; tuning per-platform is manual.
  - The `Destroy(gameObject)` else-branch is effectively unreachable given the loop-back logic (dead code / latent bug).
- **Extensibility:** Move the rider via velocity or a platform-rider component instead of re-parenting; ping-pong vs. loop options; ease-in/out.

## 9. Parallax & Scrolling Backgrounds

- **`LayerParallax.cs`:** Moves a background layer opposite to the `viewTarget` (camera) delta, scaled by `scrollSpeed + offSet`, lerped by `smoothing`. Multiple instances at different speeds create depth.
- **`VerticalScroll.cs`:** Translates a transform upward at `scrollRate` (used for a scrolling background element, e.g. stars/clouds). **(ASSUMPTION — exact scene usage not verified per-scene.)**
- **Weaknesses:** Parallax reads camera position directly rather than being camera-driven; `viewTarget` must be wired per background. No tiling/wrap, so finite backgrounds can reveal edges.

## 10. Camera

- **Implementation:** **Cinemachine** (`Cameras.prefab` contains multiple Cinemachine components incl. a **State Driven Camera**, driven by `Animations/State Driven Camera Blends.asset`). Follows the player; likely swaps vcams by player state. **(ASSUMPTION — exact state bindings not fully traced.)**
- **Strengths:** Using Cinemachine is a good, modern-for-2018 choice (smoothing, dead zones, blends for free).
- **Weaknesses:** State-Driven-Camera adds complexity for a simple follow; confiner/bounds usage not confirmed (player may see outside level edges).

## 11. Audio System

- **Music:** `MusicPlayer.cs` — `DontDestroyOnLoad` singleton, picks a random clip from `audioClips[]`, replays a new random track when the current finishes (`Update` polls `isPlaying`). Volume initialized from `PlayerPrefsController.GetMasterVolume()`; `SetMasterVolume()` exposed for the options slider. `MenuMusic.cs` is a menu-only variant (singleton, sets volume, but does **not** start playback — relies on an AudioSource set to play-on-awake). **(ASSUMPTION.)**
- **SFX:** Coin pickup via `AudioSource.PlayClipAtPoint` (not routed through master volume).
- **Weaknesses:** Polling `isPlaying` in `Update` for track chaining; SFX bypasses the volume setting; no audio mixer / groups.

## 12. UI Interactions

- **HUD:** `GameSession` writes `livesText`/`scoreText` (TextMeshProUGUI) on start and on change.
- **Menus:** `Menu.StartFirstLevel()` (build index 1), `OptionsControllers` (volume slider → PlayerPrefs, "Save and Exit", "Set Defaults"). Buttons wire to `LevelLoader` methods (`LoadMainMenu`, `LoadOptionsMenu`, `QuitGame`).
- **Weaknesses:** Difficulty slider is **commented out** everywhere; `PlayerPrefsController.SetDifficulty` has a copy-paste bug (calls `SetMasterVolume` instead of writing difficulty).

---

## Mechanic Coverage Matrix

| Mechanic | Present? | Script |
|----------|:--:|--------|
| Walk / run | ✅ | Player.cs |
| Jump | ✅ (single) | Player.cs |
| Double jump | ❌ | — |
| Wall jump / slide | ❌ | — |
| Dash | ❌ | — |
| Crouch | ❌ | — |
| Ladder climb | ✅ | Player.cs |
| Attack | ❌ | — |
| Health (HP) | ❌ (lives only) | GameSession.cs |
| Lives | ✅ | GameSession.cs |
| Coins / score | ✅ | CoinPickup.cs |
| Checkpoints | ❌ (level-restart only) | LevelLoader.cs |
| Save system | ⚠️ options only | PlayerPrefsController.cs |
| Respawn | ✅ (restart level) | GameSession/LevelLoader |
| Enemy AI | ✅ (patrol) | EnemyMovement.cs |
| Moving platforms | ✅ | MovingPlatform/Platform.cs |
| Switches | ⚠️ partial (`startMovingOnPlayerCollsion`) | Platform.cs |
| Doors | ❌ | — |
| Hazards | ✅ (layer-based) | Player.cs |
| Camera | ✅ (Cinemachine) | Cameras.prefab |
| Parallax | ✅ | LayerParallax.cs |

---

## Top Gameplay Improvements

### 🔴 High Impact
1. **Jump feel overhaul** — coyote time, jump buffering, variable jump height, and *set* (not *add*) vertical velocity. Cheapest, biggest boost to a platformer's feel. (`Player.Jump`)
2. **Real HP + hearts UI** — the `heart.png` art already exists; move from one-hit-kill to an HP model with i-frames and knockback. Adds depth and fairness. (`Player.Die`, `GameSession`)
3. **Checkpoints** — level restart on every death is punishing. Add checkpoint triggers that set the respawn point. (new `Checkpoint.cs` + `GameSession`)
4. **Player-only trigger checks** — coins, exits, and platform parenting should verify it's the player (tag/interface), not "any collider" / "any capsule". Fixes several latent bugs. (`CoinPickup`, `LevelExit`, `Platform`)

### 🟡 Medium Impact
5. **Enemy stomp-to-kill** — let the cat defeat enemies by landing on them; add enemy death anim. (`EnemyMovement`, `Player`)
6. **Score/coin persistence & totals** — carry score across levels; show a run total on the Success screen. (`GameSession`)
7. **Ladder polish** — snap-to-center, climb-idle pose, jump-off-ladder. (`Player.ClimbLadder`)
8. **Robust moving platforms** — replace re-parenting with a rider component to kill jitter/scale bugs. (`Platform`, `MovingPlatform`)

### 🟢 Nice to Have
9. Double-jump / dash as unlockable abilities.
10. SFX routed through master volume + an AudioMixer.
11. Camera confiner so the player never sees past level bounds.
12. A real lose screen (currently referenced but missing) and a pause menu.

# Character Documentation

> **Purpose:** Reference for the two legacy characters that exist in code today: the player Cat and the enemy Rat.
> **Owner:** Franco Fusaro · **Status:** Baseline (as-built) · **Last Updated:** 2026-07-10
> **Related:** [../../design/narrative/Characters](../../design/narrative/Characters.md), [GameplayMechanics](GameplayMechanics.md)

The game has **two characters**: the player Cat and the enemy Rat. There are no
NPCs, bosses, or friendly characters.

---

## 🐱 Cat (Player)

| Field | Value |
|-------|-------|
| Role | Player-controlled protagonist |
| Prefab | `Assets/Prefabs/Player.prefab` |
| Script | `Assets/Scripts/Player.cs` (155 LOC) |
| Layer | `Player` |
| Animator | `Assets/Animations/Player.controller` |
| Sprites | `Sprites/Player/sprite_base_addon_2012_12_14.png`, `cat_climb2.png` |
| Components | Rigidbody2D, CapsuleCollider2D (body), BoxCollider2D (feet), Animator, SpriteRenderer |

### Abilities
| Ability | Tuning field | Default | Mechanic |
|---------|--------------|:-------:|----------|
| Walk | `walkSpeed` | 3 | Horizontal velocity from input |
| Jump | `jumpSpeed` | 5 | Adds vertical velocity when grounded |
| Climb | `climbSpeed` | 3 | Vertical velocity on ladders; gravity → 0 |
| Die | `deathKick` (15,20), `SecondsToReloadOnDeath` (1) | — | On enemy/hazard contact |

### Animation states
| State | Clip | Trigger |
|-------|------|---------|
| Idle | `CatIdle.anim` | default |
| Walking | `CatWalk.anim` | bool `Walking` = has horizontal speed |
| Climbing | `CatClimb.anim` | bool `Climbing` = vertical speed on ladder |
| Dying | `CatDeath.anim` | trigger `Dying` on death |

> **Missing:** jump/fall/land and hurt animations (see `docs/design/art/StyleGuide.md`).

### Interactions
- **Coins:** collected by body collider (`CoinPickup` checks `is CapsuleCollider2D`).
- **Ladders:** feet collider vs. `Ladder` layer.
- **Ground:** feet collider vs. `Ground` layer (jump gate).
- **Enemies/Hazards:** body collider vs. `Enemy`/`Hazards` layers → death.
- **Moving platforms:** re-parented to the platform while riding (`Platform.cs`).
- **Level exit:** entering the exit trigger advances the level.

### Dependencies
- `GameSession` (via `FindObjectOfType`) for death handling.
- `CrossPlatformInputManager` for input (Horizontal/Vertical/Jump).
- Cinemachine camera follows this transform.

### Improvement ideas
- Coyote time + jump buffer + variable jump height (biggest feel win).
- Set (not add) jump velocity.
- Feet-centered pivot to stop `localScale`-flip jitter.
- Real HP + i-frames; wire the existing `heart.png`.
- Player state machine to replace the flat `Update` if-chain.

---

## 🐀 Rat (Enemy)

| Field | Value |
|-------|-------|
| Role | Ground patrol hazard (one-hit-kills the player on contact) |
| Prefab | `Assets/Prefabs/EnemyRat.prefab` |
| Script | `Assets/Scripts/EnemyMovement.cs` (49 LOC) |
| Layer | `Enemy` |
| Animator | `Assets/Animations/Enemy.controller` |
| Sprite | `Sprites/Enemies/mouse.png` |
| Components | Rigidbody2D, Collider2D(s), Animator, SpriteRenderer |

### Behavior / "AI"
- Moves at constant `moveSpeed = 1` in the direction of `transform.localScale.x`.
- Turns around when its trigger **exits a `Tilemap`** (`OnTriggerExit2D` → `FlipSprite`),
  i.e. at platform edges.
- No player detection, no attack logic, no death state — contact damage is entirely
  handled on the **player** side (`Player.Die` reads the `Enemy` layer).

### Animation states
| State | Clip |
|-------|------|
| Walk | `EnemyWalk.anim` |

> **Missing:** idle, turn, and death/squashed animations.

### Interactions
- Kills the player on body-collider contact (player-side check).
- Cannot currently be defeated (no stomp mechanic).

### Dependencies
- Relies on a `Tilemap` collider at platform edges for turnaround — behavior is
  coupled to level geometry/collider setup.

### Improvement ideas
- Stomp-to-kill: compare player feet vs. enemy top; add squashed frame + score.
- Raycast-based edge/wall detection instead of Tilemap trigger exit (more robust).
- An `Enemy` base class / `IDamageable` interface for future enemy types.
- Recolor variants for level themes (cheap variety from one base sprite).

---

## Character comparison

| Aspect | Cat | Rat |
|--------|-----|-----|
| Controlled by | Player input | Script |
| Can die | Yes (lives) | No |
| Deals damage | No | Yes (contact) |
| Physics | Rigidbody2D velocity | Rigidbody2D velocity |
| Animations | 4 states | 1 state |
| Complexity | 155 LOC | 49 LOC |

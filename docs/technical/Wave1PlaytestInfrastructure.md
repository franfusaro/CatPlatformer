# Wave 1 Playtest Infrastructure

> **Purpose:** Everything needed to make Wave 1 playtesting sessions fast and productive — debug
> tools, cheats, logging, metrics, and the build/scene plumbing they need. This is *tooling*
> around `docs/technical/Wave1Architecture.md`'s systems, not a new gameplay system.
> **Owner:** Franco Fusaro · **Status:** Draft (proposed — awaiting review gate) · **Last Updated:** 2026-07-26
> **Related:** [Wave1Architecture](Wave1Architecture.md), [../production/Wave1Backlog](../production/Wave1Backlog.md), [../production/Roadmap](../production/Roadmap.md), [../production/Playbook](../production/Playbook.md), [baseline/CodeReview](baseline/CodeReview.md), [../production/KnownRisks](../production/KnownRisks.md)

This doc does **not design gameplay**. Every cheat and tool below observes or drives
Wave 1's existing systems through their existing public APIs — it never adds a mechanic,
tunes a number, or decides something the Design Bible or `Wave1Architecture.md` left open.
Where a cheat needs a code touchpoint on a real gameplay class, that touchpoint is called
out explicitly and kept to the smallest possible addition (see
[Touchpoints in existing systems](#touchpoints-in-existing-systems)).

**Goal, stated once so every decision below can be checked against it:** minimize the time
between "I changed something" and "I'm looking at its effect in the greybox level." Every
section is in service of that, nothing else.

## Contents

1. [Principles](#principles)
2. [Systems](#systems)
3. [Touchpoints in existing systems](#touchpoints-in-existing-systems)
4. [Assembly & build configuration](#assembly--build-configuration)
5. [Sequencing into the Wave 1 backlog](#sequencing-into-the-wave-1-backlog)
6. [Playtest checklist](#playtest-checklist)
7. [Explicitly out of scope](#explicitly-out-of-scope)

---

## Principles

1. **Observe and invoke, never fork.** Every cheat calls a real public method
   (`AbilityGrantService.Grant`, `GameManager.RestartFromCheckpoint`, `ActiveCatManager.TrySwap`)
   instead of reaching into private state or duplicating logic. A debug-triggered ability
   unlock fires the same toast/audio/HUD path a real pickup would — so a playtest exercises
   the real code, not a shortcut around it.
2. **Free from what already exists.** Wave 1 already has ~11 typed event channels
   ([event table](Wave1Architecture.md#event-architecture)) covering every state change worth
   knowing about. Logging and metrics are built as *one more listener* on each channel, not a
   parallel instrumentation system.
3. **Zero cost when it isn't there.** Debug tooling must not exist in a non-development
   build — not "hidden behind a flag," but not compiled in at all. See
   [Assembly & build configuration](#assembly--build-configuration). This is what keeps it
   consistent with CLAUDE.md's WebGL-size guardrail without a second conversation about it.
4. **No service accounts.** No remote analytics/crash-reporting SaaS. This is a solo project
   at the pre-alpha/one-playtester stage; the payoff of standing up a third-party pipeline
   doesn't clear its setup and trust cost yet. Revisit if Wave 3+ puts a build in front of
   playtesters you aren't sitting next to.

---

## Systems

### DebugTools assembly

- **Purpose:** the home for everything in this doc — kept out of `Core`/`Gameplay`/`UI`
  entirely rather than sprinkling `#if` blocks through production code.
- **Why a fourth assembly, contradicting Wave1Architecture's "stick to three":** that decision
  was about avoiding bookkeeping overhead for *shipping* code split too finely. Debug tooling
  is different in kind — it legitimately needs to read `Gameplay` state **and** render through
  `UI`-shaped overlays, which the one dependency rule that matters (`UI` and `Gameplay` only
  ever meet through `Core`) explicitly forbids for shipping code. A separate assembly that
  *only* debug code lives in, and that nothing else references back into, adds no bookkeeping
  to the three production assemblies and costs nothing at runtime once stripped (see below).
  Flag this deviation at the review gate; if accepted, it's worth a one-line addendum next to
  ADR-0002 rather than a full new ADR.
- **Public API:** none — this is an assembly boundary, not a class.
- **Dependencies:** `TwoCats.Core`, `TwoCats.Gameplay`, `TwoCats.UI`. Nothing references it back.
- **Risks:** none beyond the asmdef-erosion risk `Wave1Architecture.md` already names for the
  other three — same mitigation (compile-error enforcement, not review discipline).

### DebugHarness

- **Purpose:** the single persistent owner of every tool below — the debug-world equivalent
  of `GameManager`.
- **Responsibilities:** boot alongside `GameManager` (via `Singleton<T>`, same base the three
  production persistents use); own the toggle key for the debug overlay (backtick, off by
  default each session); host `GameplayEventLogger` and `PlaytestMetrics`; expose the cheat
  command list to the debug menu UI.
- **Public API:**
  ```csharp
  class DebugHarness : Singleton<DebugHarness> {
      bool OverlayVisible { get; }
      void ToggleOverlay();
      GameplayEventLogger Log { get; }
      PlaytestMetrics Metrics { get; }
  }
  ```
- **Dependencies:** Event system (subscribes, never publishes gameplay events), `GameManager`/
  `ActiveCatManager`/`AbilityGrantService`/`SceneFlowManager` (calls their existing public APIs
  for cheats).
- **Extension points:** new cheats are new methods here calling existing systems' APIs — no
  new event channels needed unless a genuinely new observable state appears.
- **Risks:** the temptation to let this become a second `GameManager` with its own gameplay
  opinions. **Rule, same shape as `GameManager`'s:** `DebugHarness` orchestrates debug UX, it
  never contains gameplay logic — every cheat body is one call into a real system's public API.

### Debug HUD

- **Purpose:** at-a-glance state without opening the Unity Editor — the thing that makes a
  WebGL playtest build as inspectable as an in-Editor session.
- **Responsibilities:** render, via `OnGUI` (deliberately IMGUI, not a Canvas — see below):
  FPS/frame time; per-cat HP (current/max), downed state, unlocked abilities, exclusivity-group
  lock; current checkpoint id; `CombatContext` flag; a scrolling tail of the last ~20 entries
  from `GameplayEventLogger`.
- **Why `OnGUI`, not a Canvas:** the real HUD (`TwoCats.UI`) is production-quality UI that this
  doc must not touch or fight for canvas space. `OnGUI` is self-contained inside
  `TwoCats.DebugTools`, draws over anything, and needs zero prefab/scene wiring — the debug
  overlay for a solo dev doesn't need to look good, it needs to require zero setup.
- **Public API:** none new beyond `DebugHarness.ToggleOverlay()`.
- **Dependencies:** reads `HealthComponent`, `ActiveCatManager`, `RuntimeGameState`,
  `CombatContext`, `GameplayEventLogger` — read-only, never mutates.
- **Extension points:** each new gameplay system that lands in later Wave 1 tasks gets one more
  read-only line here; this is meant to grow incrementally, not be "finished" up front.
- **Risks:** low. The one discipline: HUD code reads state, cheat code calls APIs — don't let a
  HUD line quietly start mutating something to "keep the display consistent."

### Input visualization

- **Purpose:** make the `IInputReader` seam ([Wave1Architecture](Wave1Architecture.md#input-layer))
  visible, which matters specifically for verifying `PartnerAI`'s synthetic-input behavior
  (Technical risk #3 in that doc) without adding any new hook.
- **Responsibilities:** for each `CatController`, read whichever `IInputReader` is currently
  assigned and render its five fields (`MoveAxis`, `JumpDown`, `SwapDown`, `AbilityDown`,
  `InteractDown`) plus a label for which concrete reader type it is (`PlayerInputReader` vs.
  `PartnerAI`). This is pure display over an interface that already exists — no new reader
  capability needed.
- **Public API:** none — a rendering concern inside the Debug HUD panel.
- **Dependencies:** `IInputReader` (read-only), `ActiveCatManager` (which reader is assigned
  to which cat).
- **Extension points:** none needed at Wave 1 scope.
- **Risks:** none — it's read-only reflection over five booleans/floats.

### Camera debugging

- **Purpose:** make swap-triggered retargeting ([Camera Integration](Wave1Architecture.md#camera-integration))
  observable and let a freeze/step be used while chasing a specific frame of a camera bug.
- **Responsibilities:** show current vcam `Follow` target name in the Debug HUD; a
  freeze-camera toggle (sets the vcam's `Priority` to hold position, doesn't fight Cinemachine's
  blending logic); a single-frame-step button useful while `Time.timeScale` is 0.
- **Public API:** requires one small, justified addition to `CameraDirector` — see
  [Touchpoints](#touchpoints-in-existing-systems).
- **Dependencies:** `CameraDirector`, Cinemachine.
- **Extension points:** none needed at Wave 1 scope — puzzle-room dual-cat framing (already
  flagged as open in `Wave1Architecture.md`) gets its own debug view only if/when it exists.
- **Risks:** none — this is inspection, not new camera behavior.

### Gameplay event logger

- **Purpose:** turn the Wave 1 event architecture into a free audit trail. Every producer
  already raises events; this is one more listener, added once, per channel.
- **Responsibilities:** subscribe to all ~11 channels in
  [Wave1Architecture's event table](Wave1Architecture.md#event-architecture); append each
  `(timestamp, channel, payload.ToString())` to a fixed-size ring buffer (~200 entries); mirror
  every entry to `Debug.Log` so it also lands in the browser console / `Player.log` for
  post-session review without needing the in-game overlay open at the time.
- **Public API:**
  ```csharp
  class GameplayEventLogger {
      IReadOnlyList<string> RecentEntries { get; }
      void Clear();
  }
  ```
- **Dependencies:** Event system only — genuinely a leaf, same as the channels themselves.
- **Extension points:** a new Wave 2/3 event channel is a one-line subscription add here; treat
  this file as living in lockstep with the event table the same way
  `Wave1Architecture.md`'s [technical risk #4](Wave1Architecture.md#technical-risks) already
  asks the event table itself to be kept current.
- **Risks:** none functional. Don't let this grow into a structured-logging framework
  (categories, log levels, filters) — a flat ring buffer plus `Debug.Log` is the entire scope
  a one-playtester project needs.

### Playtest metrics

- **Purpose:** replace "how did that session feel, roughly" with a small number of concrete
  counters Franco can glance at right after a session, feeding
  [Playbook's playtest-notes step](../production/Playbook.md#stage-by-stage).
- **Responsibilities:** count, per session (reset on `DebugHarness` boot): elapsed session time;
  swap count (`OnCatSwapped`); downs/revives per cat (`OnCatDowned`/`OnCatRevived`); checkpoint
  restarts (`OnBothCatsDowned`); ability activations per ability
  (`CatAbility.TryActivate` return-true, observed the same way the logger observes events —
  see the one small touchpoint below); time from level start to first `OnPuzzleRoomEntered`.
- **Public API:**
  ```csharp
  class PlaytestMetrics {
      TimeSpan SessionDuration { get; }
      int SwapCount { get; }
      IReadOnlyDictionary<CatId,int> DownsByCateory { get; }
      IReadOnlyDictionary<AbilityDefinition,int> ActivationsByAbility { get; }
      string ToSummaryString();  // rendered in an end-of-session HUD panel
      void Reset();
  }
  ```
- **Dependencies:** Event system; no new channels — everything it counts already fires one.
- **Extension points:** new counters as new Wave 1 systems land, same incremental posture as
  the Debug HUD.
- **Risks:** the pull toward an export/upload pipeline. Explicitly out of scope for Wave 1 (see
  [below](#explicitly-out-of-scope)) — an on-screen summary string is the entire deliverable;
  Franco copies what's worth keeping into playtest notes by hand, same as any other observation.

### Cheat commands

All cheats are thin methods on `DebugHarness` that call existing public APIs. Listed here as
a catalogue, not as new systems:

| Cheat | Calls | Notes |
|---|---|---|
| Force swap | `ActiveCatManager.TrySwap()` | Exercises the real swap path, including the `CanRelinquishControl` guard — a cheat that bypassed the guard would be testing something that can't happen in real play. |
| Unlock ability (one / all) | `AbilityGrantService.Grant(cat, ability, GrantSource.DevCheat)` | New enum value only — see [Touchpoints](#touchpoints-in-existing-systems). Fires the same `OnAbilityUnlocked` path a pickup would. |
| God mode / infinite HP | `HealthComponent.SetInvulnerable(bool)` | New method, small — see Touchpoints. Damage still applies visually/audibly (`OnDamaged` still fires) but never reaches 0, so the rest of the game reacts normally while testing without dying constantly. |
| Instant revive both cats | `DownedState.Revive()` on each cat | No touchpoint needed — already public. |
| Teleport to debug spawn point | scene-placed `Transform`s, teleports both `CatController`s | See [Level reload & checkpoint skipping](#level-reload-workflow--checkpoint-skipping) below — deliberately not a second checkpoint system. |
| Time scale slider | `Time.timeScale` | No gameplay touchpoint at all; useful for feel-checking jump arcs and swap timing in slow motion, which Playbook names as the one thing automation can't judge. |
| Reload current scene / full app restart | `SceneFlowManager.LoadScene(currentId)` / re-trigger boot | Two distinct speeds — see below. |

**Deliberately not included:** noclip/freefly camera movement. It has no analog in any real
system (`CatController` has no such mode), so it would be a genuinely new movement path built
solely for debugging — more debug-tooling surface than Wave 1's actual bug surface justifies.
Add it later only if a specific, recurring bug demands seeing geometry from an arbitrary angle
that the fixed follow-camera can't reach.

### Level reload workflow & checkpoint skipping

- **Two distinct reload speeds, kept separate because they exercise different things:**
  1. **Scene reload** (`SceneFlowManager.LoadScene(currentSceneId)`) — fast, re-enters the
     greybox scene fresh. Tests level content, not the boot sequence.
  2. **Full restart** — tears down the persistent singletons and re-runs boot from scratch.
     Slower, but it's the only way to actually exercise `GameManager`/`SceneFlowManager`/
     `AudioService`'s init-order code, which a scene-only reload never touches after the first
     time.
- **Checkpoint skipping ≠ a second checkpoint system.** `Wave1Architecture.md` is explicit that
  Wave 1 ships exactly **one** checkpoint (level start) and deliberately not a
  multi-checkpoint manager. Building "skip to checkpoint N" would mean building the thing that
  doc explicitly says not to build yet. Instead: a handful of plain, hand-placed `Transform`s
  in the greybox scene (`DebugSpawnPoint_PuzzleRoom`, `DebugSpawnPoint_Exit`, …), and a cheat
  that teleports both `CatController`s to the chosen one. This is scene data, not a system —
  it never touches `RuntimeGameState.CurrentCheckpoint`, `CheckpointComponent`, or the restart
  logic in `GameManager`. It exists purely so testing "does the puzzle room work" doesn't
  require re-walking the flow corridor every single iteration.
- **Public API:** `DebugHarness.TeleportTo(string spawnPointName)`; the list of names is
  populated at scene load from tagged `Transform`s, no manual registration step per level.
- **Dependencies:** `CatController` (position), `SceneFlowManager`/`GameManager` (for the two
  reload speeds).
- **Risks:** debug spawn points are per-scene data — the greybox scene's Editor pass (W1-33)
  needs to drop 2–3 of these in while building the level, not as an afterthought. Flag it in
  that task's checklist rather than treating it as separate follow-up work.

### Debug scene bootstrap

- **Purpose:** the single highest-leverage item in this doc for the stated goal. Without it,
  every code iteration means Play → MainMenu → click through to the level — real, repeated
  friction on every single change.
- **The problem it solves:** `Wave1Architecture.md`'s boot sequence assumes
  MainMenu → `GameManager.Awake()` → level load. `GameManager`/`SceneFlowManager`/
  `AudioService` are `DontDestroyOnLoad` singletons that only come into existence because
  MainMenu's scene contains (or triggers) them. Pressing Play directly on
  `Wave1Greybox.unity` in the Editor — which is what fast iteration actually looks like —
  skips that trigger entirely, so `GameManager.Instance` is null and everything downstream
  breaks.
- **Responsibilities:** a `DebugSceneBootstrap` component, placed once in the greybox scene,
  that in `Awake()` checks `GameManager.Instance == null` and if so calls the **exact same**
  boot entry point MainMenu's path would have called (see the one small touchpoint below). It
  does not duplicate boot logic or branch behavior — it only fires the real entry point
  earlier, when nothing else already has.
- **Public API:** none beyond the touchpoint.
- **Dependencies:** `GameManager`'s boot entry point.
- **Extension points:** none needed — this is a fixed, small utility.
- **Risks:** if `DebugSceneBootstrap` ever grows its own boot logic instead of calling the
  production entry point, "works when I press Play on the greybox scene" and "works from
  MainMenu" can silently diverge — exactly the kind of debug-only code path this doc's
  principle #1 exists to prevent. Keep it a one-line `if (null) Bootstrap();` forever.

### Error reporting

- **Purpose:** WebGL playtesters have no Unity Editor console and no writable filesystem to
  fall back on — an unhandled exception needs to be visible without either.
- **Responsibilities:** subscribe to `Application.logMessageReceived`; on `LogType.Exception`/
  `LogType.Error`, push a short-lived red toast into the Debug HUD and append the full message
  to `GameplayEventLogger` (so it's in the same scrollback as everything else, and mirrored to
  `Debug.Log`, which Unity WebGL always forwards to the browser console for free).
- **Public API:** none new.
- **Dependencies:** `GameplayEventLogger`.
- **Extension points:** none at Wave 1 scope.
- **Risks:** none. This is deliberately *not* a remote crash-reporting integration (see
  [Principles](#principles) #4) — the browser console plus this toast is the entire
  "error reporting" a solo/local playtest needs.

---

## Touchpoints in existing systems

The debug layer is additive everywhere except these five small, justified changes to
production classes — each is called out here so it's reviewed alongside this doc, not
discovered later as a surprise diff:

| Class | Addition | Why it's minimal and safe |
|---|---|---|
| `HealthComponent` | `void SetInvulnerable(bool)` + a check in `ApplyDamage` | One bool, defaults `false`, unconditionally compiled (harmless in a release build even though only `DebugTools` ever calls the setter) — no `#if` needed for this one because a stray `false`-defaulted flag costs nothing and keeps `Gameplay` free of any awareness of `DEV_TOOLS`. |
| `AbilityGrantService` | `GrantSource.DevCheat` enum value | Same pattern the architecture doc already used for `Mentor`/`Quest` existing-but-unused — an enum value costs nothing and keeps every unlock path (real or debug) going through one `Grant()` call. |
| `CameraDirector` | `CatController CurrentTarget { get; }` read-only property | Exposes what the one-method class already tracks internally; doesn't add camera logic, just observability. |
| `GameManager` | Boot logic factored into a callable `Bootstrap()` (if not already its own method rather than inline in `Awake()`) | Needed so `DebugSceneBootstrap` can call the *same* entry point instead of re-implementing it. Pure refactor, no behavior change. |
| `CatAbility` (base) | `TryActivate` result observed by `PlaytestMetrics` | No code change if `PlaytestMetrics` subscribes via the existing `OnAbilityUnlocked`-style event pattern; only add an `OnAbilityActivated` channel if per-activation counts (not just per-unlock) turn out to matter once playtesting starts — don't add it speculatively before that's known. |

Everything else — the HUD, the logger, the cheats, the metrics object, the bootstrap check —
lives entirely inside `TwoCats.DebugTools` and touches production code only through APIs that
already exist.

---

## Assembly & build configuration

- **One new Scripting Define Symbol: `DEV_TOOLS`.** Set on the WebGL build profile CI uses for
  playtesting (Playbook Part 3's existing GameCI pipeline); left off any future public/release
  profile (that split doesn't need to exist until Wave 4.5's tagged release — nothing here
  changes `.github/workflows/ci.yml`'s current single-profile shape, it's one added define on
  the existing build step).
- **`TwoCats.DebugTools`'s asmdef sets `DEV_TOOLS` as a Define Constraint.** Unity excludes the
  entire assembly from any build lacking the symbol — not a runtime `if`, a compile-time
  exclusion. This is what makes [Principle #3](#principles) (zero cost when it isn't there)
  literally true rather than aspirational, and it's the same enforcement style
  (`Wave1Architecture.md`'s asmdef references are "a compile error, not a lint warning")
  applied to a build-config axis instead of a dependency-direction axis.
- **In-Editor Play mode:** `DEV_TOOLS` should also be set as a default Editor scripting define
  so the tools are available with a normal Editor Play session, not only in CI-built WebGL —
  the Editor is where most iteration actually happens.
- **No change to the three production asmdefs' reference lists** — `DebugTools` depends on
  them; they remain unaware it exists.

---

## Sequencing into the Wave 1 backlog

This doc doesn't renumber `Wave1Backlog.md` — these slot in alongside it, each depending on
the real system it observes existing first. Rough landing points, ordered by leverage:

| Infra piece | Lands after | Why then |
|---|---|---|
| `TwoCats.DebugTools` asmdef + `DebugHarness` skeleton + Debug HUD shell | W1-05/06 (`GameManager`/`SceneFlowManager` exist) | Needs the two persistents to exist before there's anything to boot alongside. |
| Debug scene bootstrap | W1-11 (first test prefab/scene) | This is the item that makes every task *after* W1-11 faster to iterate on — land it the moment there's a scene to bootstrap into. |
| Input visualization | W1-12 (`ActiveCatManager`, both readers assignable) | Nothing to visualize before two `IInputReader`s exist. |
| Camera debug panel | W1-13 (`CameraDirector`) | Needs the class and its touchpoint property. |
| Ability unlock cheats | W1-22 (`AbilityGrantService` + all four abilities) | Needs a real `Grant()` and real abilities to grant. |
| God mode / revive cheats | W1-24/25 (Health, Downed/Revive) | Needs the systems it toggles. |
| Debug spawn points / checkpoint skip | W1-27 or W1-33 | Scene data — natural to add while placing the real checkpoint and level geometry, not before geometry exists. |
| Gameplay event logger + playtest metrics | incremental from W1-05 onward | Each subscribes to a channel as soon as that channel exists; don't wait for the whole wave. |
| Error reporting toast | any time after Debug HUD shell | No dependency beyond having somewhere to render the toast. |

**Recommendation:** pull the assembly + `DebugHarness` + scene bootstrap forward to run
**immediately after W1-11**, ahead of everything else in this table, since that's the one
piece that pays back on every subsequent Wave 1 task rather than only its own corner.

---

## Playtest checklist

A session protocol, not a post-session report — `docs/production/Playbook.md`'s Tier 3 list
already names `docs/production/PlaytestNotes.md` as where post-session write-ups belong; this
checklist is what happens before and during a session so there's something worth writing up.

**Before:**
- [ ] Latest build running (CI WebGL artifact, or a fresh Editor Play session) — confirm
      `DEV_TOOLS` is on and the debug overlay toggles.
- [ ] `PlaytestMetrics` reset (fresh session, not carried over from an earlier one today).
- [ ] Know today's debug spawn points if testing a specific section (puzzle room, exit) rather
      than the full corridor.

**During:**
- [ ] Play the golden path once with the overlay hidden — feel is the thing automation can't
      judge (Playbook); don't let the HUD become a crutch for the very first pass.
- [ ] On anything odd, open the overlay and check the event-log scrollback before trying to
      reproduce from memory — the last ~20 entries usually show the actual sequence.
- [ ] Note frame-drops, wrong exclusivity-group blocks, or partner desync explicitly — these
      are the failure modes `Wave1Architecture.md`'s technical risks already anticipate, so
      recognizing them fast means checking a known list, not diagnosing from scratch.

**After:**
- [ ] Copy the `PlaytestMetrics` summary string + freeform notes into
      `docs/production/PlaytestNotes.md` (create it if this is the first session — Tier 3 per
      Playbook) with today's date.
- [ ] File anything actionable into `docs/production/Backlog.md`.
- [ ] If a bug traces back to a technical risk this doc or `Wave1Architecture.md` already
      named, note it against that risk entry rather than as a fresh one-off.

---

## Explicitly out of scope

Named once, here, so it doesn't get quietly re-proposed mid-implementation:

- **Remote analytics / crash reporting** (Sentry, custom telemetry backend). No external
  playtesters yet; the browser console + in-game log/toast covers a solo session.
- **A second checkpoint system for "skip to checkpoint N."** Wave 1 ships one checkpoint by
  explicit design; checkpoint *skipping* is debug-only scene-data teleports, not a real
  checkpoint feature.
- **Noclip/freefly debug camera.** No existing movement path to hook; would be new debug-only
  gameplay code with no current bug justifying it.
- **A structured logging framework** (levels, categories, filters, file sinks). A flat ring
  buffer plus `Debug.Log` is the entire scope this project's playtest volume needs.
- **Export/upload pipeline for playtest metrics.** An on-screen summary string is the
  deliverable; anything worth keeping gets copied into `PlaytestNotes.md` by hand.
- **A second WebGL build profile without `DEV_TOOLS`.** Not needed until Wave 4.5's tagged
  release actually ships something to the public.

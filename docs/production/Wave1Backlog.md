# Wave 1 Implementation Backlog — Swap Core, Abilities, HP

> **Purpose:** Atomic, one-session (~1–4 hr) development tasks that implement
> `docs/technical/Wave1Architecture.md` in order. This is the task-level breakdown of the
> architecture doc's [11-step build order](Wave1Architecture.md#wave-1-implementation-roadmap) —
> it does not redesign any system, add scope, or reorder the approved sequence. Each task now
> also carries a **design lens** (player experience goal, DCR alignment, non-blocking design
> validation questions) so implementation stays legible against [Movement
> Philosophy](../design/gameplay/Movement.md#movement-philosophy), [Swap
> Philosophy](../design/CoreLoop.md#swap-philosophy), and [Cooperation
> Philosophy](../design/CoreFantasy.md#cooperation-philosophy) — this is a review aid, not new
> scope. See [Design & playtest framework](#design--playtest-framework) below.
> **Owner:** Franco Fusaro · **Status:** Living (implementation not yet started) · **Last Updated:** 2026-08-02
> **Related:** [Wave1Architecture](../technical/Wave1Architecture.md), [Wave1PlaytestInfrastructure](../technical/Wave1PlaytestInfrastructure.md), [Roadmap](Roadmap.md), [Backlog](Backlog.md), [baseline/CodeReview](../technical/baseline/CodeReview.md), [DECISION_LOG](../decisions/DECISION_LOG.md) (DL-013 Movement Philosophy / DL-014 Swap Philosophy / DL-015 Cooperation Philosophy)

**Preconditions (must already be true, Wave 0 scope, not re-listed per task):**
Unity on target LTS · Input System package installed with an `InputActionAsset` in place ·
`Core`/`Gameplay`/`UI` asmdefs exist (Roadmap 0.7) · Cinemachine package present (already in
the legacy project). Every task below assumes these; none of them re-does Wave 0.

Legend: **S** ≈ 1–2 hr · **M** ≈ 2–3 hr · **L** ≈ 3–4 hr · 🔒 needs an Editor pass (hand off per CLAUDE.md) · 🚦 marks a Review Gate (see below) after that task's block

---

## Design & playtest framework

> Additive review scaffolding, not new scope or a reordering of the sequence below. It exists so
> implementation stays traceable back to DCR-001/DCR-002/DCR-003 (all three **accepted and not
> reopened** — see [DECISION_LOG](../decisions/DECISION_LOG.md) DL-013/DL-014/DL-015) without
> reopening any of them.

### Per-task fields

Each task below gains up to three fields, added after **Risks**:

- **Player Experience Goal** — the player-facing moment this task moves toward (e.g. *Foundation
  only*, *First controllable cat*, *First successful swap*). Purely infrastructural tasks are
  tagged *Foundation only* — that's not a lesser task, it's an honest label that there's nothing
  to playtest yet.
- **DCR Alignment** — which of DCR-001 (Movement), DCR-002 (Swap), DCR-003 (Cooperation) this
  task's output primarily serves, informationally. Most tasks map to one; a few touch two.
  Purely technical/infra tasks are marked *N/A*. This never changes acceptance criteria and is
  never grounds to reopen a DCR — it's a cross-reference, not a new requirement.
- **Design Validation Questions** *(only where there's something to ask; omitted on pure infra
  tasks)* — lightweight, non-blocking prompts to raise once a task (or the gate it feeds) is
  actually playable. These are conversation-starters for a playtest between the Lead Gameplay
  Engineer and Lead Game Designer roles, not new Definition-of-Done items — a task can be
  "done" per its DoD while its validation questions stay open, carried into the next gate.

### Review Gates

Six natural pause points, each a short playtest session before continuing implementation. None
of these add new deliverables — the earlier gates just need Editor Play mode; the [Wave 1
Playtest Infrastructure](../technical/Wave1PlaytestInfrastructure.md) tooling, once it lands,
makes the later ones faster to run and easier to observe (its Debug HUD surfaces HP/downed
state/unlocked abilities without waiting for production UI). Gate F is the wave's existing 🚦
gate (Roadmap 1.7) — this framework adds design questions to bring into that already-scheduled
session, not a seventh checkpoint.

| Gate | After | Player experience milestone | Why here |
|---|---|---|---|
| **A** | W1-11 | First controllable cat / first satisfying movement | Movement is the foundation everything else sits on (DCR-001: "flow is the primary movement goal"). Cheapest possible point to catch "the jump doesn't feel good" — before swap, abilities, or partner AI are built on top of it. |
| **B** | W1-13 | First successful swap | The core verb (DCR-002) exists end-to-end for the first time: control-flip + camera retarget. Confirms the swap itself feels good in isolation, before partner AI or abilities add complexity that could mask a bad base feel. |
| **C** | W1-16 | First feeling of companionship | Partner AI, teleport-recover, and the puzzle-room gate are all live — the full "persistent partner" promise (DCR-002, DCR-003) is testable for the first time: does the inactive cat read as a companion, not cargo? |
| **D** | W1-22 | First personality expression | Both starting kits are live and grantable. First point where Orange vs. Tuxedo can be compared side by side with real abilities, not just tuning numbers (DCR-001: "character is communicated through movement"). |
| **E** | W1-27 | First cooperative moment | Downed/revive-by-proximity and real checkpoint restart are both wired. This is DCR-003's "trust" principle made concrete for the first time — worth checking before layering HUD/audio polish on top of a mechanic that might still need adjustment. |
| **F** | W1-33 | Full loop, in a real level | The existing Roadmap 1.7 🚦 gate. Everything above compounds here inside real level rhythm (flow → puzzle → flow). Use Gates A–E's still-open questions as the starting checklist for this session rather than starting from a blank page. |

**What each gate is for, concretely:**
- Ask the gate's validation questions (collected from the tasks that feed it) while the feel is fresh.
- Note anything that reads as "technically correct, doesn't feel right" — these are the bugs
  automation can't catch (per [Playbook](Playbook.md)'s stage-by-stage process).
- Fix feel problems *before* the next block of tasks builds on top of the thing that felt off —
  the point of gating here rather than only at W1-33 is to catch a bad foundation while it's
  still cheap to adjust.
- Carry anything unresolved forward explicitly (a line in `PlaytestNotes.md` or `Backlog.md`) —
  a gate is a checkpoint, not a blocker; it's fine to proceed with a known open question as long
  as it's written down.

---

## Task list, in implementation order

### W1-00 — Folder restructure + legacy quarantine

- **Goal:** Stand up the target folder layout from [Wave1Architecture §Folder structure](../technical/Wave1Architecture.md#folder-structure) and move all 16 existing scripts into `Scripts/Legacy/` unchanged, so every later task lands new code in its final home instead of the flat `Scripts/` root.
- **Prerequisites:** none.
- **Files affected:** `Assets/Scripts/*.cs` → `Assets/Scripts/Legacy/*.cs` (git mv, `.meta` follows); create empty `Scripts/{Core,Cats,Abilities,Interaction,Camera,Audio,UI}/`, `ScriptableObjects/{Characters,Abilities,Events}/`, `Prefabs/{Cats,UI,Interactables}/`, `Tests/{EditMode,PlayMode}/`.
- **Complexity:** S
- **Risks:** Moving files outside the Editor is safe for GUIDs *only* if each `.meta` file is moved alongside its asset with `git mv` (never regenerated) — a stray orphaned `.meta` breaks the reference. No scene/prefab rewiring happens here, so no Editor pass is required, but open the project once afterward to confirm Unity re-resolves the moved scripts without new GUIDs.
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** N/A — infrastructure.
- **Definition of Done:** New folders exist; all 16 legacy scripts compile from `Scripts/Legacy/`; `git status` shows renames, not add+delete; project opens in Unity with zero new missing-script warnings.
- **Suggested commit message:** `Restructured Scripts/ per Wave 1 architecture and quarantined legacy code`

---

### W1-01 — `Singleton<T>` base

- **Goal:** One generic `MonoBehaviour` singleton base (`DontDestroyOnLoad`, duplicate-instance guard) to replace the four hand-rolled singleton bodies CodeReview flags (`ScenePersist`, `MusicPlayer`, `MenuMusic`, and legacy `LevelLoader`'s implicit pattern).
- **Prerequisites:** W1-00.
- **Files affected:** `Assets/Scripts/Core/Singleton.cs`; `Tests/EditMode/SingletonTests.cs`.
- **Complexity:** S
- **Risks:** Getting the duplicate-destroy timing wrong (destroying the *new* instance vs. the *old* one) is the classic bug here — write the EditMode test for "second instance in scene destroys itself, first stays" before wiring any real singleton to it.
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** N/A — infrastructure.
- **Definition of Done:** `Singleton<T>` compiles in `TwoCats.Core`; EditMode test covers single-instance and duplicate-destroy cases and passes.
- **Suggested commit message:** `Added generic Singleton<T> base to replace duplicated singleton bodies`

---

### W1-02 — `GameEventChannel<T>` base + zero-listener smoke test

- **Goal:** The one typed-channel base every Wave 1 event (11 of them, see [event table](../technical/Wave1Architecture.md#event-architecture)) will be an instance of.
- **Prerequisites:** W1-00.
- **Files affected:** `Assets/Scripts/Core/GameEventChannel.cs`; `Tests/EditMode/GameEventChannelTests.cs`.
- **Complexity:** S
- **Risks:** None functional — the real risk this task guards against (stale listeners) is a *usage* discipline, not something this base class can enforce; the doc calls out review + this smoke test as the only mitigation, so don't skip the test thinking the class is "too simple to test."
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** N/A — infrastructure.
- **Definition of Done:** `GameEventChannel<T>` compiles; EditMode test raises a channel instance with zero listeners and asserts no exception; pattern documented in a one-line class comment for future channel authors.
- **Suggested commit message:** `Added GameEventChannel<T> base for the Wave 1 event system`

---

### W1-03 — `RuntimeGameState` + `ISaveService`/`NullSaveService`/`SaveData`

- **Goal:** The plain-C#, session-scoped progress object (`RuntimeGameState`) and the save seam that lets Wave 1 code be save-ready without a real save file.
- **Prerequisites:** W1-00.
- **Files affected:** `Assets/Scripts/Core/RuntimeGameState.cs`, `Assets/Scripts/Core/ISaveService.cs`, `Assets/Scripts/Core/NullSaveService.cs`, `Assets/Scripts/Core/SaveData.cs`; `Tests/EditMode/RuntimeGameStateTests.cs`.
- **Complexity:** S/M
- **Risks:** Temptation to add fields `SaveSystem.md` eventually wants (cosmetics, currency) — architecture doc is explicit these don't belong yet. Keep `SaveData` to `schemaVersion`, `checkpointId`, `unlockedAbilities` only.
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** N/A — infrastructure.
- **Definition of Done:** `RuntimeGameState` holds unlocked-abilities set + current checkpoint; `NullSaveService.Load()`/`Save()` are no-ops that don't throw; a `RuntimeGameState → SaveData` conversion round-trips in a test.
- **Suggested commit message:** `Added RuntimeGameState and the ISaveService seam (NullSaveService for Wave 1)`

---

### W1-04 — `ICatIdentity`/`CatTag` marker component

- **Goal:** The one shared "is this actually a cat" check that fixes three separate legacy bugs at once (`CoinPickup`, `LevelExit`, `Platform` all currently guess from collider type).
- **Prerequisites:** W1-00.
- **Files affected:** `Assets/Scripts/Cats/ICatIdentity.cs`, `Assets/Scripts/Cats/CatTag.cs`.
- **Complexity:** S
- **Risks:** Low — it's a marker component. The only failure mode is forgetting to attach it to both cat prefabs later (W1-11), which silently breaks every consumer; add a note to W1-11's DoD to check for it explicitly.
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** DCR-003 (Cooperation) — informational; correct cat identity is what every later cooperative/interaction system (W1-23 Interaction, W1-25 Downed/Revive, W1-32 legacy fixes) depends on to tell "the other cat" from "anything else."
- **Definition of Done:** `CatTag : MonoBehaviour, ICatIdentity` compiles in `TwoCats.Gameplay`; no consumers wired yet (that happens per-consumer in later tasks).
- **Suggested commit message:** `Added ICatIdentity/CatTag marker to replace collider-type identity checks`

---

### W1-05 — `GameManager` (boot, `RuntimeGameState` ownership, pause)

- **Goal:** The persistent session owner: boot sequence, `State` accessor, pause/resume, and the `OnCatDowned` × 2 → `OnBothCatsDowned` derivation (restore wiring itself lands in W1-26 once Checkpoint exists).
- **Prerequisites:** W1-01, W1-02, W1-03.
- **Files affected:** `Assets/Scripts/Core/GameManager.cs`; `Tests/EditMode/GameManagerTests.cs`.
- **Complexity:** M
- **Risks:** God-object drift — the architecture doc's hard rule is GameManager **orchestrates**, never contains gameplay logic (no HP math, no swap logic). Enforce this while writing it, not in a later cleanup pass.
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** N/A — infrastructure (orchestration only, per its own god-object guardrail).
- **Definition of Done:** `GameManager : Singleton<GameManager>` exposes `State`, `Pause()`/`Resume()` (raises `OnGamePaused`/`OnGameResumed`); subscribes to two `OnCatDowned` payloads and raises `OnBothCatsDowned` on the second; `RestartFromCheckpoint()` exists as a stub (no-op body, real wiring in W1-26); resolves `ISaveService` to `NullSaveService` at boot.
- **Suggested commit message:** `Added GameManager: boot sequence, pause/resume, both-cats-downed detection`

---

### W1-06 — `SceneFlowManager` (refactor from `LevelLoader`/`Menu`)

- **Goal:** Replace `LevelLoader.cs` and update `Menu.cs`, fixing the destroy-then-null NRE (#1), the missing `"LoseScreen"` scene (#2), and the build-index math (R5) with named-scene constants — while keeping the slow-mo level-exit transition feel.
- **Prerequisites:** W1-01.
- **Files affected:** new `Assets/Scripts/Core/SceneFlowManager.cs`, `Assets/Scripts/Core/SceneId.cs` (named-scene enum/constants); refactor `Assets/Scripts/Legacy/Menu.cs` to call the new manager; delete `Assets/Scripts/Legacy/LevelLoader.cs` and `Assets/Scripts/Legacy/ScenePersist.cs` (superseded — `ScenePersist`'s only field was already dead per CodeReview #5).
- **Complexity:** M
- **Risks:** `SceneId` constants must match actual Build Settings scene names/order exactly — a typo here reproduces bug #2 in a new form. Cross-check against `Assets/Levels/` scene list before hardcoding.
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** N/A — infrastructure, though the slow-mo transition it preserves is a small piece of DCR-001's flow feel worth not losing sight of while refactoring.
- **Definition of Done:** `SceneFlowManager : Singleton<SceneFlowManager>` loads scenes by `SceneId`, no cached-ref NRE on repeated loads; `Menu.cs` compiles against the new API; `LevelLoader.cs`/`ScenePersist.cs` deleted; slow-mo transition preserved as a configurable field.
- **Suggested commit message:** `Replaced LevelLoader/ScenePersist with SceneFlowManager, fixed scene-load bugs #1/#2/R5`

---

### W1-07 — `AbilityDefinition` ScriptableObject

- **Goal:** The metadata SO (name/icon/description/exclusivity-group) each `CatAbility` and the HUD read from.
- **Prerequisites:** W1-00.
- **Files affected:** `Assets/Scripts/Core/AbilityDefinition.cs`.
- **Complexity:** S
- **Risks:** None functional — but this type lives in `TwoCats.Core` (not `Gameplay`), per the architecture doc's asmdef rationale. Placing it in the wrong assembly here creates a `UI → Gameplay` reference the moment HUD needs it (W1-30) — get the assembly right now, it's expensive to move later.
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** DCR-001 (Movement) — informational; the metadata every future personality-expressing ability (W1-18–21) will read from.
- **Definition of Done:** `[CreateAssetMenu] class AbilityDefinition : ScriptableObject` with read-only fields (`displayName`, `icon`, `description`, `exclusivityGroup`); compiles in `TwoCats.Core`.
- **Suggested commit message:** `Added AbilityDefinition ScriptableObject`

---

### W1-08 — `CharacterData` SO + Orange/Tuxedo asset authoring 🔒

- **Goal:** The code-free per-cat identity (`walkSpeed`, `jumpForce`, `maxHP`, `animatorController`, `startingAbilities`, `catId`), plus the two actual asset instances.
- **Prerequisites:** W1-07.
- **Files affected:** `Assets/Scripts/Core/CharacterData.cs`, `Assets/Scripts/Core/CatId.cs` (enum); Editor-created `Assets/ScriptableObjects/Characters/Orange.asset`, `Tuxedo.asset`.
- **Complexity:** S
- **Risks:** The class itself is code-only and safe to write blind, but the **class must expose every field as read-only from gameplay code** (the architecture doc's #1 SO footgun callout — never stash runtime HP/unlock state here). Creating the two `.asset` instances and tuning placeholder values is an Editor step — hand off per CLAUDE.md; don't fabricate tuning numbers in code comments as a substitute.
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** DCR-001 (Movement) — informational; this is where "shared foundation, character-driven asymmetry" first becomes data — one shared `CatController` (W1-10) will read per-cat `walkSpeed`/`jumpForce`/`maxHP` from here.
- **Design Validation Questions:** Once Orange/Tuxedo tuning values are authored (the Editor pass), do the raw numbers already suggest two different physical presences — e.g. Tuxedo heavier/slower — before any ability exists to reinforce it?
- **Definition of Done:** `CharacterData : ScriptableObject` compiles with read-only serialized fields; `Orange.asset`/`Tuxedo.asset` exist with placeholder tuning values, created via an Editor pass.
- **Suggested commit message:** `Added CharacterData ScriptableObject and Orange/Tuxedo data assets`

---

### W1-09 — `IInputReader` + `PlayerInputReader`

- **Goal:** The one seam between the Input System and every gameplay consumer.
- **Prerequisites:** W1-00 (Input System package assumed installed per Wave 0.2 precondition).
- **Files affected:** `Assets/Scripts/Core/IInputReader.cs`, `Assets/Scripts/Core/PlayerInputReader.cs`.
- **Complexity:** M
- **Risks:** If the `InputActionAsset`'s action map doesn't yet have all five actions (`Move`, `Jump`, `Swap`, `Ability`, `Interact`) defined, this task blocks on an Editor edit to that asset first — check the asset before writing the wrapper, don't guess action names.
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** N/A — infrastructure (the seam both `PlayerInputReader` and, later, `PartnerAI` implement).
- **Definition of Done:** `IInputReader` interface with `MoveAxis`, `JumpDown`, `SwapDown`, `AbilityDown`, `InteractDown`; `PlayerInputReader` wraps real `InputAction` callbacks and implements it; compiles in `TwoCats.Core`.
- **Suggested commit message:** `Added IInputReader abstraction and PlayerInputReader over the Input System`

---

### W1-10 — `CatController` generic locomotion

- **Goal:** One script, both cats — walk/jump/grounded state driven entirely by `CharacterData` + `IInputReader`, with zero cat-identity branching (Roadmap 1.1, no abilities yet).
- **Prerequisites:** W1-08, W1-09, W1-04.
- **Files affected:** `Assets/Scripts/Cats/CatController.cs`.
- **Complexity:** L
- **Risks:** This is the architecture doc's single highest-emphasis rule: *if a line would read differently for Orange vs. Tuxedo, it doesn't belong here.* Any temptation toward `if (catId == Orange)` in this file is a design smell — resist it now, since retrofitting is a rewrite, not a patch. Also: cache all `GetComponent` calls in `Start`/`Awake` per `CLAUDE.md`.
- **Player Experience Goal:** Foundation only — the movement code this task writes is the literal implementation of Gate A's experience, but it isn't provable/playable until W1-11 puts it on screen.
- **DCR Alignment:** DCR-001 (Movement) — primary; this is where "shared foundation, no per-cat branching" is enforced in code, not just stated as a principle.
- **Definition of Done:** `CatController` reads `CharacterData` + assigned `IInputReader`, moves/jumps correctly with no compile-time or runtime cat-identity branching; exposes `IsGrounded`, `LastSafeGroundedPosition`, `CanRelinquishControl` (hardcode `true` for now — abilities set it later), `IsActive`; drives Animator via hashed param IDs, not string lookups every frame.
- **Suggested commit message:** `Added CatController: data-driven locomotion shared by both cats`

---

### W1-11 — Test prefab/scene wiring for `CatController` 🔒

- **Goal:** Get one cat actually walking/jumping on screen, provable in isolation, before building anything on top of it.
- **Prerequisites:** W1-10.
- **Files affected:** `Assets/Prefabs/Cats/Orange.prefab` (new); a scratch test scene (or reuse an existing level scene temporarily).
- **Complexity:** S
- **Risks:** Editor-only — per CLAUDE.md this is handed to the user, not driven from the shell. Confirm the `CatTag` marker (W1-04) is attached to the prefab now, while it's fresh, so it isn't silently missing when W1-23's `InteractionDetector` or the legacy-fix task (W1-32) go looking for it later.
- **Player Experience Goal:** First controllable cat / first satisfying movement.
- **DCR Alignment:** DCR-001 (Movement) — primary.
- **Design Validation Questions:**
  - Does moving Orange already feel enjoyable on nothing but walk/jump, with no abilities yet?
  - Does input response feel immediate, or is there perceptible lag between press and reaction?
  - Is the jump arc/landing weight readable, or does it feel floaty/heavy in an unintentional way?
- **Definition of Done:** Orange prefab exists with `CatController` + `CatTag` + `CharacterData` reference wired, moves/jumps correctly in Play mode in a test scene.
- **Suggested commit message:** `Added Orange test prefab and verified CatController locomotion in-editor`

---

> 🚦 **Gate A — First controllable cat / first satisfying movement.** Pause here for a short
> playtest before starting W1-12: does base locomotion already feel good on its own? See [Review
> Gates](#review-gates) above. Cheapest point in the wave to fix a movement-feel problem, before
> swap/abilities/partner AI are all built on top of it.

---

### W1-12 — `ActiveCatManager` (swap authority + presence)

- **Goal:** The single source of truth for who's active/partner/present, and the swap itself — `TrySwap()` is the entire "Swap System" per the architecture doc's explicit no-new-class call.
- **Prerequisites:** W1-10, W1-11 (needs both cat prefabs to test against), W1-02 (raises `OnCatSwapped`).
- **Files affected:** `Assets/Scripts/Cats/ActiveCatManager.cs`; `Assets/ScriptableObjects/Events/CatSwappedChannel.asset` (+ `CatSwapEventArgs.cs`), `CatPresenceChangedChannel.asset`.
- **Complexity:** M
- **Risks:** Swapping mid-ability without a guard orphans physics state — this task must include the `CanRelinquishControl` check on `CatController` before `TrySwap()` commits, even though no ability sets it to `false` yet (that lands in Wave 1's ability tasks). Resolve both `CatController` refs via `[SerializeField]`, never `FindObjectOfType` (hard rule, CodeReview R1).
- **Player Experience Goal:** First successful swap (mechanic only — camera hasn't caught up yet, that's W1-13).
- **DCR Alignment:** DCR-002 (Swap) — primary; DCR-003 (Cooperation) — secondary (presence tracking).
- **Design Validation Questions:**
  - Does the control hand-off itself feel instant, with no perceptible "select" pause, even before the camera follows?
  - Does the `CanRelinquishControl` guard ever produce a swap that reads as blocked for no visible reason?
- **Definition of Done:** `ActiveCatManager.Awake()` resolves both cats via serialized refs; `TrySwap()` flips `IInputReader` assignment + `IsActive` + raises `OnCatSwapped`, respecting `CanRelinquishControl`; `SetCatPresence`/`OnCatPresenceChanged` wired even though unused until Wave 3.
- **Suggested commit message:** `Added ActiveCatManager: swap authority and cat presence tracking`

---

### W1-13 — `CameraDirector` (Cinemachine retarget) 🔒

- **Goal:** Camera follows the active cat, retargeting instantly on swap (Roadmap 1.2), using Cinemachine's own blending — no hand-rolled lerp.
- **Prerequisites:** W1-12.
- **Files affected:** `Assets/Scripts/Camera/CameraDirector.cs`; Editor: vcam setup in the test scene.
- **Complexity:** S/M
- **Risks:** Vcam scene wiring is an Editor step (hand off per CLAUDE.md). Code-side, resist adding camera logic Cinemachine already provides — the doc is explicit this should stay a one-method class.
- **Player Experience Goal:** First successful swap (full feel, camera included).
- **DCR Alignment:** DCR-002 (Swap) — primary.
- **Design Validation Questions:**
  - Does the camera retarget/blend read as "confidence → continuity → reunion," or as a jarring cut?
  - Would you swap here just because it feels good, even with no mechanical reason to?
- **Definition of Done:** `CameraDirector.FollowCat(CatController)` retargets vcam `Follow`/`LookAt`; subscribed to `OnCatSwapped`; vcam blends smoothly on swap in Play mode.
- **Suggested commit message:** `Added CameraDirector: Cinemachine retarget on cat swap`

---

> 🚦 **Gate B — First successful swap.** Pause here for a short playtest before starting W1-14:
> does the swap itself — control-flip plus camera retarget — feel good in isolation? See [Review
> Gates](#review-gates) above. Confirming this now means any bad "base swap feel" is caught
> before partner AI or abilities add complexity that could mask it.

---

### W1-14 — `PartnerAI` core (follow steering)

- **Goal:** `PartnerAI` implements `IInputReader` and produces synthetic move/jump-axis input toward the leader — the mechanism that makes leader/follower symmetry and ability-leader-only-ness fall out for free.
- **Prerequisites:** W1-12.
- **Files affected:** `Assets/Scripts/Cats/PartnerAI.cs`.
- **Complexity:** M
- **Risks:** The temptation is to give `PartnerAI` its own movement code path "since it's simpler" — don't; the entire point is that it's *only* a synthetic `IInputReader`, assigned to the inactive `CatController` exactly like `PlayerInputReader` is assigned to the active one. Emit move/jump axis only, never ability-activation input — that's what keeps abilities leader-only without extra enforcement code.
- **Player Experience Goal:** First feeling of companionship (partial — follow-steering only, no safety net yet).
- **DCR Alignment:** DCR-003 (Cooperation) — primary; DCR-002 (Swap) — secondary ("the partnership never stops existing").
- **Design Validation Questions:** Does the partner read as moving with intent — a companion — or as an object being dragged along?
- **Definition of Done:** `PartnerAI : MonoBehaviour, IInputReader` steers toward `SetFollowTarget(Transform)` within the partner's own `CharacterData` limits; gets assigned to the inactive `CatController` by `ActiveCatManager` on swap; role flips correctly (partner becomes leader's `IInputReader` source when swapped).
- **Suggested commit message:** `Added PartnerAI: synthetic-input follow behavior for the inactive cat`

---

### W1-15 — Teleport-recover + hazard-safe `LastSafeGroundedPosition`

- **Goal:** The safety net for synthetic-input edge cases — teleport the partner to the leader if it falls too far behind or offscreen, without teleporting it into a hazard.
- **Prerequisites:** W1-14, W1-10 (`LastSafeGroundedPosition` is written by `CatController`).
- **Files affected:** `Assets/Scripts/Cats/CatController.cs` (add hazard-layer-guarded write to `LastSafeGroundedPosition`), `Assets/Scripts/Cats/PartnerAI.cs` (teleport logic + `LockTeleportRecover(bool)`).
- **Complexity:** S/M
- **Risks:** The architecture doc calls this out by name: guard the `LastSafeGroundedPosition` write with a hazard-layer check, not just an `IsGrounded` check, or a cat can teleport-recover straight into spikes right after a bad landing.
- **Player Experience Goal:** First feeling of companionship (continued) — the "never babysit it" promise.
- **DCR Alignment:** DCR-003 (Cooperation) — primary; DCR-001 (Movement) — secondary (teleport-recover exists specifically to protect flow).
- **Design Validation Questions:**
  - Does teleport-recovery feel like an invisible safety net, or does it happen often enough to be distracting?
  - Does a recovered partner ever visibly land somewhere that looks wrong, even though the hazard-layer guard passed?
- **Definition of Done:** `CatController` only updates `LastSafeGroundedPosition` when grounded *and* not on a hazard layer; `PartnerAI` teleports to it when the partner falls outside a configurable follow-distance threshold, unless `LockTeleportRecover(true)` is set.
- **Suggested commit message:** `Added teleport-recover with hazard-safe grounded-position tracking`

---

### W1-16 — Puzzle-room gate trigger volume

- **Goal:** The simplest possible version of the room-gate that disables teleport-recover in co-op puzzle rooms (per Movement.md's documented rule, and the architecture doc's explicit "don't build the general case yet" call).
- **Prerequisites:** W1-15.
- **Files affected:** `Assets/Scripts/Cats/PuzzleRoomTrigger.cs`; `Assets/ScriptableObjects/Events/PuzzleRoomEnteredChannel.asset`, `PuzzleRoomExitedChannel.asset`.
- **Complexity:** S
- **Risks:** Don't build the `SceneFlowManager`-owned room-gate model this doc explicitly defers — a plain trigger volume raising two events is the entire scope.
- **Player Experience Goal:** First feeling of companionship (complete — full "persistent partner" promise now testable).
- **DCR Alignment:** DCR-003 (Cooperation) — primary; DCR-002 (Swap) — secondary (a locked mandatory gate, reaffirmed rather than reopened by DL-014/DL-015).
- **Design Validation Questions:**
  - Does losing teleport-recover on puzzle-room entry read as "we're in this together now," or as an arbitrary rule flipping off?
  - Is it clear *why* the partner stopped auto-catching-up, without needing a tutorial line to explain it?
- **Definition of Done:** `PuzzleRoomTrigger` raises `OnPuzzleRoomEntered(RoomId)`/`OnPuzzleRoomExited(RoomId)` on 2D trigger enter/exit; `PartnerAI` subscribes and calls `LockTeleportRecover` accordingly.
- **Suggested commit message:** `Added puzzle-room trigger volume gating partner teleport-recover`

---

> 🚦 **Gate C — First feeling of companionship.** Pause here for a short playtest before starting
> W1-17: partner AI, teleport-recover, and the puzzle-room gate are all live — does the inactive
> cat read as a companion, not cargo? See [Review Gates](#review-gates) above.

---

### W1-17 — `CatAbility` base + exclusivity-group handling

- **Goal:** The abstract base every ability subclasses, and the one-active-per-group rule in `CatController` that prevents ability-vs-ability spaghetti.
- **Prerequisites:** W1-10, W1-07.
- **Files affected:** `Assets/Scripts/Abilities/CatAbility.cs`; `Assets/Scripts/Cats/CatController.cs` (enumerate + tick unlocked, non-exclusivity-blocked abilities).
- **Complexity:** M
- **Risks:** Resist building a full ability state machine for four abilities — the doc is explicit a simple `exclusivityGroup` string/enum tag on `AbilityDefinition` is sufficient scope.
- **Player Experience Goal:** Foundation only.
- **DCR Alignment:** DCR-001 (Movement) — informational; the exclusivity-group mechanism is what keeps future abilities character-driven rather than stackable-for-power (per DL-013/DL-014's future-mechanics evaluation lens).
- **Definition of Done:** `abstract class CatAbility : MonoBehaviour` with `Definition`, `IsUnlocked`, `TryActivate(IInputReader)`, `Tick()`; `CatController` enumerates attached `CatAbility`s, ticks only unlocked ones, and blocks a second active ability in the same `exclusivityGroup`.
- **Suggested commit message:** `Added CatAbility base and exclusivity-group handling in CatController`

---

### W1-18 — `ZoomiesAbility` (Orange)

- **Goal:** First concrete ability, proving the `CatAbility` seam end-to-end.
- **Prerequisites:** W1-17.
- **Files affected:** `Assets/Scripts/Abilities/ZoomiesAbility.cs`; `Assets/ScriptableObjects/Abilities/Zoomies.asset`.
- **Complexity:** S
- **Risks:** Low — first instance of the pattern, so expect this task to also shake out any rough edges in `CatAbility`'s base contract from W1-17 (acceptable, don't gold-plate the base speculatively before this).
- **Player Experience Goal:** First personality expression (first data point).
- **DCR Alignment:** DCR-001 (Movement) — primary; DCR-003 (Cooperation) — informational (amplifies a strength rather than covering a weakness).
- **Design Validation Questions:**
  - Does Zoomies feel like *Orange* — chaotic, fast, a little reckless — rather than a generic speed buff?
  - Would this ability make obvious sense on Tuxedo? (If yes, it may not be expressing character yet.)
- **Definition of Done:** `ZoomiesAbility` triggers a speed-boost on `AbilityDown` while unlocked; respects `exclusivityGroup`; `Zoomies.asset` (`AbilityDefinition`) authored.
- **Suggested commit message:** `Added ZoomiesAbility`

---

### W1-19 — `WallClingAbility` (Orange)

- **Goal:** Second Orange starting ability.
- **Prerequisites:** W1-17 (can run parallel to W1-18/20/21 — see [dependency summary](#dependency-summary)).
- **Files affected:** `Assets/Scripts/Abilities/WallClingAbility.cs`; `Assets/ScriptableObjects/Abilities/WallCling.asset`.
- **Complexity:** S/M
- **Risks:** Needs a wall-detection raycast/overlap check — verify against the existing tilemap collision layers so it doesn't false-positive on non-wall geometry.
- **Player Experience Goal:** First personality expression (second Orange data point).
- **DCR Alignment:** DCR-001 (Movement) — primary.
- **Design Validation Questions:** Combined with Zoomies, does Orange now read as one consistent personality, or as two unrelated mechanics sharing a character?
- **Definition of Done:** Cat sticks to a qualifying wall surface while `AbilityDown` held and unlocked, releases on input release or ground contact; `WallCling.asset` authored.
- **Suggested commit message:** `Added WallClingAbility`

---

### W1-20 — `GlideAbility` (Tuxedo)

- **Goal:** First Tuxedo starting ability.
- **Prerequisites:** W1-17 (parallel to W1-18/19/21).
- **Files affected:** `Assets/Scripts/Abilities/GlideAbility.cs`; `Assets/ScriptableObjects/Abilities/Glide.asset`.
- **Complexity:** S/M
- **Risks:** Interacts with gravity/fall-speed on `CatController` — make sure it modifies velocity through a path that doesn't fight the base locomotion code (e.g. a clamped fall-speed override while active, not a competing force).
- **Player Experience Goal:** First personality expression (first Tuxedo data point, and first direct contrast to Orange).
- **DCR Alignment:** DCR-001 (Movement) — primary.
- **Design Validation Questions:**
  - Does Glide feel like *Tuxedo* — controlled, weighty, unhurried — in clear contrast to Zoomies?
  - Does the contrast read as two philosophies of movement, or just "ability A vs. ability B"?
- **Definition of Done:** Cat's fall speed clamps to a glide rate while airborne, `AbilityDown` held, and unlocked; `Glide.asset` authored.
- **Suggested commit message:** `Added GlideAbility`

---

### W1-21 — `LoafAbility` (Tuxedo)

- **Goal:** Second Tuxedo starting ability.
- **Prerequisites:** W1-17 (parallel to W1-18/19/20).
- **Files affected:** `Assets/Scripts/Abilities/LoafAbility.cs`; `Assets/ScriptableObjects/Abilities/Loaf.asset`.
- **Complexity:** S
- **Risks:** Low, but check its `exclusivityGroup` against Wall-cling/Glide now — "Loaf while gliding?" is the exact ambiguity the architecture doc flags as needing the group tag resolved before both abilities ship.
- **Player Experience Goal:** First personality expression (starting kit's asymmetry complete).
- **DCR Alignment:** DCR-001 (Movement) — primary; DCR-003 (Cooperation) — informational (the "Loaf while gliding?" exclusivity check is exactly the kind of cross-checking DL-015's consistency principle asks for).
- **Design Validation Questions:**
  - Does Loaf read as characterful (Tuxedo-specific) rather than an arbitrary fourth mechanic?
  - With all four abilities in hand, does the Orange-kit/Tuxedo-kit split feel like two coherent personalities rather than four disconnected toys?
- **Definition of Done:** Loaf activates/deactivates on `AbilityDown` while unlocked, with its effect (per Abilities.md) implemented; exclusivity group assignment reviewed against the other three abilities; `Loaf.asset` authored.
- **Suggested commit message:** `Added LoafAbility`

---

### W1-22 — `AbilityGrantService` + starting-kit grant at spawn

- **Goal:** The one grant API (Roadmap 1.4) — pickup/mentor/quest all funnel through `Grant()`, and the starting kit itself uses the same call at spawn, not a separate bootstrap path.
- **Prerequisites:** W1-18, W1-19, W1-20, W1-21 (needs all four concrete abilities to grant a real starting kit), W1-12 (spawn sequencing lives in `ActiveCatManager`'s post-spawn step).
- **Files affected:** `Assets/Scripts/Core/AbilityGrantService.cs`; `Assets/Scripts/Cats/ActiveCatManager.cs` (call `Grant(..., GrantSource.StartingKit)` for both cats post-spawn); `Assets/ScriptableObjects/Events/AbilityUnlockedChannel.asset`.
- **Complexity:** M
- **Risks:** Scope creep into an actual quest system — this task is `Grant()`/`IsUnlocked()` and nothing upstream (no quest state machine, no NPC dialogue, even though `GrantSource.Mentor`/`Quest` exist in the enum for a future API that costs nothing today).
- **Player Experience Goal:** First personality expression (complete — both cats live with full starting kits).
- **DCR Alignment:** DCR-001 (Movement) — primary; DCR-003 (Cooperation) — informational (the same `Grant()` call this task wires for the starting kit is designed to later carry Mentor/Quest unlocks).
- **Design Validation Questions:**
  - With both starting kits live, does swapping now feel like switching between two personalities, not just two movesets?
  - Do all four abilities feel individually necessary, or does any pair feel redundant?
- **Definition of Done:** `Grant(CatId, AbilityDefinition, GrantSource)` is idempotent, updates `RuntimeGameState`'s unlocked set, flips the matching `CatAbility.IsUnlocked` on the target cat, raises `OnAbilityUnlocked`; `ActiveCatManager` grants each cat's starting kit through this same call at spawn.
- **Suggested commit message:** `Added AbilityGrantService and wired starting-kit grants at spawn`

---

> 🚦 **Gate D — First personality expression.** Pause here for a short playtest before starting
> W1-23: with both starting kits live, can Orange and Tuxedo be compared side by side as real
> personalities, not just tuning numbers? See [Review Gates](#review-gates) above.

---

### W1-23 — `IInteractable` + `InteractionDetector`

- **Goal:** The generic "walk up, press button" affordance that Downed/Revive builds on top of.
- **Prerequisites:** W1-09, W1-11.
- **Files affected:** `Assets/Scripts/Interaction/IInteractable.cs`, `Assets/Scripts/Interaction/InteractionDetector.cs`; `Assets/ScriptableObjects/Events/InteractableFocusChangedChannel.asset`.
- **Complexity:** M
- **Risks:** The opposite temptation from most tasks here — don't add dialogue trees, multi-step interactions, or interaction "types." Scope is exactly `CanInteract`/`Interact`/`Prompt` plus nearest-in-range tracking.
- **Player Experience Goal:** Foundation only — the affordance exists, nothing cooperative is wired to it yet.
- **DCR Alignment:** DCR-003 (Cooperation) — informational; this is the mechanism Downed/Revive (W1-25) turns into the wave's concrete trust moment.
- **Definition of Done:** `InteractionDetector` (per-cat trigger) tracks the nearest in-range `IInteractable`, calls `Interact` on `InteractDown`, raises `OnInteractableFocusChanged` on focus change; no concrete `IInteractable` implementations yet (that's W1-25).
- **Suggested commit message:** `Added IInteractable contract and InteractionDetector`

---

### W1-24 — `HealthComponent` + `CombatContext` flag

- **Goal:** Per-cat HP (Roadmap 1.6), plus the minimal room-scoped flow/arena flag Health reads for the invulnerable-partner rule.
- **Prerequisites:** W1-08 (reads `CharacterData.maxHP`), W1-02.
- **Files affected:** `Assets/Scripts/Cats/HealthComponent.cs`, `Assets/Scripts/Cats/CombatContext.cs`; `Assets/ScriptableObjects/Events/DamagedChannel.asset`.
- **Complexity:** M
- **Risks:** Keep this decoupled from Downed/Revive — "HP hits 0" and "what happens next" must stay separate classes (next task), or the two responsibilities tangle immediately.
- **Player Experience Goal:** Foundation only — HP exists, no downed/revive experience yet.
- **DCR Alignment:** DCR-003 (Cooperation) — informational (the vulnerability that W1-25 turns into a cooperative beat).
- **Definition of Done:** `HealthComponent` initializes `Max` from `CharacterData.maxHP`, `ApplyDamage`/`Heal` work, `Current == 0` exposes `IsDowned`, raises `OnDamaged`; `CombatContext` exists as a room-scoped flag defaulting to "flow" (arena/vulnerability logic itself is Wave 2 — only the flag needs to exist).
- **Suggested commit message:** `Added HealthComponent and the CombatContext flow/arena flag`

---

### W1-25 — `DownedState` (Downed/Revive)

- **Goal:** "Downed, not dead" plus revive-by-proximity, reusing the Interaction System rather than a bespoke trigger.
- **Prerequisites:** W1-24, W1-23, W1-15 (must disable teleport-recover for a downed cat).
- **Files affected:** `Assets/Scripts/Cats/DownedState.cs`; `Assets/ScriptableObjects/Events/CatDownedChannel.asset`, `CatRevivedChannel.asset`.
- **Complexity:** M
- **Risks:** The teleport-recover interaction the architecture doc calls out by name: a downed cat teleporting to the leader mid-revive is a visible bug. `Down()` must call `PartnerAI.LockTeleportRecover(true)` on itself (or the equivalent) before anything else touches it. Also: this class only *reports* state — "both down → checkpoint" stays `GameManager`'s call, not this one's.
- **Player Experience Goal:** First cooperative moment — the concrete trust payoff.
- **DCR Alignment:** DCR-003 (Cooperation) — primary; directly implements the "how do these two trust each other here?" standard.
- **Design Validation Questions:**
  - Does reviving a downed partner feel like an act of care, or a mechanical proximity check?
  - Does being downed feel tense-but-safe while waiting, rather than punishing?
- **Definition of Done:** `DownedState : MonoBehaviour, IInteractable` — `Down()` on `HealthComponent.Current == 0` disables input/abilities and locks teleport-recover; `CanInteract` true only for the other cat while downed; `Interact` calls `Revive()`; raises `OnCatDowned`/`OnCatRevived`.
- **Suggested commit message:** `Added DownedState: down-and-revive via the Interaction System`

---

### W1-26 — `GameManager` ↔ `OnBothCatsDowned` → restart wiring

- **Goal:** Close the loop from W1-05's stub: both cats down restarts from the current checkpoint.
- **Prerequisites:** W1-25, W1-05 (stub already exists).
- **Files affected:** `Assets/Scripts/Core/GameManager.cs` (implement `RestartFromCheckpoint()` body — real logic depends on `RuntimeGameState.CurrentCheckpoint`, added properly in W1-27, so this task's body may stay a placeholder respawn-at-origin until W1-27 lands; sequence them back-to-back in the same session if convenient).
- **Complexity:** S
- **Risks:** Ordering nuance only — `RestartFromCheckpoint` doesn't have real checkpoint data until W1-27. Fine to land this task first with a placeholder position and finish it in W1-27, or merge the two into one session; call it out in the PR either way so it's not mistaken for done-done.
- **Player Experience Goal:** Foundation only — closes W1-05's stub, no new player-facing moment beyond what W1-25/27 already deliver.
- **DCR Alignment:** DCR-003 (Cooperation) — informational ("we go down together, we come back together").
- **Definition of Done:** `OnBothCatsDowned` → `GameManager.RestartFromCheckpoint()` fires automatically; both cats respawn at full HP (via `HealthComponent` reset) at whatever position is currently available.
- **Suggested commit message:** `Wired GameManager to restart from checkpoint on both-cats-downed`

---

### W1-27 — `CheckpointComponent`

- **Goal:** One checkpoint (level start) for the Wave 1 greybox — explicitly not a multi-checkpoint manager.
- **Prerequisites:** W1-26, W1-03 (`RuntimeGameState.CurrentCheckpoint`).
- **Files affected:** `Assets/Scripts/Interaction/CheckpointComponent.cs`; `Assets/ScriptableObjects/Events/CheckpointReachedChannel.asset`.
- **Complexity:** S/M
- **Risks:** Don't build ordering/furthest-reached logic — one level, one checkpoint, full stop, per the architecture doc's explicit scope call. Also: this is deliberately *not* routed through the Interaction System (automatic trigger, no prompt) — don't make it `IInteractable`.
- **Player Experience Goal:** First cooperative moment (complete loop: down → revive-by-proximity → both-down → real checkpoint restart).
- **DCR Alignment:** DCR-003 (Cooperation) — primary; DCR-001 (Movement) — secondary (a real, low-friction restart keeps flow intact rather than punishing exploration).
- **Design Validation Questions:**
  - Does restarting from checkpoint feel like a reset, not a punishment — does it preserve the will to keep exploring/swapping freely?
  - Across the whole down/revive/restart loop, does the partnership feel like the thing that pulled you through, or just a system state that happened to resolve?
- **Definition of Done:** `CheckpointComponent` raises `OnCheckpointReached(Vector2)` on 2D trigger enter (no interact prompt); `GameManager` records it into `RuntimeGameState.CurrentCheckpoint` and `RestartFromCheckpoint()` now uses the real position.
- **Suggested commit message:** `Added CheckpointComponent and wired real checkpoint respawn`

---

> 🚦 **Gate E — First cooperative moment.** Pause here for a short playtest before starting
> W1-28: downed/revive-by-proximity and real checkpoint restart are both wired — does DCR-003's
> "trust" principle already read as intended? See [Review Gates](#review-gates) above. The [Debug
> HUD](../technical/Wave1PlaytestInfrastructure.md#debug-hud) is enough to run this gate even
> before the production HUD (W1-30/31) lands, so there's no need to wait for polish to check the
> mechanic underneath it.

---

### W1-28 — `AudioService`

- **Goal:** One persistent, event-driven audio service, folding `MusicPlayer` + `MenuMusic` into one (CodeReview's duplicate-singleton flag) and fixing the per-frame `FindObjectOfType<MusicPlayer>()` in `OptionsControllers` (#1/R1) at the root.
- **Prerequisites:** W1-01, W1-02, and ideally most gameplay events already exist to wire real cues (W1-12, W1-22, W1-25, W1-27) — can start once `Singleton<T>`/events exist and add subscriptions incrementally.
- **Files affected:** new `Assets/Scripts/Audio/AudioService.cs`; delete `Assets/Scripts/Legacy/MusicPlayer.cs`, `Assets/Scripts/Legacy/MenuMusic.cs`.
- **Complexity:** M
- **Risks:** Scope creep into a full audio/mixer pass — that's Wave 4.2. This task is "hooks exist, 2–3 real cues wired" (swap stinger, downed sting, checkpoint chime), nothing more. Keep `MusicPlayer`'s "random track from playlist" behavior — it's the one piece of real logic worth carrying forward.
- **Player Experience Goal:** Foundation only / polish — no gate of its own.
- **DCR Alignment:** DCR-002 (Swap) + DCR-003 (Cooperation) — informational; audio is one of [CoreFantasy](../design/CoreFantasy.md#why-this-exists)'s named primary vehicles for emotion (alongside animation/gameplay), so the swap stinger / downed sting / checkpoint chime are small but real reinforcements of those philosophies.
- **Design Validation Questions:** Do the swap stinger and downed sting read as emotionally appropriate (confidence/continuity for swap; tension-not-punishment for downed), or as generic UI beeps?
- **Definition of Done:** `AudioService : Singleton<AudioService>` exposes `PlaySfx`/`PlayMusic`/`SetMasterVolume` (matches `PlayerPrefsController`'s existing contract); subscribes to `OnCatSwapped`/`OnCatDowned`/`OnCheckpointReached`; `MusicPlayer.cs`/`MenuMusic.cs` deleted.
- **Suggested commit message:** `Added AudioService, consolidating MusicPlayer/MenuMusic into one event-driven service`

---

### W1-29 — `OptionsControllers` refactor + `PlayerPrefsController` fix

- **Goal:** Remove the per-frame `FindObjectOfType<MusicPlayer>()` (#1/R1) and the no-op `[SerializeField] public static` (#9); fix `SetDifficulty` calling `SetMasterVolume` (#3) by deleting the not-yet-designed difficulty methods on both sides rather than un-commenting a known-buggy stub.
- **Prerequisites:** W1-28.
- **Files affected:** `Assets/Scripts/Legacy/OptionsControllers.cs` → `Assets/Scripts/UI/OptionsControllers.cs` (moves out of Legacy since it's now fixed), `Assets/Scripts/Legacy/PlayerPrefsController.cs` → `Assets/Scripts/UI/PlayerPrefsController.cs`.
- **Complexity:** S
- **Risks:** The commented-out difficulty UI is a "no half-finished implementations" trap — the architecture doc explicitly recommends cutting it rather than wiring a stub, since difficulty isn't designed yet. Don't reintroduce it under this task.
- **Player Experience Goal:** Foundation only / bugfix.
- **DCR Alignment:** N/A — infrastructure/bugfix.
- **Definition of Done:** `OptionsControllers` calls `AudioService` directly, no `FindObjectOfType`; dead `public static` field removed; both difficulty methods deleted from `PlayerPrefsController` and `OptionsControllers`; both files moved out of `Legacy/`.
- **Suggested commit message:** `Fixed OptionsControllers/PlayerPrefsController: removed FindObjectOfType, dead static field, difficulty stub`

---

### W1-30 — HUD core (health, portrait swap, ability unlock)

- **Goal:** HUD reacts to events/state instead of being polled or discovered — root-fixes CodeReview #7 (stale HUD refs across scene loads).
- **Prerequisites:** W1-24 (`OnDamaged`), W1-12 (`OnCatSwapped`), W1-22 (`OnAbilityUnlocked`).
- **Files affected:** `Assets/Scripts/UI/HealthHUD.cs`, `Assets/Scripts/UI/PortraitHUD.cs`, `Assets/Scripts/UI/AbilityToastHUD.cs`.
- **Complexity:** M/L
- **Risks:** The rule this task exists to enforce: **HUD is always scene-local; state is never cached across a scene load.** Read `GameManager.Instance.State` once in `OnEnable` for initial values only, then update purely from event callbacks — caching a value across a scene boundary reintroduces #7/R4.
- **Player Experience Goal:** First successful swap / first personality expression — a legibility pass. This doesn't create either feeling, it sharpens what W1-13/W1-22 already built.
- **DCR Alignment:** DCR-002 (Swap) + DCR-001 (Movement) — informational.
- **Design Validation Questions:**
  - Does the portrait swap make it instantly legible who's leading, without needing a HUD glance to double-check?
  - Does the ability-unlock toast make gaining a new expression of personality feel like a moment, not just a checkbox?
- **Definition of Done:** Health bar(s) update from `OnDamaged`; active-cat portrait updates from `OnCatSwapped`; unlock toast fires from `OnAbilityUnlocked`; no `Update()`-loop polling of gameplay state anywhere in these three files.
- **Suggested commit message:** `Added event-driven HUD: health, portrait swap, ability unlock toast`

---

### W1-31 — Interaction/revive/checkpoint prompt UI

- **Goal:** The remaining HUD surface: interact prompts and downed/checkpoint feedback.
- **Prerequisites:** W1-23 (`OnInteractableFocusChanged`), W1-25 (`OnCatDowned`/`OnCatRevived`), W1-27 (`OnCheckpointReached`), W1-30 (shares the HUD subscribe-and-render pattern).
- **Files affected:** `Assets/Scripts/UI/InteractPromptHUD.cs`, `Assets/Scripts/UI/DownedFadeHUD.cs`.
- **Complexity:** S/M
- **Risks:** Same HUD rule as W1-30 — no cross-scene state caching.
- **Player Experience Goal:** First cooperative moment — a legibility pass, making W1-25's trust beat readable to a real player without relying on the debug HUD.
- **DCR Alignment:** DCR-003 (Cooperation) — primary.
- **Design Validation Questions:**
  - Does the revive prompt make helping your partner feel like a deliberate choice, rather than something that just happens automatically?
  - Does the downed-fade communicate urgency without reading as punishing or panic-inducing?
- **Definition of Done:** Interact prompt shows/hides from `OnInteractableFocusChanged` (uses `IInteractable.Prompt`); downed/both-down fade responds to `OnCatDowned`/`OnBothCatsDowned`; checkpoint chime/flash responds to `OnCheckpointReached`.
- **Suggested commit message:** `Added interact prompt and downed/checkpoint HUD feedback`

---

### W1-32 — Legacy platform/exit fixes (`Platform`, `MovingPlatform`, `LevelExit`)

- **Goal:** Fix the three remaining legacy bugs the greybox level actually needs working: `Platform`'s re-parent-any-collider bug (#5/R9), `MovingPlatform`'s empty `Start`/unreachable `Destroy` branch (#8), and `LevelExit`'s missing player check (#6/R6) — all three now share the `ICatIdentity` check from W1-04 instead of three separate ad-hoc guesses.
- **Prerequisites:** W1-04, W1-11 (needs the tagged cat prefab to test against), W1-06 (`LevelExit` redirects to `SceneFlowManager` instead of `FindObjectOfType<LevelLoader>()`).
- **Files affected:** `Assets/Scripts/Legacy/Platform.cs` → `Assets/Scripts/Cats/Platform.cs`, `Assets/Scripts/Legacy/MovingPlatform.cs` → `Assets/Scripts/Cats/MovingPlatform.cs`, `Assets/Scripts/Legacy/LevelExit.cs` → `Assets/Scripts/Interaction/LevelExit.cs`.
- **Complexity:** M
- **Risks:** Low individually — these are the "keep the mechanism, fix the bug" refactors the migration table calls for, not rewrites. Verify the `OnTriggerStay2D` re-parent fix doesn't break legitimate rider physics (test on an actual moving platform, not just statically).
- **Player Experience Goal:** Foundation only / bugfix.
- **DCR Alignment:** DCR-003 (Cooperation) — informational; correct `ICatIdentity` checks are what the interaction/cooperation systems built on top of these fixes depend on.
- **Definition of Done:** `Platform` only re-parents colliders carrying `ICatIdentity`; `MovingPlatform`'s dead code paths removed, waypoint-follow behavior unchanged; `LevelExit` checks `ICatIdentity` and calls `SceneFlowManager` (not `FindObjectOfType`); all three moved out of `Legacy/`.
- **Suggested commit message:** `Fixed Platform/MovingPlatform/LevelExit using shared ICatIdentity check`

---

### W1-33 — Greybox test level assembly 🔒

- **Goal:** Assemble the one Wave 1 test level (flow corridor → co-op puzzle room → flow, Roadmap 1.7) — the deliverable the 🚦 gate is judged on.
- **Prerequisites:** every task above (this is the integration step).
- **Files affected:** new scene, e.g. `Assets/Levels/Wave1Greybox.unity`; tilemap/level geometry; prefab placement (both cat prefabs, `CheckpointComponent`, `PuzzleRoomTrigger`, `LevelExit`, any `MovingPlatform`s); vcam setup.
- **Complexity:** L
- **Risks:** Entirely Editor-driven — level layout, puzzle-room co-location gate placement, checkpoint/exit placement, Cinemachine vcam setup. Per CLAUDE.md this is handed to the user; flag it, don't fake it. Budget real playtest-and-iterate time after first assembly — the architecture doc expects this gate to take iteration, not land clean on the first pass.
- **Player Experience Goal:** The full loop — first controllable cat through first cooperative moment, together, inside real level rhythm (flow → puzzle → flow).
- **DCR Alignment:** DCR-001 + DCR-002 + DCR-003 — all three, integrated; this is where they're judged together rather than in isolation.
- **Design Validation Questions:**
  - Carry forward whatever's still open from Gates A–E and check it again inside a real level, not just the test scene/greybox stub.
  - Across a full flow → puzzle → flow segment, does the game read as "two companions, conducted as one" (the Wave 1 end-state of the [mastery curve](../design/gameplay/Movement.md#mastery-curve)), or still as "two separate characters I switch between"?
- **Definition of Done:** Level loads, both cats spawn with starting kits granted, swap + camera retarget works, partner AI follows and teleport-recovers, the puzzle room gates teleport-recover correctly, at least one ability is usable, downed/revive and both-down/checkpoint-restart all function, `LevelExit` completes the level.
- **Suggested commit message:** `Assembled Wave 1 greybox test level`

---

> 🚦 **Gate F — Full loop, in a real level.** This is the existing Roadmap 1.7 gate, not a new
> checkpoint — see [Review Gates](#review-gates) above. Bring Gates A–E's still-open design
> questions into this session as the starting checklist rather than starting from a blank page.

---

## Dependency summary

**Hard sequence (each blocks the next major phase):**
W1-00 → W1-01/02/03/04 (core infra, mutually independent, can be split across sessions) →
W1-05/06 (needs 01–03) → W1-08/09 (needs 07 / needs Input System) → W1-10 (needs 08+09+04) →
W1-11 (Editor) → W1-12 (needs 10+11) → { W1-13, W1-14 } (both need 12) → W1-15 (needs 14) →
W1-16 (needs 15) → W1-17 (needs 10+07) → { W1-18…21 } (each needs 17 only) → W1-22 (needs
18–21 + 12) → W1-23 (needs 09+11) → W1-24 (needs 08+02) → W1-25 (needs 24+23+15) →
W1-26 (needs 25, stub from 05) → W1-27 (needs 26+03) → W1-28 (needs 01+02) →
W1-29 (needs 28) → W1-30 (needs 24+12+22) → W1-31 (needs 23+25+27+30) →
W1-32 (needs 04+11+06) → **W1-33 (needs everything)**.

**Can be developed independently / in parallel once their one shared prerequisite lands:**
- W1-01, W1-02, W1-03, W1-04 — independent of each other, only need W1-00.
- W1-18, W1-19, W1-20, W1-21 (the four abilities) — independent of each other, only need W1-17. Good candidates for splitting across sessions or, per Roadmap's tooling plan, for extracting a `/new-ability` skill after the first one (W1-18) lands.
- W1-13 (Camera) and W1-14 (Partner AI) — both only need W1-12, no dependency on each other.
- W1-28 (Audio) can start as soon as W1-01/02 exist and have cues added incrementally as later events land, rather than waiting for the whole wave.
- W1-32 (legacy platform/exit fixes) only needs W1-04/W1-06/W1-11 — doesn't depend on the ability, health, or interaction systems at all, so it can run any time after those three.
- W1-30 and W1-31 (HUD) can split across two sessions/people once their respective event sources exist.

**True bottlenecks (everything downstream stalls if these slip):**
- **W1-00** — blocks literally everything; do it first, alone.
- **W1-10 (`CatController`)** — the whole Cats/Abilities/Interaction/Health branch is downstream of this one file.
- **W1-12 (`ActiveCatManager`)** — Camera, Partner AI, and (transitively) Downed/Revive's teleport-lock all need it.
- **W1-17 (`CatAbility` base)** — all four abilities and the grant service wait on this.
- **W1-33 (greybox assembly)** — the 🚦 gate itself; nothing else can substitute for it, and it's 100% Editor time.

**Review gates relative to this sequence** (see [Design & playtest framework](#design--playtest-framework) above — none of these change the sequence, only when to pause and look at it): Gate A after W1-11 · Gate B after W1-13 · Gate C after W1-16 · Gate D after W1-22 · Gate E after W1-27 · Gate F (existing Roadmap gate) after W1-33.

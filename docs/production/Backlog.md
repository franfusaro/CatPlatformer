# Backlog — Two Cats

> **Purpose:** Living, finer-grained queue of concrete work items not yet scheduled into the active wave.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [Roadmap](Roadmap.md), [KnownRisks](KnownRisks.md), [../technical/baseline/CodeReview](../technical/baseline/CodeReview.md)

Living list of concrete work items not yet scheduled into the active wave. The
**wave plan** lives in `docs/production/Roadmap.md`; this is the finer-grained queue and the
"don't forget this" list. Keep it pruned.

Legend: **P1** now-ish · **P2** soon · **P3** eventually · ✅ done · 🔒 needs Unity Editor

---

## Wave 0 — Foundation (active)

| ID | Item | Pri | Ref | Status |
|----|------|:---:|-----|--------|
| 0.1 | Upgrade Unity 2018.3 → target LTS; fix compile errors | P1 | `docs/technical/baseline/DependencyAudit.md`, ADR-0001 | ✅ done |
| 0.2 | Migrate CrossPlatformInput → Input System | P1 | `docs/technical/baseline/DependencyAudit.md` | 🔒 todo |
| 0.3 | Swap vendored 2d-extras → `com.unity.2d.tilemap.extras`; drop unused pkgs/modules | P1 | `docs/technical/baseline/DependencyAudit.md` | 🔒 todo |
| 0.4 | Phase-1 bug fixes (see "Known defects" below) | P1 | `docs/technical/baseline/CodeReview.md` | todo |
| 0.5 | Scaffolding: CLAUDE.md, .editorconfig, LICENSE, BACKLOG, ADR template | P1 | — | ✅ done |
| 0.6 | CI/CD: GameCI build+test on push, deploy WebGL to Pages on merge | P1 | `docs/production/Playbook.md` §3 | todo |
| 0.7 | Assembly definitions (Core/Gameplay/UI) + first EditMode test | P2 | `docs/technical/Architecture.md` | 🔒 partial |

## Known defects (Phase-1 fix set → item 0.4)

Source: `docs/technical/baseline/CodeReview.md` Technical-Debt table. Fix these once we're on the LTS
(some overlap the input migration — do 0.4 after 0.1/0.2 to avoid rework).

- [ ] **#1** `LevelLoader.NextLevel`/`LoadMainMenu`: destroy-then-null `ScenePersist` → NRE. *(High)*
- [ ] **#2** `LevelLoader.LoadYouLoseScene`: loads `"LoseScreen"` — scene doesn't exist. *(High)*
- [ ] **#3** `PlayerPrefsController.SetDifficulty`: copy-paste bug, sets volume not difficulty. *(Med)*
- [ ] **#4** `Player.Jump`: `velocity +=` should be `=` → inconsistent jump height. *(Med)*
- [ ] **#5** `Platform.OnTriggerStay2D`: re-parents any collider every frame → jitter. *(Med)*
- [ ] **#6** `CoinPickup`/`LevelExit`: no player check → wrong collider triggers. *(Med)*
- [ ] **#7** `GameSession` HUD refs go stale across scenes. *(Med)*
- [ ] **#8** `MovingPlatform`: empty `Start`; unreachable `Destroy` branch. *(Low)*
- [ ] **#9** `OptionsControllers.defaultVolume`: `[SerializeField] public static` is a no-op. *(Low)*

## Tooling to extract (not before the pattern repeats — see ROADMAP §Tooling)

- [ ] **`/new-ability`** skill — extract after hand-building the 1st `CatAbility` (Wave 1).
- [ ] **`/new-level`** skill — extract after the 1st greybox level (Wave 1).
- [ ] Evaluate a **Unity Editor ↔ Claude MCP bridge** when we hit Wave 0 upgrade /
      Wave 1 scene wiring (trust/security review required first).

## Open decisions (also tracked in ADRs as they're made)

- [x] Pick the exact target LTS — **6000.5.8f1** (Unity Hub's recommended Tech Stream
      build), confirmed 2026-08-15 — ADR-0001.
- [ ] Confirm **LICENSE**: MIT placeholder committed in 0.5. Art/music/SFX assets may
      warrant separate terms — revisit before any public release.
- [ ] GitHub Pages: confirm publishing source (GitHub Actions) and that the repo is
      allowed to deploy Pages.

## Later waves (headlines only — detail in ROADMAP)

- Wave 1: `ActiveCatManager` + `CatController`, instant swap, partner AI, ability
  system + grant API, starting kit, HP + down/revive, greybox test level.
  **Architecture accepted** (ADR-0002) — full system design, event list,
  folder/asmdef plan, legacy migration table, and an 11-step build order in
  `docs/technical/Wave1Architecture.md`. **Implementation backlog** (34 atomic
  tasks, ordered, with dependency map) in `docs/production/Wave1Backlog.md`.
  Implementation not yet started.
- Wave 2: `IDamageable` + enemies, combat abilities, first boss, recall/toss, checkpoints.
- Wave 3: NPC/quest system, ability unlocks, PixelLab art pass, real levels, currency.
- Wave 4: URP 2D + lighting, AudioMixer, perf pass, menus/accessibility, tagged release.

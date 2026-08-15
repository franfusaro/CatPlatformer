# ADR-0001: Target Unity LTS for the modernization upgrade

- **Status:** Accepted (confirmed 2026-08-15, running 6000.5.8f1)
- **Date:** 2026-07-09
- **Deciders:** Franco Fusaro
- **Wave/Item:** Wave 0 · 0.1

## Context
The project is on **Unity 2018.3.0f2** (Dec 2018, long out of support, no LTS). Every
Wave-0 item past scaffolding depends on moving to a supported LTS first: the Input
System migration (0.2), the `2d-extras` → `com.unity.2d.tilemap.extras` package swap
(0.3), and CI builds via GameCI all assume a modern editor. `docs/technical/baseline/DependencyAudit.md`
recommends "a current LTS — 2021/2022 LTS as a staged path, or the latest LTS."

Constraints:
- The upgrade is the **riskiest single step** and must be done **in the Unity Editor**
  (the AI assistant cannot drive the GUI); it's done in isolation on a branch.
- Existing subsystems in use: Cinemachine 2.2.9, TextMeshPro 1.3.0, Tilemap +
  vendored 2d-extras, legacy CrossPlatformInput. All have clean upgrade paths on 2021/2022 LTS.
- GameCI's `unity-builder`/`unity-test-runner` read the version from
  `ProjectSettings/ProjectVersion.txt`, so CI tracks whatever version we land on — no
  hardcoding needed, but the chosen LTS must have a matching GameCI editor image.

## Decision
*(Pending — to be finalized when you open the project in the Editor.)*

**Revised 2026-08-02:** upgrade to **Unity 6 (6000.x, "Unity 6.5" install stream)**
instead of the originally-recommended 2022.3 LTS. The original recommendation was
made 2026-07-09 when 2022.3 was the safer, more mature choice and Unity 6's GameCI
image support was still new. Nearly a month later that calculus has flipped: 2022.3
LTS's standard support window (~2 years from its April 2023 release) has lapsed,
while Unity 6 is now the actively patched, current LTS-equivalent line with mature
GameCI support. Landing on 2022.3 today would mean upgrading onto an engine that's
*already* unsupported — defeating the point of this migration.

**Confirmed 2026-08-15:** Unity Hub installed **6000.5.8f1** as its default
"recommended" build. Worth being explicit about a nuance this surfaced: 6000.5 is
Unity's **Preview/Tech Stream** release, not one of the LTS-designated lines
(6000.0 LTS, supported to ~Oct 2026; 6000.3, Unity's newer "Update release" with
LTS-level support to ~Dec 2027). Tech Stream trades some stability for being
first in line for new features/fixes — concretely, it's why the project hit a
hard `CS0619` compile error (`GetInstanceID()`→`GetEntityId()`) in vendored code
that would still just be a warning on the LTS lines. Given the choice, we're
**staying on 6000.5.8f1** — Hub presents it as the recommended install and the
one breaking change hit so far was a one-line, mechanical fix. Revisit this if
Tech Stream churn becomes a recurring time sink; 6000.3 is the fallback.

## Consequences
- **Good:** supported toolchain; Input System, tilemap-extras package, URP-2D (Wave 4),
  Brotli WebGL compression + growable heap all become available; GameCI images exist
  for Unity 6.
- **Bad / cost:** a one-time Editor-driven upgrade with likely compile/API fixes —
  larger delta than 2018.3→2022.3 would have been, since Unity 6 is a bigger jump;
  some scenes/prefabs may need re-serialization; WebGL build settings change.
- **Follow-ups:** BACKLOG 0.2 (input), 0.3 (packages), 0.6 (CI needs the chosen editor
  image). Update `CLAUDE.md` status + `ProjectVersion.txt` after the upgrade.

## Alternatives considered
- **Unity 2022.3 LTS** (original recommendation) — rejected on revision: its support
  window has lapsed by the time of upgrade, so it no longer satisfies "a current LTS."
- **Staged 2021.3 → 2022.3 → 6** — safer per hop, but triple the Editor work for a
  ~700 LOC codebase; not worth it here.
- **Stay on 2018.3** — rejected: unsupported, blocks input migration, CI, and URP.

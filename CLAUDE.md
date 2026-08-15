# CLAUDE.md — Two Cats (CatPlatformer)

> Context anchor for AI-assisted development. Read this first every session.
> The authoritative build plan is **`docs/production/Roadmap.md`**; the game vision is
> **`docs/design/Vision.md`**. This file captures conventions, guardrails, and the
> "how we work" that those docs assume.

## What this is
A 2D cat platformer built in Unity. The long-term vision (`docs/design/Vision.md`)
is a **two-cat swap** game — Orange and Tuxedo — with a persistent AI partner,
ability system, HP + down/revive, and flow→puzzle→boss level rhythm. Today the
repo is the **legacy base**: a single-cat platformer (4 levels, coins, ladders,
hazards) on Unity 2018.3, which we are modernizing wave by wave.

## Current status (keep this honest)
- **Engine:** Unity **2018.3.0f2** (target: a current LTS — see ADR-0001, upgrade
  pending, must be done in the Editor).
- **Roadmap position:** **Wave 0 — Foundation.** See `docs/production/Roadmap.md`.
- **Input:** legacy `UnityStandardAssets.CrossPlatformInput` (to be replaced with
  the Input System in 0.2).
- **Vendored third-party** (excluded from our quality bar): `Assets/Standard
  Assets/` (CrossPlatformInput, `2d-extras-master`) and `Assets/TextMesh Pro/`.

## Layout
- `Assets/Scripts/` — the 16 custom gameplay scripts (~700 LOC). This is *our* code.
- `Assets/Levels/` — 7 scenes: `MainMenu`, `OptionsMenu`, `Level 1`–`4`, `Success`.
- `Assets/{Animations,Sprites & Tiles,Music,SFX,Prefabs,Materials,Fonts}/` — assets.
- `docs/` — domain-organized knowledge base (`design/` bible · `technical/` incl.
  `baseline/` as-built · `production/` · `decisions/` · `Ideas.md` · `archive/`).
  **Start at `docs/README.md`** — the index explains where everything lives.
- `Build/` — a stale 2018 WebGL build (will be replaced by CI output; don't rely on it).

## How we work (from `docs/production/Playbook.md`)
- Each **Wave** in `docs/production/Roadmap.md` ends in something playable you approve at a 🚦 gate.
- Dev cycle: research → design → build → verify → **playtest** → ship.
- **I cannot drive the Unity Editor** from the shell. Editor-only work (engine
  upgrade, Input System migration, scene/prefab wiring, package swaps) is handed to
  you with clear steps; I do the code, text, and CI that don't need the GUI.

## Conventions
- **C#:** MonoBehaviour components. Cache `GetComponent` in `Start`/`Awake`, never in
  `Update`. Prefer named `const string` over magic strings (as `Player.cs` does).
- **No `FindObjectOfType` in `Update`** — it's O(n) over the scene. The legacy code
  does this in several places; new/refactored code caches the reference once.
- **Player identity:** don't use collider *type* as a proxy for "is the player"
  (legacy bug). Use a tag/marker component — see BACKLOG / code-review #6.
- **Naming:** `PascalCase` types & methods, `camelCase` fields. Serialized tuning
  fields use `[SerializeField]`. (Legacy code is inconsistent — new code isn't.)
- **Namespaces / asmdefs:** none yet; introduced in Wave 0 (0.7).
- **Meta files:** every asset has a committed `.meta` — never delete or reorder GUIDs
  by hand; let Unity manage them.

## Guardrails
- Don't upgrade the Unity version, migrate input, or rewire scenes without an
  Editor pass by the user — flag it, don't fake it.
- Don't commit or push unless asked. Branch before committing on `master`.
- Keep `docs/production/Roadmap.md`'s status line and `docs/production/Backlog.md` current as work lands.
- WebGL/CI is the shipped target; keep changes WebGL-safe (no threads, no reflection
  emit, watch download size).

## Key references
- Build order → `docs/production/Roadmap.md`
- Vision → `docs/design/Vision.md`
- Known defects & refactors → `docs/technical/baseline/CodeReview.md`, `docs/production/KnownRisks.md`
- Upgrade/tech detail → `docs/technical/Architecture.md`, `docs/technical/baseline/DependencyAudit.md`
- Process/CI → `docs/production/Playbook.md`
- Backlog → `docs/production/Backlog.md`
- Decisions → game-design: `docs/decisions/DECISION_LOG.md` · technical: `docs/decisions/adr/`
- Docs index & conventions → `docs/README.md`

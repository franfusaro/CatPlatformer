# Two Cats — Documentation

> **Purpose:** The index and operating manual for this knowledge base — what lives where, how it's organized, and how to contribute.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [design/Vision](design/Vision.md), [production/Roadmap](production/Roadmap.md), [decisions/DECISION_LOG](decisions/DECISION_LOG.md)

## Documentation philosophy

This knowledge base is **domain-oriented, not document-oriented** — you navigate by
*what you want to know* (design? technical? production?), not by remembering a filename.
Two rules make it scale:

1. **Separate "what we're building" from "what exists today."** The forward-looking
   **Design Bible** (`design/`) is the north star. The **as-built baseline**
   (`technical/baseline/`) is forensic reverse-engineering of the inherited legacy code —
   accurate, still-true, but describing the *old* single-cat game we're modernizing.
   Never let the two blur.
2. **Nothing is lost.** Superseded docs are **archived** (`archive/`), not deleted.
   Unapproved ideas are **parked** (`Ideas.md`), not deleted.

## Folder structure

```
docs/
├── README.md            ← you are here (index + conventions)
├── design/              ═ THE DESIGN BIBLE — the game we're building (canonical) ═
│   ├── Vision · CoreFantasy · DesignPillars · CoreLoop · PlayerExperience · GameRules · Glossary
│   ├── gameplay/   Movement · Abilities · Combat · Progression · Collectibles
│   ├── narrative/  Story · Characters · NPCs · Bosses · Lore · Timeline · SideQuests
│   ├── world/      Regions · Biomes · WorldMap · Secrets
│   ├── levels/     LevelStructure · Tutorial · DifficultyCurve
│   └── art/        StyleGuide · Animations · Tilesets · VFX · AudioDirection
├── technical/           ═ HOW IT'S BUILT ═
│   ├── Architecture · Wave1Architecture · Wave1PlaytestInfrastructure · UnityStructure · SceneManagement · SaveSystem · AI
│   └── baseline/   as-built reverse-engineering of the legacy game (see its README)
├── production/          ═ HOW WE RUN THE PROJECT ═
│   └── Roadmap · Milestones · Backlog · KnownRisks · Playbook
├── decisions/           ═ WHY ═
│   ├── DECISION_LOG.md   game-design decisions
│   └── adr/              numbered technical/architectural decisions
├── Ideas.md             ═ idea parking lot (nothing deleted) ═
└── archive/             ═ superseded docs, kept for history ═
```

## Where do I start?

| I want to… | Go to |
|------------|-------|
| Understand the game | [design/Vision](design/Vision.md) → [design/CoreLoop](design/CoreLoop.md) |
| Build the next thing | [production/Roadmap](production/Roadmap.md) → [production/Backlog](production/Backlog.md) |
| Understand the current code | [technical/baseline/README](technical/baseline/README.md) |
| Know the target architecture | [technical/Architecture](technical/Architecture.md) |
| See why a design choice was made | [decisions/DECISION_LOG](decisions/DECISION_LOG.md) |
| See why a technical choice was made | [decisions/adr/](decisions/adr/0000-template.md) |
| Make/regenerate art | [design/art/StyleGuide](design/art/StyleGuide.md) |
| Park a new idea | [Ideas](Ideas.md) |

## Where new documentation belongs

- A **game-design** topic (mechanic, character, level, art) → the matching `design/`
  sub-folder. If it doesn't fit an existing file, add one (see naming below).
- A **technical** topic → `technical/` (or `technical/baseline/` if it documents current
  code as-built).
- **Planning / process** → `production/`.
- A **decision** → `decisions/DECISION_LOG.md` (design) or a new numbered `decisions/adr/`
  file (technical).
- An **unapproved idea** → `Ideas.md`. When approved, move it into the Design Bible and
  log the decision.

## Naming & document conventions

- **Files:** `PascalCase.md` inside `design/` and `technical/` (e.g. `Movement.md`);
  folder READMEs are `README.md`. Baseline docs keep the `Legacy…`/as-built naming.
- **Every doc starts with a metadata header:**
  ```
  > **Purpose:** one line — what this doc is for.
  > **Owner:** … · **Status:** Living | Draft | Planned | Baseline (as-built) | Superseded · **Last Updated:** YYYY-MM-DD
  > **Related:** [Doc](path), [Doc](path)
  ```
- **Status values:** *Living* (canonical, actively maintained) · *Draft* (real content,
  in progress) · *Planned* (stub, to be written) · *Baseline (as-built)* (describes
  current code) · *Superseded* (see `archive/`).
- **Cross-link liberally** with relative links; a long doc should have a table of contents.
- **Dates are absolute** (`2026-07-10`), never "last week".
- **Meta files** (Unity `.meta`) are never touched by docs work.

## How ADRs work

**Architectural / technical** decisions get a numbered ADR in `decisions/adr/`:
- Copy [`adr/0000-template.md`](decisions/adr/0000-template.md) to
  `adr/NNNN-short-title.md` (next number).
- Fill in Context · Decision · Consequences · Alternatives considered; set Status
  (Proposed → Accepted → Superseded/Deprecated).
- Link the ADR from the affected technical doc and from `production/Backlog.md`.

## How the Decision Log works

**Game-design** decisions (mechanics, feel, progression, narrative) go in
[`decisions/DECISION_LOG.md`](decisions/DECISION_LOG.md), newest first, using
**Decision · Alternatives · Reasoning · Consequences · Status.** This keeps design "why"
out of your head and separate from technical ADRs. Superseded entries are marked, never
deleted.

## How to contribute

1. Follow the **dev cycle** in [production/Playbook](production/Playbook.md)
   (research → design → build → verify → playtest → ship), with approval gates after
   Design and after Playtest.
2. Put the change in the right domain folder; add the metadata header; cross-link.
3. Record the *why*: a Decision Log entry (design) or an ADR (technical).
4. Keep [production/Roadmap](production/Roadmap.md) status and
   [production/Backlog](production/Backlog.md) current as work lands.

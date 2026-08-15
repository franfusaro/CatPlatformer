# Save System

> **Purpose:** Persistence — what's saved today (nothing but volume) and the target save system for progress, unlocks and cosmetics.
> **Owner:** Franco Fusaro · **Status:** Planned · **Last Updated:** 2026-07-10
> **Related:** [baseline/DependencyAudit](baseline/DependencyAudit.md), [Architecture](Architecture.md), [../design/gameplay/Progression](../design/gameplay/Progression.md), [../design/gameplay/Collectibles](../design/gameplay/Collectibles.md)

## As-built (today)

**There is no save system.** The only persistence is `PlayerPrefs` for master volume
(via `PlayerPrefsController.cs`). Score, coins, and level progress do **not** persist —
they reset on restart. (See [baseline/GameplayMechanics § UI/Save](baseline/GameplayMechanics.md).)

## Target — real save system

A JSON/binary save file for:
- **Progress:** furthest level / checkpoint reached.
- **Unlocks:** abilities gained (found/taught/earned — see [Progression](../design/gameplay/Progression.md)).
- **Cosmetics & currency:** yarn/coins and purchased cosmetics (see [Collectibles](../design/gameplay/Collectibles.md)).
- Optional high score / stats.

Keep it **WebGL-safe** (IndexedDB-backed `Application.persistentDataPath`; no threads,
no reflection emit).

## TODO (to design)

- [ ] Save format + schema/versioning.
- [ ] When we save (checkpoint, level end, ability unlock).
- [ ] Migration/versioning strategy as content grows.

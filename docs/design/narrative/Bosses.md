# Bosses

> **Purpose:** Boss design — the stun→DPS loop, the puzzle↔action variety spectrum, and arenas as puzzle rooms.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [../gameplay/Combat](../gameplay/Combat.md), [../levels/LevelStructure](../levels/LevelStructure.md), [../GameRules](../GameRules.md)

## Boss loop (the swap *is* the combat)

```
Boss armored / invulnerable
   → ⬛⬜ Tuxedo: FLOP stuns / cracks armor  (opens a window)
   → ⟳ SWAP
   → 🟠 Orange: dash-combo DPS during the window
   → window closes → repeat, dodging the whole time
```

Full combat mechanics: [gameplay/Combat](../gameplay/Combat.md).

## Arenas = puzzle rooms

**Boss arenas = puzzle rooms** (teleport-recover off, both cats present, exit locked
until the boss falls) — reuses the same tech as [levels/LevelStructure](../levels/LevelStructure.md).
In arenas the boss can target *either* cat (see [GameRules](../GameRules.md)).

## Variety spectrum

Puzzle-leaning (weak point only wall-cling Orange can reach while Tuxedo holds a lever)
↔ action-leaning (pure stun→DPS dance).

## Open tuning

Boss-phase count and stun/DPS windows — decide while playtesting.

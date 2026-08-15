# Level Structure — Flow, Puzzle Rooms & Rhythm

> **Purpose:** How levels are built from the rhythm of modes, and the co-location puzzle-room gate that anchors puzzles and boss arenas.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [../CoreLoop](../CoreLoop.md), [../gameplay/Movement](../gameplay/Movement.md), [../gameplay/Combat](../gameplay/Combat.md), [Tutorial](Tutorial.md), [DifficultyCurve](DifficultyCurve.md), [../../technical/SceneManagement](../../technical/SceneManagement.md)

## Level rhythm (the macro loop)

The macro structure is a **rhythm of modes**, layout-signaled so the player knows
which gear they're in:

```
flow corridor → puzzle room → flow corridor → mini-combat → puzzle room → BOSS arena
   (open)         (enclosed)      (open)        (open)        (enclosed)     (enclosed)
```

Flow is the release valve; enclosed rooms are the tension. Never two enclosed rooms
back-to-back without flow between them.

## Puzzle rooms (co-location gates)

- Visually **enclosed** rooms with a **locked exit**; entering **disables
  teleport-recover** (see [Movement — persistent partner](../gameplay/Movement.md)).
- Solved by combining abilities: e.g. **Tuxedo Loafs into a platform / holds a plate →
  Orange wall-clings & dashes to a switch → exit opens for both.**
- Neither cat can solve it alone — that's the definition of a puzzle room.

## Boss arenas reuse the gate

Boss arenas are puzzle rooms with the same teleport-off / both-present / locked-exit
tech. See [narrative/Bosses](../narrative/Bosses.md) and [gameplay/Combat](../gameplay/Combat.md).

> The legacy 4-level, build-index-ordered scene flow (as-built) is documented in
> [baseline/LevelArchitecture](../../technical/baseline/LevelArchitecture.md).

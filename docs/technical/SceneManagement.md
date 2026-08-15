# Scene Management

> **Purpose:** How scenes load and persist — today's build-index flow (as-built) and the target SceneFlowManager, including the puzzle-room / arena gate.
> **Owner:** Franco Fusaro · **Status:** Draft (target + as-is pointer) · **Last Updated:** 2026-07-10
> **Related:** [baseline/LevelArchitecture](baseline/LevelArchitecture.md), [Architecture](Architecture.md), [../design/levels/LevelStructure](../design/levels/LevelStructure.md)

## As-built (today)

Full detail in [baseline/LevelArchitecture](baseline/LevelArchitecture.md). Summary:
- 7 scenes navigated by **build index** (`currentSceneIndex + 1`); order is load-bearing.
- Three `DontDestroyOnLoad` duplicate-destroying singletons (`GameSession`,
  `ScenePersist`, `MusicPlayer`).
- All loads are single-scene **synchronous** `SceneManager.LoadScene`; no async, no
  loading screens, no additive scenes.
- **Known hazards:** missing `LoseScreen` scene, double-`FindObjectOfType` teardown NRE,
  stale HUD refs, scene-order coupling (see [../production/KnownRisks](../production/KnownRisks.md)).

## Target — SceneFlowManager

- Named-scene constants + bounds checks (kills index fragility; fixes the `LoseScreen`
  and teardown-NRE defects).
- Cache manager references before destroy; async loads + a loading screen as content grows.
- Level metadata as ScriptableObjects.

## Puzzle-room / boss-arena gate

The design's enclosed rooms disable teleport-recover on entry and lock the exit (see
[../design/levels/LevelStructure](../design/levels/LevelStructure.md)). Boss arenas reuse
the same gate. This is scene/room-scoped state the SceneFlowManager (or a room controller)
owns.

## TODO

- [ ] Decide room-gate ownership (SceneFlowManager vs. per-room controller).
- [ ] Additive-scene strategy for hub + level streaming (if Hub-and-spoke is chosen).

# AI — Enemy & Partner Behaviour

> **Purpose:** The game's AI — today's patrol enemy (as-built) and the planned partner-follow / teleport-recover system that makes the persistent-partner design work.
> **Owner:** Franco Fusaro · **Status:** Draft (as-is + target) · **Last Updated:** 2026-07-10
> **Related:** [baseline/GameplayMechanics](baseline/GameplayMechanics.md), [Architecture](Architecture.md), [../design/gameplay/Movement](../design/gameplay/Movement.md), [../design/narrative/NPCs](../design/narrative/NPCs.md)

## As-built — patrol enemy

The only AI today is the patrol Rat (`EnemyMovement.cs`): constant-speed move in the
facing direction, turns around when its trigger exits a `Tilemap` (platform edge). No
player detection, no attack, no death state. Full detail:
[baseline/GameplayMechanics § Enemy AI](baseline/GameplayMechanics.md),
[baseline/LegacyCharacters § Rat](baseline/LegacyCharacters.md).

Target improvements: raycast-based edge/wall detection, stomp-to-kill, an enemy base
class + `IDamageable`, chase behaviour.

## Target — partner-follow AI (the design pillar)

The persistent-partner model (see [../design/gameplay/Movement](../design/gameplay/Movement.md))
needs a swappable **`InactiveCatStrategy`**:
- **Follow:** the inactive cat follows the leader within its own ability limits.
- **Teleport-recover:** if it falls too far behind / offscreen, warp to the leader's last
  safe grounded spot. **Disabled** inside puzzle rooms and boss arenas.
- **Absent:** for solo-cat levels — the controller must handle a cat being *absent*, not
  just inactive (swap disabled, single HP, camera single-target). See
  [../design/levels/Tutorial](../design/levels/Tutorial.md).

The partner **never replicates an ability in real time** — abilities are leader-only and
instantaneous; the partner reconciles position afterward.

## Target — NPC "AI"

NPCs are data-driven (affinity + quest + reward) and query `ActiveCatManager` for the
current cat rather than running behaviour trees. See [../design/narrative/NPCs](../design/narrative/NPCs.md).

## TODO

- [ ] Follow-path algorithm (waypoint trail vs. simple seek).
- [ ] Teleport-recover "last safe grounded spot" bookkeeping.
- [ ] Boss AI phase/telegraph model.

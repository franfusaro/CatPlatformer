# NPCs — Personality-Reactive Characters

> **Purpose:** The personality-affinity NPC system that gates quests and drives ability progression — the swap verb's fourth expression.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [SideQuests](SideQuests.md), [../gameplay/Progression](../gameplay/Progression.md), [Characters](Characters.md), [../../technical/Architecture](../../technical/Architecture.md)

## NPCs & quests (a core progression system)

- NPCs have a **personality affinity** (prefers Orange, prefers Tuxedo, or neutral).
- Affinity **personality-gates the quest**: which cat can help → which cat learns the
  ability → and **the ability's flavor matches the deed + the cat.**
- This is the swap verb's **fourth expression** ("who can *relate* to this?") doing real
  progression work, not just flavor.

## Data-driven design

Data-driven NPC definitions (affinity + quest + reward); NPCs query the active-cat
manager for the current cat — the same "who is active" check the game already runs. See
[technical/Architecture](../../technical/Architecture.md).

## Future texture (ambient, non-gating)

Retained flavor on top of the core system: a hyper kitten that only follows Orange, a
grumpy shopkeeper who only bargains with Tuxedo, ambient reactions that don't gate
anything. (See [Ideas](../../Ideas.md).)

## Open fork — NPC centrality

Light-touch (default) vs. Hub-and-spoke. See [gameplay/Progression](../gameplay/Progression.md)
and [DECISION_LOG](../../decisions/DECISION_LOG.md).

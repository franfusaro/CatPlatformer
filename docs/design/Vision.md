# Vision — Two Cats (working title)

> **Purpose:** The one-line pitch and the core fantasy — the north star every feature, mechanic, level and asset is measured against.
> **Owner:** Franco Fusaro (creative director) · **Status:** Living · **Last Updated:** 2026-08-02
> **Related:** [CoreFantasy](CoreFantasy.md), [DesignPillars](DesignPillars.md), [CoreLoop](CoreLoop.md), [PlayerExperience](PlayerExperience.md), [narrative/Characters](narrative/Characters.md), [gameplay/Movement](gameplay/Movement.md)

This is the north star. It supersedes the single-cat assumptions in the
[as-built baseline docs](../technical/baseline/README.md) — those describe the
*inherited* project; this describes what we are *building*. See
[Playbook](../production/Playbook.md) for how we execute.

---

## One-line pitch

**A flowing action-platformer about the bond between two inseparable cats — swap
between a courageous, impulsive orange tabby and an observant, quietly protective
tuxedo to combine their opposite strengths across high-speed traversal, cooperative
puzzle rooms, and stun-and-DPS boss fights.**

## The fantasy

You aren't just controlling two cats — you're experiencing the relationship between
two inseparable ones. One is pure **zoomies** — courageous, impulsive, expressive,
always moving, always first through the gap. The other is a **loaf** — observant,
thoughtful, quietly protective, immovable, hits like a truck when the moment calls
for it. Alone, each is half a game, and half of who they are; their bond is what
makes them whole. Together — and only by **swapping between them** — they flow
through the world, solve rooms that need both, and take down bosses. The
relationship isn't a layer on top of the gameplay — it's the foundation the gameplay
is built on: the swap works because these two are complementary both in what they
can do and in who they are to each other. The game is dynamic and fast by default,
with puzzle and combat beats as punctuation.

The two aren't just mechanically opposite — they're bonded. What that means for who
they are and how they grow: [CoreFantasy](CoreFantasy.md).

## Emotional goals

What we want the player to *feel*, distinct from what the systems *do* (full feel
targets and metrics: [PlayerExperience](PlayerExperience.md)):

- Form a real emotional attachment to Orange and Tuxedo, not just competence with
  their kits.
- Discover each cat's personality through how they move, animate, and play — not
  through being told.
- Feel curiosity pulling them through the world.
- Feel concern when the cats are separated — the bond should register as a stake,
  not just a system state.
- Finish the game feeling like they shared meaningful memories with these two, not
  just that they cleared levels.

## Design philosophy

Principles every feature is measured against, alongside the mechanical
[DesignPillars](DesignPillars.md):

- **Gameplay before exposition.** The fantasy is discovered by playing, not by
  reading.
- **Emotion through interaction.** Feelings are earned by what the player *does*
  with the cats, not delivered by cutscenes.
- **Expressive movement.** How a cat moves is how a cat feels — animation and
  controls carry personality. Full principles: [gameplay/Movement — Movement
  Philosophy](gameplay/Movement.md#movement-philosophy).
- **Environmental storytelling.** The world communicates character and history
  without dialogue.
- **Strong character identity.** Orange and Tuxedo are always instantly
  recognizable — in silhouette, motion, and behavior.
- **Production realism.** Ambition is scoped to what this team can actually build
  and polish (see [Roadmap](../production/Roadmap.md)).
- **Mechanics serve the bond.** A new mechanic earns its place by reinforcing the
  emotional relationship between the two cats — novelty alone isn't a reason to
  add it. What this means for the swap verb specifically: [CoreLoop — Swap
  Philosophy](CoreLoop.md#swap-philosophy); for cooperative mechanics broadly:
  [CoreFantasy — Cooperation Philosophy](CoreFantasy.md#cooperation-philosophy).

## World direction

- A grounded, human world — ordinary rooms, streets, and buildings — experienced
  entirely from a cat's-eye scale and perspective.
- Everyday environments become adventurous *because* of that scale and
  perception, not because the world itself is fantastical.
- Hidden feline routes and territories (gaps, ledges, pipes, roofs) thread through
  the human world, supporting exploration and secrets while keeping the wider
  world mysterious and only partly mapped. Both still early — this direction
  should inform their first real content pass: [world/Regions](world/Regions.md),
  [world/Secrets](world/Secrets.md).

## Established

Living design bible · Established 2026-07-05 · Owner Franco (creative director).
Full decision history in [DECISION_LOG](../decisions/DECISION_LOG.md).

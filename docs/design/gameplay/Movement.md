# Movement — Traversal, Flow & the Persistent Partner

> **Purpose:** How the two cats move, lead and follow — the movement philosophy, the relationship model, teleport-recover, and the flow-driving traversal verbs.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-08-02
> **Related:** [Abilities](Abilities.md), [Combat](Combat.md), [../CoreLoop](../CoreLoop.md), [../CoreFantasy](../CoreFantasy.md), [../levels/LevelStructure](../levels/LevelStructure.md), [../../technical/AI](../../technical/AI.md), [../PlayerExperience](../PlayerExperience.md)

## Movement Philosophy

Movement is the default gameplay state, not a means to an end — see
[CoreLoop](../CoreLoop.md#movement-is-the-default-gameplay-state). These principles
guide how movement should *feel* and how future movement mechanics get evaluated
(adopted from DCR-001; see [DECISION_LOG](../../decisions/DECISION_LOG.md) DL-013):

- **Flow is the primary movement goal.** Movement encourages uninterrupted rhythm;
  players chain actions together; mastery feels fluid, not technical (Pillar 1,
  [DesignPillars](../DesignPillars.md)).
- **Shared foundation, character-driven asymmetry.** Both cats sit on the same
  underlying movement system — physics feel, input handling, control architecture.
  The already-locked starting kit is asymmetric *by design* and is **not** being
  reopened here: Orange's Zoomies/Wall-cling vs. Tuxedo's Glide/Loaf
  ([Abilities](Abilities.md)) exists to express who these two are (Pillar 2 & 4,
  [DesignPillars](../DesignPillars.md)). This section is a lens for evaluating
  **future** movement mechanics, not a redesign of the current kit.
- **Character is communicated through movement.** Personality reads through
  responsiveness, acceleration, and recovery before dialogue does (Pillar 4,
  [../narrative/Characters](../narrative/Characters.md)).
- **Cooperation emerges through movement.** The strongest movement moments combine
  both cats; swapping should continue flow, not interrupt it (see teleport-recover,
  below). The broader emotional standard for cooperation generally: [CoreFantasy —
  Cooperation Philosophy](../CoreFantasy.md#cooperation-philosophy).
- **Separation communicates completeness, not incompleteness.** If/when solo-cat
  sections happen (parked, [DL-006](../../decisions/DECISION_LOG.md)), each cat
  should read as fully capable alone — the loss is losing the pair's *combined*
  flow, not losing core ability. Reunion should feel like gaining more, not
  regaining function.

### Design rule for future movement mechanics
A new movement mechanic should satisfy at least two of: **improve flow, reinforce
character identity, strengthen cooperation.** A mechanic that exists only to make
the cats different — with no flow or cooperation payoff — gets challenged before it
ships. This extends the existing [ability design rule](Abilities.md#ability-design-rule)
specifically to movement.

### Mastery curve
Beginner → learns to control both cats. Intermediate → learns *when* to swap.
Advanced → swaps naturally as part of traversal. Expert → stops thinking in terms of
two separate characters and experiences Orange/Tuxedo as one continuous movement
system expressed through two complementary personalities. Feel targets:
[../PlayerExperience](../PlayerExperience.md).

### Shared Momentum (parked, experimental)
Preserving momentum/trajectory across a swap remains a **prototype-candidate idea,
not canon** — already parked in [../../Ideas](../../Ideas.md#needs-prototype) since
[DL-008](../../decisions/DECISION_LOG.md) rejected it as the *default* swap feel. Not
to be built until validated through prototyping and playtesting.

## Relationship model — persistent partner (DECISION)

- **Both cats always exist in the world** (persistent partner); you control one at a time.
- **Default (flow):** the inactive cat **follows** via simple AI within its own ability
  limits; if it falls too far behind or goes offscreen, it **teleport-recovers** to the
  leader's last safe grounded spot. You never babysit it during flow.
- **Puzzle rooms & boss arenas:** crossing the room's entrance **disables
  teleport-recover** — both cats must physically be present and solve/fight together.
- The partner **never replicates an ability in real time.** Abilities are leader-only
  and instantaneous; the partner *reconciles its position afterward*. This decouples
  ability execution from following and is what makes asymmetry tractable.

> **Rejected alternatives:** freeze/independent (too slow for our action feel);
> pure cooperative-only (too much per-ability design load as the default). We chose the
> **hybrid**: follow+teleport for flow, cooperative combos for punctuation. See
> [DECISION_LOG](../../decisions/DECISION_LOG.md) and [Ideas](../../Ideas.md).

## Traversal & flow

- Active cat leads; partner follows with teleport-recover.
- Orange is the natural flow driver (dash + wall-jump = fast, expressive). Tuxedo
  keeps up via double-jump/glide; when it can't, it warps.
- **Enemy gating in flow:** trash mobs die to orange's dash-pounce and stay in flow;
  occasional **armored/heavy enemies** force a swap to tuxedo — same design move as a
  ledge only one cat can reach. (Combat detail in [Combat](Combat.md).)

## Flow verbs (starting kit)

See [Abilities](Abilities.md) for the full kit. In brief:
- 🟠 **Orange:** Zoomies — dash / dash-cancel, wall-jump, pounce.
- ⬛⬜ **Tuxedo:** Double-jump / heavy glide (keeps up, lands hard).

> Legacy movement (single-cat walk/jump/climb, no dash/wall-jump) is documented as-built in
> [baseline/GameplayMechanics](../../technical/baseline/GameplayMechanics.md).

# Idea Parking Lot

> **Purpose:** Where every not-yet-approved idea lives so nothing is lost. Ideas move *out* of here into the Design Bible when approved, and *into* "Rejected" (never deleted) when dropped.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-26
> **Related:** [decisions/DECISION_LOG](decisions/DECISION_LOG.md), [design/Vision](design/Vision.md), [production/Backlog](production/Backlog.md)

> **Rule:** nothing here is deleted. Ideas are parked until approved (→ Design Bible) or
> explicitly rejected (→ "Rejected ideas", kept for the reasoning). Concrete scheduled
> work goes in [production/Backlog](production/Backlog.md); *decisions* go in the
> [DECISION_LOG](decisions/DECISION_LOG.md).

## Future mechanics
- **Solo-cat levels / sections** — you have only one cat (the pair got separated).
  Orange-only speed gauntlet; Tuxedo-only tank/puzzle level. Great onboarding + a
  dramatic separation/reunion beat. Approved direction, unscoped. Design seed in
  [design/levels/Tutorial](design/levels/Tutorial.md). Separation framing (each cat
  reads as complete alone; reunion is a gain, not a fix): [design/gameplay/Movement —
  Movement Philosophy](design/gameplay/Movement.md#movement-philosophy). *(Wave 3
  optional.)*
- **Playable memories** — short, optional interactive vignettes revealing how Orange
  and Tuxedo's bond formed (kitten Orange following Tuxedo, learning cat behaviors
  together, shared meals/naps, favorite places). Direction approved (positive/warm,
  not tragic; reinforces existing personalities, doesn't replace them) — see
  [design/narrative/Story](design/narrative/Story.md#delivery-playable-memories) and
  [DL-010](decisions/DECISION_LOG.md). Unscoped: discovery method, optionality vs.
  main-narrative support, reward type, and how the final memory ties to the ending
  are all still open. *(Wave 3+ content, needs dedicated environments/animation/
  scripting per memory — keep them concise to contain scope.)*
- **Unlockable third ability per cat**, late-game (after the starting four *sing*).
- **Ambient personality-reactive NPCs** that don't gate anything (hyper kitten only
  follows Orange; grumpy shopkeeper only bargains with Tuxedo).

## Needs prototype
- **Swap feel: carries-momentum vs. instant control-flip.** *Resolved to instant* (see
  DECISION_LOG), but momentum-carry survives here as a prototype-worthy variant —
  reaffirmed as "Shared Momentum" by DCR-001, see
  [design/gameplay/Movement](design/gameplay/Movement.md#shared-momentum-parked-experimental).
- **[RECALL/TOSS] verb** — summon partner / hurl Tuxedo as an attack; item hand-off.
  Approved as a separate verb; exact feel needs prototyping.
- **NPC/quest centrality: Light-touch vs. Hub-and-spoke.** Defaulted Light-touch; a
  Hub-and-spoke town is the bigger, RPG-flavored alternative — prototype before committing.
- **Boss phase count & stun/DPS timing windows** — decide while playtesting.
- **Revive cost/time; downed cats auto-recover over time?** — playtest.

## Experiments
- Catnip = temporary zoomies frenzy; fish heals / aids revive (consumable feel tests).
- Orange bats projectiles (yarn ball, bottlecap) vs. Tuxedo shoves heavy objects —
  "weapons as object-play" experiments (must stay cat-specific, never universal gear).

## DLC / post-launch ideas
- *(none captured yet — park them here.)*

## Rejected ideas (kept for the reasoning — never delete)
- **Freeze / independent partner** — rejected: too slow for the action feel. (→ chose
  hybrid follow+teleport.)
- **Pure cooperative-only partner** — rejected: too much per-ability design load as the default.
- **Abilities bought with currency** — rejected: makes you feel like a gear-hero, not
  *more cat*. Abilities are found/learnt; only cosmetics are bought.
- **Weapon / gear inventory** — rejected: a parallel system that collapses asymmetry.
- **Grey cat** — rejected in favor of Orange + Tuxedo for maximum colour/silhouette
  contrast (readability under speed).
- **Overloading [SWAP] with throw/summon** — rejected: split into a separate [RECALL/TOSS]
  verb so the swap stays a clean instant control-flip.

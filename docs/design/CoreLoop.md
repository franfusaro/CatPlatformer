# Core Loop — the SWAP verb

> **Purpose:** The single core verb (the swap) and the macro rhythm of modes it drives. This is what makes the game cohere.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-08-02
> **Related:** [DesignPillars](DesignPillars.md), [CoreFantasy](CoreFantasy.md), [gameplay/Movement](gameplay/Movement.md), [gameplay/Abilities](gameplay/Abilities.md), [gameplay/Combat](gameplay/Combat.md), [levels/LevelStructure](levels/LevelStructure.md)

## Movement is the default gameplay state

The player spends most of the game simply moving — traversal is intrinsically
enjoyable on its own, not just a means to reach the next puzzle or fight. Puzzle
rooms and combat **punctuate** movement (Pillar 5); they don't replace it as the
baseline activity. Full principles, the movement design rule for future mechanics,
and the intended mastery curve: [gameplay/Movement — Movement
Philosophy](gameplay/Movement.md#movement-philosophy). (Adopted from DCR-001, see
[DECISION_LOG](../decisions/DECISION_LOG.md).)

## The core verb — SWAP (everything is one mechanic)

The swap is the single core verb. It expresses itself four ways:

| Pillar | The question asymmetry asks | Swap gives you | Status |
|--------|----------------------------|----------------|:------:|
| **Traversal (flow)** | *Who can **reach** this?* | speed & reach | Core |
| **Puzzle rooms** | *Who can **key** this?* | co-location combos | Core |
| **Combat / bosses** | *Who can **hurt** this?* | stun → DPS role-switch | Core |
| **NPCs / world** | *Who can **relate** to this?* | quest/mentor ability-unlocks + social gating | Core |

Keeping all four under one verb is what makes the game cohere — the player never
learns a separate sub-game; they learn *when to be which cat*.

- **[SWAP] is an instant control-flip** (flow-first). A separate, lower-frequency
  **[RECALL/TOSS]** verb (summon the partner / hurl it) handles repositioning, item
  hand-off, and launching Tuxedo as an attack. Two buttons, two jobs; the swap is
  never overloaded. See [DECISION_LOG](../decisions/DECISION_LOG.md).

## Swap Philosophy

Swapping is **shared leadership**, not character selection — the emotional and
mechanical expression of the bond between Orange and Tuxedo, across all four
expressions of the verb above (adopted from DCR-002; see
[DECISION_LOG](../decisions/DECISION_LOG.md) DL-014). This is the swap-specific
lens; the broader emotional standard for cooperation generally (not tied to the
swap verb) is [CoreFantasy — Cooperation Philosophy](CoreFantasy.md#cooperation-philosophy)
(DL-015):

- **Shared leadership, not character selection.** The player is never asking "which
  character do I want?" — they're asking "who should lead this moment?" Leadership
  shifts situationally; both cats stay emotionally present throughout.
- **The partnership never stops existing.** Neither cat reads as emotionally "gone"
  while the other leads — reinforced mechanically by the persistent-partner model
  (follow + teleport-recover, idle-partner invulnerability in flow — see
  [gameplay/Movement](gameplay/Movement.md), [GameRules](GameRules.md)).
- **Swapping is intrinsically enjoyable**, the same way movement is — players should
  eventually swap because it maintains flow, expresses mastery, and feels natural, not
  solely because a gate requires it.
- **Swapping preserves flow.** The intended emotional sequence is *confidence →
  continuity → reunion*: the player understands why leadership is changing, momentum
  continues, and taking over the other cat feels satisfying rather than corrective —
  never a stop-and-select interruption.
- **The non-leading cat still matters.** Whichever cat isn't leading should still read
  as present and relevant: "we're doing this together," never "the other cat is
  temporarily gone."
- **Swapping should not be artificially discouraged.** No cooldowns, resource costs,
  stamina costs, or arbitrary usage limits on [SWAP] unless future prototyping
  demonstrates a clear improvement — reinforces the instant-control-flip decision
  ([DL-008](../decisions/DECISION_LOG.md)).
- **Mastery is conducting two personalities, not switching between them.** Same
  beginner→expert arc already defined in [gameplay/Movement — Mastery
  curve](gameplay/Movement.md#mastery-curve): the player gradually stops thinking "now
  I'm Orange" / "now I'm Tuxedo" and starts thinking "Orange should lead here," then
  "now Tuxedo takes over."

### Evaluation lens for future swap-driven mechanics

This is a **forward-looking lens for new mechanics only** — it does not reopen what's
already locked. Pillar 2's "every challenge asks which cat," and the existing
asymmetric ability-lock design (wall-cling gaps, armored-enemy-needs-Tuxedo, puzzle
co-location — see [gameplay/Combat](gameplay/Combat.md),
[levels/LevelStructure](levels/LevelStructure.md)), remain exactly as designed:

- **Future asymmetry should be character-driven, not arbitrary.** A new ability-lock
  should exist because it expresses who a cat *is*, not merely to force a swap for its
  own sake.
- **Don't stack additional mandatory gates without payoff.** Puzzle rooms, armored
  enemies, and bosses remain the defining, locked mandatory-cooperation moments
  (Pillar 2, Pillar 5) — going forward, avoid piling *more* mandatory gates on top of
  these without the same cooperative payoff. New gates should read as meaningful
  cooperation, not repetitive gating, and opportunity-driven swapping should still
  outnumber obligation-driven swapping in ordinary flow.

Before shipping a new swap-driven mechanic, check it against: does it encourage
leadership transitions rather than character replacement; does it strengthen the bond;
does it preserve flow; would players still enjoy swapping here even if it weren't
required; does it reinforce complementary personalities over isolated tool identities;
does the player feel they're travelling with two companions rather than operating
whichever one is currently useful?

## Level rhythm (the macro loop)

The macro structure is a **rhythm of modes**, layout-signaled so the player knows
which gear they're in:

```
flow corridor → puzzle room → flow corridor → mini-combat → puzzle room → BOSS arena
   (open)         (enclosed)      (open)        (open)        (enclosed)     (enclosed)
```

Flow is the release valve; enclosed rooms are the tension. Never two enclosed rooms
back-to-back without flow between them. Full construction detail in
[levels/LevelStructure](levels/LevelStructure.md).

# Characters — Orange & Tuxedo

> **Purpose:** The two playable cats as characters — personality, art direction and the readability rule. (Design intent; the legacy Cat/Rat as coded are in the baseline.)
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-08-02
> **Related:** [../gameplay/Abilities](../gameplay/Abilities.md), [../art/StyleGuide](../art/StyleGuide.md), [NPCs](NPCs.md), [../CoreFantasy](../CoreFantasy.md), [Story](Story.md), [Timeline](Timeline.md), [../../technical/baseline/LegacyCharacters](../../technical/baseline/LegacyCharacters.md), [../../decisions/DECISION_LOG](../../decisions/DECISION_LOG.md) (DL-009, DL-010, DL-015)

> **Note:** This describes the *design* of the two cats we're building. The single
> legacy Cat (player) and Rat (enemy) that exist in code today are documented as-built
> in [baseline/LegacyCharacters](../../technical/baseline/LegacyCharacters.md).

## The two cats (personality)

| | 🟠 **Orange cat** — *chaos / speed* | ⬛⬜ **Tuxedo cat** — *weight / control* |
|---|---|---|
| **Personality** | Hyperactive, zoomies, fearless, fragile | Observant, protective, immovable, tanky, high-impact |
| **Survivability** | Low HP, high mobility | High HP, low mobility |

Mechanics kits live in [gameplay/Abilities](../gameplay/Abilities.md).

## Character philosophy

The mechanics table above (chaos/speed vs. weight/control) is the gameplay-facing
summary. Underneath it, each cat has a consistent internal logic that should drive
writing, animation, and gameplay tuning alike — see [CoreFantasy](../CoreFantasy.md)
for the full treatment. Summary:

| | 🟠 **Orange** | ⬛⬜ **Tuxedo** |
|---|---|---|
| **Core instinct** | Act first | Observe first |
| **Strength** | Courageous, curious | Perceptive, patient, thoughtful |
| **Flaw** | Impulsive — rarely weighs long-term consequences | Overthinks, out of a sense of responsibility |
| **Hidden fear** | Prolonged separation from Tuxedo | Failing to protect Orange |
| **Expresses affection through** | Physical closeness, play, shared experience | Quiet acts of care and protection |

Neither cat is "the flawed one" — the flaw is the shadow side of the same trait that
makes them useful. This is what Pillar 4 ([DesignPillars](../DesignPillars.md),
*Personality = mechanics*) means concretely for these two.

## Shared history

*(Direction accepted; delivery mechanics still open — see [Story](Story.md) and
[DL-010](../../decisions/DECISION_LOG.md).)*

Tuxedo lived independently before meeting Orange, who entered his life as a very
young kitten. What began as Tuxedo protecting Orange out of a sense of
responsibility grew into genuine companionship — Orange gave Tuxedo's life new
meaning and spontaneity in return. Orange grew up always knowing Tuxedo as his
constant companion, which is the root of his deep trust in Tuxedo and his hidden
fear of prolonged separation (see the Character philosophy table above).

This origin is *why* the two fears in that table exist, not a replacement for
them: Tuxedo's protective instinct predates Orange, but the relationship gives it
purpose; Orange's adventurous spirit is innate, but growing up alongside Tuxedo is
what shapes his confidence and attachment. See [Story](Story.md#shared-history) for
how this is planned to be delivered in-game, and [Timeline](Timeline.md) for where
it sits chronologically (before the game begins).

## Shared relationship & rituals

Orange and Tuxedo are bonded, not just paired. Recurring rituals carry the
relationship through animation and gameplay without dialogue:

- Grooming each other after stressful moments.
- Sleeping together in warm spots — Orange invades personal space, Tuxedo tolerates it.
- Sharing food; mealtimes are a beat, not just a resource pickup.
- Solving problems together (mechanically: this is the swap — see
  [../CoreLoop](../CoreLoop.md)).
- Turning play into friendly competition over toys/boxes.
- Mutual dislike of water.
- Visible anxiety when separated too long — ties into
  [DL-002](../../decisions/DECISION_LOG.md)'s partner-follow model and the
  [DL-006](../../decisions/DECISION_LOG.md) solo-cat-level direction (separation is a
  planned narrative beat, not just a system state).

Not every ritual needs to become an interactive mechanic — some are ambient
(idle/environment), some are set-piece moments. Which is which is an open design
question; don't force all of them into systems.

## Character growth

- **Orange** learns that real courage includes patience and considering how his
  actions land on others — without losing the adventurous spirit.
- **Tuxedo** learns that not everything can be planned for; trust and spontaneity
  matter as much as preparation.

Each cat grows by absorbing a *small* piece of the other's perspective. Neither
abandons their core instinct — growth is additive, not a personality swap.

## Behavioral storytelling

Personality and relationship should read through **behavior before dialogue**:
idle animations, environmental interactions, gameplay behaviors, non-verbal
reactions, and the shared rituals above. The opening minutes of the game should
establish who these two are with minimal or no dialogue.

- **Orange idle:** investigates the environment, gets distracted, initiates play,
  bursts of energy, seeks physical closeness with Tuxedo.
- **Tuxedo idle:** calmly watches surroundings, grooms, keeps an eye on Orange,
  subtly keeps the pair together without drawing attention to the care.

Risks to watch for (don't over-index on any one):
- Leaning on cat stereotypes instead of letting these become specific individuals.
- Overusing idle/ritual beats until they read as repetitive filler.
- Mechanics that reward one cat's approach so much they undercut the asymmetry
  (Pillar 2, [DesignPillars](../DesignPillars.md)).
- Reaching for dialogue to explain what animation/interaction could show instead.

## Art direction

- **Orange cat:** warm orange tabby, lean, animated with twitchy energy. Low, fast poses.
- **Tuxedo cat:** black-and-white, chunky, heavy poses, half-lidded grumpy expression.
- **Readability rule (gameplay-critical):** in a fast action game the player must know
  *at a glance* which cat they control. **Orange + tuxedo** gives maximum color **and**
  silhouette contrast — chosen over grey for exactly this reason.

Art specs, animation lists, and PixelLab prompts: [art/StyleGuide](../art/StyleGuide.md),
[art/Animations](../art/Animations.md). The existing cat sprite kit (jump/fall/land,
climb-idle, hurt) is now the *Orange* base; a parallel Tuxedo set + new ability
animations get generated the same way.

## Character Invariants

Everything above — personality tables, mechanics, art direction, shared history — is
*implementation*: it can and will evolve as mechanics, progression, and narrative
structure are built out. This section is different: it names the small set of core
identity truths for each cat that are expected to hold regardless of how the
implementation changes. Treat these as a summary derived from the sections above, not
a new source of characterization.

**Orange:**
- Acts before thinking.
- Finds joy in the present.
- Expresses affection physically.
- Naturally trusts others.
- Cannot stay still for long.
- Faces problems directly rather than avoiding them.
- Learns through experience more than planning.

**Tuxedo:**
- Thinks before acting.
- Finds comfort in preparation.
- Protects quietly.
- Observes before intervening.
- Carries responsibility without seeking recognition.
- Notices details others overlook.
- Prefers solving problems carefully rather than quickly.

**Shared relationship:**
- They naturally seek each other's company.
- Their personalities complement rather than oppose one another.
- Neither character is "the main protagonist."
- Their bond is communicated primarily through behavior rather than dialogue.
- They make each other better without becoming the same.
- Their relationship should always feel genuine rather than idealized.

**Why this section exists:** future mechanics, story beats, animations, and
progression systems should reinforce these truths unless an intentional redesign is
approved. Use this list as a quick validation check in future design discussions —
if a proposed change would violate one of these, that's a signal to flag it
explicitly rather than let it drift in silently.

The **Shared relationship** invariants above are formalized into a forward-looking
evaluation lens for future cooperative mechanics specifically: [CoreFantasy —
Cooperation Philosophy](../CoreFantasy.md#cooperation-philosophy) (DL-015).

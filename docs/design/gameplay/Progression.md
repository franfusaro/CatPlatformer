# Progression — Items, Abilities & Unlocks

> **Purpose:** How the player grows: abilities are found or learnt (never bought), the three unlock sources, and the guardrail that keeps items from becoming a parallel system.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [Abilities](Abilities.md), [Collectibles](Collectibles.md), [../narrative/NPCs](../narrative/NPCs.md), [../narrative/SideQuests](../narrative/SideQuests.md), [../../technical/Architecture](../../technical/Architecture.md)

## Progression model (DECISION)

Abilities are **found or learnt from others — never bought.** Cosmetics are the only
thing you *buy*. There is **no weapon/gear inventory.** (See [DECISION_LOG](../../decisions/DECISION_LOG.md).)

**The guardrail (the filter test for any item idea):**
> *Items amplify the cats; they never become a parallel system.* If an item makes you
> feel like a generic gear-holding hero instead of *more* like the chaos-cat or the
> lazy-cat, cut it or reskin it.

## How you gain an ability — three sources, one system

| Source | How | Feel |
|--------|-----|------|
| **Found** | Reach a hidden/gated place → get the ability (Metroidvania) | Exploration reward |
| **Taught** | A **mentor** NPC teaches after a trial / challenge room | Mastery milestone |
| **Earned** | A **quest-giver** NPC rewards it for a task | Relationship payoff |

- **Starting kit is given, not gated:** you begin with Zoomies + Wall-cling (Orange) and
  Glide + Loaf (Tuxedo). Finds/quests grant **additional** abilities and upgrades only.
- All three sources feed the **same ability-grant API** (see
  [technical/Architecture](../../technical/Architecture.md)) — an ability can be granted
  by a pickup trigger, a mentor, or a quest reward interchangeably.

## NPCs & quests as progression

The personality-affinity NPC/quest system is the backbone of "learnt from others."
Full design in [narrative/NPCs](../narrative/NPCs.md) and
[narrative/SideQuests](../narrative/SideQuests.md). In short: an NPC's affinity
(prefers Orange / Tuxedo / neutral) gates which cat can help → which cat learns the
ability → and the ability's flavor matches the deed + the cat. This is the swap verb's
**fourth expression** ("who can *relate* to this?") doing real progression work.

## Open fork — how central are NPCs/quests?

**Defaulted to Light-touch** (adjustable): a handful of optional quest-givers/mentors,
most abilities world-found, **flow stays dominant**. Alternative: *Hub-and-spoke* (a
central NPC town where quests are a main pillar — more content, more RPG-flavored, more
flow risk). Tracked in [Ideas](../../Ideas.md) / [DECISION_LOG](../../decisions/DECISION_LOG.md).

# Milestones — Wave Gates

> **Purpose:** The 🚦 review gates and exit criteria for each wave, extracted from the Roadmap so "are we done with this wave?" is a one-screen check.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [Roadmap](Roadmap.md), [Backlog](Backlog.md), [Playbook](Playbook.md)

Full deliverables per wave are in [Roadmap](Roadmap.md). This is just the gates.

| Wave | 🚦 Gate (what you approve) | Exit criteria | Effort |
|------|---------------------------|---------------|--------|
| **0 — Foundation** | You play the *existing* game on the new engine at the Pages URL; confirm all 4 levels still work. | Green CI, playable build, no regressions. | ~4–6 days |
| **1 — Swap core + HP** | You play the greybox level and judge whether **swap + flow + one puzzle** *feels* right. **The make-or-break feel check.** | Two-cat swap loop playable in one test level. | ~1–1.5 weeks |
| **2 — Combat & depth** | You clear the first boss and confirm combat feels like an extension of the swap, not a bolted-on mini-game. | Enemies + stun→DPS loop + first boss playable. | ~1.5–2 weeks |
| **3 — Content & world** | You play a themed level start-to-finish with real art and one quest-unlock. | Vertical slice → real content. | ongoing |
| **4 — Polish & release** | Public playtest build. | Ship-quality feel, public WebGL build. | ongoing |

## Where approval gates sit (per feature, from the Playbook)

1. After **Design** (before any code).
2. After **Playtest** (before merge/deploy).

Everything between those two is Claude's to run autonomously. See [Playbook](Playbook.md).

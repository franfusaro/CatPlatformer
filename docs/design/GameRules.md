# Game Rules — Health, Down & Revive

> **Purpose:** The health model that replaces the legacy one-hit lives system: per-cat HP, downed-not-dead, and revive.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [gameplay/Combat](gameplay/Combat.md), [gameplay/Movement](gameplay/Movement.md), [gameplay/Collectibles](gameplay/Collectibles.md), [../technical/baseline/GameplayMechanics](../technical/baseline/GameplayMechanics.md)

## Health — HP + down / revive (replaces one-hit lives)

- **Per-cat HP** (Orange low, Tuxedo high). Finally uses the existing `heart.png`.
- A cat at 0 HP is **downed, not dead** — you keep playing as the other cat and can
  **revive your buddy** by reaching it (risk/tension beat; a natural mini-puzzle in a
  boss fight).
- **Both cats down = checkpoint / game over.**
- **Idle-partner safety:** in *flow*, the inactive cat is invulnerable. In **boss
  arenas**, the boss can target *either* cat — that vulnerability is what makes
  swapping meaningful under pressure.

## Open tuning (decide while playtesting)

- Revive cost/time and whether downed cats auto-recover over time.

> This replaces the legacy lives model (default 3 lives, one-hit kill) documented in
> [baseline/GameplayMechanics §4](../technical/baseline/GameplayMechanics.md). New to
> Wave 1's scope.

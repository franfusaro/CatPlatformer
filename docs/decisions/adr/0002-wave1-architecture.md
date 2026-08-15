# ADR-0002: Wave 1 software architecture (swap core, abilities, HP)

- **Status:** Accepted
- **Date:** 2026-07-26
- **Deciders:** Franco Fusaro
- **Wave/Item:** Wave 1 · 1.1–1.7

## Context
`docs/technical/Architecture.md` sketched a target (SO state + events + `Core`/`Gameplay`/`UI`
assemblies) but stopped short of the system-by-system detail Wave 1 needs to actually build
the swap core, ability system, partner AI, and HP/down-revive loop without rework. Design
scope was frozen (Phase 1 continues in a separate design track); this decision covers only
the software architecture built on top of that frozen design.

## Decision
Adopt `docs/technical/Wave1Architecture.md` as the accepted architecture for Wave 1. Notably:
- `CatController` is a single, data-driven component for both cats; all asymmetric behavior
  lives in `CatAbility` components or `CharacterData`, never in the controller itself.
- `PartnerAI` implements the same `IInputReader` interface as human input, so the controller
  never distinguishes leader from follower — this is the mechanism behind Movement.md's
  "abilities are leader-only, partner reconciles after" rule, not a separate enforcement.
- Mutable runtime/session state (`RuntimeGameState`, HP) is **plain C#**, not ScriptableObjects
  — refining `Architecture.md`'s original "GameState ScriptableObject" sketch to avoid the
  Play-Mode-edits-leak-onto-the-asset footgun. SO usage is scoped to immutable designer data
  (`CharacterData`, `AbilityDefinition`) and event channels.
- Three assemblies only (`Core`/`Gameplay`/`UI`, per Roadmap 0.7) — no additional splitting.
- "Swap System" and checkpoint-as-interaction are explicitly *not* separate classes.

## Consequences
- **Good:** every Wave 1 deliverable (1.1–1.7) has a concrete, cross-referenced design before
  code starts; the legacy migration table gives a keep/refactor/replace/delete verdict for all
  16 existing scripts, so Wave 1 work won't rediscover CodeReview's defects mid-build.
- **Bad / cost:** none beyond the doc-review time already spent; no code has been written yet.
- **Follow-ups:** implementation follows the 11-step build order in `Wave1Architecture.md`
  §Wave 1 implementation roadmap, starting with Core infra. Editor-heavy steps (prefab/scene
  wiring, greybox level assembly) are flagged there for the user, per `CLAUDE.md`'s guardrail
  that the assistant cannot drive the Unity Editor.

## Alternatives considered
- **Keep HP/session state on a ScriptableObject** (as `Architecture.md`'s original sketch
  implied) — rejected: known Unity footgun (Play-Mode edits persist to the asset) for no
  benefit over a plain C# class owned by `GameManager`.
- **Per-feature assemblies** (e.g. split `Gameplay` into `Gameplay.Abilities`, `Gameplay.AI`,
  etc.) — rejected as premature for a solo-dev, ~2–3k-LOC-post-Wave-1 codebase; the one
  dependency rule that matters (`UI` and `Gameplay` never reference each other directly) is
  already enforced by the three-assembly split.
- **Dedicated `SwapSystem` class** — rejected: it would own no state of its own and only
  forward a call `ActiveCatManager` already owns; adding a type here is indirection without
  payoff.

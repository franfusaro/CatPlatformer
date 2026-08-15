# Baseline — As-Built Reference

> **Purpose:** Index to the reverse-engineering docs that describe the **inherited legacy game as it exists in code today**. Forensic history, not the target design.
> **Owner:** Franco Fusaro · **Status:** Baseline (as-built) · **Last Updated:** 2026-07-10
> **Related:** [../Architecture](../Architecture.md), [../UnityStructure](../UnityStructure.md), [../../design/Vision](../../design/Vision.md)

> **Read this first:** everything in this folder documents the **current shipping code**
> (Unity 2018.3, single-cat platformer, Wave 0). It is accurate and heavily cross-
> referenced — it is *not* outdated. The forward-looking game we are building lives in
> [`docs/design/`](../../design/Vision.md); the target code architecture is in
> [`../Architecture.md`](../Architecture.md).

| Doc | Contents |
|-----|----------|
| [GameplayMechanics](GameplayMechanics.md) | Every legacy mechanic reverse-engineered from source + coverage matrix + improvement list |
| [LevelArchitecture](LevelArchitecture.md) | Scene build-index flow, persistence singletons, per-scene manager placement |
| [LegacyCharacters](LegacyCharacters.md) | The player Cat and enemy Rat as coded today |
| [AssetInventory](AssetInventory.md) | Sprites, tiles, audio, prefabs, fonts, verified import specs |
| [CodeReview](CodeReview.md) | Architecture-at-a-glance, strengths/weaknesses, technical-debt defects, SOLID scorecard |
| [DependencyAudit](DependencyAudit.md) | Packages, deprecated APIs, upgrade sequence |
| [PerformanceReview](PerformanceReview.md) | WebGL perf findings (FindObjectOfType, allocations, download size) |
| [LegacyAssessment](LegacyAssessment.md) | Health scorecard (6/10) and modernization effort estimate |

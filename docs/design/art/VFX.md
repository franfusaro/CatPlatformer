# VFX

> **Purpose:** Visual-effects direction — dust, impacts, swap/ability feedback. Placeholder; the project has no VFX yet.
> **Owner:** Franco Fusaro · **Status:** Planned · **Last Updated:** 2026-07-10
> **Related:** [StyleGuide](StyleGuide.md), [../gameplay/Abilities](../gameplay/Abilities.md), [../gameplay/Combat](../gameplay/Combat.md), [../../technical/baseline/AssetInventory](../../technical/baseline/AssetInventory.md)

## Current state (as-built)

**No particle systems, shaders, or VFX prefabs exist** in the project's own assets. The
only "effect" today is slow-motion on level exit (`Time.timeScale`). The `particlesystem`
module is available but unused. (See [baseline/AssetInventory § Visual Effects](../../technical/baseline/AssetInventory.md).)

## TODO (to design)

- [ ] Swap feedback (the single most important juice — the swap must *pop*).
- [ ] Movement dust (dash, wall-cling, landing squash), Loaf/Flop impact.
- [ ] Combat hit-sparks, stun stars, DPS-window telegraph.
- [ ] Hurt flash / revive glow tied to the [HP system](../GameRules.md).
- [ ] Keep VFX WebGL-safe (no heavy overdraw); consistent with the [style guide](StyleGuide.md).

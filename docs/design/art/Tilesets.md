# Tilesets & Environment Art

> **Purpose:** Per-asset PixelLab prompts for missing terrain tiles and seamless day/night parallax layers; environment art conventions.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [StyleGuide](StyleGuide.md), [Animations](Animations.md), [../world/Biomes](../world/Biomes.md), [../../technical/baseline/AssetInventory](../../technical/baseline/AssetInventory.md)

Prepend the **STYLE ANCHOR** from [StyleGuide](StyleGuide.md). Tile sizes/PPU are in
[StyleGuide § Verified specs](StyleGuide.md). The existing tileset inventory (BFT,
starynight, SPA, goodly, RuleTiles) is in
[baseline/AssetInventory § Environment](../../technical/baseline/AssetInventory.md).

## 🧱 Terrain — inner corners + spike hazard (MISSING tiles)
```
STYLE ANCHOR + a 16x16 px platformer terrain tile set extension for a mossy-green
grass-over-brown-dirt theme. Generate: inner corner tiles (top-left, top-right,
bottom-left, bottom-right), single-tile-wide pillar caps, and a clearly dangerous
spike/thorn hazard tile that reads as "do not touch". Seamless with a 16px grid,
match the existing BFT Metroidvania palette.
```

## 🌲 Parallax layer (seamless day + night pair)
```
STYLE ANCHOR + a horizontally TILEABLE/SEAMLESS background forest layer,
32 PPU. Produce a matched pair: a DAY version (soft greens, hazy light) and a
NIGHT version (deep blues, silhouetted black trees, subtle stars). Edges must wrap
seamlessly for infinite parallax scrolling. Match existing tree1..tree5 style.
```

## Conventions

- Keep a **day** and **night** variant for every environment asset.
- Bottom-left pivots for tiles; seamless 16px (terrain) / 32px (background) grids.
- Fix `generic_platformer_tiles` PPU 31→32 during any pass.

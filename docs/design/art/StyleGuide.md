# Art Style Guide

> **Purpose:** The canonical visual identity, verified sprite/tile specs, the PixelLab style anchor + prompt factory, and consistency rules for all new art.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [Animations](Animations.md), [Tilesets](Tilesets.md), [VFX](VFX.md), [AudioDirection](AudioDirection.md), [../narrative/Characters](../narrative/Characters.md), [../../technical/baseline/AssetInventory](../../technical/baseline/AssetInventory.md)

Purpose: regenerate/expand the game's pixel art with PixelLab (or similar) **while
preserving the existing visual identity** — a cozy 2D cat platformer with day/night
forest parallax and a Metroidvania-style tileset.

## Verified specs (match these when generating)

All dimensions are **VERIFIED** from the Unity sprite import settings (`.png.meta`), so
new art can drop in without re-slicing. (Canonical copy; the
[AssetInventory](../../technical/baseline/AssetInventory.md) links here.)

| Asset | Frame / tile size | PPU | Source file |
|-------|------------------|:---:|-------------|
| **Cat (player)** | ~**18×29 px** per frame (variable 18×28–30) | 32 | `Sprites/Player/sprite_base_addon_2012_12_14.png` (+ `cat_climb2.png`) |
| **Rat (enemy)** | ~**25×14 px** per frame | 32 | `Sprites/Enemies/mouse.png` |
| **BFT terrain tiles** | **16×16 px** | 16 | `TileSets/BFT - Mega Metroidvania Tileset.png` |
| **starynight bg tiles** | **32×32 px** | 32 | `TileSets/starynight.png` |
| **SPA / goodly bg tiles** | **32×32 px** | 32 | respective sheets |
| **generic_platformer_tiles** | 32×32 px | **31** ⚠ | fix PPU to 32 |
| **Coin** | (verify) | 32 | `Sprites/Pickups/SPA_Coins.png` |

> ⚠️ The cat frames are only ~18–30 px tall — **very small**. For crisper modern art
> you may want to author at **2× (36×58)** and set PPU to 64, keeping the same
> world-space size. Decide this before generating; it affects every character asset.

## Visual identity to preserve

- **Palette:** earthy greens/browns (grass/dirt/rock RuleTiles + BFT), plus a distinct
  **night** palette (deep blues/blacks, `starynight`, `*_night` trees). Keep a **day**
  and **night** variant for every environment asset.
- **Scale/readability:** small, chunky, high-contrast, single clean outline.
- **Mood:** friendly, storybook (reinforced by the "Caramel Candy" font).
- **Character readability rule:** Orange + tuxedo colours chosen for maximum
  colour/silhouette contrast (see [narrative/Characters](../narrative/Characters.md)).

## Style anchor (prepend to every direct prompt)

> **STYLE ANCHOR** — `pixel art, side-view 2D platformer, single dark outline,
> limited earthy palette (mossy greens, warm browns) with a matching cool night
> palette, cozy storybook mood, clean readable silhouette, transparent background,
> no dithering noise, consistent top-left light source`

## Meta-prompt: "PixelLab Prompt Factory" (paste into ChatGPT)

```
You are a pixel-art art director for a 2D Unity cat platformer. Your job is to
produce a single, tightly-scoped image-generation prompt for the tool "PixelLab".

CONSTRAINTS you must always honor and bake into the prompt:
- Style: pixel art, side-view 2D platformer, single dark outline, limited earthy
  palette (mossy greens, warm browns) plus a matching cool NIGHT palette, cozy
  storybook mood, clean readable silhouette, transparent background, consistent
  top-left light source, no dithering noise.
- Technical: state exact pixel dimensions and pivot. Character frames are 32 PPU;
  the cat is ~18x29 px, the rat ~25x14 px; BFT terrain tiles are 16x16 px; most
  background tiles are 32x32 px. If I ask for 2x art, double the pixels and note
  the new PPU.
- Consistency: new art must visually match the existing cat/rat/tileset proportions
  and palette; mention this explicitly.

PROCESS:
1. Ask me at most 3 questions: (a) which asset/animation, (b) day or night variant,
   (c) any mood/pose notes.
2. Then output ONLY the final PixelLab prompt in a code block, plus a one-line
   "why this works" note.
3. If I say "give me variations", output 3 alternative prompts.

Wait for my first answer before generating.
```

## Sprite consistency recommendations

1. **Decide 1× vs 2×** authoring resolution before generating anything (see warning above).
2. **One cell size per role**; **consistent pivots** (feet-centered chars, bottom-left tiles).
3. **Shared master palette** (day + night sub-palettes) constraining all new art.
4. **Uniform outline policy** across characters/props.
5. Fix `generic_platformer_tiles` PPU 31→32; retire `test.png` and dated/duplicate sheets after migration.

## Suggested generation order (highest value first)

1. Cat: Jump/Fall/Land + Climb-idle → unlocks jump-feel & ladder polish. ([Animations](Animations.md))
2. Hearts HUD (full/half/empty) → pairs with the HP mechanic. ([Animations](Animations.md))
3. Rat: Turn + Squashed → unlocks stomp-to-kill. ([Animations](Animations.md))
4. Terrain inner-corners + spike hazard tiles. ([Tilesets](Tilesets.md))
5. Seamless day/night parallax pairs. ([Tilesets](Tilesets.md))

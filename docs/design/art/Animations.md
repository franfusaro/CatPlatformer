# Animations — Character & Sprite Prompts

> **Purpose:** Per-asset PixelLab prompts for the missing/expanded character animations (cat, rat) and the hearts HUD.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-07-10
> **Related:** [StyleGuide](StyleGuide.md), [Tilesets](Tilesets.md), [../narrative/Characters](../narrative/Characters.md), [../../technical/baseline/LegacyCharacters](../../technical/baseline/LegacyCharacters.md)

Prepend the **STYLE ANCHOR** from [StyleGuide](StyleGuide.md) to every direct prompt.
Verified frame sizes are in [StyleGuide § Verified specs](StyleGuide.md).

## 🐱 Cat — Jump / Fall / Land (MISSING, highest value)
```
STYLE ANCHOR + a small orange-and-white house cat, side view, mid-jump:
body stretched upward, front paws forward, tail streaming behind, ears back.
18x29 px sprite, 32 PPU, feet-centered pivot, transparent background.
Generate a 3-frame set: (1) crouch/anticipation, (2) apex stretch, (3) landing squash.
Match the existing walk/idle cat proportions and palette.
```

## 🐱 Cat — Climb-idle (hold pose on ladder)
```
STYLE ANCHOR + the same house cat clinging to a ladder, side/front three-quarter,
front and back paws gripping rungs, tail down, calm expression. Single static frame,
18x29 px, 32 PPU. Must visually match the existing CatClimb animation frames.
```

## 🐱 Cat — Hurt / knockback (for the future HP system)
```
STYLE ANCHOR + the same house cat recoiling from a hit, ears flat, eyes squeezed,
body arched backward, small motion. 2-frame hurt flash, 18x29 px, 32 PPU.
```

## 🐀 Rat — Turn + Squashed (enables stomp-to-kill)
```
STYLE ANCHOR + a small grey field mouse/rat, side view, 25x14 px, 32 PPU.
Generate: (a) a 2-frame turn-around (pivoting to face the other way),
(b) a 1-frame "squashed/defeated" pose (flattened, X eyes, little poof).
Match the existing EnemyWalk mouse sprite proportions and palette.
```

## ❤️ Hearts HUD (uses existing heart.png as base)
```
STYLE ANCHOR + a UI heart icon for a lives/HP bar: full, half, and empty states.
Clean 2D UI sprite, crisp outline, readable at small size, transparent background.
Match the warm storybook palette.
```

> Existing animation controllers/clips (as-built) are catalogued in
> [baseline/AssetInventory § Animations](../../technical/baseline/AssetInventory.md).

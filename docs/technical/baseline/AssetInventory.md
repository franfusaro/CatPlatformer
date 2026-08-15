# Asset Inventory

> **Purpose:** Inventory of significant project assets (sprites, tiles, audio, prefabs, fonts) with verified import specs.
> **Owner:** Franco Fusaro · **Status:** Baseline (as-built) · **Last Updated:** 2026-07-10
> **Related:** [../../design/art/StyleGuide](../../design/art/StyleGuide.md), [../../design/art/Tilesets](../../design/art/Tilesets.md)

Inventory of significant project assets by category, with location, usage, and
the prefabs/scripts that consume them. Vendored packages (`TextMesh Pro/`,
`Standard Assets/`) are summarized, not enumerated per-file.

## Characters

### Player — Cat
| | |
|--|--|
| Sprite sheets | `Sprites & Tiles/Sprites/Player/sprite_base_addon_2012_12_14.png`, `cat_climb2.png`, `Sprites & Tiles/cat-face.png` (HUD/icon) |
| Prefab | `Prefabs/Player.prefab` |
| Scripts | `Player.cs` |
| Animator | `Animations/Player.controller` |
| Clips | `CatIdle.anim`, `CatWalk.anim`, `CatClimb.anim`, `CatDeath.anim` |
| Colliders | CapsuleCollider2D (body), BoxCollider2D (feet), Rigidbody2D |

### Enemy — Rat / Mouse
| | |
|--|--|
| Sprite | `Sprites & Tiles/Sprites/Enemies/mouse.png` |
| Prefab | `Prefabs/EnemyRat.prefab` (Layer: `Enemy`) |
| Scripts | `EnemyMovement.cs` |
| Animator | `Animations/Enemy.controller` |
| Clips | `EnemyWalk.anim` |

### NPCs
None. This is a single-enemy-type game with no NPCs.

## Environment

### Tilemaps / Tilesets (source sheets)
| Sheet | Location | Apparent use |
|-------|----------|--------------|
| BFT - Mega Metroidvania Tileset | `Sprites & Tiles/Sprites/TileSets/` | Primary platform/terrain tiles (~120 sliced tile assets `_0`…`_60`, plus a `_335`–`_394` band) |
| starynight | `.../TileSets/starynight.png` | Night background tilemap (**128** sliced tiles `starynight_0..127`) |
| generic_platformer_tiles | `.../TileSets/` | Additional platform tiles (`_121`, `_149`) |
| goodly-2x | `.../TileSets/` | Misc tiles (`goodly-2x_1`) |
| SPA_Background brown tint | `.../TileSets/` | Brown-tint background (16 sliced tiles) |

### Terrain / Palettes / Rule assets
| Asset | Location | Purpose |
|-------|----------|---------|
| `Dirt.asset`, `GrassPlatforms.asset`, `RockPlatforms.asset` | `Sprites & Tiles/` | RuleTile/terrain tile definitions |
| `1_ladder.asset`, `2_ladder.asset`, `ladder2.asset` | `Sprites & Tiles/Tiles/` | Ladder tiles (climb — `Ladder` layer) |
| `water_1..3.asset` | `Sprites & Tiles/Tiles/` | Water tiles (`Water` layer) |
| `Main Palette.prefab` | `Sprites & Tiles/Tiles/` | Tile Palette used for painting |

### Backgrounds / Parallax (trees & sky)
Location: `Sprites & Tiles/Sprites/Trees/` and `Sprites/`
- Day set: `tree1..tree5.png`, `trees.png`, `far-grounds.png`, `background_obj.png`, `whiteclouds.png`
- Night set: `tree1_night.png`, `tree3-night.png`, `bakcground_night3.png` (sic), `blacktrees.png`, `starynight.png`
- Material: `Sprites/Materials/trees.mat`
- Consumed by `LayerParallax.cs` (per-layer scroll) and `VerticalScroll.cs`.

## Interactive Objects

| Object | Sprite | Prefab | Script |
|--------|--------|--------|--------|
| Coin pickup | `Sprites/Pickups/SPA_Coins.png` | `Prefabs/Coin.prefab` | `CoinPickup.cs` (+ `Coin.controller`, `CoinSpin.anim`) |
| Level exit | `Sprites & Tiles/ExitPixelated.png` | `Prefabs/ExitPixelated.prefab` | `LevelExit.cs` |
| Moving platform | `Sprites/platform.png` | `Prefabs/Platform.prefab` | `MovingPlatform.cs`, `Platform.cs` |
| Hazards | tilemap tiles (`Hazards` layer) | — | handled in `Player.Die` |

## UI

| Asset | Location | Usage |
|-------|----------|-------|
| Heart icon | `Sprites & Tiles/Sprites/heart.png` | Present but **no HP system consumes it** (candidate for future hearts UI) |
| Cat face | `Sprites & Tiles/cat-face.png` | Likely HUD/menu icon **(ASSUMPTION)** |
| Font (TTF) | `Fonts/Caramel Candy .ttf` | Game font |
| Font (TMP SDF) | `Fonts/Caramel Candy  SDF.asset` | TextMeshPro HUD/menu text |
| TextMesh Pro resources | `Assets/TextMesh Pro/` | Vendored TMP shaders, sprites, default assets |

HUD text objects (`livesText`, `scoreText`) are `TextMeshProUGUI`, wired into
`GameSession` per scene.

## Visual Effects
- No particle systems, shaders, or VFX prefabs found in the project's own assets.
  (The `particlesystem` module is available but unused.) Slow-motion on level exit
  (`Time.timeScale`) is the only "effect."

## Audio

### Music (`Assets/Music/`)
| Track | Format |
|-------|--------|
| Victoriana Loop | mp3 |
| Ove - Earth Is All We Have | ogg |
| Malloga_Ballinga_Mastered_mp | mp3 |
| Grasslands Theme | mp3 |
| It Is | mp3 |

Consumed by `MusicPlayer.cs` (`audioClips[]`, random playback) / `MenuMusic.cs`.

### SFX (`Assets/SFX/`)
| Clip | Format | Usage |
|------|--------|-------|
| handleCoins2 | ogg | Coin pickup (`CoinPickup.coinPickUpSFX`) **(one of these)** |
| Coins_Few_00 | mp3 | Coin pickup alt |

## Animations (`Assets/Animations/`)
| Asset | Type | For |
|-------|------|-----|
| `Player.controller` | Animator controller | Cat (Idle/Walk/Climb/Death) |
| `CatIdle/CatWalk/CatClimb/CatDeath.anim` | Clips | Cat states |
| `Enemy.controller` + `EnemyWalk.anim` | Controller/clip | Rat |
| `Coin.controller` + `CoinSpin.anim` | Controller/clip | Coin spin |
| `State Driven Camera Blends.asset` | Cinemachine blends | Camera state transitions |

## Prefabs (`Assets/Prefabs/`) — full list
| Prefab | Purpose |
|--------|---------|
| `Player.prefab` | Player cat |
| `EnemyRat.prefab` | Patrol enemy |
| `Coin.prefab` | Collectible |
| `Platform.prefab` | Moving platform |
| `ExitPixelated.prefab` | Level exit trigger |
| `Cameras.prefab` | Cinemachine camera rig |
| `GameSession.prefab` | Lives/score manager |
| `LevelLoader.prefab` | Scene navigation |
| `ScenePersist.prefab` | Scene-persistence marker |
| `MusicPlayer.prefab` | Background music |
| `OptionsController.prefab` | Options menu logic |

## Materials (`Assets/Materials/`) — VERIFIED
| Asset | Type | Usage |
|-------|------|-------|
| `Enemy Bounciness.physicsMaterial2D` | 2D physics material | Applied to enemy/collider for bounce behavior |
| `Zero Friction.physicsMaterial2D` | 2D physics material | Frictionless surface — almost certainly on the **Player** so it doesn't stick to walls |
| `Sprites/Materials/trees.mat` | Render material | Background tree rendering |

No custom shaders. The two physics materials are the entire "materials" system.

## Fonts
- `Caramel Candy` (TTF + TMP SDF). Single custom font family. OpenSans ships with Standard Assets (vendored).

## Orphan / cleanup candidates
- `Sprites/test.png` — likely a scratch/test asset.
- `heart.png` — unused by code (reserved for future HP UI).
- `sprite_base_addon_2012_12_14.png` — dated filename; verify it's the active cat sheet vs. leftover.
- Ads/Analytics/IAP packages carry no asset footprint but bloat the manifest.

## VERIFIED sprite facts
- **Player.prefab's active sprite = `sprite_base_addon_2012_12_14.png`** (guid
  `c22fc30923bc19a46b44e4f7b9a5a938`), sliced (`spriteMode: 2`), **32 PPU**,
  per-frame ~**18×29 px**. `cat_climb2.png` supplies climb frames; `cat-face.png` is a UI/icon.
- **Mouse/rat:** `mouse.png`, 32 PPU, ~**25×14 px** per frame.
- **Tilesets:** BFT Metroidvania = **16 px** tiles (16 PPU); `starynight`, `SPA_Background
  brown tint`, `goodly-2x` = **32 px** (32 PPU); `generic_platformer_tiles` = 32 px but
  **31 PPU** (a slight authoring inconsistency worth fixing).

## TODO (licensing only — needs external check)
- [ ] Confirm license/attribution for each third-party sprite sheet & music track before redistribution
      (this is the one item not resolvable from the repo — it requires knowing each asset's origin).

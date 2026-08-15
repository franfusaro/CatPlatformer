# Audio Direction

> **Purpose:** Music and SFX direction, the current audio assets, and the target audio architecture (mixer, routed volume).
> **Owner:** Franco Fusaro · **Status:** Draft · **Last Updated:** 2026-07-10
> **Related:** [StyleGuide](StyleGuide.md), [../../technical/baseline/AssetInventory](../../technical/baseline/AssetInventory.md), [../../technical/baseline/PerformanceReview](../../technical/baseline/PerformanceReview.md)

## Current audio (as-built)

- **Music:** 5 background tracks (Victoriana Loop, Ove – Earth Is All We Have,
  Malloga Ballinga, Grasslands Theme, It Is), random playback via `MusicPlayer.cs`.
- **SFX:** coin pickup only (`handleCoins2` / `Coins_Few_00`).
- Full inventory: [baseline/AssetInventory § Audio](../../technical/baseline/AssetInventory.md).

## Known issues to fix (from performance review)

- SFX plays via `AudioSource.PlayClipAtPoint` — **not routed through master volume**,
  and allocates a throwaway AudioSource per pickup (GC churn on WebGL).
- No AudioMixer / groups; `MusicPlayer` polls `isPlaying` each frame to chain tracks.

## Target direction

- **AudioMixer** with routed SFX/music groups; SFX through master volume (Wave 4).
- Pool a single 2D SFX source or an AudioManager.
- Mood: cozy, storybook — consistent with the [visual identity](StyleGuide.md); consider
  distinct day/night or biome-themed music as content grows.

## TODO (to design)

- [ ] Per-biome / per-mode (flow vs. boss) music intent.
- [ ] Ability and swap SFX palette.

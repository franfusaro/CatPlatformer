# Performance Review

> **Purpose:** WebGL-focused performance findings: per-frame FindObjectOfType, allocations, polling and download size.
> **Owner:** Franco Fusaro · **Status:** Baseline (as-built) · **Last Updated:** 2026-07-10
> **Related:** [CodeReview](CodeReview.md), [../../design/art/AudioDirection](../../design/art/AudioDirection.md)

Context: a small 2D platformer targeting WebGL. Absolute performance risk is
**low** (tiny scenes, few objects), but several patterns are wasteful and will not
scale. Findings are ordered by concern.

## 🔴 Notable

### 1. Per-frame `FindObjectOfType` in `OptionsControllers.Update`
```csharp
void Update() {
    var musicPlayer = FindObjectOfType<MusicPlayer>(); // every frame
    ...
}
```
`FindObjectOfType` scans all objects of the type in the scene **every frame** while
the options menu is open. Cache it in `Start`/`Awake` and only re-resolve if null.

### 2. `FindObjectOfType` on gameplay events
- `CoinPickup.OnTriggerEnter2D` → `FindObjectOfType<Player>()` **and** `FindObjectOfType<GameSession>()` on **every coin** pickup.
- `Player.ProcessDeath` → `FindObjectOfType<GameSession>()`.
- `LevelExit`, `GameSession.ProcessPlayerDeath`, `LevelLoader.*` → repeated scans.

Each call is O(total objects). Not fatal at this scale but a bad habit and the
first thing to bite as levels grow. Cache references or move to events.

### 3. `AudioSource.PlayClipAtPoint` per coin
```csharp
AudioSource.PlayClipAtPoint(coinPickUpSFX, FindObjectOfType<Player>().transform.position);
```
Instantiates a temporary GameObject + AudioSource for every pickup, destroyed when
the clip ends → **GC churn** on WebGL (where GC hitches are most visible). Pool a
single 2D SFX source or use an AudioManager.

## 🟡 Minor

### 4. Polling in `Update`
- `MusicPlayer.Update` polls `audioSource.isPlaying` every frame to chain tracks.
  Use a coroutine scheduled for `clip.length` instead.
- `EnemyMovement.Update` and `Player.Update` set velocity every frame — normal for
  physics, but `Player.Update` also calls `LayerMask.GetMask(...)` repeatedly.

### 5. `LayerMask.GetMask(string...)` allocations
`Player` computes masks by **name** each call (`PlayerIsOnGround`,
`PlayerIsTouchingLadder`, `Die`). `LayerMask.GetMask` does string lookups each
frame. Cache the masks once in `Start` as `int` fields.

### 6. Moving-platform re-parenting every frame
`Platform.OnTriggerStay2D` reassigns `transform.parent` while the player stands on
it. Transform hierarchy changes are not free and can dirty the transform system;
also risks scale inheritance. Use a rider offset instead.

### 7. Empty `Update`/`Start` bodies
`ScenePersist`, `MovingPlatform.Start` — empty MonoBehaviour messages still cost a
tiny per-frame call overhead across many instances. Remove them.

## 🟢 Good practices already present
- Component references cached in `Start` (`Player`, `EnemyMovement`) — not fetched in `Update`.
- Velocity-based movement is framerate-independent.
- No `Camera.main` in loops; Cinemachine handles the camera.
- Scenes are small; no obvious overdraw beyond parallax layers.

## Duplicated prefabs / memory
- Manager prefabs (`GameSession`, `LevelLoader`, `ScenePersist`, `MusicPlayer`) are
  placed per-scene and deduped at runtime by singleton guards — fine, but means
  each scene loads/instantiates then destroys duplicates. Minor.
- Hundreds of individual `starynight_*`/`BFT_*` **TileBase `.asset`** files are
  normal for sliced tilesets; ensure the source textures use a **Sprite Atlas** or
  appropriate compression for WebGL download size (`Build.data.unityweb` is ~13 MB).

## WebGL-specific notes
- **Download size** matters most on web: 5 music tracks (mp3/ogg) + large tilesets
  dominate `Build.data`. Consider compressing/streaming audio and atlasing sprites.
- `Application.Quit()` is a no-op on WebGL (`LevelLoader.QuitGame`) — hide that button on web.
- GC hitches (from `PlayClipAtPoint`, `FindObjectOfType`) are more perceptible on WebGL.

## Priority fixes (perf)
1. Cache `LayerMask` ints in `Player` (trivial, per-frame win).
2. Cache/replace all `FindObjectOfType` calls.
3. Pool coin SFX / add an AudioManager.
4. Atlas sprites + compress audio for WebGL download size.

## TODO (manual verification)
- [ ] Profile a level in the editor/WebGL to confirm GC spikes on coin pickup.
- [ ] Check texture import settings (compression, atlas) for the large tilesets.
- [ ] Measure WebGL first-load time and `Build.data` breakdown.

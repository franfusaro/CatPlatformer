<h1 align="center">🐱 CatPlatformer</h1>

<p align="center">
  A 2D pixel-art platformer where you guide a cat through four levels of
  jumping, climbing, coin-collecting and hazard-dodging.
</p>

<p align="center">
  <img alt="Unity" src="https://img.shields.io/badge/Unity-2018.3.0f2-black?logo=unity">
  <img alt="Language" src="https://img.shields.io/badge/C%23-MonoBehaviour-blue">
  <img alt="Target" src="https://img.shields.io/badge/Build-WebGL-orange">
  <img alt="Version" src="https://img.shields.io/badge/version-0.1-lightgrey">
</p>

---

## 📖 Overview

CatPlatformer is a small single-player 2D platformer built with the classic
Unity 2D toolchain (Tilemaps, Rigidbody2D physics, Cinemachine, TextMeshPro).
The player controls a cat with a lives/score system across four hand-built
tilemap levels, from a main menu through to a success screen.

> This README was reconstructed from source during a technical audit. See the
> [`docs/`](docs/) folder for the full architectural assessment.

## ✨ Features

- 🏃 Tight platformer movement (walk, jump, ladder climb)
- 🪜 Ladder climbing with gravity toggling
- 🪙 Collectible coins with score + SFX
- 💀 Death by enemies/hazards with a dramatic "death kick" and respawn
- ❤️ 3-life session that persists across scene loads
- 👾 Patrolling rat enemies that turn around at platform edges
- 🛗 Waypoint-based moving platforms that carry the player
- 🎥 Cinemachine State-Driven camera + multi-layer parallax backgrounds
- 🎵 Randomized background music with a persistent audio player
- ⚙️ Options menu with a master-volume slider saved to PlayerPrefs
- 🌐 Ships as a WebGL build

## 🖼️ Screenshots

> _Placeholders — add captures under `docs/screenshots/`._

| Main Menu | Gameplay | Level Exit |
|-----------|----------|------------|
| _`docs/screenshots/menu.png`_ | _`docs/screenshots/gameplay.png`_ | _`docs/screenshots/exit.png`_ |

## 📂 Folder Structure

```
CatPlatformer/
├── Assets/
│   ├── Scripts/          # 16 gameplay scripts (all game logic)
│   ├── Levels/           # 7 scenes (menus, 4 levels, success)
│   ├── Prefabs/          # Player, enemies, managers, pickups
│   ├── Animations/       # Animator controllers + clips
│   ├── Sprites & Tiles/  # Sprite sheets + tilemap tile assets
│   ├── Music/ · SFX/     # Audio
│   ├── Fonts/            # Caramel Candy font + TMP asset
│   └── Standard Assets/  # Vendored CrossPlatformInput + 2d-extras
├── Build/                # Committed WebGL build
├── index.html            # WebGL launcher
├── Packages/ · ProjectSettings/
└── docs/                 # Technical documentation
```

## 🔧 Requirements

- **Unity 2018.3.0f2** (exact version recommended — see [dependency audit](docs/technical/baseline/DependencyAudit.md))
- A WebGL-capable browser to run the committed build
- Key packages: Cinemachine 2.2.9, TextMeshPro 1.3.0, 2d-extras, Standard Assets CrossPlatformInput

## 🚀 Getting Started

### Run in the editor
1. Install **Unity 2018.3.0f2** (via Unity Hub → Installs → Add).
2. Clone this repo and open the root folder as a Unity project.
3. Open `Assets/Levels/MainMenu.unity` and press **Play**.

### Play the committed WebGL build
Serve the repo root over HTTP (WebGL cannot run from `file://`):
```bash
python3 -m http.server 8080
# open http://localhost:8080/index.html
```

### Make a fresh build
`File → Build Settings → WebGL → Build`. Ensure the scene list matches
`ProjectSettings/EditorBuildSettings.asset` (MainMenu first).

## 🎮 Controls

| Action | Input |
|--------|-------|
| Move | `←` / `→` or `A` / `D` (Horizontal axis) |
| Climb ladder | `↑` / `↓` or `W` / `S` (Vertical axis, while touching a ladder) |
| Jump | `Space` / configured **Jump** button (only when grounded) |

> Input is routed through Unity **Standard Assets CrossPlatformInput**, so mobile
> on-screen controls can be enabled. There is currently no dash, attack, crouch,
> double-jump or wall-jump.

## 🕹️ Gameplay Overview

Start with 3 lives. Collect coins for score (50 each). Touching an **Enemy** or
**Hazards** layer kills the cat, plays a death animation, and either respawns you
(restart level) or, at 0 lives, returns to the main menu. Reaching a `LevelExit`
trigger slow-motions the screen and loads the next level. Clear Level 4 to reach
the **Success** screen.

## 🛠️ Development Workflow

- All logic is plain MonoBehaviours in `Assets/Scripts/` — no assembly definitions.
- Cross-object communication is via `FindObjectOfType<T>()` (see [code review](docs/technical/baseline/CodeReview.md)).
- Persistent managers (`GameSession`, `ScenePersist`, `MusicPlayer`) use a
  `DontDestroyOnLoad` + "destroy the duplicate" singleton pattern.
- Scenes are loaded by **build index**, so scene order in Build Settings is load-bearing.

## ⚠️ Known Limitations

- `LevelLoader.LoadYouLoseScene()` references a **`LoseScreen` scene that does not exist** in the project.
- Jump uses a collider ground-check with no coyote-time / jump buffering.
- Heavy per-frame `FindObjectOfType` usage (see [performance review](docs/technical/baseline/PerformanceReview.md)).
- Ads / Analytics / IAP packages are included but unused.
- No automated tests, no CI, no assembly definitions.
- Difficulty slider is stubbed out (commented) in the options menu.

## 👤 Credits

- **Author / Programming / Design:** Franco Fusaro (sole git contributor)
- **Framework patterns:** based on GameDev.tv "Complete Unity 2D" course conventions _(assumed)_
- **Third-party:** Unity Standard Assets (CrossPlatformInput), Unity 2d-extras, Cinemachine, TextMeshPro
- **Art/Audio:** third-party sprite sheets & music tracks (see [asset inventory](docs/technical/baseline/AssetInventory.md); licenses **TODO — verify before redistribution**)

## 📄 License

No license file is present. Add one before distributing. Note third-party art and
music assets may carry their own licenses that must be verified.

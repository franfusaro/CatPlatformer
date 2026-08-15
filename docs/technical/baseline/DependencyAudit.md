# Package & Dependency Audit

> **Purpose:** Editor/engine version, package manifest audit, deprecated APIs and the recommended upgrade sequence.
> **Owner:** Franco Fusaro · **Status:** Baseline (as-built) · **Last Updated:** 2026-07-10
> **Related:** [../../decisions/adr/0001-unity-lts-target](../../decisions/adr/0001-unity-lts-target.md), [../../production/Roadmap](../../production/Roadmap.md)

Source: `Packages/manifest.json`, `ProjectSettings/ProjectVersion.txt`, and code
usage grep. "Latest compatible" refers to what is reasonable when upgrading the
Unity editor itself; exact numbers should be confirmed against Unity's package
registry at migration time.

## Editor / Engine

| Item | Current | Notes |
|------|---------|-------|
| Unity Editor | **2018.3.0f2** | Released Dec 2018; long out of official support. No LTS. |
| Scripting runtime | .NET 4.x era (2018.3) | Verify `apiCompatibilityLevel` in ProjectSettings |
| Render pipeline | Built-in (Legacy) | No SRP; URP/HDRP not present |

**Upgrading the editor is the central decision.** Everything else follows from the
target Unity version chosen (recommended: **Unity 6 / 6000.x** — see
[ADR-0001](../../decisions/adr/0001-unity-lts-target.md), revised 2026-08-02 after
2022.3 LTS's support window lapsed).

## Unity Packages (manifest)

| Package | Current | Used | Migration difficulty | Risk | Recommendation |
|---------|:-------:|:----:|:--------------------:|:----:|----------------|
| com.unity.cinemachine | 2.2.9 → **2.10.7** (verified) | ✅ camera | Low | Low | **Correction (2026-08-15):** the actual Unity 6000.5.8f1 auto-upgrade kept Cinemachine on the **2.x line** (bumped to 2.10.7), it did **not** force 3.x. The 3.x breaking-API risk noted below no longer applies to the current install; a 2.x→3.x migration is still worth doing eventually (Wave 4+) for URP/Cinemachine 3 feature parity, but it's optional, not a Wave-0 blocker. |
| com.unity.textmeshpro | 1.3.0 → folded into `com.unity.ugui` 2.5.0 (verified) | ✅ HUD | Low | Low | TMP is now bundled as part of UGUI in Unity 6, no separate package entry; upgrade was silent, no re-import prompt needed |
| com.unity.ads | 2.3.1 | ❌ | — | — | **Remove** (unused) or migrate only if monetization planned |
| com.unity.analytics | 3.2.2 | ❌ | — | — | **Remove** (unused) |
| com.unity.purchasing | 2.0.3 | ❌ | — | — | **Remove** (unused) |
| com.unity.collab-proxy | 1.2.15 | editor | Low | Low | Remove if not using Unity Collab/PlasticSCM |
| com.unity.package-manager-ui | 2.0.3 | editor | — | — | Obsolete in newer Unity (folded into editor); drops automatically on upgrade |
| Unity modules (physics2d, tilemap, ui, audio, animation, …) | 1.0.0 | mixed | Auto | Low | Managed by editor version; unused modules (vehicles, cloth, terrain, vr, xr, wind, cloth) can be trimmed |

### Unused engine modules worth trimming
`vehicles`, `cloth`, `terrain`, `terrainphysics`, `vr`, `xr`, `wind`, `umbra`,
`director` (Playables is imported in `GameSession` but unused), `ai` — none are
used by a 2D platformer. Removing them slims builds and the manifest.

## Vendored third-party (in `Assets/`)

| Dependency | Location | Used | Migration path |
|------------|----------|:----:|----------------|
| Standard Assets — CrossPlatformInput | `Standard Assets/CrossPlatformInput/` | ✅ (all player input) | **Deprecated by Unity.** Replace with the **new Input System** package. Medium effort, high value. |
| 2d-extras (master snapshot) | `Standard Assets/2d-extras-master/` | ✅ (RuleTile/AnimatedTile/brushes) | Now shipped as **com.unity.2d.tilemap.extras** package — swap vendored copy for the package on upgrade. Low–Med. |
| TextMesh Pro (vendored resources) | `Assets/TextMesh Pro/` | ✅ | Managed by the TMP package once upgraded; regenerate essentials. |

## Obsolete / deprecated APIs in project code
| API | Where | Status | Replacement |
|-----|-------|--------|-------------|
| `CrossPlatformInputManager` | `Player.cs` | Deprecated Standard Asset | Input System actions |
| `FindObjectOfType` (hot paths) | 11 sites | Slow, discouraged | Cached refs / DI / events |
| `Application.Quit()` from WebGL | `LevelLoader.QuitGame` | No-op on WebGL | Hide quit on web builds |
| `using UnityEngine.Playables` | `GameSession.cs` | Imported, unused | Remove |

## Migration verdict

| Track | Recommendation |
|-------|----------------|
| **Critical upgrades** | Move off Unity 2018.3 to a supported LTS; replace deprecated CrossPlatformInput. Security/support and toolchain longevity depend on this. |
| **Recommended upgrades** | Cinemachine + TMP + 2d-extras → package versions matching the new LTS; remove Ads/Analytics/IAP/PackageManagerUI; trim unused modules. |
| **Optional upgrades** | URP for 2D lighting; Addressables (overkill at current scale). |

### Migration risk factors
- **Blockers:** none technical — codebase is tiny (703 LOC) and uses stable APIs.
  The main friction is CrossPlatformInput → Input System and re-validating physics
  behavior after an engine version jump (2D collision/timescale nuances).
- **Highest-risk area:** re-parenting moving platforms + `IsTouchingLayers` timing
  can behave differently across physics versions — test levels after upgrade.
- **Superseded (2026-08-15):** the Cinemachine 2.x→3.x risk noted above didn't
  materialize — the actual install kept Cinemachine on 2.x. See the package table.
- **New, found on the real 6000.5.8f1 install (2026-08-15):** 6000.5 is a
  Preview/Tech Stream release, not LTS. Unity is mid-migration from
  `Object.GetInstanceID()` to `GetEntityId()` for DOTS unification; 6000.5 is the
  first version to make this a hard `CS0619` compile error instead of a warning.
  Hit once so far in vendored `Standard Assets/2d-extras-master` (`GameObjectBrush.cs`,
  a `GetHashCode()` override) — fixed with `GetEntityId().GetHashCode()` in place of
  `GetInstanceID()`. Being on a Tech Stream release means more of these may surface
  as the install patches forward; each is a small, mechanical fix, not a redesign.

## Suggested upgrade sequence
1. Branch. Open in target **LTS**; let it auto-upgrade packages; fix compile errors.
2. Remove unused packages/modules; delete `using Playables`.
3. Swap vendored 2d-extras for the package; re-point RuleTiles if needed.
4. Migrate input to the Input System (or Input System's compatibility mode first).
5. Playtest all 4 levels: jump, ladder, platforms, death/respawn, level flow.

## VERIFIED build settings (from ProjectSettings.asset)
- `scriptingRuntimeVersion: 1` → **.NET 4.x equivalent runtime** (the modern one for 2018.3).
- `apiCompatibilityLevel: 6` → **.NET 4.x** API profile.
- WebGL: `webGLMemorySize: 256` MB, `webGLCompressionFormat: 1` (Gzip),
  `webGLExceptionSupport: 1` (explicit-throw), `webGLLinkerTarget: 1` (WebAssembly).
- On a modern LTS, WebGL builds switch to **Brotli** compression and drop the fixed
  memory heap for a growable one — expect smaller downloads and fewer OOMs after upgrade.

## TODO (decision, not verification)
- [x] Pick the target Unity version — **Unity 6 / 6000.x** (ADR-0001, revised 2026-08-02).
- [x] Once installed, record the exact 6000.x patch and resolved package versions here
      (ADR-0001, confirmed 2026-08-15): **6000.5.8f1** (Preview/Tech Stream — Hub's
      default "recommended" install, chosen deliberately, see ADR-0001 for the
      tradeoff). Resolved packages: Cinemachine **2.10.7**, TextMeshPro folded into
      `com.unity.ugui` **2.5.0**, `com.unity.package-manager-ui` dropped automatically.

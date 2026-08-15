> **⚠️ SUPERSEDED / ARCHIVED (2026-07-10).** This single-file design bible has been
> split into the `docs/design/` tree, which is now canonical. Kept here unedited for
> history. Do not update this file — edit the split docs. Map:
>
> - §1–2 → design/Vision.md · §3 → design/DesignPillars.md · §4,§11 → design/CoreLoop.md
> - §5,§7 → design/gameplay/Movement.md · §6,§10.5 → design/gameplay/Abilities.md,Progression.md,Collectibles.md
> - §8 → design/levels/LevelStructure.md · §9 → design/gameplay/Combat.md, design/narrative/Bosses.md
> - §10 → design/GameRules.md · §10.5,§12.2 → design/narrative/NPCs.md,SideQuests.md · §13 → design/narrative/Characters.md, design/art/StyleGuide.md
> - §15 → technical/Architecture.md · §17 → decisions/DECISION_LOG.md · §12,§14 → Ideas.md

# Game Design Document — Two Cats (working title)

> **Status:** Living design bible · **Established:** 2026-07-05 · **Owner:** Franco (creative director)
>
> This is the north star. Every feature, mechanic, level, and asset is measured
> against the pillars below. It supersedes the single-cat assumptions in the older
> reverse-engineering docs — those describe the *inherited* project; this describes
> what we are *building*. See `docs/vibe-coding-playbook.md` for how we execute.

---

## 1. One-line pitch

**A flowing action-platformer starring a chaos-orange cat and a lazy tuxedo cat,
who you swap between to combine their opposite abilities across high-speed
traversal, cooperative puzzle rooms, and stun-and-DPS boss fights.**

## 2. The fantasy

You control two cats with opposite souls. One is pure **zoomies** — fast, fearless,
fragile, always moving. The other is a **loaf** — grumpy, heavy, immovable, hits like
a truck when it can be bothered. Alone, each is half a game. Together — and only by
**swapping between them** — they flow through the world, solve rooms that need both,
and take down bosses. The game is dynamic and fast by default, with puzzle and combat
beats as punctuation.

## 3. Design pillars

1. **Flow first.** The default feel is fast, expressive, uninterrupted movement.
2. **Asymmetry is the game.** The two cats are opposites; every challenge asks *which cat*.
3. **One verb: the swap.** Traversal, puzzles, and combat are all expressions of swapping.
4. **Personality = mechanics.** How a cat *acts* is what a cat *does*.
5. **Punctuation, not walls.** Puzzles and bosses interrupt flow deliberately, then release it.

---

## 4. The core verb — SWAP (everything is one mechanic)

The swap is the single core verb. It expresses itself four ways; the fourth is a
future layer (see §12):

| Pillar | The question asymmetry asks | Swap gives you | Status |
|--------|----------------------------|----------------|:------:|
| **Traversal (flow)** | *Who can **reach** this?* | speed & reach | Core |
| **Puzzle rooms** | *Who can **key** this?* | co-location combos | Core |
| **Combat / bosses** | *Who can **hurt** this?* | stun → DPS role-switch | Core |
| **NPCs / world** | *Who can **relate** to this?* | quest/mentor ability-unlocks + social gating | **Core (§10.5)** |

Keeping all four under one verb is what makes the game cohere — the player never
learns a separate sub-game; they learn *when to be which cat*.

## 5. Relationship model — persistent partner (DECISION)

- **Both cats always exist in the world** (persistent partner), you control one at a time.
- **Default (flow):** the inactive cat **follows** via simple AI within its own ability
  limits; if it falls too far behind or goes offscreen, it **teleport-recovers** to the
  leader's last safe grounded spot. You never babysit it during flow.
- **Puzzle rooms & boss arenas:** crossing the room's entrance **disables
  teleport-recover** — both cats must physically be present and solve/fight together.
- The partner **never replicates an ability in real time.** Abilities are leader-only
  and instantaneous; the partner *reconciles its position afterward*. This decouples
  ability execution from following and is what makes asymmetry tractable.

> **Rejected alternatives:** freeze/independent (too slow for our action feel);
> pure cooperative-only (too much per-ability design load as the default).
> We chose the **hybrid**: follow+teleport for flow, cooperative combos for punctuation.

---

## 6. The two cats

| | 🟠 **Orange cat** — *chaos / speed* | ⬛⬜ **Tuxedo cat** — *weight / control* |
|---|---|---|
| **Personality** | Hyperactive, zoomies, fearless, fragile | Lazy, grumpy, immovable, tanky, high-impact |
| **Flow verb** | **Zoomies** — dash / dash-cancel, wall-jump, pounce | Double-jump / heavy glide (keeps up, lands hard) |
| **Puzzle verb** | **Wall-cling** — cling/climb walls, squeeze into gaps | **Loaf** — plants into a loaf: immovable platform / anchor / holds pressure plates |
| **Combat role** | Fast combo DPS: dash-through swipes, air combos | Crowd control & **stun**: *the Flop* (ground-pound AoE), body-slam, big slow swats |
| **Survivability** | Low HP, high mobility | High HP, low mobility |
| **You mostly play as...** | this cat, in flow | this cat, for gates/puzzles/stuns |

**Ability design rule:** start with **exactly 2 abilities each** (a flow verb + a
puzzle verb; combat reuses them). Resist adding more until these four *sing* in all
three pillars. Every new ability must be good **solo for flow** *and* combine
**cooperatively for puzzles/bosses**, or it doesn't ship.

**Theming note:** lean into real cat behaviors for ability names/flavor — *zoomies,
loaf, the flop, biscuits/knead, pounce, the toe-bean high-five*. It's a cat game; the
vocabulary is free and delightful.

---

## 7. Traversal & flow

- Active cat leads; partner follows with teleport-recover.
- Orange is the natural flow driver (dash + wall-jump = fast, expressive). Tuxedo
  keeps up via double-jump/glide; when it can't, it warps.
- **Enemy gating in flow:** trash mobs die to orange's dash-pounce and stay in flow;
  occasional **armored/heavy enemies** force a swap to tuxedo — same design move as a
  ledge only one cat can reach.

## 8. Puzzle rooms (co-location gates)

- Visually **enclosed** rooms with a **locked exit**; entering disables teleport-recover.
- Solved by combining abilities: e.g. **Tuxedo Loafs into a platform / holds a plate →
  Orange wall-clings & dashes to a switch → exit opens for both.**
- Neither cat can solve it alone — that's the definition of a puzzle room.

## 9. Combat & bosses

- **Regular combat** = "who can hurt this?" gating (see §7).
- **Boss loop** (the swap *is* the combat):
  ```
  Boss armored / invulnerable
     → ⬛⬜ Tuxedo: FLOP stuns / cracks armor  (opens a window)
     → ⟳ SWAP
     → 🟠 Orange: dash-combo DPS during the window
     → window closes → repeat, dodging the whole time
  ```
- **Boss arenas = puzzle rooms** (teleport off, both cats present, exit locked until
  the boss falls) — reuses the same tech.
- Boss variety spectrum: puzzle-leaning (weak point only wall-cling Orange can reach
  while Tuxedo holds a lever) ↔ action-leaning (pure stun→DPS dance).

## 10. Health — HP + down / revive (replaces one-hit lives)

- **Per-cat HP** (Orange low, Tuxedo high). Finally uses the existing `heart.png`.
- A cat at 0 HP is **downed, not dead** — you keep playing as the other cat and can
  **revive your buddy** by reaching it (risk/tension beat; a natural mini-puzzle in a
  boss fight).
- **Both cats down = checkpoint / game over.**
- **Idle-partner safety:** in *flow*, the inactive cat is invulnerable. In **boss
  arenas**, the boss can target *either* cat — that vulnerability is what makes
  swapping meaningful under pressure.

## 10.5 Items, abilities & progression (CORE)

**Progression model (DECISION):** abilities are **found or learnt from others — never
bought.** Cosmetics are the only thing you *buy*. There is **no weapon/gear inventory.**

**The guardrail (the filter test for any item idea):**
> *Items amplify the cats; they never become a parallel system.* If an item makes you
> feel like generic gear-holding hero instead of *more* like the chaos-cat or the
> lazy-cat, cut it or reskin it.

### How you gain an ability — three sources, one system
| Source | How | Feel |
|--------|-----|------|
| **Found** | Reach a hidden/gated place → get the ability (Metroidvania) | Exploration reward |
| **Taught** | A **mentor** NPC teaches after a trial / challenge room | Mastery milestone |
| **Earned** | A **quest-giver** NPC rewards it for a task | Relationship payoff |

- **Starting kit is given, not gated:** you begin with Zoomies + Wall-cling (Orange) and
  Glide + Loaf (Tuxedo). Finds/quests grant **additional** abilities and upgrades only.
- All three sources feed the **same ability-grant API** (see §15) — an ability can be
  granted by a pickup trigger, a mentor, or a quest reward interchangeably.

### NPCs & quests (promotes §12.2 from "future" to a core progression system)
- NPCs have a **personality affinity** (prefers Orange, prefers Tuxedo, or neutral).
- Affinity **personality-gates the quest**: which cat can help → which cat learns the
  ability → and **the ability's flavor matches the deed + the cat.**
- **Canonical example:** a nervous bird, its eggs under threat, begs the fast, fearless
  **Orange** cat to defend the nest / fetch food → Orange learns a combat/pounce ability.
  A slow elder cat who only trusts the patient **Tuxedo** → teaches a Loaf/anchor upgrade.
- This is the swap verb's **fourth expression** ("who can *relate* to this?") now doing
  real progression work, not just flavor.

### Pitfall to design against — fetch-quest overload
"Bring me food / protect my eggs" is a fetch/escort quest — fine in small doses, deadly
in bulk. Rules:
- Keep quests **short, diegetic**; favor **protect / escort / reach** over pure fetch.
- **Mix** quest-unlocks with world-found unlocks so it never becomes an errand simulator.
- **Never** gate a *core traversal* ability behind a tedious quest.

### Consumables & cosmetics
- **Consumables (flow-safe, diegetic, no menus):** e.g. **catnip** = temporary zoomies
  frenzy; a **fish** heals / helps revive. Feed the §10 HP/down-revive system.
- **Cosmetics:** bought with collectible currency (yarn / coins), **purely visual**, tied
  to personality (chaos collar for Orange, bowtie for Tuxedo); optional tailor NPC.

### Swap-flavored items — carry & hand-off
Because the partner is always present, carried items get a co-op verb no solo game has:
**a carried item drops or hands off on swap.** Tuxedo lugs a heavy key, then **hands it
to Orange** at a gap only Orange can cross. Item-carrying becomes cooperative, flowing
out of the swap spine rather than sitting beside it. *(Interacts with the swap-feel fork,
§14.1.)*

### "Weapons" = personality-flavored object-play, not gear
No swords/guns/loadouts. Instead: **Orange bats projectiles** (yarn ball, bottlecap —
light, fast, chaotic ranged tool); **Tuxedo shoves heavy objects** off ledges / body-slams.
Same asymmetry logic as everything else. Combat items, if any, must be **cat-specific or a
costly resource** — never a universal item that collapses an asymmetry gate (e.g. a generic
stun grenade would erase the "swap to Tuxedo to stun" boss loop).

### Flow protection
No inventory-management **menus** during flowing sections. Item/cosmetic/quest management
lives at hubs or in menus only. Contextual, quick-use, or auto — never pause-and-fiddle.

### Open sub-fork (§14.4) — how central are NPCs/quests?
**Defaulted to Light-touch** (adjustable): a handful of optional quest-givers/mentors, most
abilities world-found, **flow stays dominant**. Alternative: *Hub-and-spoke* (a central NPC
town where quests are a main pillar — more content, more RPG-flavored, more flow risk).

## 11. Level rhythm

The macro structure is a **rhythm of modes**, layout-signaled so the player knows
which gear they're in:

```
flow corridor → puzzle room → flow corridor → mini-combat → puzzle room → BOSS arena
   (open)         (enclosed)      (open)        (open)        (enclosed)     (enclosed)
```

Flow is the release valve; enclosed rooms are the tension. Never two enclosed rooms
back-to-back without flow between them.

---

## 12. Future / backlog ideas (captured, not yet designed)

These are approved directions, deliberately **not** in the initial scope. They fit the
pillars, which is why they're worth keeping.

### 12.1 Solo-cat levels / sections
Levels where you have **only one cat** — the pair got separated.
- **Orange-only:** a pure high-speed platforming gauntlet (zoomies flow, no anchor).
- **Tuxedo-only:** a slower, heavier tank/puzzle level.
- **Value:** teaches each kit in isolation (great onboarding/tutorial), adds difficulty
  variety, and doubles as a **dramatic "separation" beat** narratively (reunion = payoff).
- **Design impact:** swap/follow/teleport systems must gracefully handle "one cat
  present" (swap disabled, single HP, camera single-target). Build the controller so a
  cat can be *absent*, not just inactive.

### 12.2 Personality-reactive NPCs → PROMOTED to core (§10.5)
This is **no longer just future flavor** — it became the backbone of the progression
system (abilities "learnt from others"). Full design lives in **§10.5**. Retained here as
a pointer. Remaining *future* texture on top of the core system: a hyper kitten that only
follows Orange, a grumpy shopkeeper who only bargains with Tuxedo, ambient reactions that
don't gate anything.

### 12.3 Other parked ideas
- Swap-carries-momentum vs. instant control-flip (see §14 open fork).
- Unlockable third ability per cat, late-game.
- Cosmetic collars/skins (regenerated via PixelLab).

---

## 13. Characters & art direction

- **Orange cat:** warm orange tabby, lean, animated with twitchy energy. Low, fast poses.
- **Tuxedo cat:** black-and-white, chunky, heavy poses, half-lidded grumpy expression.
- **Readability rule (gameplay-critical):** in a fast action game the player must know
  *at a glance* which cat they control. **Orange + tuxedo** gives maximum color **and**
  silhouette contrast — chosen over grey for exactly this reason.
- Art specs, animation lists, and PixelLab prompts: **`docs/pixellab-improvements.md`**
  (the cat sprite kit there — jump/fall/land, climb-idle, hurt — is now the *Orange*
  base; a parallel Tuxedo set + the new ability animations get generated the same way).

## 14. Open design forks (next decisions)

1. ~~**Swap feel:** instant vs. throw/summon.~~ **RESOLVED:** **[SWAP] is an instant
   control-flip** (flow-first). The throw/summon idea survives as a **separate lower-frequency
   [RECALL/TOSS] verb** — summon the partner / hurl it — used for gathering before a puzzle,
   **hand-off of carried items** (§10.5), and launching Tuxedo as an attack. Two buttons, two
   jobs; the swap is never overloaded.
2. Exact stun/DPS timing windows and boss-phase count. *(decide while playtesting)*
3. Revive cost/time and whether downed cats auto-recover over time. *(decide while playtesting)*
4. **NPC/quest centrality:** Light-touch (defaulted) vs. Hub-and-spoke (§10.5).

## 15. How this maps to the build (technical)

No new *architecture* beyond what the modernization plan already calls for — the design
was chosen to fit it:
- **Swap & follow:** `ActiveCatManager` + one `CatController` on both cats + a
  swappable `InactiveCatStrategy` (follow / teleport-recover; "absent" for solo levels).
- **Abilities:** small `CatAbility` components per cat (Zoomies, Loaf, Flop, Wall-cling…),
  granted through **one ability-grant API** so a pickup, a mentor, or a quest reward can all
  unlock the same way. Build the ability set to *grow at runtime*, and to support a cat being
  *absent* (for solo-cat levels), not just inactive.
- **NPCs/quests:** data-driven NPC definitions (affinity + quest + reward); NPCs query
  `ActiveCatManager` for the current cat — the same "who is active" check the game already runs.
- **Enemies/bosses:** `IDamageable`, enemies with phases + an `arena` flag reusing the
  puzzle-room teleport-gate.
- **Health:** per-cat HP + down/revive (new to Wave 1's scope; replaces lives).
- **Backbone:** ScriptableObject state + events + assembly definitions, per
  `docs/code-review.md` and `docs/modernization-roadmap.md`.
- This is now the **Wave 1 pillar**: *multi-cat controller + swap + ability system + HP*,
  built on the Wave 0 LTS/Input-System foundation.

## 16. Cross-references
- Execution model, dev cycle, CI → `docs/vibe-coding-playbook.md`
- Technical foundation & sequencing → `docs/modernization-roadmap.md`, `docs/code-review.md`
- Art regeneration & specs → `docs/pixellab-improvements.md`
- Inherited-project baseline → `docs/executive-summary.md` and siblings

## 17. Decision log
| Date | Decision |
|------|----------|
| 2026-07-05 | Persistent-partner relationship model; hybrid follow+teleport (flow) / cooperative (puzzles). |
| 2026-07-05 | Primary feel: flowing action-platformer with puzzle & boss punctuation. |
| 2026-07-05 | Two cats: Orange (chaos/speed, low HP) + Tuxedo (weight/control, high HP). Colors chosen for readability. |
| 2026-07-05 | Combat = third expression of the swap verb (stun→DPS). HP + down/revive replaces one-hit lives. |
| 2026-07-05 | Future (approved, unscoped): solo-cat levels. |
| 2026-07-05 | Progression: abilities are **found or learnt (quest/mentor NPCs)**, never bought; cosmetics bought with currency; no weapon/gear inventory. Personality-NPCs promoted from future → core progression driver (§10.5). Default NPC centrality = light-touch. |
| 2026-07-05 | Swap feel: **[SWAP] = instant control-flip**; throw/summon kept as a separate **[RECALL/TOSS]** verb (repositioning, item hand-off, Tuxedo-as-projectile). |

# Decision Log — Game Design

> **Purpose:** The canonical record of significant **game-design** decisions (kept separate from technical ADRs). Newest first.
> **Owner:** Franco Fusaro · **Status:** Living · **Last Updated:** 2026-08-02
> **Related:** [adr/](adr/0000-template.md) (technical decisions), [../design/Vision](../design/Vision.md), [../Ideas](../Ideas.md)

## How this works

- **Game-design decisions** (mechanics, feel, progression, narrative direction) go here.
- **Technical/architectural decisions** (engine version, packages, code architecture) go
  in numbered [ADRs](adr/0000-template.md).
- Each entry uses: **Decision · Alternatives · Reasoning · Consequences · Status.**
- Cross-reference the relevant Design Bible doc. Don't delete superseded decisions —
  mark them `Superseded by DL-XXX`.

---

### DL-015 — Cooperation Philosophy adopted as a forward-looking evaluation lens for future cooperative mechanics (DCR-003)
- **Date:** 2026-08-02 · **Status:** Accepted
- **Decision:** Cooperation between Orange and Tuxedo is canonized as an emotional
  philosophy, complementing [Movement Philosophy](../design/gameplay/Movement.md#movement-philosophy)
  (DL-013) and [Swap Philosophy](../design/CoreLoop.md#swap-philosophy) (DL-014) rather
  than tying to one specific verb: cooperation expresses companionship, not
  dependency; it amplifies each cat's strengths rather than covering weaknesses;
  trust ("how do these two trust each other here?") is the intended feeling over
  simple task-sequencing ("which cat solves this?"); the partnership itself is the
  hero — neither cat should consistently read as protagonist, assistant, or "the
  useful one"; cooperation should reveal each cat's personality rather than suspend
  it; and the cooperative identity should emerge from recurring patterns, with
  occasional exceptions welcome only when they reinforce rather than redefine the
  partnership. A forward-looking **evaluation lens** for future cooperative
  mechanics is adopted (see [CoreFantasy — Cooperation
  Philosophy](../design/CoreFantasy.md#cooperation-philosophy)).
- **Alternatives:** DCR-003's principle 2 ("elevate strengths rather than reduce
  either character to solving the other's deficiencies") read literally as
  reopening Pillar 2 ("every challenge asks which cat") and the already-locked
  asymmetric ability-lock design (wall-cling gaps, armored-enemy-needs-Tuxedo,
  puzzle co-location — [gameplay/Combat](../design/gameplay/Combat.md),
  [levels/LevelStructure](../design/levels/LevelStructure.md)) — that design *is*
  each cat compensating for what the other can't do. This is the same structural
  ambiguity DL-013 and DL-014 each resolved as a forward-looking lens rather than a
  retroactive rewrite. The author confirmed (2026-08-02) the same resolution here,
  **with one addition not present in DL-013/DL-014's resolutions**: the current
  ability-lock design should be understood as the *present implementation* of this
  philosophy, not immutable doctrine — future playtesting may justify evolving how
  cooperation is expressed mechanically, even though it is not being reopened by
  this DCR itself.
- **Reasoning:** DCR-003 (external design discussion) proposed an emotional
  philosophy for cooperation, paralleling DL-013 and DL-014's structure but scoped
  to cooperation broadly rather than one verb (movement or swap specifically). Most
  of the DCR (companionship over dependency, trust as foundation,
  partnership-as-hero, personality-revealed-not-suspended, consistency-over-
  absolutes) integrated cleanly and reinforces already-canonical material
  ([narrative/Characters — Character
  Invariants](../design/narrative/Characters.md#character-invariants), DL-012;
  [CoreFantasy — Complementary
  philosophy](../design/CoreFantasy.md#complementary-philosophy), DL-009) with new
  emotional framing and an explicit evaluation lens. One principle needed the
  author's clarification before being written into the Design Bible without
  silently overriding Pillar 2, following the same process as DL-013/DL-014.
- **Consequences:** [CoreFantasy](../design/CoreFantasy.md) gains a new
  **Cooperation Philosophy** section (principles + evaluation lens), positioned as
  the general cooperation-emotional lens that [Movement
  Philosophy](../design/gameplay/Movement.md#movement-philosophy) and [Swap
  Philosophy](../design/CoreLoop.md#swap-philosophy) sit underneath, alongside
  [Vision](../design/Vision.md)'s existing "Mechanics serve the bond" principle.
  [Vision](../design/Vision.md), [CoreLoop](../design/CoreLoop.md),
  [gameplay/Movement](../design/gameplay/Movement.md), and
  [narrative/Characters](../design/narrative/Characters.md) gain cross-references.
  No change to `DesignPillars.md`, `Abilities.md`, `Combat.md`,
  `LevelStructure.md`, `production/Roadmap.md`, or `production/Backlog.md` — the
  already-locked asymmetric ability-lock design and mandatory puzzle-room/armored-
  enemy gating are reaffirmed as the current implementation, not reopened. Risk
  flagged and carried forward: this entry's "present implementation, not immutable
  doctrine" framing is softer than DL-013/DL-014's firmer "not reopened" language —
  future DCRs touching the ability-lock design should reconcile against this entry
  rather than treat Pillar 2 as unconditionally fixed.

### DL-014 — Swap Philosophy adopted as an emotional/evaluation lens; existing mandatory gates and asymmetric ability-locks are not reopened (DCR-002)
- **Date:** 2026-07-27 · **Status:** Accepted
- **Decision:** Swapping is canonized as **shared leadership**, not character
  selection: the player is always answering "who should lead this moment?", not
  "which character do I want?" Both cats remain emotionally present regardless of who
  leads (reinforcing the persistent-partner model, DL-002); swapping should feel
  intrinsically good and preserve flow (confidence → continuity → reunion), never
  read as a stop-and-select interruption; [SWAP] stays free of cooldowns/resource
  costs/stamina/usage limits unless prototyping proves otherwise (reinforces DL-008's
  instant control-flip); and expert mastery is conducting one continuous
  movement/personality language rather than switching between two characters — the
  same curve already stated in [gameplay/Movement — Mastery
  curve](../design/gameplay/Movement.md#mastery-curve). A forward-looking
  **evaluation lens** for future swap-driven mechanics is adopted: future asymmetry
  should be character-driven rather than arbitrary, and future mandatory-gate design
  should avoid stacking *additional* mandatory gates beyond the already-locked core
  ones without a clear cooperative payoff.
- **Alternatives:** DCR-002's original §5 ("avoid teaching players Orange solves
  Orange problems / Tuxedo solves Tuxedo problems... rather than isolated gameplay
  locks") and §6 ("avoid excessive mandatory character gates") read literally as
  reopening Pillar 2 ("every challenge asks which cat") and the already-locked
  ability-lock design (wall-cling gaps, armored-enemy-needs-Tuxedo, puzzle
  co-location — [gameplay/Combat](../design/gameplay/Combat.md),
  [levels/LevelStructure](../design/levels/LevelStructure.md)), plus the mandatory
  cooperative gates that are Wave 1–2 Roadmap scope. Rejected as literally written:
  the author confirmed (2026-07-27) both sections are a forward-looking evaluation
  lens for *future* mechanics, not a retroactive rewrite — existing asymmetric
  abilities, armored-enemy gating, and puzzle-room co-location remain exactly as
  designed and Core/locked. This mirrors DL-013's resolution of a structurally
  identical ambiguity in DCR-001.
- **Reasoning:** DCR-002 (external design discussion) proposed a philosophical
  foundation for the swap verb, paralleling DL-013's Movement Philosophy but scoped
  to the swap itself across all four of its expressions (traversal, puzzle, combat,
  NPC/world — [CoreLoop](../design/CoreLoop.md)), not just movement/flow. Most of the
  DCR (shared leadership, partnership persistence, flow preservation, no artificial
  swap costs, conducting-two-personalities mastery) integrated cleanly and mainly
  reinforces already-canonical mechanics (DL-002, DL-008, DL-013,
  [GameRules](../design/GameRules.md) idle-partner invulnerability) with new
  emotional framing. Two sections needed the author's clarification before being
  written into the Design Bible without silently overriding Pillar 2 or the locked
  gating design.
- **Consequences:** [CoreLoop](../design/CoreLoop.md) gains a new **Swap Philosophy**
  section (principles + forward-looking evaluation lens for future swap-driven
  mechanics); [PlayerExperience](../design/PlayerExperience.md) and
  [Vision](../design/Vision.md) cross-reference it; [Glossary](../design/Glossary.md)
  gains a **Leadership** term. No change to `DesignPillars.md`, `Abilities.md`,
  `Combat.md`, `LevelStructure.md`, or `production/Roadmap.md`/`Backlog.md` — the
  already-locked asymmetric ability-lock design and mandatory puzzle-room/armored-
  enemy gating are explicitly reaffirmed, not reopened. Risk flagged and carried
  forward (from the DCR's own risk section, not resolved here): future level/
  encounter design needs discipline to avoid over-relying on new mandatory gates now
  that "opportunity over obligation" is canon guidance, and care that neither cat
  becomes the obvious default leader.

### DL-013 — Movement Philosophy adopted; asymmetry stays a starting-kit given, not reopened (DCR-001)
- **Date:** 2026-07-26 · **Status:** Accepted
- **Decision:** Movement is the default gameplay state — traversal is intrinsically
  enjoyable, not just a means to reach the next puzzle or fight; puzzle rooms and
  combat punctuate movement rather than replace it. Flow is the primary movement
  goal; character is communicated through movement; cooperation emerges through
  movement; separation (if/when solo-cat sections ship) should read as "complete
  individuals," not mechanical incompleteness. Both cats sit on one shared movement
  **foundation** (physics feel, input handling, control architecture), but the
  **already-locked starting kit stays exactly as designed** — Orange's
  Zoomies/Wall-cling vs. Tuxedo's Glide/Loaf remains asymmetric by intent, because
  that asymmetry is what expresses who these two are (Pillar 2 & 4). A design rule
  is adopted for *future* movement mechanics only: a new mechanic must satisfy at
  least two of improve-flow / reinforce-character / strengthen-cooperation, or it's
  challenged before it ships. A beginner→expert mastery curve (control both → know
  when to swap → swap naturally → stop thinking in terms of two characters) is
  adopted as the intended player-skill arc. **Shared Momentum** (preserving
  momentum/trajectory across a swap) remains parked/experimental, not canon —
  reaffirms the alternative already noted in DL-008 and [Ideas](../Ideas.md).
- **Alternatives:** DCR-001's original wording ("both protagonists use the same
  fundamental movement system... never entirely separate movesets") read literally
  as a superseding redesign — it would have reopened Pillar 2 (Asymmetry is the
  game) and the shipped starting kit. Rejected: the author confirmed (2026-07-26)
  the intent was narrower — a shared *foundation* distinct from character-driven
  asymmetry, evaluated going forward, not a retroactive rewrite of what's already
  locked.
- **Reasoning:** DCR-001 (external design discussion) proposed Phase-1 movement
  principles without committing to specific mechanics. Everything except the
  literal "shared movement language" phrasing integrated cleanly with existing
  canon ([DesignPillars](../design/DesignPillars.md) Pillar 1, 4;
  [PlayerExperience](../design/PlayerExperience.md) "Default state = flow"); that one
  phrase needed the author's clarification before it could be written into the
  Design Bible without silently overriding Pillar 2.
- **Consequences:** [design/CoreLoop](../design/CoreLoop.md) states movement as the
  default state; [design/gameplay/Movement](../design/gameplay/Movement.md) gains a
  Movement Philosophy section (principles, future-mechanics design rule, mastery
  curve, Shared Momentum pointer); [design/Vision](../design/Vision.md)
  cross-references it; [design/PlayerExperience](../design/PlayerExperience.md) gets
  a mastery-curve feel target; [Ideas](../Ideas.md)'s Shared Momentum and solo-cat
  entries now point here. No change to `DesignPillars.md`, `Abilities.md`'s starting
  kit, or any prior locked decision (DL-001–DL-012).

### DL-012 — Character Invariants section added to Characters.md
- **Date:** 2026-07-12 · **Status:** Accepted
- **Decision:** [narrative/Characters](../design/narrative/Characters.md#character-invariants)
  gains a new **Character Invariants** section naming the small set of core-identity
  truths for Orange, Tuxedo, and their relationship that are expected to stay stable
  regardless of how mechanics, progression, or narrative structure evolve — explicitly
  separated from the personality tables, art direction, and shared-history sections
  above it, which remain free to change as implementation.
- **Alternatives:** Fold the invariants into the existing "Character philosophy" table
  (rejected — that table is gameplay-facing and gets revised as mechanics are tuned;
  invariants need to read as more durable than that); create a new standalone doc
  (rejected — scope is too small, and it's a summary of content that already lives in
  Characters.md, not new characterization).
- **Reasoning:** Design exploration moved from defining personalities to defining
  enduring character principles. Separating permanent character truths from
  implementation details gives future mechanics, story beats, and animation work a
  stable reference to check against, reducing the risk of personality drift over a
  long production. Content was checked against [CoreFantasy](../design/CoreFantasy.md)
  and [DesignPillars](../design/DesignPillars.md) — the "complement rather than
  oppose" framing matches existing canon, which already uses "opposite" (trait-level,
  Pillar 2, CoreFantasy title) and "complementary" (functional/relational, Vision.md,
  CoreFantasy's "Complementary philosophy" heading) as compatible, not conflicting,
  terms.
- **Consequences:** No change to gameplay scope, Roadmap sequencing, or Backlog —
  documentation-only. Future design discussions should use this list as a quick
  validation check; a proposed change that would violate an invariant should be
  flagged explicitly rather than silently absorbed. Minor wording note carried
  forward, not treated as a contradiction: "Naturally trusts others" (Orange)
  generalizes what Characters.md's existing shared-history section grounds
  specifically as trust *in Tuxedo* — worth revisiting if a future NPC/trust
  mechanic needs the more general claim to hold literally.

### DL-011 — Vision.md rebalanced toward emotional identity, alongside gameplay
- **Date:** 2026-07-12 · **Status:** Accepted
- **Decision:** [Vision](../design/Vision.md) now states the emotional relationship
  between Orange and Tuxedo as co-equal with the gameplay pitch, not subordinate to
  it: the one-line pitch and fantasy section frame the player as experiencing the
  cats' *bond*, not just controlling two characters; new **Emotional goals**,
  **Design philosophy**, and **World direction** sections make the project's
  emotional and creative principles explicit at the north-star level. Outdated
  "lazy tuxedo" personality language is replaced with the terms already established
  in [narrative/Characters](../design/narrative/Characters.md#character-philosophy)
  (Orange: courageous, impulsive, expressive · Tuxedo: observant, thoughtful,
  quietly protective) — also fixing an internal inconsistency where Characters.md's
  own gameplay-facing table still said "Lazy, grumpy" despite its philosophy table
  already using the corrected terms.
- **Alternatives:** Leave Vision.md gameplay-only and let CoreFantasy.md carry all
  emotional framing (rejected — Vision is the doc every new contributor reads
  first, and a mechanics-only pitch undersells the project's actual
  differentiator); rewrite Vision.md wholesale (rejected — incremental edits keep
  existing structure and cross-references intact, per the doc's own conventions).
- **Reasoning:** Recent design discussion established the emotional relationship
  between the two cats as the project's primary differentiator, not a downstream
  consequence of the swap mechanic. Vision.md accurately communicated gameplay but
  underrepresented the emotional experience and creative philosophy that
  [CoreFantasy](../design/CoreFantasy.md) and
  [narrative/Characters](../design/narrative/Characters.md) already treat as
  established canon — this brings the north-star doc in line with what the rest of
  the design bible already assumes.
- **Consequences:** Future design decisions should be evaluated against emotional
  impact as well as gameplay excellence. New "Emotional goals" section
  intentionally stays short/aspirational and defers detailed feel-metrics to
  [PlayerExperience](../design/PlayerExperience.md) (status: Draft, seeded from
  Vision) to avoid duplicating that doc's charter — worth revisiting once
  PlayerExperience's TODOs are filled in, to confirm the two stay in sync. No
  change to Roadmap scope/sequencing; this is a documentation-only update.

### DL-010 — Shared history direction: positive-memory backstory, not tragic exposition
- **Date:** 2026-07-12 · **Status:** Accepted (direction) — delivery mechanics open
- **Decision:** Tuxedo lived independently before Orange entered his life as a very
  young kitten; Tuxedo's initial sense-of-responsibility protection grew into genuine
  companionship, with Orange giving Tuxedo's life new meaning and spontaneity; Orange
  grew up always knowing Tuxedo as his constant companion, which is the origin of his
  trust in Tuxedo and his fear of prolonged separation (already established in
  DL-009). The preferred delivery is **short, optional playable memories** —
  interactive vignettes of everyday warmth and humor (following each other around,
  learning cat behaviors together, shared meals/naps, favorite places, rituals) —
  instead of cinematic flashbacks or tragedy-driven exposition.
- **Alternatives:** Traditional cinematic flashback cutscenes; leaning on a tragic
  origin (loss, danger) to manufacture stakes; leaving the backstory unwritten and
  relying on DL-009's fears without a causal origin.
- **Reasoning:** Emotional investment builds more effectively from positive shared
  experience the player *plays through* than from repeated tragedy or being told
  about the past; this also keeps the two cats' established personalities (DL-009)
  primary — history explains their instincts, it doesn't replace them. Matches
  [CoreFantasy](../design/CoreFantasy.md)'s "behavior before dialogue" principle.
- **Consequences:** [narrative/Characters](../design/narrative/Characters.md#shared-history),
  [narrative/Story](../design/narrative/Story.md#shared-history), and
  [narrative/Timeline](../design/narrative/Timeline.md#before-the-game-begins) now
  carry this backstory as working canon. The **playable-memory system itself is not
  yet scoped** — how memories are discovered in the world, whether any are mandatory,
  what completing one rewards, and how the final memory ties to the ending are open
  (tracked in [Story](../design/narrative/Story.md#open-questions-not-yet-decided--do-not-assume-answers)
  and parked unscoped in [Ideas](../Ideas.md#future-mechanics) — Wave 3+ content,
  no effect on current Wave 0 scope). Risk flagged and carried forward: don't let
  memories become the *sole* explanation for either cat's personality, and don't let
  them disrupt Pillar 1 (Flow first) pacing if overused.

### DL-009 — Character philosophy: complementary instincts, not just complementary stats
- **Date:** 2026-07-12 · **Status:** Accepted
- **Decision:** Orange and Tuxedo each get a core instinct, strength, flaw, and hidden
  fear (act-first/courageous/impulsive/fears separation vs. observe-first/perceptive/
  overthinks/fears failing to protect), a set of shared bonding rituals (grooming,
  co-sleeping, shared meals, competitive play, mutual dislike of water, separation
  anxiety), and a growth arc where each absorbs a small piece of the other's
  perspective without losing their core identity. Characterization is established
  primarily through behavior (idle, environment interaction, rituals), not dialogue.
- **Alternatives:** Leave personality at the gameplay-stat level (chaos/speed vs.
  weight/control) with no deeper internal logic or relationship model; lean on
  dialogue/cutscenes to establish who they are.
- **Reasoning:** Attachment comes from watching consistent, authentic behavior over
  time, not exposition. Giving both the flaw and the fear equal narrative weight (not
  just Orange being "the flawed one") keeps the pair complementary rather than
  hierarchical, matching Pillar 2/4 ([DesignPillars](../design/DesignPillars.md)).
- **Consequences:** Opening-minutes onboarding must carry characterization without
  relying on dialogue (see [../design/PlayerExperience](../design/PlayerExperience.md)).
  Some rituals become interactive mechanics, some stay ambient animation — which is
  which is still open (see [../design/CoreFantasy](../design/CoreFantasy.md) Open
  Questions). Risk: over-indexing on cat stereotypes or repetitive ritual beats — flagged
  as a risk to watch, not solved here. See [../design/narrative/Characters](../design/narrative/Characters.md),
  [../design/CoreFantasy](../design/CoreFantasy.md).

### DL-008 — Instant swap; separate Recall/Toss verb
- **Date:** 2026-07-05 · **Status:** Accepted
- **Decision:** **[SWAP] is an instant control-flip.** The throw/summon idea becomes a
  separate lower-frequency **[RECALL/TOSS]** verb (summon the partner / hurl it).
- **Alternatives:** Swap carries momentum; overload swap with throw/summon.
- **Reasoning:** Flow-first feel; keeping two jobs on two buttons stops the core verb
  from becoming muddy. Momentum-carry parked in [Ideas](../Ideas.md).
- **Consequences:** [RECALL/TOSS] handles repositioning, item hand-off, and
  Tuxedo-as-projectile. See [../design/CoreLoop](../design/CoreLoop.md).

### DL-007 — Progression: found/learnt, never bought; NPCs are core
- **Date:** 2026-07-05 · **Status:** Accepted
- **Decision:** Abilities are **found or learnt from others** (quest/mentor NPCs), never
  bought; cosmetics are bought with currency; **no weapon/gear inventory.**
  Personality-reactive NPCs promoted from "future" to the **core progression driver.**
  Default NPC centrality = **light-touch**.
- **Alternatives:** Buy abilities with currency; gear/weapon inventory; NPCs as pure flavor.
- **Reasoning:** Items must make you feel *more cat*, not like a gear-hero (the guardrail).
- **Consequences:** One ability-grant API for pickup/mentor/quest; fetch-quest-overload is
  a named pitfall. Hub-and-spoke alternative parked. See
  [../design/gameplay/Progression](../design/gameplay/Progression.md), [../design/narrative/NPCs](../design/narrative/NPCs.md).

### DL-006 — Solo-cat levels approved (unscoped)
- **Date:** 2026-07-05 · **Status:** Accepted (future, unscoped)
- **Decision:** Levels where only one cat is present are an approved future direction.
- **Reasoning:** Teaches each kit in isolation; adds difficulty variety; a dramatic
  separation/reunion beat.
- **Consequences:** The controller must support a cat being *absent*, not just inactive.
  See [../design/levels/Tutorial](../design/levels/Tutorial.md), [../Ideas](../Ideas.md).

### DL-005 — Combat is the third swap expression; HP + down/revive replaces lives
- **Date:** 2026-07-05 · **Status:** Accepted
- **Decision:** Combat = "who can hurt this?" — a stun→DPS role-switch. Per-cat HP with
  **downed-not-dead + revive** replaces one-hit lives.
- **Alternatives:** Keep one-hit lives; bolt combat on as a separate mini-game.
- **Reasoning:** Keeps everything under the one swap verb; adds depth and fairness.
- **Consequences:** New to Wave 1 scope. See [../design/gameplay/Combat](../design/gameplay/Combat.md),
  [../design/GameRules](../design/GameRules.md).

### DL-004 — Two cats: Orange + Tuxedo (colors chosen for readability)
- **Date:** 2026-07-05 · **Status:** Accepted
- **Decision:** Orange (chaos/speed, low HP) + Tuxedo (weight/control, high HP).
- **Alternatives:** A grey cat.
- **Reasoning:** Orange + tuxedo = maximum colour **and** silhouette contrast — the player
  must know at a glance which cat they control in a fast game.
- **Consequences:** Drives all character art. See [../design/narrative/Characters](../design/narrative/Characters.md).

### DL-003 — Primary feel: flowing action-platformer with puzzle & boss punctuation
- **Date:** 2026-07-05 · **Status:** Accepted
- **Decision:** Fast, expressive flow by default; puzzles and bosses are deliberate
  punctuation, not walls.
- **Reasoning:** Pillar 1 (Flow first) + Pillar 5 (Punctuation, not walls).
- **Consequences:** The macro level rhythm. See [../design/CoreLoop](../design/CoreLoop.md).

### DL-002 — Persistent-partner relationship model (hybrid follow+teleport)
- **Date:** 2026-07-05 · **Status:** Accepted
- **Decision:** Both cats always exist; you control one. Inactive cat follows + teleport-
  recovers in flow; cooperative co-location in puzzle rooms / arenas.
- **Alternatives:** Freeze/independent partner; pure cooperative-only.
- **Reasoning:** Freeze is too slow for the action feel; cooperative-only is too much
  per-ability design load as the default. Hybrid gets both.
- **Consequences:** Partner never replicates abilities in real time (reconciles position
  afterward). See [../design/gameplay/Movement](../design/gameplay/Movement.md).

### DL-001 — The core verb is the SWAP (one mechanic, four expressions)
- **Date:** 2026-07-05 · **Status:** Accepted
- **Decision:** Traversal, puzzles, combat, and NPC/world gating are all expressions of
  one verb: swapping between the two cats.
- **Reasoning:** Cohesion — the player learns *when to be which cat*, never a separate
  sub-game.
- **Consequences:** Every system is measured against "which cat does this ask for?" See
  [../design/CoreLoop](../design/CoreLoop.md), [../design/DesignPillars](../design/DesignPillars.md).

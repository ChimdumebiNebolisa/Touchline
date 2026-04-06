# Plan

## 1. Status

**Active**

* Step 6: Save/load and career continuity

**Backlog**

* Step 7: Calibration and regression gate

**Blocked**

* None

**Done**

* Step 1: First build slice
* Step 2: Full season loop and board-context model
* Step 3: Transfer and reputation expansion
* Step 4: Academy, loans, and pathway pressure
* Step 5: Shadow world and country-pack scaling

**Dropped**

* deep staff sim
* full scouting micro
* deep finance ledger
* playable youth leagues
* dialogue-heavy media trees
* owner mode
* real licensed clubs and players
* dual-engine match system 

## First build slice

The first build slice must be the smallest end-to-end playable core. It is not setup work. It must let a player take one club, set tactics and lineup, play one match either instantly or in live 2D, then see persistent fallout in morale, board confidence, fan sentiment, and one transfer-related consequence. That is the minimum slice that proves the game’s identity. The workflow file also makes this non-negotiable: only one active step at a time, no moving on before verification, and the first slice must include one playable club, one shipped country pack, the shared match engine, instant sim and live 2D mode, lineup and tactics control, post-match morale/board/fan updates, and at least one transfer, promise, or squad-pressure follow-up consequence.  

## 3. Step 1

**Status**
Done

**Objective**
Build one playable vertical slice with:

* one shipped country content pack
* two deep divisions in the data contract
* one playable club
* shared match engine
* instant sim and live 2D mode
* lineup and tactics control
* post-match perception and morale update
* one surfaced transfer decision affected by reputation or promise context 

**Dependencies**

* PRD frozen for the slice
* Architecture frozen for the slice
* core entity schema
* one country content pack stubbed with believable fictional clubs and players 

**Artifacts to create, edit, or validate**

* `packages/sim-core/src/shared/types.ts`
* `packages/sim-core/src/match/*`
* `packages/sim-core/src/club/morale.ts`
* `packages/sim-core/src/club/board.ts`
* `packages/sim-core/src/club/reputation.ts`
* `packages/sim-core/src/transfers/transferEngine.ts`
* `apps/game-client/src/screens/{SquadScreen,MatchScreen,PostMatchScreen}.tsx`
* first country content JSON
* integration tests for the end-to-end slice
* manual check script for the slice 

**Expected output**
A player can:

1. choose a club
2. set lineup and tactics
3. play or sim a match
4. see stats and event log
5. see morale, board confidence, and fan sentiment change
6. receive one transfer or squad-pressure follow-up event tied to that outcome 

**Verification type**

* integration test
* manual UI check
* sample input/output match log
* typecheck and lint 

**Verification method**

* run one scripted match in instant mode and live mode with the same seed and compare high-level outputs
* confirm post-match state writes to club state
* confirm one negative media or promise context reduces at least two downstream values
* confirm one positive result improves at least two downstream values 

**Exit criteria**

* slice is fully playable end to end
* same engine powers instant and live
* post-match fallout persists
* no role boundary violations
* no placeholder logic in the core loop 

**What must not change**

* single shared match engine
* constrained player agency
* no extra systems beyond slice needs 

## 4. Step 2

**Status**
Done

**Objective**
Extend the slice into a full season loop with fixtures, standings, board expectations, sack risk, and promotion-relegation resolution. 

**Dependencies**

* Step 1 verified 

**Artifacts to create, edit, or validate**

* `packages/sim-core/src/world/*`
* `packages/sim-core/src/club/board.ts`
* `packages/sim-core/src/club/identity.ts`
* season integration tests
* board-context test fixtures 

**Expected output**
A full season can run with contextual board judgment based on club stature, finances, preseason objective, recent form, and derby/cup context. 

**Verification type**

* integration test
* sample season outputs
* calibration check 

**Verification method**

* run full season sims for clubs of different stature
* verify expectation changes are not table-only
* verify sack risk differs for elite vs small clubs under similar form 

**Exit criteria**

* season completes with standings and promotion-relegation
* board decisions visibly react to context
* sackings can happen without breaking save state 

**What must not change**

* board is still separate from manager
* finances stay minimal
* no owner features added 

## 5. Step 3

**Status**
Done

**Objective**
Make transfers human and multi-factor, and wire manager reputation into trust, board patience, transfer pull, and career leverage. 

**Dependencies**

* Step 2 verified 

**Artifacts to create, edit, or validate**

* `packages/sim-core/src/transfers/demandModel.ts`
* `packages/sim-core/src/transfers/negotiation.ts`
* `packages/sim-core/src/club/reputation.ts`
* transfer sample fixtures
* negotiation tests 

**Expected output**
Players and clubs accept or reject based on wages, role, club stature, manager reputation, development path, playing time promise, squad competition, and project fit. 

**Verification type**

* unit test
* integration test
* sample negotiation logs 

**Verification method**

* prove two equal-fee offers can produce different outcomes because of project fit or playing time
* prove low-reputation manager loses deals more often than high-reputation manager in comparable context
* prove broken promises lower future transfer trust 

**Exit criteria**

* transfer market is no longer fee-only
* sporting director remains primary executor
* reputation affects at least three systems 

**What must not change**

* manager cannot directly bypass board budget or wage structure
* no deep contract clause sprawl 

## 6. Step 4

**Status**
Done

**Objective**
Add light but real academy behavior, rare meaningful youth intake, loans, and pathway friction with first-team pressure. 

**Dependencies**

* Step 3 verified 

**Artifacts to create, edit, or validate**

* `packages/sim-core/src/academy/*`
* youth calibration tests
* loan-path sample fixtures 

**Expected output**
Each club has academy quality and pathway bias. Youth arrives annually, elite prospects are rare, loans matter, and youth use interacts with board expectations and squad congestion. 

**Verification type**

* batch calibration
* integration test
* sample pathway logs 

**Verification method**

* run long-run intake simulations
* check elite prospect frequency
* verify blocked pathways increase transfer pressure or loan usage
* verify clubs with stronger academy identity produce more usable youth over time 

**Exit criteria**

* youth quality is rare and meaningful
* loans are part of the real pathway
* academy affects at least finances, transfers, and identity 

**What must not change**

* no playable youth leagues
* no deep academy staffing sim 

## 7. Step 5

**Status**
Done

**Objective**
Add shadow leagues and make content packs scale to additional countries without architecture changes. 

**Dependencies**

* Step 4 verified 

**Artifacts to create, edit, or validate**

* `packages/sim-core/src/world/shadowLeagues.ts`
* content schema validators
* second country pack smoke test 

**Expected output**
Other leagues generate believable transfer and loan context without deep simulation cost. 

**Verification type**

* integration test
* sample world-state output
* content validation 

**Verification method**

* verify shadow leagues create transfer targets, loan destinations, and promotions-relegations
* verify adding a second country pack requires content only, not core rewrites 

**Exit criteria**

* shadow world supports market and career continuity
* top-two-deep / rest-shadow contract holds 

**What must not change**

* no deep sim outside declared top two divisions
* no backend introduced to solve content scaling 

## 8. Step 6

**Status**
Active

**Objective**
Persist long saves, manager reputation history, sackings, and future job leverage. 

**Dependencies**

* Step 5 verified 

**Artifacts to create, edit, or validate**

* `packages/save/*`
* serialization tests
* multi-season manual check results 

**Expected output**
The player can save, reload, continue career, and carry reputation and relationship history forward. 

**Verification type**

* integration test
* manual UI check 

**Verification method**

* save before a major event, reload, and verify deterministic continuation
* verify sackings and job offers respect reputation history 

**Exit criteria**

* stable multi-season save/load
* no data loss in morale, promises, or reputation state 

**What must not change**

* local-first persistence
* no cloud-service dependency for v1 

## 9. Step 7

**Status**
Backlog

**Objective**
Lock the game against drift, random-feeling outcomes, and scope creep. The workflow explicitly requires the plan to end with a calibration and regression gate.  

**Dependencies**

* Steps 1 through 6 verified 

**Artifacts to create, edit, or validate**

* calibration suite
* regression suite
* design rule checklist
* final verification report 

**Expected output**
A release candidate with stable systemic behavior and evidence artifacts for all must-have flows. 

**Verification type**

* benchmark
* regression tests
* manual QA report 

**Verification method**

* batch sim matches to compare goal rates, upset rates, red-card impact, late-goal distribution, and youth rarity
* rerun end-to-end core flow after every balancing pass
* verify no dropped guardrail 

**Exit criteria**

* all must-have PRD items complete
* core flows have evidence artifacts
* no active blockers
* no unresolved guardrail violations
* system still matches PRD and Architecture 

**What must not change**

* core loop
* role boundaries
* one-engine rule
* ruthless scope control 

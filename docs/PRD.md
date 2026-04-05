# PRD

## 1. Positioning and core fantasy

This is a believable, scoped football management sim built around a **club ecosystem**, not a total football life sim. The fantasy is **constrained agency under pressure**. The player is a manager or head coach operating inside a real club hierarchy, trying to survive and succeed through tactics, matchday choices, morale, promises, media pressure, transfer influence, youth pathways, and board patience. The emotional center is the match engine. The strategic center is handling the second-order effects of results.  

## 2. What the app is

A single-player football management sim centered on one playable club at a time.

The game uses:

* real countries
* fictional clubs and players
* top 2 divisions per shipped country as deep simulations
* other leagues as shadow simulations
* one shared match engine for instant sim and live 2D watch mode
* persistent systems for morale, trust, fan sentiment, media tone, board confidence, manager reputation, transfers, and youth pathway

The core loop is:

world state -> season loop -> match engine -> perception and pressure -> transfers and squad planning

Youth and finances feed that loop. They do not become separate management games.  

## 3. What the app is not

* Not an owner sim
* Not a full football life sim
* Not a Football Manager clone with endless micro
* Not a detailed accounting sim
* Not a full staffing sim
* Not a scouting spreadsheet simulator
* Not a dialogue-heavy narrative game
* Not a god-role where coach, sporting director, board, and owner collapse into one actor
* Not a youth spam machine where elite prospects appear constantly
* Not a transfer market where fee alone decides outcomes  

## 4. Primary actor

Manager or head coach of one club. 

## 5. Optional secondary actor

Sporting director AI and board AI as persistent constraint actors.

They are not cosmetic. They must be able to block, redirect, or narrow the player’s choices in believable ways.  

## 6. Main job to be done

Build and manage a functioning club ecosystem through a season by:

* setting tactics, lineups, substitutions, and matchday adjustments
* managing media and sporting discipline
* influencing transfers without controlling everything
* balancing short-term survival or success against morale, reputation, youth pathways, and board patience
* working within financial and structural limits set by the club hierarchy 

## 7. Must-have v1 capabilities

### 7.1 Club-world structure

* Real countries, fictional clubs
* Two deeply simulated divisions per shipped country
* Other leagues run as shadow simulations for transfer supply, loan destinations, promotion-relegation continuity, and world plausibility
* Must affect transfers, loans, budgets, expectations, and career movement  

### 7.2 One shared match engine

* Same underlying engine powers instant sim and live 2D watch mode
* Match model must account for chance quality, tactical shape, pressing, fatigue, cards, injuries, substitutions, and variance
* Must affect results, morale, injuries, board perception, fan sentiment, and reputation
* Hidden information is allowed, but outcomes must remain explainable after the fact  

### 7.3 Tactical and matchday control

* Player controls formation, roles, block height, pressing intensity, width, tempo, risk, lineup, and substitutions
* Tactical choices must trade off chance quality, compactness, fatigue, and injury risk
* Must affect match output, player trust, morale, and board judgment of style  

### 7.4 Perception and pressure system

* Persistent board confidence, fan sentiment, media tone, and manager reputation
* Board expectations must react to context, not just league position
* Media and fan pressure must be able to create negativity loops and patience buffers
* Must affect sack risk, morale, transfer pull, and future job leverage  

### 7.5 Morale, trust, promises, and discipline

* Per-player morale and trust in manager
* Team cohesion as a real system
* Explicit playing-time and role promises with time windows
* Discipline actions controlled by the player in sporting terms only
* Must affect on-pitch performance, dressing-room stability, transfer unrest, and reputation  

### 7.6 Transfer system with constrained agency

* Sporting director and board remain real actors
* Player sets needs, requests profiles, reviews targets, and has meaningful but partial influence
* Transfer success depends on wages, club stature, manager reputation, development path, playing-time promise, squad competition, project fit, and culture/lifestyle factors
* Must affect squad quality, morale, finances, youth blockage, and future reputation
* Transfers may not resolve on fee alone  

### 7.7 Manager reputation

* Reputation affects player trust, board patience, transfer pull, and future career leverage
* Must affect morale recovery, transfer acceptance, job security, and power inside the club hierarchy
* Reputation must move from results and perception, not from arbitrary scripted events  

### 7.8 Light but real academy and loans

* One academy quality value per club in v1
* One pathway bias value per club in v1
* Rare meaningful youth intake
* Pathway is academy -> loan or development minutes -> first team
* Must affect squad planning, transfer spending, club identity, fan perception, and homegrown value
* Youth quality must be rare and meaningful, not spammed  

### 7.9 Minimal but meaningful finances

* Bank balance
* Wage budget
* Transfer budget
* Board spending posture
* No full ledger
* Financial state must constrain transfers, board patience, and academy reliance
* Must affect recruitment, expectations, youth use, and job pressure  

### 7.10 Club identity and context-sensitive expectations

* Clubs have identity tags such as youth-focused, pragmatic, ambitious, possession-oriented, survival-focused
* Identity shapes how fans and board judge results and style
* Must affect pressure, transfer pitch, youth expectations, and media framing
* Over-performance and under-performance must be judged relative to club context, not generic table rules  

## 8. Should-have after core is verified

* Country-specific promotion and relegation detail beyond the first shipped country
* Simplified homegrown registration rules
* Manager vs head coach mode presets by club structure
* Assistant and physio influence as light staff systems
* Career hiring market after sackings and over-performance
* Light philosophy friction with sporting director
* More variation in media event types

These are supported by the docs, but they are not the first slice.  

## 9. Nice-to-have later

* More country packs
* More detailed contract terms
* More academy infrastructure detail
* More tactical presets and set-piece depth
* More media flavor
* Broader career progression layers

These can wait because none of them define the identity of v1. 

## 10. Out of scope for v1

* Full owner control
* Deep staff hiring tree
* Detailed scouting department simulation
* Playable youth teams
* Full training calendar micro
* Full finance ledger and commercial sim
* Rich social feed or dialogue-tree media system
* Real clubs and real-player licenses
* Multi-club control
* Full player family sim beyond transfer weighting
* Deep legal contract clauses, image-rights detail, or law-heavy negotiation systems 

## 11. Acceptance criteria

v1 is acceptable only if all of these are true:

* The player can start as manager or head coach of one club and play through a believable season loop
* Every match can be run either as instant sim or watched in live 2D mode using the same engine
* Match outcomes visibly reflect tactics, chance quality, fatigue, cards, injuries, substitutions, and variance
* Board confidence, fan sentiment, media tone, morale, and manager reputation persist across matches and create second-order effects
* A public comment, broken promise, or disciplinary action can materially affect at least two of: morale, fan sentiment, board confidence, transfer willingness, or sack risk
* Transfer acceptance is not fee-only and uses the required human factors
* Sporting director and board can block or redirect the player
* Youth intake is rare enough to feel meaningful and loans matter as a pathway
* Finances constrain recruitment without turning into bookkeeping
* The game never gives the player owner-level power
* Every major system affects at least two other systems
* Outcomes can be explained after the fact and never feel like random nonsense  

## 12. Constraints

* One playable club at a time
* Real countries, fictional clubs
* Deep simulation applies only to top 2 divisions of shipped countries
* Other leagues are lighter shadow simulations
* Match engine is the emotional center
* Constrained agency is part of the fantasy
* Hidden information is allowed, but outcomes must remain explainable
* Every major system must affect at least two others
* If a feature does not support the core loop, cut it
* Board expectations may not resolve on league position alone
* Manager permissions may not include owner powers  

## 13. Assumptions

* v1 ships with one fully authored country first
* Architecture must support more countries later without redesign
* v1 uses fictional clubs and players to avoid licensing dependency
* v1 is single-player
* v1 is local-first
* Homegrown rules can ship as a simplified shared rule layer first, then expand later
* The first slice should prove the game’s identity early, not spend time on setup theater  

## 14. Blocked unknowns that affect implementation

No hard blocker is exposed by the source docs right now.

Open decisions with defaults:

* **First shipped country:** default to one country content pack first
* **Exact homegrown-rule detail by country:** default to a simplified shared rule layer first
* **Initial content volume per division:** default to believable full division sizes in the shipped country, not prototype mini-leagues

If any of those decisions start changing system boundaries or scope, stop and update the artifact before coding. 

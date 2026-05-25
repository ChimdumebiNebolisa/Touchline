# Touchline Master Design Decisions

## Purpose

This document captures the current design direction for **Touchline**, a fictional club-football career and management simulator. It records the decisions already made and proposes firm structures for the remaining systems.

Touchline is not an arcade football game. It is a serious football-career simulator with a tactical match engine, club politics, player personalities, partial information, licensing, staff influence, transfers, media pressure, and career progression.

## Core Game Identity

Touchline is a fictional club-football career simulator where the user starts at a chosen club as an **Assistant Manager**, **Head Coach**, or **Manager**. The game focuses on tactical identity, partial player knowledge, licensing progression, staff influence, transfers, player personalities, board and fan pressure, dynamic development, and match outcomes shown through instant simulation or tactical live playback.

The tone should be:

- 70 to 80 percent serious and realistic football world
- 20 to 30 percent dramatic but believable football politics

Drama should come from football logic: agent leaks, board pressure, Director of Football conflict, fan unrest, player frustration, transfer hijacks, media narratives, and interim-job uncertainty.

Drama should not become random nonsense, constant scandals, or unrealistic betrayal spam.

---

# 1. Roles

## Locked Roles

Touchline has three playable roles:

1. Assistant Manager
2. Head Coach
3. Manager

Role selection is not cosmetic. It determines authority, information access, pressure, job risk, and career path.

## Role Hierarchy

Inside Touchline:

- **Manager** = highest playable football authority
- **Head Coach** = first-team football authority with recruitment limits
- **Assistant Manager** = support role with low authority but easier access to bigger clubs

## Assistant Manager

### Core Fantasy

“I am not in charge yet, but I am close enough to influence the club and earn my chance.”

### Authority

Can influence:

- Lineup suggestions
- Tactical suggestions
- Training recommendations
- Opposition reports
- Player development notes
- Player morale reports
- Substitution suggestions
- Youth player recommendations

Cannot control:

- Final lineup
- Final tactics
- Transfers
- Contracts
- Staff hiring
- Press strategy
- Board objectives
- Director of Football decisions

### Main Systems

Assistant Manager gameplay is built around influence.

Influence increases through:

- Accurate tactical suggestions
- Useful training recommendations
- Strong player relationships
- Good opposition analysis
- Supporting the Manager effectively
- Building trust with players, staff, and the board

Influence can unlock:

- More tactical input
- More training input
- Being asked for lineup advice
- Being allowed to handle cup matches
- Being trusted with internal reports
- Interim Manager consideration
- License sponsorship

### Career Path

Assistant Manager can bypass normal license limits at bigger clubs. This creates a hidden route:

Assistant Manager at bigger club -> Manager leaves or gets sacked -> Caretaker role -> Interim Manager -> Permanent Manager if performance is strong.

This path should only trigger when:

- The Manager is sacked due to bad performance
- The Manager leaves the club

## Head Coach

### Core Fantasy

“I control the football on the pitch, but I must work within the club structure.”

### Authority

Can control:

- Lineups
- Formations
- Tactical style
- Player roles
- Training focus
- Match preparation
- Substitutions
- Player development plans
- Dressing room management
- Press comments about football matters

Can influence, but not fully control:

- Transfers
- Contracts
- Player sales
- Staff changes
- Youth promotions
- Scouting priorities

Cannot fully control:

- Final transfer fees
- Final wage structure
- Director of Football decisions
- Board budget
- Club ownership strategy
- Major staff hiring

### Control Growth

Head Coach control can expand through trust, not direct negotiation.

Trust is affected by:

- Board relationship
- Dressing room control
- Media handling
- Transfer judgment
- Tactical consistency
- Professionalism
- Meeting club philosophy
- Avoiding unnecessary conflict

High trust can lead to more influence over:

- Transfer shortlists
- Player sales
- Staff recommendations
- Youth promotions
- Training structure
- Contract priorities

## Manager

### Core Fantasy

“I run the football project, but the board still owns the club.”

### Authority

Can control:

- Lineups
- Tactics
- Training
- Transfers within limits
- Contracts within limits
- Wage offers within wage structure
- Squad role promises
- Player sales input
- Scouting priorities
- Staff recommendations
- Long-term squad planning
- Football identity
- Media direction

Can challenge:

- Director of Football decisions
- Board transfer decisions
- Budget restrictions
- Forced player sales
- Club philosophy conflicts

Cannot control:

- Club ownership
- Board membership
- Director of Football hiring or firing
- Absolute budget limits
- Executive-level club decisions

### Key Rule

More power means more blame.

- Assistant Manager: low power, lower blame, high opportunity through influence
- Head Coach: medium power, high blame for football performance
- Manager: high power, highest blame for the football project

---

# 2. Licenses

## License Ladder

Touchline uses five licenses:

1. Grassroots License
2. National C License
3. National B License
4. National A License
5. Pro License

## What Licenses Affect

Licenses affect:

- Job eligibility
- Club trust
- Board confidence
- Player respect
- Tactical report depth
- Scouting information quality
- Staff information quality
- Media credibility
- Access to bigger roles

## License Requirement Logic

A club’s license requirement depends on a combination of:

1. Club reputation
2. League level
3. Budget and squad quality
4. Media pressure
5. Fan expectations
6. Board ambition

Strongest factors:

- Reputation
- League level

Weakest factor:

- Board ambition

A desperate board can gamble on an underqualified user, especially through the Assistant Manager or Interim Manager route.

## License Upgrade Methods

Licenses can be upgraded through:

- Board sponsorship
- Self-funded course
- Federation invitation
- Club recommendation
- Performance-based opportunity
- Assistant Manager pathway

## License Rule

A license qualifies the user for bigger rooms. It does not replace reputation.

A Pro License with poor reputation should not automatically lead to elite jobs.

---

# 3. Manager Backgrounds

Touchline uses seven manager backgrounds.

## Background List

1. Former Club Legend
2. Unknown Upstart
3. Assistant Manager Promotion
4. Youth Academy Coach
5. Former Player
6. Tactical Specialist
7. Crisis Interim

## Former Club Legend

- Reputation: high at chosen club, medium elsewhere
- Starting license: National B or National A
- Board trust: medium to high
- Fan trust: very high
- Player respect: high
- Media pressure: high
- Job security: medium

Main effect:

Fans forgive early mistakes more than usual, but expectations are inflated.

## Unknown Upstart

- Reputation: low
- Starting license: Grassroots or National C
- Board trust: low to neutral
- Fan trust: low
- Player respect: low
- Media pressure: low
- Job security: medium due to lower expectations

Main effect:

Harder to get big jobs and attract players, but overachievement gives bigger reputation gains.

## Assistant Manager Promotion

- Reputation: medium internally, low externally
- Starting license: National C or National B
- Board trust: medium
- Fan trust: neutral
- Player respect: medium depending on relationships
- Media pressure: medium
- Job security: medium to low if club is unstable

Main effect:

The user understands the squad better than an outsider, but fans and media may question readiness.

## Youth Academy Coach

- Reputation: low to medium
- Starting license: National C or National B
- Board trust: medium at youth-focused clubs
- Fan trust: neutral
- Player respect: high from academy players, lower from senior stars
- Media pressure: low to medium
- Job security: better at youth-development clubs, worse at win-now clubs

Main effect:

Youth players develop better and trust the user more, but senior players may take longer to respect the user.

## Former Player

- Reputation: medium
- Starting license: National C or National B
- Board trust: neutral
- Fan trust: medium
- Player respect: medium to high
- Media pressure: medium
- Job security: medium

Main effect:

Players listen earlier, but tactical credibility must be proven.

## Tactical Specialist

- Reputation: medium
- Starting license: National B or National A
- Board trust: medium
- Fan trust: neutral
- Player respect: medium
- Media pressure: medium
- Job security: medium

Main effect:

Better tactical-fit reports, better match-analysis explanations, faster tactical familiarity growth.

Weakness:

Less natural charisma, weaker player pull, and less patience from fans if style is boring.

## Crisis Interim

- Reputation: low to medium
- Starting license: flexible depending on club desperation
- Board trust: low
- Fan trust: depends on club mood
- Player respect: neutral to low
- Media pressure: high
- Job security: very low

Main effect:

Access to a job the user may not normally qualify for, but with immediate pressure.

---

# 4. Club Archetypes

Touchline uses ten club archetypes.

## Archetype List

1. Title Contender
2. Fallen Giant
3. Mid-table Stabilizer
4. Relegation Fighter
5. Youth Academy Club
6. Selling Club
7. Financially Restricted Club
8. Ambitious New-Money Club
9. Chaotic Club
10. Community Club

## Title Contender

- Reputation: high
- Budget: high
- Board patience: low
- Fan expectations: very high
- Pressure: very high
- License requirement: high
- Best role fit: Manager, Head Coach

Core feel:

The user is expected to win immediately.

## Fallen Giant

- Reputation: high but declining
- Budget: medium
- Board patience: low to medium
- Fan expectations: high
- Pressure: high
- License requirement: medium to high
- Best role fit: Manager, Head Coach

Core feel:

The club still thinks it is bigger than it currently is.

## Mid-table Stabilizer

- Reputation: medium
- Budget: medium
- Board patience: medium
- Fan expectations: medium
- Pressure: medium
- License requirement: medium
- Best role fit: Head Coach, Manager

Core feel:

Safe, but not special yet.

## Relegation Fighter

- Reputation: low to medium
- Budget: low
- Board patience: low
- Fan expectations: survival
- Pressure: high
- License requirement: low to medium
- Best role fit: Head Coach, Crisis Interim, Assistant Manager path

Core feel:

Every point matters.

## Youth Academy Club

- Reputation: low to medium
- Budget: low to medium
- Board patience: medium to high
- Fan expectations: development and identity
- Pressure: medium
- License requirement: low to medium
- Best role fit: Head Coach, Manager, Youth Academy Coach background

Core feel:

Build through young players.

## Selling Club

- Reputation: medium
- Budget: medium but unstable
- Board patience: medium
- Fan expectations: smart replacement
- Pressure: medium to high
- License requirement: medium
- Best role fit: Manager

Core feel:

The best players may always be for sale.

## Financially Restricted Club

- Reputation: low to medium
- Budget: very low
- Board patience: medium
- Fan expectations: realistic
- Pressure: medium
- License requirement: low
- Best role fit: Manager, Head Coach, Unknown Upstart

Core feel:

The user must be smarter than richer clubs.

## Ambitious New-Money Club

- Reputation: medium and rising
- Budget: high
- Board patience: low to medium
- Fan expectations: rising quickly
- Pressure: high
- License requirement: medium to high
- Best role fit: Manager

Core feel:

The club wants to become elite fast.

## Chaotic Club

- Reputation: variable
- Budget: variable
- Board patience: very low
- Fan expectations: unstable
- Pressure: very high
- License requirement: flexible
- Best role fit: Crisis Interim, Manager, Assistant Manager hidden path

Core feel:

The football is only half the problem.

## Community Club

- Reputation: low to medium
- Budget: low
- Board patience: high if values are respected
- Fan expectations: identity and loyalty
- Pressure: low to medium
- License requirement: low
- Best role fit: Head Coach, Manager, Unknown Upstart

Core feel:

The club has strong values and local identity.

---

# 5. Board Philosophy

Board philosophy answers:

“What does ownership reward, tolerate, and punish?”

## Board Philosophy List

1. Win-Now Board
2. Patient Long-Term Board
3. Financially Strict Board
4. Youth Development Board
5. Commercial Growth Board
6. Data-Driven Board
7. Traditionalist Board
8. Trigger-Happy Board

## Win-Now Board

Rewards:

- Winning runs
- Trophies
- Experienced players
- Big signings
- Strong league position

Punishes:

- Slow rebuilds
- Development excuses
- Too many young players
- Experimental tactics
- Long losing streaks

## Patient Long-Term Board

Rewards:

- Clear tactical identity
- Player development
- Squad stability
- Smart recruitment
- Financial control

Punishes:

- Panic spending
- Constant tactical changes
- Broken promises
- Dressing-room chaos

## Financially Strict Board

Rewards:

- Profit
- Wage control
- Selling at the right time
- Loans and free transfers
- Low-risk contracts

Punishes:

- Expensive wages
- Older players with no resale value
- Big transfer risks
- Financial losses

## Youth Development Board

Rewards:

- Academy minutes
- Youth promotions
- Player development
- Long-term squad planning
- Selling developed players for profit

Punishes:

- Blocking youth with veterans
- Ignoring academy players
- Short-term signings
- Poor development plans

## Commercial Growth Board

Rewards:

- Star signings
- High-profile managers
- Attractive football
- Media attention
- Marketable players
- Growing reputation

Punishes:

- Boring football
- Low-profile recruitment
- Selling popular stars
- Embarrassing media moments

## Data-Driven Board

Rewards:

- Efficient transfers
- Strong analytics
- Good wage-to-output value
- Tactical consistency
- Smart wage structure

Punishes:

- Emotional signings
- Overpaying for reputation
- Ignoring analysis
- Poor value transfers

## Traditionalist Board

Rewards:

- Club identity
- Loyal players
- Local or academy talent
- Disciplined squads
- Stable leadership

Punishes:

- Radical changes
- Selling beloved players
- Ignoring tradition
- Disrespecting club legends

## Trigger-Happy Board

Rewards:

- Immediate results
- Public confidence
- Quick fixes
- Media control
- Visible changes after poor form

Punishes:

- Bad runs
- Fan anger
- Media criticism
- Internal conflict
- Slow explanations

---

# 6. Fan Culture

Fan culture decides what supporters emotionally react to.

## Fan Culture List

1. Results First
2. Attacking Football
3. Defensive Grit
4. Academy Loyalists
5. Star Power Fans
6. Anti-Selling Fans
7. Derby Obsessed
8. Underdog Loyalists
9. Traditional Identity Fans

## Results First

Fans care mostly about winning. Style matters less.

## Attacking Football

Fans want bold, exciting, high-tempo football.

A 3-2 win may excite them more than a safe 1-0. A boring 0-0 may hurt fan morale even if the board is calm.

## Defensive Grit

Fans value discipline, toughness, clean sheets, and effort.

## Academy Loyalists

Fans want the club to trust its own young players.

## Star Power Fans

Fans see big names as ambition.

## Anti-Selling Fans

Fans hate losing key players and repeated profit-first sales.

## Derby Obsessed

Rivalry matches define the season.

A derby loss can hurt fan morale more than a normal loss. A derby win can rescue a poor run temporarily.

## Underdog Loyalists

Fans value effort, honesty, smart recruitment, and survival.

## Traditional Identity Fans

Fans care about history, loyalty, recognizable style, and club culture.

## Board-Fan Conflict Rule

Board approval and fan approval should often disagree.

Example:

Selling a star at a Financially Strict Board may raise board morale while crushing Anti-Selling fan morale.

---

# 7. Director of Football Styles

The Director of Football is a powerful club figure who sits between the user, board, transfers, scouting, and squad planning.

## Director Style List

1. Talent Trader
2. Star Chaser
3. Academy Builder
4. Data Operator
5. Bargain Hunter
6. Control Freak
7. Club Loyalist
8. Political Survivor

## Talent Trader

Belief:

Buy young, develop, sell high.

Conflict example:

The user wants to keep a 22-year-old star. The Director wants to sell because the offer is above market value.

## Star Chaser

Belief:

Big names move the club forward.

Conflict example:

The user wants a tactically perfect unknown midfielder. The Director pushes a famous striker instead.

## Academy Builder

Belief:

The club should produce its own players.

Conflict example:

The user wants an experienced fullback. The Director wants to promote a 19-year-old academy player.

## Data Operator

Belief:

Every decision should be supported by evidence.

Conflict example:

The user wants a popular former player. The Director blocks it because decline risk is too high.

## Bargain Hunter

Belief:

Value matters more than status.

Conflict example:

The user wants a proven starter. The Director proposes cheaper rotation options.

## Control Freak

Belief:

Recruitment should run through me.

Conflict example:

The user shortlists three players. The Director signs someone else and expects the user to make it work.

## Club Loyalist

Belief:

The club’s identity matters.

Conflict example:

The user wants to sell an aging captain. The Director warns that the dressing room and fans may turn.

## Political Survivor

Belief:

Stay close to the board and protect my position.

Conflict example:

A signing fails. The Director quietly frames it as the user’s tactical misuse.

## Director Relationship States

Use:

- Ally
- Supportive
- Neutral
- Tense
- Hostile

Relationship affects:

- Transfer cooperation
- Scouting access
- Board reports
- Media leaks
- Staff support
- Player sales
- Contract flexibility

---

# 8. Staff Roles

Staff should affect information quality, training, development, risk control, scouting, morale, media, and tactical understanding.

## Staff Role List

1. Assistant Manager
2. First-Team Coach
3. Goalkeeping Coach
4. Fitness Coach
5. Physio
6. Youth Coach
7. Scout
8. Head of Recruitment
9. Data Analyst
10. Media Officer

## Assistant Manager

Affects:

- Lineup suggestions
- Tactical advice
- Player mood reports
- Opposition analysis
- Training feedback
- Dressing-room insight

Ratings:

- Tactical Knowledge
- Man Management
- Player Judgment
- Opposition Analysis
- Communication
- Loyalty

## First-Team Coach

Affects:

- Training quality
- Tactical familiarity
- Player development
- Role familiarity
- Team cohesion

Ratings:

- Coaching
- Tactical Training
- Technical Development
- Motivation
- Adaptability

## Goalkeeping Coach

Affects:

- Goalkeeper development
- Shot-stopping growth
- Positioning growth
- Keeper confidence
- Set-piece defense

Ratings:

- Goalkeeping Coaching
- Positioning
- Reflex Training
- Mental Coaching
- Youth Keeper Development

## Fitness Coach

Affects:

- Stamina
- Match sharpness
- Training intensity
- Late-game performance
- Fatigue recovery
- Pressing sustainability

Ratings:

- Fitness Training
- Conditioning
- Workload Management
- Recovery Planning
- Injury Prevention

## Physio

Affects:

- Injury recovery time
- Injury-risk estimates
- Medical reports
- Return-to-play confidence
- Long-term injury warnings

Ratings:

- Medical Knowledge
- Rehabilitation
- Injury Diagnosis
- Risk Assessment
- Recovery Planning

## Youth Coach

Affects:

- Youth development
- Academy reports
- Potential discovery
- Young player morale
- Transition to first team

Ratings:

- Youth Development
- Potential Judgment
- Mentoring
- Technical Coaching
- Patience

## Scout

Affects:

- Player reports
- Hidden stat discovery
- Personality clues
- Tactical fit reports
- Potential accuracy
- Transfer-risk warnings

Ratings:

- Scouting Accuracy
- Potential Judgment
- Personality Judgment
- Tactical Fit Judgment
- Market Knowledge
- Reliability

## Head of Recruitment

Affects:

- Shortlists
- Transfer priorities
- Squad planning
- Replacement planning
- Market timing
- Negotiation preparation

Ratings:

- Recruitment Strategy
- Negotiation Support
- Market Knowledge
- Squad Planning
- Value Judgment
- Board Communication

## Data Analyst

Affects:

- Post-match reports
- Tactical diagnosis
- Player trend analysis
- Team weakness detection
- Opponent analysis
- Transfer value assessment

Ratings:

- Data Analysis
- Tactical Interpretation
- Trend Detection
- Player Evaluation
- Communication

## Media Officer

Affects:

- Press conference advice
- Media risk warnings
- Fan reaction management
- Player controversy handling
- Narrative control
- Reputation protection

Ratings:

- Media Handling
- Crisis Management
- Fan Understanding
- Communication
- Reputation Management

## Shared Staff Traits

Every staff member can also have:

- Reputation
- Loyalty
- Ambition
- Personality
- Preferred Style
- Relationship With User
- Relationship With Board
- Relationship With Players
- Pressure Handling
- Adaptability

## Role-Based Staff Access

Assistant Manager:

- Receives limited staff reports
- Cannot hire or fire staff
- Cannot control scouting or recruitment structures

Head Coach:

- Sees most first-team staff reports
- Can request staff changes
- Cannot freely hire or fire major staff

Manager:

- Sees full football-department reports
- Can strongly influence staff upgrades
- Cannot fire the Director of Football
- Still needs board approval for major staff decisions

---

# 9. Player Identity

Players should feel like footballers with style, personality, uncertainty, development, relationships, and tactical fit.

## Player Identity Layers

1. Ability
2. Knowledge state
3. Playing style
4. Tendencies
5. Traits
6. Personality
7. Tactical fit
8. Current context
9. Development curve
10. Player-manager alignment

## Ability

Core outfield attributes:

- Pace
- Shooting
- Passing
- Dribbling
- Defending
- Physicality
- Stamina
- Composure
- Work Rate
- Positioning

Goalkeeper attributes:

- Reflexes
- Handling
- Positioning
- Distribution
- One-on-Ones
- Command of Area
- Composure

Important distinction:

- True Ability = what the player actually is
- Known Ability = what the user currently knows
- Match Performance = how the player performs today after form, morale, fitness, tactical fit, and pressure

## Knowledge State

The game should use scouting knowledge instead of calling it hidden information.

Each attribute can be:

- Known
- Estimated
- Unknown

Examples:

- Pace: 88
- Passing: 72 to 78
- Defending: ?

Profile confidence:

- Low Confidence
- Medium Confidence
- High Confidence
- Fully Scouted

## Playing Styles

Examples:

- Direct Winger
- Inverted Winger
- Target Forward
- Poacher
- False Nine
- Box-to-Box Midfielder
- Deep Playmaker
- Ball-Winning Midfielder
- Progressive Fullback
- Defensive Fullback
- Ball-Playing Center Back
- No-Nonsense Center Back
- Sweeper Keeper
- Shot-Stopper

## Tendencies

Examples:

- Cuts inside often
- Shoots early
- Holds position
- Dribbles under pressure
- Avoids risky passes
- Attempts through balls
- Tracks back aggressively
- Presses often
- Stays wide
- Drops deep
- Makes late box runs
- Commits tactical fouls

## Traits

Examples:

- Big-Game Player
- Injury Prone
- Late Bloomer
- Consistent Performer
- Confidence Player
- Leader
- Hot-Headed
- Press Resistant
- Clutch Finisher
- Poor Trainer
- Adaptable
- One-Footed
- Set-Piece Specialist
- High Ceiling
- Early Plateau Risk

## Personality

Examples:

- Professional
- Ambitious
- Loyal
- Emotional
- Quiet
- Ego-Driven
- Team-First
- Volatile
- Resilient
- Complacent

Personality affects:

- Training attitude
- Reaction to being benched
- Contract demands
- Transfer interest
- Morale swings
- Response to criticism
- Relationship with manager
- Squad influence

## Tactical Fit

Tactical fit should be described in scouting language, not only numbers.

Example:

“Strong fit for direct counterattacking systems. He attacks space early and carries the ball aggressively.”

## Current Context

Temporary player state includes:

- Form
- Morale
- Fitness
- Fatigue
- Injury risk
- Sharpness
- Playing-time happiness
- Contract happiness
- Manager relationship
- Squad status
- Media pressure
- Transfer rumors
- Tactical familiarity

## Dynamic Development

Ratings can rise and fall.

Form changes quickly. True ability changes slowly.

A player can move like:

82 -> 84 -> 83 -> 80 -> 82 -> 85

Not every player grows linearly.

Development patterns:

- Early Bloomer
- Late Bloomer
- Steady Developer
- Inconsistent Developer
- Rapid Rise, Early Plateau
- Injury-Disrupted Talent
- Veteran Revival
- High Ceiling, Low Discipline
- Low Ceiling, High Reliability

---

# 10. Tactics

The tactical system should have six layers.

## Tactical Layers

1. Formation
2. Team style
3. Team instructions
4. Player roles
5. Player instructions
6. Tactical familiarity

## Formations

Examples:

- 4-3-3
- 4-2-3-1
- 4-4-2
- 3-5-2
- 3-4-3
- 4-1-4-1
- 5-3-2
- 4-3-1-2

Formation affects base shape, positioning, width, defensive structure, and passing lanes.

## Team Styles

Use:

- Balanced
- Possession
- Direct Play
- Counterattack
- High Press
- Low Block
- Wide Attack
- Central Overload
- Defensive Solidity

## Team Instructions

Use readable labels or sliders.

Tempo:

- Slow
- Balanced
- Fast
- Very Fast

Passing Directness:

- Short
- Mixed
- Direct
- Very Direct

Pressing Intensity:

- Low
- Medium
- High
- Relentless

Defensive Line:

- Deep
- Medium
- High
- Very High

Width:

- Narrow
- Balanced
- Wide
- Very Wide

Attacking Risk:

- Safe
- Balanced
- Aggressive
- Very Aggressive

Tackling:

- Conservative
- Normal
- Aggressive

## Player Roles

Striker roles:

- Poacher
- Target Forward
- Pressing Forward
- False Nine
- Complete Forward

Winger roles:

- Traditional Winger
- Inverted Winger
- Inside Forward
- Wide Playmaker
- Defensive Winger

Midfielder roles:

- Box-to-Box Midfielder
- Deep Playmaker
- Ball-Winning Midfielder
- Holding Midfielder
- Advanced Playmaker
- Mezzala-style Runner

Fullback roles:

- Defensive Fullback
- Balanced Fullback
- Overlapping Fullback
- Inverted Fullback
- Wingback

Center back roles:

- No-Nonsense Center Back
- Ball-Playing Center Back
- Stopper
- Cover Defender

Goalkeeper roles:

- Shot Stopper
- Sweeper Keeper
- Distributor
- Commanding Keeper

## Player Instructions

Examples:

- Stay wider
- Cut inside
- Shoot more
- Shoot less
- Cross early
- Dribble more
- Hold position
- Roam from position
- Make forward runs
- Drop deeper
- Press more
- Press less
- Mark tightly
- Take fewer risks
- Take more risks

## Tactical Familiarity

Use this scale:

- Excellent
- Very Familiar
- Familiar
- Neutral
- Unfamiliar
- Poor
- Very Poor

Track familiarity for:

- Formation
- Team style
- Pressing
- Defensive line
- Build-up play
- Counterattack
- Wing play
- Central play
- Set pieces
- Player roles

---

# 11. Transfers and Contracts

Transfers should connect tactics, scouting, personality, board trust, fan culture, Director of Football conflict, money, and promises.

## Transfer Flow

1. Identify need
2. Scout player
3. Check tactical fit
4. Check player interest
5. Check board and Director support
6. Make club bid
7. Negotiate fee structure
8. Negotiate personal terms
9. Make promises
10. Final approval
11. Fan and media reaction
12. Player integration

## Transfer Authority by Role

Assistant Manager:

- Can suggest player profiles
- Can give internal squad reports
- Cannot bid, negotiate, or approve transfers

Head Coach:

- Can request profiles and recommend targets
- Can influence sales and squad needs
- Cannot finalize fees, wages, or contracts

Manager:

- Can set transfer priorities
- Can approve bids within budget
- Can negotiate wages within structure
- Can make squad role promises
- Still limited by board, budget, player interest, agent demands, and Director politics

## Offer Structures

- Flat fee
- Installments
- Add-ons
- Sell-on clause
- Loan with option
- Loan with obligation
- Player swap
- Release clause trigger

## Agent Demands

Agents can demand:

- Higher wage
- Signing bonus
- Appearance bonus
- Goal bonus
- Release clause
- Guaranteed role
- Contract length
- Wage increase after promotion
- Preferred position promise
- Squad improvement promise

## Agent Types

- Aggressive
- Practical
- Loyalty-focused
- Money-first
- Career-focused
- Opportunist
- Patient negotiator
- Media leaker

## Promises

Possible promises:

- Star player
- Important player
- Regular starter
- Rotation option
- Development prospect
- Preferred position
- Preferred role
- Captaincy consideration
- Squad improvement promise
- Release clause promise
- Contract renewal promise
- Loan pathway

## Transfers After Signing

Transfers do not end when the player signs. New players need adaptation.

Adaptation depends on:

- Personality
- Tactical fit
- Squad support
- Playing time
- Manager relationship
- Pressure
- Fan reaction
- Form

---

# 12. Match Simulation

Touchline uses one match engine for:

- Instant Sim
- Live Match Playback

The match is simulated first. Instant Sim displays the result immediately. Live Match plays the same event timeline visually.

## Match Simulation Layers

1. Player ability
2. Player current context
3. Tactical setup
4. Tactical familiarity
5. Player tactical fit
6. Team morale
7. Match momentum
8. Staff preparation
9. Opponent strength and style

## Event Types

- Kickoff
- Buildup
- Pass
- Progressive pass
- Dribble
- Cross
- Tackle
- Interception
- Foul
- Yellow card
- Red card
- Shot
- Blocked shot
- Save
- Goal
- Corner
- Free kick
- Injury
- Substitution
- Tactical adjustment
- Halftime
- Full time

## Event Rhythm

Football should not feel evenly spaced.

The match should include phases:

- Opening feeling-out period
- Control phase
- Pressure spell
- Counterattack burst
- Scrappy midfield battle
- Late-game fatigue phase
- Desperate final push

## Live Match Playback

Live playback should show:

- Top-down pitch
- Player circles
- Name labels
- Ball movement
- Team colors
- Side commentary panel
- Score and time display
- Major event highlights

Live playback should show football behavior, not formulas.

## Post-Match Explanation

Post-match should explain why things happened.

Example:

“Your high press created seven turnovers in the attacking third, but poor pressing familiarity caused late defensive gaps.”

---

# 13. News, Media, and World Events

News should do four things:

1. Report what happened
2. Create pressure
3. Reveal world movement
4. Trigger decisions

## News Categories

1. Club news
2. Player news
3. Transfer news
4. Match news
5. Board news
6. Fan reaction
7. Media pressure
8. Rival club news
9. Staff news
10. Career news

## News Reliability Labels

- Official
- Reported
- Rumor
- Internal Source
- Fan Reaction
- Media Speculation
- Agent Leak

## News Should Trigger Decisions

Examples:

- Player complains privately
- Media asks about poor form
- Board questions spending
- Rival bids for star
- Agent leaks contract frustration
- Staff disagree about tactics

## Role-Based News Focus

Assistant Manager:

- Internal politics
- Manager pressure
- Player mood
- Interim opportunities

Head Coach:

- Tactics
- Results
- Player development
- Dressing room control

Manager:

- Transfers
- Contracts
- Board trust
- Fan culture
- Director of Football conflict
- Long-term project

---

# 14. Morale, Trust, Reputation, and Pressure

These systems must be separated.

- Morale = how people feel right now
- Trust = how much they believe in the user
- Reputation = how the football world sees the user
- Pressure = how close the situation is to consequences

## Morale Types

- Board morale
- Fan morale
- Individual player morale
- Squad morale

## Trust Types

- Board Trust
- Fan Trust
- Player Trust
- Staff Trust
- Director of Football Trust
- Media Trust

## Reputation Types

- Overall Reputation
- Tactical Reputation
- Development Reputation
- Transfer Reputation
- Financial Reputation
- Player Management Reputation
- Media Reputation
- Big Match Reputation
- Crisis Management Reputation

## Pressure Types

- Job Pressure
- Media Pressure
- Fan Pressure
- Board Pressure
- Dressing Room Pressure
- Transfer Pressure
- Financial Pressure

## Stability Score

The game should have a hidden or semi-visible club stability score.

Low stability makes events more volatile.

## Threshold Example

Player tension:

- 0 to 25: normal
- 26 to 50: concern
- 51 to 70: private complaint
- 71 to 85: agent or media pressure
- 86 to 100: transfer request or public conflict

Job pressure:

- 0 to 30: safe
- 31 to 55: watched
- 56 to 75: under pressure
- 76 to 90: ultimatum risk
- 91 to 100: sacking likely

---

# 15. Objectives, Job Security, and Sackings

Job security should not depend only on losses.

It depends on:

- Results
- Club objectives
- Board trust
- Fan pressure
- Dressing room control
- Financial performance
- Transfer decisions
- Tactical identity
- Role authority
- Club stability
- License level
- Reputation

## Contract Terms

Every job should start with a contract containing:

- Role
- Contract length
- Board expectations
- Control level
- Transfer authority
- Staff authority
- License expectations
- Performance targets
- Style expectations
- Sacking risk
- Renewal conditions

## Objective Types

- League objective
- Cup objective
- Style objective
- Squad objective
- Financial objective
- Reputation objective

## Objective Priority

Use:

- Critical
- Important
- Preferred
- Optional

## Job Security States

- Secure
- Stable
- Watched
- Under Pressure
- Ultimatum
- Near Sacking
- Sacked

## Ultimatums

Ultimatums should be specific.

Examples:

- Win one of the next three matches
- Get 6 points from the next 4 league matches
- Avoid defeat in the derby
- Improve dressing-room morale within 30 days
- Reduce wage bill before the transfer deadline
- Reach the next cup round

## Sacking Aftermath

Getting sacked is not game over.

After sacking:

- Reputation changes
- Specific reputation categories change
- Some clubs may remain interested
- Smaller clubs may offer rebuild jobs
- Assistant roles may become available
- Media narrative follows the user
- License progress remains

---

# 16. Career Progression and Job Market

Career progression should be unstable, political, and opportunity-based.

## Career Reputation Types

- Overall Reputation
- Tactical Reputation
- Development Reputation
- Transfer Reputation
- Financial Reputation
- Player Management Reputation
- Media Reputation
- Big Match Reputation
- Crisis Management Reputation

## Club Hiring Logic

Clubs hire based on:

- User reputation
- User license
- Role history
- Club archetype
- Board philosophy
- Fan culture
- Current crisis level
- League level
- Budget
- Recent manager history
- Director of Football style

## Job Market States

Each club has a manager situation:

- Secure Manager
- Under Pressure
- Likely Vacancy
- Vacant
- Interim Appointed
- Actively Hiring
- Monitoring Candidates

## Job Offer Types

- Assistant Manager offer
- Head Coach offer
- Manager offer
- Interim Manager offer
- End-of-season approach
- Emergency approach
- Interview invitation

## Applying for Jobs

Possible outcomes:

- Rejected immediately
- Interview offered
- Shortlisted
- Board considering
- Offer made
- Club chooses another candidate

Rejections should be explained in football language.

---

# 17. Season Structure and Calendar

## Season Length

A full season should run approximately 10 to 11 in-game months.

Recommended structure:

- Preseason: 4 weeks
- Main season: 38 league matchdays
- Domestic cups: spread across the season
- Transfer windows: preseason/summer and midseason/winter
- End-of-season review: 2 weeks
- Offseason: compressed planning period

## Weekly Cycle

Each in-game week should have a predictable rhythm.

Standard week:

1. Monday: recovery, injury updates, staff reports
2. Tuesday: training focus, tactical preparation
3. Wednesday: scouting updates, player meetings, media stories
4. Thursday: tactical work, lineup planning
5. Friday: match preview, press, final training
6. Saturday or Sunday: matchday
7. Sunday or Monday: post-match review

The weekly cycle should adapt for midweek matches.

## Training Days

Training days should support:

- Team training
- Individual training
- Tactical familiarity
- Fitness intensity
- Recovery
- Youth development
- Role training

## Matchdays

Matchdays include:

1. Match preview
2. Staff advice
3. Lineup confirmation
4. Tactical confirmation
5. Instant Sim or Live Match
6. Post-match report
7. Morale, trust, reputation, and pressure updates
8. News reaction

## Transfer Windows

Use two transfer windows:

- Main window before and early into the season
- Midseason window

During windows:

- Buy players
- Sell players
- Loan players
- Renew contracts
- Register squads
- Handle deadline pressure

Outside windows:

- Scout
- Shortlist
- Plan future deals
- Negotiate renewals
- Handle rumors

## Cup Rounds

Cup rounds should be spaced throughout the season and can create role-specific opportunities.

Assistant Managers may be delegated cup matches if trusted.

Cup competitions should matter differently based on club archetype and board objective.

## Contract Deadlines

Important dates:

- Contract expiry warnings at 12 months remaining
- Renewal pressure at 6 months remaining
- Pre-contract risk if the game uses that mechanic
- End-of-season free-agent releases

## License Course Timing

License courses should have scheduled windows:

- Preseason course window
- Midseason course window
- Offseason course window

Taking a license course can temporarily reduce availability or create staff-delegation moments.

## Job Openings

Job openings can occur:

- After bad runs
- During international-style breaks if fictional calendar has breaks
- After cup exits
- Around transfer-window failures
- At end of season
- When another manager is poached

## Scouting Report Timing

Reports should not be instant unless the player is already well-known or fully scouted.

Suggested timing:

- Initial report: 3 to 7 days
- Deeper report: 2 to 4 weeks
- Full profile confidence: repeated scouting or strong staff/data support

## Board Review Dates

Board reviews should happen:

- Start of season
- After first 10 league matches
- Midseason
- After transfer windows
- End of season
- Any time job pressure reaches dangerous levels

---

# 18. League Structure

## Number of Leagues

Use a fictional pyramid with four playable tiers.

Recommended structure:

- Tier 1: Premier-level league
- Tier 2: Championship-level league
- Tier 3: Regional professional league
- Tier 4: Semi-professional or lower professional league

This gives career movement without becoming too large.

## Promotion and Relegation

Each league should have promotion and relegation.

Recommended:

- Tier 1: bottom 3 relegated
- Tier 2: top 2 promoted, playoff for 3rd promotion, bottom 3 relegated
- Tier 3: top 2 promoted, playoff for 3rd promotion, bottom 4 relegated
- Tier 4: top teams promoted, bottom teams risk exit from playable structure

## Cup Competitions

Use fictional cups:

1. National Cup: all professional clubs
2. League Cup: top two or three tiers
3. Lower Cup: lower-tier clubs only
4. Super Cup: optional, between league and cup winners

## Club Reputation Tiers

Use:

- Elite
- Major
- Established
- Mid-Level
- Developing
- Small
- Fragile

Club reputation can rise or fall over years.

## League Reputation

Each league has reputation.

League reputation affects:

- Player interest
- Manager interest
- Sponsorship/revenue
- Media attention
- Transfer values
- License expectations
- Job prestige

## Prize Money

Prize money depends on:

- League level
- Final position
- Cup progress
- Club reputation
- Broadcast/commercial structure

Prize money should not fully solve poor financial management.

## Squad Registration Rules

Registration rules should exist but stay readable.

Use:

- Senior squad registration limit
- Youth players can be used without full registration if under age threshold
- Loan limits
- Fictional homegrown-style requirement

## Fixture Generation

Fixtures should consider:

- Home and away balance
- Derby placement
- Cup rounds
- Congestion
- Rivalry weeks
- End-of-season drama

## Derbies and Rivalries

Each club can have:

- Main rival
- Secondary rival
- Historical rival
- Competitive rival

Rivalries affect fan morale, media hype, player pressure, and board reaction.

---

# 19. Squad Registration and Roster Rules

## Squad Size Limits

Recommended:

- Senior registered squad: 25 players
- Matchday squad: 18 to 20 players
- Starting XI: 11
- Substitutes used: 5
- Youth players can appear under special rules

## Youth Player Rules

Youth players under a defined age can be used without taking a full senior squad slot.

Recommended age threshold:

- Under 21 counts as youth

## Foreign Player Rules

Because the world is fictional, avoid real-world immigration complexity.

Use a fictional version:

- Domestic-trained players
- Region-trained players
- Overseas players

Limit overseas players lightly to encourage squad-building choices without creating paperwork overload.

## Loan Limits

Recommended:

- Maximum incoming loans: 5
- Maximum outgoing loans: no strict hard cap, but development quality matters
- Matchday loan limit optional
- Young players benefit from development loans only if they receive minutes

## Contract Expiry

Contracts should track:

- Expiry date
- Wage
- Role promise
- Release clause
- Extension option
- Agent mood
- Renewal interest

## Wage Budget

Each club has:

- Wage budget
- Current wage bill
- Wage structure
- Board flexibility
- Dressing-room wage balance

Breaking wage structure can cause player and board issues.

## Homegrown-Style Rule

Use a fictional homegrown system:

- Club-trained players
- Nation-trained players
- Region-trained players

This supports academy strategy without copying real rules directly.

---

# 20. Training System

Training should affect tactical familiarity, development, fatigue, morale, and injury risk.

## Weekly Training Focus

The user can choose weekly focus areas:

- Attacking movement
- Defensive shape
- Pressing
- Possession
- Counterattack
- Set pieces
- Fitness
- Recovery
- Team cohesion
- Youth integration

## Individual Training

Players can train:

- Specific attributes
- Position familiarity
- Role familiarity
- Weak-foot or trait-style improvements if supported
- Recovery and conditioning

## Role Training

Role training improves how naturally a player performs a tactical role.

Example:

A winger can improve familiarity as an Inside Forward, Traditional Winger, or Defensive Winger.

## Tactical Familiarity

Training improves familiarity for:

- Formation
- Team style
- Pressing
- Defensive line
- Build-up play
- Counterattack
- Wing play
- Central play
- Set pieces
- Player roles

## Fitness Intensity

Intensity levels:

- Light
- Normal
- High
- Extreme

Higher intensity can improve sharpness and familiarity faster but increases fatigue, injury risk, and complaints.

## Recovery

Recovery reduces:

- Fatigue
- Injury risk
- Morale issues from workload

Recovery is more important during fixture congestion.

## Youth Development

Youth training should depend on:

- Youth Coach quality
- Academy quality
- Player potential
- Personality
- Minutes
- Training focus
- Loan quality

## Staff Impact

Staff affect training outcomes:

- First-Team Coach improves tactical and technical training
- Fitness Coach improves conditioning and workload management
- Physio helps risk management
- Youth Coach improves youth progress
- Assistant Manager improves training feedback

## Player Complaints About Workload

Players can complain if:

- Intensity is too high for too long
- Fixture congestion is ignored
- Injury risk is high
- Veterans are overworked
- Low-stamina players are pushed too hard

Complaints affect morale, player trust, and injury risk.

---

# 21. Player Development and Aging

## Growth Curves

Players should have development patterns:

- Early Bloomer
- Late Bloomer
- Steady Developer
- Inconsistent Developer
- Rapid Rise, Early Plateau
- Injury-Disrupted Talent
- Veteran Revival
- High Ceiling, Low Discipline
- Low Ceiling, High Reliability

## Potential

Potential should not be a fixed visible number.

Use estimated ranges and scouting language.

Examples:

- “Could become a strong first-team player.”
- “High ceiling, but development depends heavily on discipline.”
- “Scouts are unsure how much more he can grow.”

## Late Bloomers

Some players should develop later than expected.

This can happen due to:

- Better tactical fit
- Improved training
- More playing time
- Personality maturity
- Injury recovery
- New role discovery

## Decline

Decline should depend on:

- Age
- Position
- Injury history
- Physical attributes
- Fitness work
- Playing time
- Motivation
- Role adjustment

Aging should not automatically destroy players at the same age.

## Injury Impact

Injuries can cause:

- Short-term fitness loss
- Sharpness loss
- Temporary attribute drop
- Permanent physical decline if severe
- Confidence issues
- Increased future risk

## Form vs True Ability

Form is temporary.

True ability changes slowly.

A player in bad form should not instantly lose permanent ability. Long-term poor performance, injury, poor training, or decline can reduce true ability.

## Temporary Boosts

Temporary boosts can come from:

- Great form
- High morale
- Perfect tactical fit
- Derby motivation
- Big-game trait
- Confidence after scoring

## Permanent Rating Changes

Permanent changes come from:

- Long-term performance trend
- Training
- Age and development curve
- Injury history
- Tactical adaptation
- Coaching quality
- Playing time

## Match Performance Impact

Match performance should affect:

- Form
- Confidence
- Development trend
- Morale
- Media reputation
- Transfer interest

Repeated strong performances can contribute to ability growth over time.

---

# 22. Youth Academy

## Youth Intake

Youth intake should happen once per season.

The intake quality depends on:

- Academy quality
- Youth Coach rating
- Club reputation
- Board investment
- Region quality
- Luck

## Academy Quality

Academy quality levels:

- Poor
- Basic
- Stable
- Strong
- Elite

Academy quality affects:

- Number of prospects
- Starting ability
- Potential range
- Personality quality
- Development speed

## Generated Prospects

Generated prospects should have:

- Name
- Age
- Position
- Ability estimate
- Potential estimate
- Personality
- Playing style
- Traits
- Hidden potential
- Scout confidence

## Scouting Youth Players

Youth reports should be uncertain.

Young players should have more unknowns than senior players.

## Promoting Academy Players

Promotion decisions affect:

- Youth morale
- Fan morale for Academy Loyalists
- Board trust for Youth Development Boards
- Senior squad competition
- Player development speed

## Loan Development

Loans should consider:

- Playing time
- Loan club tactical fit
- League level
- Coaching quality
- Player personality
- Pressure level

A bad loan can stall development.

## Youth Personalities

Youth players should have personality tendencies early:

- Professional
- Ambitious
- Nervous
- Resilient
- Complacent
- Team-first
- Volatile

These can mature over time.

## Hidden Potential

Potential should be uncertain and revealed gradually.

## Fan and Board Reaction

Academy use matters most for:

- Youth Academy Clubs
- Community Clubs
- Academy Loyalist Fans
- Youth Development Boards
- Financially Strict Boards

---

# 23. Rivalries and Derbies

## Main Rivals

Each club should have:

- Main rival
- Secondary rival
- Optional historical rival
- Optional competitive rival

## Derby Importance

Derby importance levels:

- Minor rivalry
- Local rivalry
- Major derby
- Historic derby
- Bitter rivalry

## Fan Morale Swings

Derby wins boost fan morale more than normal wins.

Derby losses hurt fan morale more than normal losses.

## Media Hype

Derby weeks should create:

- Special news stories
- Rival manager comments
- Fan expectations
- Player pressure
- Tactical scrutiny

## Player Pressure

Players with:

- Big-Game Player trait
- High composure
- Strong loyalty
- Club-trained background

may handle derbies better.

Players with low composure or poor morale may struggle.

## Board Reaction

Board reaction depends on board philosophy.

Results-first and traditionalist boards may care heavily about derbies.

## Historical Records

Track:

- All-time rivalry record
- Recent rivalry record
- Biggest wins
- Painful losses
- Derby scorers
- Manager derby record

## Rival Managers

Rival managers can influence:

- Media tension
- Tactical narratives
- Job comparisons
- Fan pressure
- Derby storylines

---

# 24. Finance System

## Transfer Budget

Transfer budget depends on:

- Club finances
- Board philosophy
- League level
- Prize money
- Player sales
- Board injections
- Debt
- Objectives

## Wage Budget

Wage budget includes:

- Current wage bill
- Maximum wage flexibility
- Wage structure
- Board tolerance
- Dressing-room wage balance

## Club Debt

Debt can limit:

- Transfer budget
- Wage flexibility
- Staff hiring
- Facility upgrades
- License sponsorship

## Revenue

Revenue sources:

- Ticket income
- Prize money
- Commercial income
- Player sales
- Broadcast-style league income
- Cup runs
- Board investment

## Prize Money

Prize money should scale by:

- League reputation
- Final position
- Cup progress
- Club reputation

## Ticket Income

Ticket income depends on:

- Stadium size
- Fan morale
- Club reputation
- League level
- Opponent importance
- Derby status

## Commercial Growth

Commercial growth depends on:

- Reputation
- Star players
- Style of play
- Cup runs
- League success
- Media image

## Board Injections

Board injections depend on:

- Board philosophy
- Club ambition
- Financial health
- Trust in user
- Current crisis level

## Financial Fair-Play-Style Rules

Use fictional financial rules:

- Clubs must stay within sustainable-loss limits
- Wage bill must stay within allowed structure
- Repeated overspending triggers board restrictions

Keep it readable. Do not make it accounting-heavy.

## Budget Cuts

Budget cuts happen after:

- Relegation
- Missed objectives
- Debt increases
- Failed promotion push
- Commercial decline
- Board trust collapse

## Profit Expectations

Profit expectations matter most for:

- Financially Strict Boards
- Selling Clubs
- Financially Restricted Clubs
- Data-Driven Boards

---

# 25. Staff Market and Staff Contracts

## Staff Contracts

Staff contracts include:

- Role
- Wage
- Contract length
- Reputation
- Interest level
- Preferred working style
- Loyalty
- Ambition
- Board approval requirement

## Staff Wages

Higher-reputation staff demand higher wages.

Financially restricted clubs may struggle to hire elite staff.

## Staff Reputation

Staff reputation affects:

- Job interest
- Report credibility
- Player respect
- Board trust
- Ability to get hired by bigger clubs

## Staff Job Interest

Staff interest depends on:

- Club reputation
- Role authority
- Wage
- User reputation
- Board philosophy
- Football style
- Career ambition

## Staff Poaching

Other clubs can poach staff when:

- Staff reputation rises
- Contract is running down
- Staff ambition is high
- Bigger club offers stronger role

## Staff Leaving

Staff can leave due to:

- Poaching
- Contract expiry
- Low loyalty
- Conflict with user
- Board reshuffle
- New Manager arrival

## Staff Loyalty

Loyal staff support the user during bad runs.

Low-loyalty staff may leak concerns or leave quickly.

## Board Approval

Major staff hires require board approval, especially for Head Coach and Manager roles.

## Role-Based Hiring Authority

Assistant Manager:

- No hiring authority

Head Coach:

- Can recommend staff changes
- Needs Director or board approval

Manager:

- Strong influence over staff changes
- Still needs board approval for major hires
- Cannot fire Director of Football

---

# 26. Scouting System

## Scouting Assignments

Assignments can target:

- Specific player
- Position need
- Tactical profile
- Age group
- League or region
- Free agents
- Loan market
- Youth prospects

## Scouting Regions

Since the world is fictional, use fictional regions.

Regions should have:

- Talent level
- Cost
- Scout familiarity
- Player style tendencies
- Competition level

## Report Quality

Report quality depends on:

- Scout rating
- Data Analyst support
- License level
- Time spent scouting
- Player visibility
- League reputation

## Report Delay

Reports take time.

- Quick overview: 3 to 7 days
- Standard report: 2 weeks
- Deep report: 3 to 4 weeks
- Full confidence: repeated scouting or strong staff support

## Attribute Discovery

Scouting reveals:

- Exact known attributes
- Estimated ranges
- Unknown attributes
- Overall estimate
- Potential estimate

## Personality Discovery

Personality discovery should be harder than ability discovery.

Better scouts and longer observation reveal personality clues.

## Tactical Fit Discovery

Tactical fit reports depend on:

- Scout tactical judgment
- Data Analyst support
- User license
- Existing tactical system clarity

## Scout Accuracy

Scouts can be wrong.

Weak scouts may overrate or underrate players.

## Data Analyst Overlap

Data Analysts improve:

- Performance trend analysis
- Transfer value assessment
- Tactical fit evidence
- Risk detection

Scouts provide human and football-context judgment.

## License Impact

Higher licenses improve the user’s ability to understand scouting information.

Low license:

- Basic reports
- More question marks
- Less tactical detail

High license:

- Better tactical interpretation
- More useful personality clues
- Better role-fit explanation

---

# 27. Matchday Control by Role

## Can Assistant Manager Make Suggestions Only?

Yes, by default.

Assistant Manager can:

- Suggest lineup changes
- Suggest tactical tweaks
- Recommend substitutions
- Provide opposition notes
- Warn about fatigue or morale

Assistant Manager cannot make final decisions unless delegated.

## Can Head Coach Control All Substitutions?

Yes.

Head Coach controls:

- Lineup
- Tactics
- Substitutions
- Match instructions
- Tactical changes

## Can Manager Control All Match Decisions?

Yes.

Manager controls all match decisions unless responsibilities are delegated.

## Can Responsibilities Be Delegated?

Yes.

Delegation can happen for:

- Training
- Opposition reports
- Press conferences
- Cup matches
- Youth matches
- Scouting recommendations

Delegation quality depends on staff.

## Can the Manager Ignore Staff Advice?

Yes.

Ignoring staff advice can be fine if the user is right. Repeatedly ignoring accurate advice can lower staff trust.

## Can the Head Coach Be Overruled?

Yes, but only structurally.

A Head Coach can be overruled on:

- Transfers
- Player sales
- Budget decisions
- Staff hiring
- Contract decisions

A Head Coach should not normally be overruled on matchday tactics unless the club structure is extremely chaotic.

---

# 28. Post-Match Report System

Post-match reports should explain what happened, not just show stats.

## Stats Shown

Show:

- Score
- Possession
- Shots
- Shots on target
- Expected-chance equivalent if fictionalized
- Pass completion
- Tackles
- Fouls
- Cards
- Corners
- Big chances
- Turnovers
- Pressing success
- Fitness drop

## Tactical Explanation

Explain:

- Why the tactic worked or failed
- Which areas were exposed
- Where chances came from
- Which instructions mattered
- How opponent style affected the match

## Player Ratings

Show player ratings, but explain them with context.

Example:

“Played well because his direct style matched the counterattacking setup.”

## Fit Notes

Show:

- Strong role fit
- Partial role fit
- Poor tactical fit
- Familiarity issues
- Player tendency conflict

## Fatigue Notes

Explain:

- Late-game drop-off
- High pressing fatigue
- Overworked players
- Recovery concerns
- Injury-risk warnings

## Morale Changes

Show impact on:

- Board morale
- Fan morale
- Individual player morale
- Squad morale

## Board Reaction

Explain based on board philosophy.

## Fan Reaction

Explain based on fan culture.

## Media Story

Generate the main post-match headline or narrative.

## Staff Analysis

Staff reports should depend on staff quality.

## Development Notes

Show:

- Youth progress
- Form changes
- Role familiarity changes
- Tactical familiarity changes
- Injury recovery or setback

---

# 29. Decision Events

Decision events are interactive moments outside matches.

## Player Meeting Events

Examples:

- Player wants more minutes
- Player dislikes role
- Player wants new contract
- Player wants transfer
- Captain raises squad concern

## Board Meeting Events

Examples:

- Board questions results
- Board questions spending
- Board offers license support
- Board demands wage control
- Board warns about fan pressure

## Media Questions

Examples:

- Poor form
- Tactical criticism
- Player complaint
- Transfer rumor
- Derby pressure
- Director of Football tension

## Agent Calls

Examples:

- Contract demand
- Playing-time concern
- Transfer interest
- Release clause request
- Promise dispute

## Staff Disagreements

Examples:

- Assistant Manager wants deeper line
- Data Analyst recommends pressing
- Fitness Coach warns about workload
- Director of Football rejects target

## Training Issues

Examples:

- Player complains about workload
- Senior player unhappy with intensity
- Young player impresses
- Injury risk warning

## Fan Pressure Moments

Examples:

- Fans demand attacking football
- Fans protest player sale
- Fans demand derby response
- Fans back academy player

## Director of Football Conflict

Examples:

- Signs player user did not request
- Blocks user’s preferred target
- Pushes board to sell player
- Leaks disagreement

## Crisis Events

Examples:

- Losing streak
- Dressing-room split
- Star transfer request
- Injury crisis
- Board ultimatum

## Promise Resolution

Events should track whether promises are kept, broken, renegotiated, or unresolved.

---

# 30. Promises and Relationships

## Promise Types

- Playing time
- Squad role
- Preferred position
- Preferred tactical role
- Contract renewal
- Release clause
- Captaincy consideration
- Squad improvement
- Youth pathway
- Loan pathway
- Transfer acceptance

## Promise Duration

Promise duration should vary:

- Short-term: weeks
- Medium-term: half season
- Long-term: full season or contract period

## Promise Tracking

Promises should track:

- Promise giver
- Promise recipient
- Expected action
- Deadline
- Progress
- Status
- Consequence risk

Statuses:

- Active
- On Track
- At Risk
- Broken
- Fulfilled
- Renegotiated

## Broken Promise Thresholds

Not every broken promise should explode instantly.

Repeated or serious broken promises raise tension.

## Player Reactions

Possible reactions:

- Minor morale drop
- Private concern
- Agent complaint
- Public frustration
- Transfer request
- Refusal to renew

## Agent Reactions

Agents can:

- Demand meeting
- Leak concern
- Raise wage demands
- Push for move
- Refuse renewal talks

## Squad Trust Impact

Breaking promises to respected players can reduce squad trust.

Breaking promises to isolated players has smaller squad impact.

## Board Trust Impact

Board trust drops if promises create instability, wage problems, or media pressure.

## Can Promises Be Renegotiated?

Yes.

Renegotiation depends on:

- Player personality
- Manager relationship
- Agent style
- Current morale
- Team form
- Trust

---

# 31. UI Information Philosophy

Touchline should mix exact numbers, estimates, question marks, and scouting language.

## Exact Numbers vs Scouting Language

Use exact numbers for:

- Known ratings
- Budgets
- Wages
- Contract dates
- League table
- Match stats

Use scouting language for:

- Tactical fit
- Personality
- Potential
- Role comfort
- Development projection
- Transfer risk

## When to Show Ratings

Show ratings when:

- Player is at the club and observed
- Player is fully scouted
- Staff confidence is high
- License level supports interpretation

## When to Show Question Marks

Show question marks when:

- Attribute is unknown
- Scout confidence is low
- Player is in a low-visibility league
- User license is too low
- Staff quality is weak

## When to Show Estimated Ranges

Use estimated ranges when partial information exists.

Example:

Passing: 72 to 78

## What Licenses Reveal

Higher licenses reveal:

- Better tactical detail
- Better player-fit explanation
- Better development interpretation
- Better scouting confidence
- Better post-match diagnosis

## What Staff Reveals

Staff reveal:

- Scout: player ability, potential, personality clues
- Data Analyst: performance trends and tactical fit
- Assistant Manager: dressing-room and tactical advice
- Physio: injury risk
- Fitness Coach: workload risk
- Media Officer: fan/media risk

## What the User Should Never Fully Know

The user should never fully know:

- Exact hidden personality numbers
- Exact future potential certainty
- Exact rival-club intentions
- Exact player future behavior
- Exact board internal politics

The game should reveal enough to make informed choices, not perfect choices.

---

# 32. Difficulty and Realism Settings

Touchline can let users adjust realism and pressure without changing the core design.

## Strict Realism

Options:

- Relaxed
- Balanced
- Strict

Strict realism increases board logic, transfer difficulty, sacking risk, and financial pressure.

## Drama Frequency

Options:

- Low
- Balanced
- High

Default should be balanced, matching 20 to 30 percent dramatic events.

## Scouting Difficulty

Options:

- Generous
- Balanced
- Strict

Strict scouting creates more question marks, longer delays, and more uncertainty.

## Sacking Strictness

Options:

- Forgiving
- Balanced
- Harsh

## Transfer Difficulty

Options:

- Easier
- Balanced
- Hard

Hard transfer difficulty increases agent demands, board restrictions, rival bids, and player-interest realism.

## Hidden Info Level

Options:

- Low uncertainty
- Balanced uncertainty
- High uncertainty

## Match Randomness

Options:

- Low
- Balanced
- High

Default should keep football unpredictable but not chaotic.

## Finance Difficulty

Options:

- Forgiving
- Balanced
- Strict

---

# 33. Save System and Career History

Long careers need memory.

## Manager Career Timeline

Track:

- Roles held
- Clubs managed
- Contract dates
- Promotions
- Sackings
- Resignations
- Trophies
- Reputation changes
- License progression

## Club History

Track:

- League finishes
- Cup runs
- Manager history
- Financial history
- Major transfers
- Reputation changes
- Rivalry records

## Player History

Track:

- Career clubs
- Transfer fees
- Goals and assists
- Appearances
- Injuries
- Development trend
- Promises
- Relationship with user

## Transfer History

Track:

- Fees
- Wages
- Clauses
- Selling club
- Buying club
- Director/user responsibility
- Success or failure narrative

## Trophy History

Track:

- League titles
- Cups
- Promotions
- Individual awards

## Rivalry Records

Track:

- Derby wins
- Derby losses
- Biggest derby moments
- User’s derby record

## Past Promises

Track:

- Fulfilled promises
- Broken promises
- Renegotiated promises
- Reputation impact

## Past Sackings

Track:

- Reason for sacking
- Media narrative
- Reputation impact
- Recovery path

## Reputation History

Track reputation changes over time by category.

---

# 34. Generated Content

Because Touchline is fictional, generated content matters.

## Generated Player Names

Names should match fictional regions and avoid obvious repetition.

## Generated Clubs

Generated clubs should include:

- Name
- Colors
- Archetype
- Board philosophy
- Fan culture
- Rivalries
- Reputation
- Stadium size
- Budget level

## Generated News Headlines

Headlines should be template-based with variation.

## Generated Scout Reports

Scout reports should use football language and reflect confidence level.

## Generated Media Questions

Media questions should be based on:

- Recent form
- Transfers
- Derby matches
- Player unrest
- Board pressure
- Tactical criticism

## Template vs AI Text

Prefer structured templates for consistency.

AI-style generation can be used only if constrained by:

- Event facts
- Tone
- Character limits
- No contradiction rules
- No invented consequences outside game state

## Tone Consistency

Tone should stay serious, realistic, and football-world believable.

## Avoiding Repetitive News

Use:

- Event categories
- Cooldowns
- Variation templates
- Club-specific language
- Player-specific context
- Rivalry-specific phrasing

---

# 35. Game Balance Rules

Balance prevents the sim from becoming unfair or chaotic.

## Morale Effects

Morale should tilt outcomes but not dominate them.

Recommended:

- Small morale effects most of the time
- Larger effects during pressure moments, derbies, collapses, and comeback situations

## Randomness

Football needs randomness, but ratings and tactics must matter.

Recommended:

- Stronger teams usually perform better over time
- Upsets remain possible
- Single matches can be surprising
- Long-term patterns should reveal quality

## Drama Frequency

Drama should be meaningful, not constant.

Use cooldowns and thresholds.

## Rating Change Speed

- Form changes quickly
- Confidence changes moderately
- True ability changes slowly
- Severe injury or long-term trends can cause bigger shifts

## Transfer Difficulty

Transfers should be hard enough to require planning.

Difficulty comes from:

- Player interest
- Agent demands
- Board approval
- Rival bids
- Tactical fit uncertainty
- Wage structure

## Trust Change Speed

Trust changes slower than morale.

One good win should not erase months of bad trust.

## Sacking Likelihood

Sacking depends on:

- Objectives
- Job pressure
- Board philosophy
- Club stability
- Role authority
- Recent results
- Fan pressure
- Dressing-room control

## Tactics vs Player Quality

Player quality should matter heavily, but tactics should shape how quality is expressed.

Recommended balance:

- Player quality: major factor
- Tactics: major factor
- Tactical familiarity: medium to major factor
- Morale/form/fatigue: medium factors
- Randomness: medium factor in single matches, low factor over long term

---

# 36. Conceptual Data Model

This is not implementation. It defines what each major object must represent.

## Player Object

A Player contains:

- Identity: name, age, position, nationality/region
- Ability: true attributes, known attributes, estimated attributes
- Playing style
- Tendencies
- Traits
- Personality
- Tactical fit
- Development curve
- Potential estimate
- Contract
- Morale
- Form
- Fitness
- Fatigue
- Injury risk
- Relationship with user
- Squad status
- Promise history
- Transfer interest

## Club Object

A Club contains:

- Name
- Colors
- Archetype
- Board philosophy
- Fan culture
- Director of Football style
- Reputation
- League
- Budget
- Wage budget
- Debt
- Squad
- Staff
- Academy quality
- Stadium size
- Rivalries
- Objectives
- Stability score
- Manager situation
- History

## Staff Object

A Staff member contains:

- Name
- Role
- Ratings
- Reputation
- Personality
- Preferred style
- Loyalty
- Ambition
- Wage
- Contract
- Relationship with user
- Relationship with board
- Relationship with players

## Match Object

A Match contains:

- Home club
- Away club
- Lineups
- Tactics
- Pre-match context
- Event timeline
- Match stats
- Player ratings
- Injuries
- Cards
- Goals
- Tactical analysis
- Morale impacts
- Reputation impacts
- News outputs

## NewsEvent Object

A NewsEvent contains:

- Title
- Category
- Reliability label
- Source type
- Related club
- Related player
- Related staff
- Related match
- Importance level
- Text
- Effects
- Decision options if interactive

## CareerProfile Object

A CareerProfile contains:

- User name
- Background
- Current role
- Current club
- License
- Reputation categories
- Career history
- Job offers
- Past sackings
- Past promises
- Trophy history
- Media reputation
- Preferred tactical identity if developed

## Contract Object

A Contract contains:

- Role or player/staff position
- Start date
- End date
- Wage
- Promises
- Clauses
- Renewal status
- Agent if player
- Board expectations if user contract

## Tactic Object

A Tactic contains:

- Formation
- Team style
- Team instructions
- Player roles
- Player instructions
- Tactical familiarity
- Fit analysis
- Risk analysis

---

# 37. Full Game Loop

## Daily Actions

Daily actions include:

- Read news
- Review messages
- Handle decision events
- Check player morale
- Review injuries
- Advance scouting
- Advance training
- Update world events

## Weekly Actions

Weekly actions include:

- Set training focus
- Review staff reports
- Prepare for opponent
- Handle player meetings
- Review scouting updates
- Set lineup and tactics
- Handle media
- Play match
- Review post-match report

## Matchday Flow

1. Match preview
2. Staff recommendations
3. Lineup selection
4. Tactical confirmation
5. Instant Sim or Live Match
6. Post-match report
7. Morale and pressure updates
8. News generation
9. Career and reputation updates

## Transfer Window Flow

1. Identify squad needs
2. Scout targets
3. Shortlist players
4. Check interest
5. Negotiate with clubs
6. Negotiate with agents
7. Make promises
8. Board approval
9. Fan/media reaction
10. Player integration

## End-of-Season Review

End of season should include:

- League finish
- Cup performance
- Objective review
- Board reaction
- Fan reaction
- Player development summary
- Transfer review
- Financial review
- Staff review
- Job security update
- Job offers
- License opportunities
- New season planning

## Job Offers

Job offers can happen:

- During season if club is vacant or desperate
- End of season
- After major overachievement
- After user’s contract nears expiry
- After another club sacks a manager

## License Progression

License progression should be reviewed:

- At board review points
- End of season
- After strong achievement
- During scheduled license course windows

## New Season Setup

New season setup includes:

- Budget update
- Objectives update
- Fixture generation
- Squad registration
- Transfer planning
- Staff review
- Youth intake review if timing fits
- Tactical reset or continuation
- Board expectations
- Fan expectations

---

# Final Design Rule

Touchline should always ask:

1. What role does the user have?
2. What does the club expect?
3. What does the board reward?
4. What do fans emotionally care about?
5. What does the squad believe?
6. What does the Director of Football want?
7. What does the user actually know?
8. What is uncertain?
9. What pressure is building?
10. What changes because of the user’s decisions?

The goal is not to make every system complicated for its own sake. The goal is to make every major decision feel connected to the club world.


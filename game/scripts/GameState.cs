using Godot;
using System;
using System.Collections.Generic;

public partial class GameState : Node
{
    private readonly List<string> _recentResults = new();

    public sealed class SquadPlayer
    {
        public string PlayerId { get; init; } = string.Empty;
        public required string Name { get; init; }
        public required string Position { get; init; }
        public required int Age { get; init; }
        public string Nationality { get; init; } = "Novaran";
        public int TrueAbility { get; init; } = 65;
        public int TechnicalAttribute { get; init; } = 65;
        public int TacticalAttribute { get; init; } = 65;
        public int PhysicalAttribute { get; init; } = 65;
        public int MentalAttribute { get; init; } = 65;
        public string KnownAttributesSummary { get; init; } = "Known: Form and fitness only.";
        public string EstimatedAttributesSummary { get; init; } = "Estimated: Tactical ?-?, Mental ?-?";
        public string UnknownAttributesSummary { get; init; } = "Unknown: Potential ?, personality depth ?";
        public string PlayingStyle { get; init; } = "Balanced player";
        public string Tendencies { get; init; } = "Keeps role discipline.";
        public string Traits { get; init; } = "role discipline";
        public string Personality { get; init; } = "Professional";
        public string TacticalFit { get; init; } = "Partial fit: role comfort needs observation.";
        public string DevelopmentCurve { get; init; } = "Growth curve: stable.";
        public required int Form { get; init; }
        public required int Morale { get; init; }
        public required int Fitness { get; init; }
        public int Fatigue { get; init; } = 10;
        public int InjuryRisk { get; init; } = 12;
        public int Wage { get; init; } = 45000;
        public int ContractExpiryYear { get; init; } = 2028;
        public string ContractRole { get; init; } = "Squad Player";
        public string Relationship { get; init; } = "Professional";
        public string PromiseSummary { get; init; } = "No active promise.";
        public string TransferInterest { get; init; } = "No active interest.";
        public int TacticalFitScore { get; init; } = 65;
        public int PlayerFamiliarity { get; init; } = 55;
        public int ScoutingConfidence { get; init; } = 45;
        public string KnownAttributeGroups { get; init; } = "form,fitness";
        public string EstimatedAttributeGroups { get; init; } = "technical,tactical,physical,mental";
        public string UnknownAttributeGroups { get; init; } = "potential,personality depth,agent loyalty,pressure response";
        public required bool IsStarting { get; init; }

        public SquadPlayer With(
            int? trueAbility = null,
            int? technicalAttribute = null,
            int? tacticalAttribute = null,
            int? physicalAttribute = null,
            int? mentalAttribute = null,
            int? age = null,
            int? form = null,
            int? morale = null,
            int? fitness = null,
            int? fatigue = null,
            int? injuryRisk = null,
            int? tacticalFitScore = null,
            bool? isStarting = null,
            string? developmentCurve = null,
            string? relationship = null,
            string? promiseSummary = null,
            string? transferInterest = null,
            int? playerFamiliarity = null,
            int? scoutingConfidence = null)
        {
            return new SquadPlayer
            {
                PlayerId = PlayerId,
                Name = Name,
                Position = Position,
                Age = age ?? Age,
                Nationality = Nationality,
                TrueAbility = trueAbility ?? TrueAbility,
                TechnicalAttribute = technicalAttribute ?? TechnicalAttribute,
                TacticalAttribute = tacticalAttribute ?? TacticalAttribute,
                PhysicalAttribute = physicalAttribute ?? PhysicalAttribute,
                MentalAttribute = mentalAttribute ?? MentalAttribute,
                KnownAttributesSummary = KnownAttributesSummary,
                EstimatedAttributesSummary = EstimatedAttributesSummary,
                UnknownAttributesSummary = UnknownAttributesSummary,
                PlayingStyle = PlayingStyle,
                Tendencies = Tendencies,
                Traits = Traits,
                Personality = Personality,
                TacticalFit = TacticalFit,
                DevelopmentCurve = developmentCurve ?? DevelopmentCurve,
                Form = form ?? Form,
                Morale = morale ?? Morale,
                Fitness = fitness ?? Fitness,
                Fatigue = fatigue ?? Fatigue,
                InjuryRisk = injuryRisk ?? InjuryRisk,
                Wage = Wage,
                ContractExpiryYear = ContractExpiryYear,
                ContractRole = ContractRole,
                Relationship = relationship ?? Relationship,
                PromiseSummary = promiseSummary ?? PromiseSummary,
                TransferInterest = transferInterest ?? TransferInterest,
                TacticalFitScore = tacticalFitScore ?? TacticalFitScore,
                PlayerFamiliarity = playerFamiliarity ?? PlayerFamiliarity,
                ScoutingConfidence = scoutingConfidence ?? ScoutingConfidence,
                KnownAttributeGroups = KnownAttributeGroups,
                EstimatedAttributeGroups = EstimatedAttributeGroups,
                UnknownAttributeGroups = UnknownAttributeGroups,
                IsStarting = isStarting ?? IsStarting
            };
        }
    }

    public sealed class MatchReport
    {
        public required string FixtureLabel { get; init; }
        public required string Scoreline { get; init; }
        public required string ResultLabel { get; init; }
        public required string ConsequenceSummary { get; init; }
        public required string TableImpactSummary { get; init; }
        public required string TacticalSummary { get; init; }
        public required string PressureSummary { get; init; }
        public required string CauseSummary { get; init; }
        public required string StatsSummary { get; init; }
        public required string KeyPlayerMoments { get; init; }
        public required string TacticalExplanation { get; init; }
        public required string TacticalSection { get; init; }
        public required string PlayerFitSection { get; init; }
        public required string FatigueSection { get; init; }
        public required string MoraleSection { get; init; }
        public required string BoardReactionSection { get; init; }
        public required string FanReactionSection { get; init; }
        public required string MediaStorySection { get; init; }
        public required string StaffAnalysisSection { get; init; }
        public required string DevelopmentNotesSection { get; init; }
        public required string[] KeyEvents { get; init; }
        public required int MoraleDelta { get; init; }
        public required int FanDelta { get; init; }
        public required int BoardDelta { get; init; }
    }

    public sealed class ClubPreview
    {
        public required string ClubName { get; init; }
        public required string IdentitySummary { get; init; }
        public required string ExpectationSummary { get; init; }
        public required string ArchetypeName { get; init; }
        public required string BoardPhilosophyName { get; init; }
        public required string FanCultureName { get; init; }
        public required string DirectorOfFootballStyleName { get; init; }
        public required string DirectorRelationshipName { get; init; }
        public required string BudgetSummary { get; init; }
        public required string ObjectivesSummary { get; init; }
        public required string OpeningFixtureSummary { get; init; }
    }

    public sealed class CompetitionRow
    {
        public required string ClubName { get; init; }
        public required int Played { get; init; }
        public required int Won { get; init; }
        public required int Drawn { get; init; }
        public required int Lost { get; init; }
        public required int GoalsFor { get; init; }
        public required int GoalsAgainst { get; init; }
        public int GoalDifference => GoalsFor - GoalsAgainst;
        public required int Points { get; init; }
    }

    public sealed class CompetitionFixture
    {
        public required int Matchday { get; init; }
        public required string HomeClubName { get; init; }
        public required string AwayClubName { get; init; }
        public required bool IsComplete { get; init; }
        public required string Scoreline { get; init; }
        public required string ResultSummary { get; init; }
        public string CompetitionType { get; init; } = "League";
        public string CompetitionName { get; init; } = "Novara Premier Division";
        public string RoundName { get; init; } = "League Matchday";
    }

    public static GameState? Instance { get; private set; }

    public string ManagerName { get; private set; } = "Manager";
    public int CareerSeed { get; private set; }
    public CareerProfile CareerProfile { get; private set; } = CareerFoundation.CreateCareerProfile(
        "Manager",
        0,
        ManagerRole.Manager,
        ManagerBackground.UnknownUpstart,
        ManagerLicense.NationalCLicense);
    public Club? CurrentClub { get; private set; }
    public bool CareerInitialized { get; private set; }
    public int WorldSeed { get; private set; }
    public string CountryPackId { get; private set; } = "country-pack-alpha";
    public string[] AvailableClubs { get; private set; } = Array.Empty<string>();
    public string? SelectedClubName { get; private set; }
    public string NextFixtureSummary { get; private set; } = "Fixture context unavailable.";
    public string SquadStatusSummary { get; private set; } = "Squad status unavailable.";
    public SquadPlayer[] SquadPlayers { get; private set; } = Array.Empty<SquadPlayer>();
    public string TacticalFormation { get; private set; } = "4-3-3";
    public int PressIntensity { get; private set; } = 60;
    public int Tempo { get; private set; } = 58;
    public int Width { get; private set; } = 55;
    public int Risk { get; private set; } = 52;
    public string CompetitionName { get; private set; } = "Novara Premier Division";
    public int CurrentMatchday { get; private set; } = 1;
    public string CurrentOpponentName { get; private set; } = "Harbor County";
    public int TeamMorale { get; private set; } = 72;
    public int FanSentiment { get; private set; } = 63;
    public int BoardConfidence { get; private set; } = 61;
    public DateTime CurrentDate { get; private set; } = new(2026, 8, 3);
    public int SeasonStartYear { get; private set; } = 2026;
    public string FormSummary { get; private set; } = "Form: season about to begin.";
    public MatchReport? LastMatchReport { get; private set; }
    public string[] RecentResults => _recentResults.ToArray();
    public string SeasonLabel => $"{SeasonStartYear}/{((SeasonStartYear + 1) % 100):00}";
    public string CurrentDateLabel => CurrentDate.ToString("ddd d MMM yyyy");
    public string? SelectedPlayerProfileName { get; private set; }
    public CompetitionRow[] CompetitionTable { get; private set; } = Array.Empty<CompetitionRow>();
    public CompetitionFixture[] CompetitionFixtures { get; private set; } = Array.Empty<CompetitionFixture>();
    public MatchPlaybackResult? CurrentMatchResult { get; private set; }
    public string CurrentRoleName => CareerFoundation.GetDisplayName(CareerProfile.Role);
    public string ManagerBackgroundName => CareerFoundation.GetDisplayName(CareerProfile.Background);
    public string LicenseName => CareerFoundation.GetDisplayName(CareerProfile.License);
    public string ClubArchetypeName => CurrentClub == null ? "Club archetype unavailable" : CareerFoundation.GetDisplayName(CurrentClub.Archetype);
    public string BoardPhilosophyName => CurrentClub == null ? "Board philosophy unavailable" : CareerFoundation.GetDisplayName(CurrentClub.BoardPhilosophy);
    public string FanCultureName => CurrentClub == null ? "Fan culture unavailable" : CareerFoundation.GetDisplayName(CurrentClub.FanCulture);
    public string DirectorOfFootballStyleName => CurrentClub == null ? "Director style unavailable" : CareerFoundation.GetDisplayName(CurrentClub.DirectorOfFootballStyle);
    public string DirectorRelationshipName => CurrentClub == null ? "Director relationship unavailable" : CareerFoundation.GetDisplayName(CurrentClub.DirectorRelationshipState);
    public int BoardMorale => CurrentClub?.BoardMorale ?? BoardConfidence;
    public int FanMorale => CurrentClub?.FanMorale ?? FanSentiment;
    public int SquadMorale => CurrentClub?.SquadMorale ?? TeamMorale;
    public int JobPressure => CurrentClub?.JobPressure ?? CareerFoundation.CalculateJobPressure(
        ClubArchetype.MidTableStabilizer,
        BoardPhilosophy.PatientLongTermBoard,
        CareerProfile.Role,
        CareerProfile.Background,
        CareerProfile.License,
        BoardConfidence,
        FanSentiment,
        TeamMorale);
    public string RoleAuthoritySummary => CareerFoundation.GetRoleAuthoritySummary(CareerProfile.Role);
    public string MainObjectivesSummary => CareerFoundation.BuildObjectivesSummary(CurrentClub);
    public string StaffSummary => CareerFoundation.BuildStaffSummary(CurrentClub);
    public string NewsFeedSummary => CareerFoundation.BuildNewsSummary(CurrentClub);
    public string BudgetSummary => CareerFoundation.BuildBudgetSummary(CurrentClub);
    public string CurrentFixtureCompetitionName => GetCurrentClubFixture()?.CompetitionName ?? CompetitionName;
    public string CurrentFixtureRoundName => GetCurrentClubFixture()?.RoundName ?? $"Matchday {CurrentMatchday}";
    public bool CurrentFixtureIsCup => IsCupFixture(GetCurrentClubFixture());

    public override void _EnterTree()
    {
        Instance = this;
    }

    public override void _ExitTree()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void StartNewCareer(string managerName, int seed)
    {
        TouchlineWorldGenerator.Instance?.BeginNewCareer(managerName, seed);
    }

    public void ApplyCareerBootstrap(CareerBootstrapState bootstrap)
    {
        ManagerName = bootstrap.ManagerName;
        CareerSeed = bootstrap.CareerSeed;
        CareerProfile = CareerFoundation.CreateCareerProfile(
            bootstrap.ManagerName,
            bootstrap.CareerSeed,
            bootstrap.Role,
            bootstrap.Background,
            bootstrap.License);
        CurrentClub = null;
        CareerInitialized = true;
        WorldSeed = bootstrap.WorldSeed;
        CountryPackId = bootstrap.CountryPackId;
        AvailableClubs = bootstrap.AvailableClubs;
        SelectedClubName = null;
        NextFixtureSummary = "Select a club to view the opening fixture.";
        SquadPlayers = Array.Empty<SquadPlayer>();
        TacticalFormation = bootstrap.TacticalFormation;
        PressIntensity = bootstrap.PressIntensity;
        Tempo = bootstrap.Tempo;
        Width = bootstrap.Width;
        Risk = bootstrap.Risk;
        CompetitionName = bootstrap.CompetitionName;
        CurrentMatchday = 1;
        CurrentOpponentName = "Opponent unavailable";
        TeamMorale = bootstrap.TeamMorale;
        FanSentiment = bootstrap.FanSentiment;
        BoardConfidence = bootstrap.BoardConfidence;
        CurrentDate = bootstrap.CurrentDate;
        SeasonStartYear = bootstrap.SeasonStartYear;
        FormSummary = bootstrap.FormSummary;
        _recentResults.Clear();
        LastMatchReport = null;
        SelectedPlayerProfileName = null;
        CompetitionTable = Array.Empty<CompetitionRow>();
        CompetitionFixtures = Array.Empty<CompetitionFixture>();
        CurrentMatchResult = null;
        ResetStageFoundations();
        SquadStatusSummary = BuildSquadStatusSummary();
    }

    public void SelectClub(string clubName)
    {
        TouchlineWorldGenerator.Instance?.SelectClub(clubName);
    }

    public void ApplyClubSelection(ClubSelectionState selection)
    {
        SelectedClubName = selection.ClubName;
        CurrentClub = selection.Club;
        CareerProfile.CurrentClubName = selection.ClubName;
        CompetitionName = selection.CompetitionName;
        CurrentMatchday = selection.CurrentMatchday;
        TeamMorale = selection.TeamMorale;
        FanSentiment = selection.FanSentiment;
        BoardConfidence = selection.BoardConfidence;
        LastMatchReport = null;
        SelectedPlayerProfileName = null;
        CurrentMatchResult = null;
        SquadPlayers = selection.SquadPlayers;
        CompetitionTable = selection.CompetitionTable;
        CompetitionFixtures = selection.CompetitionFixtures;
        CurrentOpponentName = selection.CurrentOpponentName;
        NextFixtureSummary = selection.NextFixtureSummary;
        SyncCurrentClubMoraleFromRuntime();
        InitializeStageFoundationsForClub();
        SquadStatusSummary = BuildSquadStatusSummary();
    }

    public void UpdateTactics(string formation, int pressIntensity, int tempo, int width, int risk)
    {
        var previousFormation = TacticalFormation;
        var previousStyle = TeamStyle;
        TacticalFormation = formation;
        PressIntensity = pressIntensity;
        Tempo = tempo;
        Width = width;
        Risk = risk;
        RefreshTacticFoundation(previousFormation, previousStyle);
    }

    public void AdvanceDate()
    {
        TouchlineCalendarSystem.Instance?.AdvanceCareerDate();
    }

    public void ApplyCalendarAdvance(CalendarAdvanceState advance)
    {
        CurrentDate = advance.CurrentDate;
        SeasonStartYear = advance.SeasonStartYear;
        CurrentMatchday = advance.CurrentMatchday;
        FormSummary = advance.FormSummary;
        SquadPlayers = advance.SquadPlayers;
        CompetitionTable = advance.CompetitionTable;
        CompetitionFixtures = advance.CompetitionFixtures;
        CurrentOpponentName = advance.CurrentOpponentName;
        NextFixtureSummary = advance.NextFixtureSummary;
        SquadStatusSummary = BuildSquadStatusSummary();

        if (advance.ResetRecentResults)
        {
            _recentResults.Clear();
        }

        LastMatchReport = null;
        CurrentMatchResult = null;
        ApplyWeeklyFoundationProgress();
        if (advance.ResetRecentResults)
        {
            RecordSeasonDevelopmentSnapshot();
        }
    }

    public MatchPlaybackResult PrepareCurrentMatchResult(bool forceNew = false)
    {
        if (!forceNew && CurrentMatchResult != null)
        {
            return CurrentMatchResult;
        }

        CurrentMatchResult = MatchSimulator.Simulate(this);
        return CurrentMatchResult;
    }

    public void ResolveCurrentMatchInstantly()
    {
        var result = PrepareCurrentMatchResult();
        ApplyMatchResult(result);
    }

    public bool IsCurrentClubFixtureComplete()
    {
        var fixture = GetCurrentClubFixture();
        return fixture?.IsComplete ?? false;
    }

    public string BuildCareerPhaseSummary()
    {
        if (!CareerInitialized)
        {
            return "Career inactive: start or load a career.";
        }

        if (string.IsNullOrWhiteSpace(SelectedClubName))
        {
            return "Club selection pending: choose a club to enter the season loop.";
        }

        var currentFixture = GetCurrentClubFixture();
        if (LastMatchReport != null)
        {
            return $"Post-match review: {LastMatchReport.Scoreline} logged. Continue to advance the calendar.";
        }

        if (currentFixture == null)
        {
            return $"Season transition: {SeasonLabel} has no active fixture for matchday {CurrentMatchday}.";
        }

        if (currentFixture.IsComplete)
        {
            return $"Between matches: matchday {CurrentMatchday} result is recorded. Advance to the next fixture.";
        }

        if (CurrentMatchday == 1 && CountCompletedFixtures() == 0)
        {
            return $"New season opener: {SeasonLabel} begins on {CurrentDateLabel} against {CurrentOpponentName}.";
        }

        return $"Ready for matchday: {CurrentDateLabel}, matchday {CurrentMatchday} vs {CurrentOpponentName}.";
    }

    public string BuildLeaguePositionSummary()
    {
        if (string.IsNullOrWhiteSpace(SelectedClubName))
        {
            return "League position unavailable until a club is selected.";
        }

        var row = GetCompetitionRow(SelectedClubName);
        var position = GetClubTablePosition(SelectedClubName);
        if (row == null || position <= 0)
        {
            return "League position unavailable.";
        }

        return $"League position: {position}/{CompetitionTable.Length} | {row.Points} pts | GD {FormatSignedDelta(row.GoalDifference)} | {row.Played} played";
    }

    public string BuildRecentResultsSummary()
    {
        if (_recentResults.Count == 0)
        {
            return "Recent results: no completed matches yet.";
        }

        return $"Recent results: {string.Join(" ", _recentResults)}";
    }

    public string BuildLineupReadinessSummary()
    {
        var starters = 0;
        var bench = 0;
        var totalFitness = 0;
        var totalForm = 0;
        foreach (var player in SquadPlayers)
        {
            if (player.IsStarting)
            {
                starters++;
                totalFitness += player.Fitness;
                totalForm += player.Form;
            }
            else
            {
                bench++;
            }
        }

        var averageFitness = starters == 0 ? 0 : totalFitness / starters;
        var averageForm = starters == 0 ? 0 : totalForm / starters;
        var readiness = starters >= 11 ? "XI ready" : $"XI incomplete ({starters}/11)";
        return $"Lineup readiness: {readiness} | avg XI fitness {averageFitness} | avg XI form {averageForm} | bench {bench}";
    }

    public string BuildTacticalPlanSummary()
    {
        return $"Tactical setup: {TacticalFormation} | style {TeamStyleName} | familiarity {TacticalFamiliarityName} | role fit {TacticalRoleFitScore}/100 | set pieces {SetPieceApproachName} | opponent prep {OpponentPreparationFocusName} | pressing {PressIntensity} | tempo {Tempo} | passing directness {PassingDirectness} | defensive line {DefensiveLine} | width {Width} | attacking risk {Risk} | tackling {Tackling}";
    }

    public string BuildOpponentContextSummary()
    {
        if (string.IsNullOrWhiteSpace(CurrentOpponentName))
        {
            return "Opponent context unavailable.";
        }

        var opponentRow = GetCompetitionRow(CurrentOpponentName);
        var opponentPosition = GetClubTablePosition(CurrentOpponentName);
        var opponentSquad = GetClubSquad(CurrentOpponentName);
        var starters = 0;
        var totalFitness = 0;
        var totalForm = 0;
        foreach (var player in opponentSquad)
        {
            if (!player.IsStarting)
            {
                continue;
            }

            starters++;
            totalFitness += player.Fitness;
            totalForm += player.Form;
        }

        var averageFitness = starters == 0 ? 0 : totalFitness / starters;
        var averageForm = starters == 0 ? 0 : totalForm / starters;
        var tableLine = opponentRow == null || opponentPosition <= 0
            ? "table line unavailable"
            : $"position {opponentPosition}/{CompetitionTable.Length}, {opponentRow.Points} pts, GD {FormatSignedDelta(opponentRow.GoalDifference)}";
        return $"Opponent context: {CurrentOpponentName} | {tableLine} | seeded XI {starters} | avg form {averageForm} | avg fitness {averageFitness}";
    }

    public string ValidateCurrentMatchPlaybackContract()
    {
        return MatchPlaybackContractValidator.Validate(PrepareCurrentMatchResult(true));
    }

    public string ValidateOpponentSquadSourcing()
    {
        var firstPlayback = PrepareCurrentMatchResult(true);
        var firstAwayNames = ExtractTeamNames(firstPlayback, CurrentOpponentName);
        var expectedSquad = GetClubSquad(CurrentOpponentName);

        if (firstAwayNames.Length != 11)
        {
            return $"Expected 11 away player states, found {firstAwayNames.Length}.";
        }

        if (ContainsOldHardcodedAwayName(firstAwayNames))
        {
            return "Opponent lineup still contains the old hardcoded away XI.";
        }

        if (!ContainsSeededOpponentName(firstAwayNames, expectedSquad))
        {
            return "Opponent lineup did not source names from the resolved club squad.";
        }

        var secondPlayback = PrepareCurrentMatchResult(true);
        var secondAwayNames = ExtractTeamNames(secondPlayback, CurrentOpponentName);
        if (!StringArraysMatch(firstAwayNames, secondAwayNames))
        {
            return "Opponent lineup is not stable for the same seed and opponent.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateMatchVariationContract()
    {
        var originalFormation = TacticalFormation;
        var originalPress = PressIntensity;
        var originalTempo = Tempo;
        var originalWidth = Width;
        var originalRisk = Risk;

        var baseline = PrepareCurrentMatchResult(true);
        var kindCount = CountDistinctActionKinds(baseline);
        if (kindCount < 7)
        {
            return $"Expected at least 7 action kinds, found {kindCount}.";
        }

        if (CountDistinctPassLanes(baseline) < 3)
        {
            return "Expected at least three distinct pass lanes across the match.";
        }

        UpdateTactics(originalFormation, 85, 78, 72, 76);
        var aggressive = PrepareCurrentMatchResult(true);
        UpdateTactics(originalFormation, 35, 42, 38, 35);
        var conservative = PrepareCurrentMatchResult(true);
        UpdateTactics(originalFormation, originalPress, originalTempo, originalWidth, originalRisk);
        CurrentMatchResult = null;

        if (BuildActionSignature(aggressive) == BuildActionSignature(conservative))
        {
            return "Different tactical inputs produced the same action signature.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePostMatchCauseContract()
    {
        var result = PrepareCurrentMatchResult(true);
        ApplyMatchResult(result);

        if (LastMatchReport == null)
        {
            return "Post-match report was not created.";
        }

        if (string.IsNullOrWhiteSpace(LastMatchReport.CauseSummary))
        {
            return "Post-match report is missing cause summary.";
        }

        if (!LastMatchReport.ConsequenceSummary.Contains("Cause:", StringComparison.Ordinal))
        {
            return "Post-match consequence summary does not include playback cause reasoning.";
        }

        if (LastMatchReport.CauseSummary.Contains("scoreline", StringComparison.OrdinalIgnoreCase) &&
            LastMatchReport.CauseSummary.Split(';', StringSplitOptions.RemoveEmptyEntries).Length < 2)
        {
            return "Post-match cause reasoning is still scoreline-only.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateActionParticipantsContract()
    {
        return MatchPlaybackContractValidator.ValidateActionParticipants(PrepareCurrentMatchResult(true));
    }

    public string ValidateMatchStatsContract()
    {
        return MatchPlaybackContractValidator.ValidateMatchStats(PrepareCurrentMatchResult(true));
    }

    public string ValidatePostMatchReportContract()
    {
        var result = PrepareCurrentMatchResult(true);
        ApplyMatchResult(result);

        if (LastMatchReport == null)
        {
            return "Post-match report was not created.";
        }

        if (string.IsNullOrWhiteSpace(LastMatchReport.StatsSummary) ||
            !LastMatchReport.StatsSummary.Contains("Shots:", StringComparison.Ordinal) ||
            !LastMatchReport.StatsSummary.Contains("Saves:", StringComparison.Ordinal))
        {
            return "Post-match report is missing stats summary.";
        }

        if (string.IsNullOrWhiteSpace(LastMatchReport.KeyPlayerMoments))
        {
            return "Post-match report is missing key player moments.";
        }

        if (string.IsNullOrWhiteSpace(LastMatchReport.TacticalExplanation))
        {
            return "Post-match report is missing tactical explanation.";
        }

        if (string.IsNullOrWhiteSpace(LastMatchReport.CauseSummary))
        {
            return "Post-match report is missing cause summary.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateMatchdayProgressionContract()
    {
        var matchdayBefore = CurrentMatchday;
        var opponentBefore = CurrentOpponentName;
        var completedBefore = CountCompletedFixtures();
        var openFixturesInRound = CountOpenFixturesForMatchday(matchdayBefore);
        var rowBefore = GetCompetitionRow(SelectedClubName ?? string.Empty);
        if (rowBefore == null)
        {
            return "Selected club row is unavailable before match progression.";
        }

        var result = PrepareCurrentMatchResult(true);
        ApplyMatchResult(result);
        if (LastMatchReport == null)
        {
            return "Match progression did not create a post-match report.";
        }

        if (!IsCurrentClubFixtureComplete())
        {
            return "Resolved match did not mark the current club fixture complete.";
        }

        var completedAfter = CountCompletedFixtures();
        if (completedAfter != completedBefore + openFixturesInRound)
        {
            return $"Expected completed fixture count to increase by {openFixturesInRound}, moved from {completedBefore} to {completedAfter}.";
        }

        var rowAfter = GetCompetitionRow(SelectedClubName ?? string.Empty);
        if (rowAfter == null || rowAfter.Played != rowBefore.Played + 1)
        {
            return "Selected club table row did not advance by exactly one match.";
        }

        var recentAfter = _recentResults.Count;
        ApplyMatchResult(result);
        var rowAfterReplay = GetCompetitionRow(SelectedClubName ?? string.Empty);
        if (CountCompletedFixtures() != completedAfter ||
            rowAfterReplay == null ||
            rowAfterReplay.Played != rowAfter.Played ||
            _recentResults.Count != recentAfter)
        {
            return "Reapplying a completed match changed career state again.";
        }

        if (TouchlineCalendarSystem.Instance == null || !TouchlineCalendarSystem.Instance.AdvanceCareerDate())
        {
            return TouchlineCalendarSystem.Instance?.LastStatusMessage ?? "Calendar system unavailable during progression validation.";
        }

        if (CurrentMatchday != matchdayBefore + 1)
        {
            return $"Expected matchday {matchdayBefore + 1} after calendar advance, found {CurrentMatchday}.";
        }

        if (CurrentOpponentName == opponentBefore)
        {
            return "Next opponent did not change after advancing to the next match context.";
        }

        if (LastMatchReport != null)
        {
            return "Post-match report was not cleared after advancing to the next match context.";
        }

        if (IsCurrentClubFixtureComplete())
        {
            return "Next match context points at an already completed fixture.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidatePlayerConditionContract()
    {
        var before = SquadPlayers;
        var result = PrepareCurrentMatchResult(true);
        ApplyMatchResult(result);

        if (SquadPlayers.Length != before.Length)
        {
            return "Post-match player update changed squad size.";
        }

        var sawStarterFitnessDrop = false;
        var sawAnyPlayerStateChange = false;
        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            var previous = before[index];
            var current = SquadPlayers[index];
            if (!IsPlayerValueInBounds(current))
            {
                return $"Player state moved out of bounds for {current.Name}.";
            }

            if (previous.IsStarting && current.Fitness < previous.Fitness)
            {
                sawStarterFitnessDrop = true;
            }

            if (!previous.IsStarting && current.Fitness < previous.Fitness)
            {
                return $"Non-starter {current.Name} lost fitness despite not starting.";
            }

            if (current.Fitness != previous.Fitness || current.Form != previous.Form || current.Morale != previous.Morale)
            {
                sawAnyPlayerStateChange = true;
            }
        }

        if (!sawStarterFitnessDrop)
        {
            return "No starting player lost fitness after the match.";
        }

        if (!sawAnyPlayerStateChange)
        {
            return "Post-match player condition update did not change any player state.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateMultiMatchRegressionContract()
    {
        var firstMatchday = CurrentMatchday;
        var firstCompletedBefore = CountCompletedFixtures();
        ApplyMatchResult(PrepareCurrentMatchResult(true));
        if (CountCompletedFixtures() <= firstCompletedBefore)
        {
            return "First match did not complete fixtures.";
        }

        if (TouchlineCalendarSystem.Instance == null || !TouchlineCalendarSystem.Instance.AdvanceCareerDate())
        {
            return TouchlineCalendarSystem.Instance?.LastStatusMessage ?? "Calendar system unavailable during multi-match validation.";
        }

        if (CurrentMatchday != firstMatchday + 1)
        {
            return "Calendar did not advance to the second matchday.";
        }

        var secondMatchday = CurrentMatchday;
        ApplyMatchResult(PrepareCurrentMatchResult(true));
        if (CurrentMatchday != secondMatchday)
        {
            return "Resolving the second match changed matchday before calendar advance.";
        }

        if (LastMatchReport == null)
        {
            return "Second match did not leave a post-match report.";
        }

        var expectedDate = CurrentDate;
        var expectedMatchday = CurrentMatchday;
        var expectedCompletedFixtures = CountCompletedFixtures();
        var expectedOpponent = CurrentOpponentName;
        var expectedNextFixtureSummary = NextFixtureSummary;
        var expectedReportScoreline = LastMatchReport.Scoreline;
        var expectedFirstPlayer = SquadPlayers.Length == 0 ? null : SquadPlayers[0];
        var expectedRow = GetCompetitionRow(SelectedClubName ?? string.Empty);
        if (expectedFirstPlayer == null || expectedRow == null)
        {
            return "Expected save/load comparison state is unavailable.";
        }

        if (SaveSystem.Instance == null)
        {
            return "Save system unavailable during multi-match validation.";
        }

        if (!SaveSystem.Instance.SaveGame(out var saveStatus))
        {
            return saveStatus;
        }

        var mutationClub = ResolveDifferentClub(SelectedClubName);
        if (TouchlineWorldGenerator.Instance != null && !string.IsNullOrWhiteSpace(mutationClub))
        {
            TouchlineWorldGenerator.Instance.BeginNewCareer("Regression Mutation", CareerSeed + 909);
            TouchlineWorldGenerator.Instance.SelectClub(mutationClub);
        }

        if (!SaveSystem.Instance.LoadGame(out var loadStatus))
        {
            return loadStatus;
        }

        if (CurrentDate != expectedDate ||
            CurrentMatchday != expectedMatchday ||
            CurrentOpponentName != expectedOpponent ||
            NextFixtureSummary != expectedNextFixtureSummary ||
            CountCompletedFixtures() != expectedCompletedFixtures)
        {
            return "Save/load did not preserve date, matchday, opponent, fixture summary, and completion count after multiple matches.";
        }

        if (LastMatchReport == null || LastMatchReport.Scoreline != expectedReportScoreline)
        {
            return "Save/load did not preserve the latest match report.";
        }

        var loadedRow = GetCompetitionRow(SelectedClubName ?? string.Empty);
        if (loadedRow == null ||
            loadedRow.Played != expectedRow.Played ||
            loadedRow.Points != expectedRow.Points ||
            loadedRow.GoalsFor != expectedRow.GoalsFor ||
            loadedRow.GoalsAgainst != expectedRow.GoalsAgainst)
        {
            return "Save/load did not preserve the selected club table row after multiple matches.";
        }

        if (SquadPlayers.Length == 0 ||
            SquadPlayers[0].Name != expectedFirstPlayer.Name ||
            SquadPlayers[0].Fitness != expectedFirstPlayer.Fitness ||
            SquadPlayers[0].Form != expectedFirstPlayer.Form ||
            SquadPlayers[0].Morale != expectedFirstPlayer.Morale)
        {
            return "Save/load did not preserve squad player condition after match progression.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateSeasonRolloverContract()
    {
        var startingSeasonYear = SeasonStartYear;
        var selectedClub = SelectedClubName;
        var startingSquadLength = SquadPlayers.Length;
        var startingFirstPlayerAge = SquadPlayers.Length == 0 ? -1 : SquadPlayers[0].Age;
        var startingFixtureCount = CompetitionFixtures.Length;
        var rolloverMessage = CompleteCurrentSeason();
        if (rolloverMessage != MatchPlaybackContractValidator.PassMessage)
        {
            return rolloverMessage;
        }

        if (SeasonStartYear != startingSeasonYear + 1)
        {
            return $"Expected season year {startingSeasonYear + 1}, found {SeasonStartYear}.";
        }

        if (CurrentMatchday != 1)
        {
            return $"Expected matchday 1 after rollover, found {CurrentMatchday}.";
        }

        if (SelectedClubName != selectedClub)
        {
            return "Selected club did not persist across season rollover.";
        }

        if (SquadPlayers.Length != startingSquadLength)
        {
            return "Squad size changed across season rollover.";
        }

        if (startingFirstPlayerAge >= 0 && SquadPlayers[0].Age != startingFirstPlayerAge + 1)
        {
            return "Players did not age by one year at season rollover.";
        }

        if (LastMatchReport != null)
        {
            return "Last match report was not cleared for the new season.";
        }

        if (CompetitionFixtures.Length != startingFixtureCount || CompetitionFixtures.Length == 0)
        {
            return "New-season fixture list was not regenerated with the expected size.";
        }

        if (CountCompletedFixtures() != 0)
        {
            return "New-season fixtures were not reset to open state.";
        }

        foreach (var row in CompetitionTable)
        {
            if (row.Played != 0 || row.Points != 0 || row.GoalsFor != 0 || row.GoalsAgainst != 0)
            {
                return "New-season standings were not reset.";
            }
        }

        if (IsCurrentClubFixtureComplete())
        {
            return "New-season current fixture is already complete.";
        }

        if (!NextFixtureSummary.Contains("Matchday 1", StringComparison.Ordinal))
        {
            return "New-season fixture summary does not point at matchday 1.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateSeasonDevelopmentContract()
    {
        var before = SquadPlayers;
        if (before.Length == 0)
        {
            return "No squad players available for season development validation.";
        }

        var rolloverMessage = CompleteCurrentSeason();
        if (rolloverMessage != MatchPlaybackContractValidator.PassMessage)
        {
            return rolloverMessage;
        }

        var sawValueChange = false;
        for (var index = 0; index < before.Length; index++)
        {
            var previous = before[index];
            var current = SquadPlayers[index];
            if (current.Name != previous.Name)
            {
                return "Squad player order or identity changed across season development.";
            }

            if (current.Age != previous.Age + 1)
            {
                return $"{current.Name} did not age by one year.";
            }

            if (!IsPlayerValueInBounds(current))
            {
                return $"Player state moved out of bounds for {current.Name}.";
            }

            if (current.Form != previous.Form || current.Morale != previous.Morale || current.Fitness != previous.Fitness)
            {
                sawValueChange = true;
            }
        }

        if (!sawValueChange)
        {
            return "Season development did not change any player values.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public string ValidateFullSeasonRegressionContract()
    {
        var rolloverMessage = CompleteCurrentSeason();
        if (rolloverMessage != MatchPlaybackContractValidator.PassMessage)
        {
            return rolloverMessage;
        }

        var expectedSeasonYear = SeasonStartYear;
        var expectedDate = CurrentDate;
        var expectedMatchday = CurrentMatchday;
        var expectedSelectedClub = SelectedClubName;
        var expectedOpponent = CurrentOpponentName;
        var expectedNextFixtureSummary = NextFixtureSummary;
        var expectedFixtureCount = CompetitionFixtures.Length;
        var expectedFirstPlayer = SquadPlayers.Length == 0 ? null : SquadPlayers[0];
        if (expectedFirstPlayer == null)
        {
            return "Expected squad state is unavailable after season rollover.";
        }

        if (SaveSystem.Instance == null)
        {
            return "Save system unavailable during full-season regression validation.";
        }

        if (!SaveSystem.Instance.SaveGame(out var saveStatus))
        {
            return saveStatus;
        }

        var mutationClub = ResolveDifferentClub(SelectedClubName);
        if (TouchlineWorldGenerator.Instance != null && !string.IsNullOrWhiteSpace(mutationClub))
        {
            TouchlineWorldGenerator.Instance.BeginNewCareer("Full Season Mutation", CareerSeed + 1200);
            TouchlineWorldGenerator.Instance.SelectClub(mutationClub);
        }

        if (!SaveSystem.Instance.LoadGame(out var loadStatus))
        {
            return loadStatus;
        }

        if (SeasonStartYear != expectedSeasonYear ||
            CurrentDate != expectedDate ||
            CurrentMatchday != expectedMatchday ||
            SelectedClubName != expectedSelectedClub ||
            CurrentOpponentName != expectedOpponent ||
            NextFixtureSummary != expectedNextFixtureSummary)
        {
            return "Save/load did not preserve new-season timeline and next-opponent context.";
        }

        if (CompetitionFixtures.Length != expectedFixtureCount || CountCompletedFixtures() != 0)
        {
            return "Save/load did not preserve reset new-season fixtures.";
        }

        foreach (var row in CompetitionTable)
        {
            if (row.Played != 0 || row.Points != 0 || row.GoalsFor != 0 || row.GoalsAgainst != 0)
            {
                return "Save/load did not preserve reset new-season standings.";
            }
        }

        if (LastMatchReport != null)
        {
            return "Save/load restored a stale last match report after season rollover.";
        }

        if (SquadPlayers.Length == 0 ||
            SquadPlayers[0].Name != expectedFirstPlayer.Name ||
            SquadPlayers[0].Age != expectedFirstPlayer.Age ||
            SquadPlayers[0].Fitness != expectedFirstPlayer.Fitness ||
            SquadPlayers[0].Form != expectedFirstPlayer.Form ||
            SquadPlayers[0].Morale != expectedFirstPlayer.Morale)
        {
            return "Save/load did not preserve post-rollover squad development state.";
        }

        return MatchPlaybackContractValidator.PassMessage;
    }

    public void ApplyMatchResult(MatchPlaybackResult result)
    {
        if (IsCurrentClubFixtureComplete())
        {
            CurrentMatchResult = null;
            return;
        }

        var activeFixture = GetCurrentClubFixture();
        var isCupFixture = IsCupFixture(activeFixture);
        CurrentMatchResult = result;
        var goalDifference = result.FinalHomeScore - result.FinalAwayScore;
        var previousPosition = GetClubTablePosition(SelectedClubName ?? string.Empty);
        var consequence = PostMatchConsequenceService.Evaluate(result, this);

        TeamMorale = Math.Clamp(TeamMorale + consequence.MoraleDelta, 0, 100);
        FanSentiment = Math.Clamp(FanSentiment + consequence.FanDelta, 0, 100);
        BoardConfidence = Math.Clamp(BoardConfidence + consequence.BoardDelta, 0, 100);
        SyncCurrentClubMoraleFromRuntime();
        if (!string.IsNullOrWhiteSpace(SelectedClubName))
        {
            SquadPlayers = DevelopmentSystem.ApplyPostMatchChanges(SquadPlayers, SelectedClubName, result);
        }

        SquadStatusSummary = BuildSquadStatusSummary();
        UpdateFormSummary(goalDifference);
        ApplyStageFoundationPostMatch(result, consequence);

        RecordCompetitionResults(result.FinalHomeScore, result.FinalAwayScore);
        RefreshFixtureContext();
        var currentPosition = GetClubTablePosition(SelectedClubName ?? string.Empty);
        var tableImpactSummary = isCupFixture && activeFixture != null
            ? ApplyCupMatchResult(activeFixture, result)
            : BuildTableImpactSummary(previousPosition, currentPosition);
        RefreshFixtureContext();

        LastMatchReport = new MatchReport
        {
            FixtureLabel = isCupFixture && activeFixture != null
                ? $"{activeFixture.CompetitionName} {activeFixture.RoundName}: {result.HomeClubName} vs {result.AwayClubName}"
                : $"{result.HomeClubName} vs {result.AwayClubName}",
            Scoreline = $"{result.FinalHomeScore} - {result.FinalAwayScore}",
            ResultLabel = consequence.ResultLabel,
            ConsequenceSummary = consequence.ConsequenceSummary,
            TableImpactSummary = tableImpactSummary,
            TacticalSummary = result.TacticalSummary,
            PressureSummary = consequence.PressureSummary,
            CauseSummary = consequence.CauseSummary,
            StatsSummary = consequence.StatsSummary,
            KeyPlayerMoments = consequence.KeyPlayerMoments,
            TacticalExplanation = consequence.TacticalExplanation,
            TacticalSection = consequence.TacticalSection,
            PlayerFitSection = consequence.PlayerFitSection,
            FatigueSection = consequence.FatigueSection,
            MoraleSection = consequence.MoraleSection,
            BoardReactionSection = consequence.BoardReactionSection,
            FanReactionSection = consequence.FanReactionSection,
            MediaStorySection = consequence.MediaStorySection,
            StaffAnalysisSection = consequence.StaffAnalysisSection,
            DevelopmentNotesSection = consequence.DevelopmentNotesSection,
            KeyEvents = consequence.KeyEvents,
            MoraleDelta = consequence.MoraleDelta,
            FanDelta = consequence.FanDelta,
            BoardDelta = consequence.BoardDelta
        };
        CurrentMatchResult = null;
    }

    public void RestoreFromSave(SaveSlotData data)
    {
        ManagerName = data.ManagerName;
        CareerSeed = data.CareerSeed;
        CareerProfile = RestoreCareerProfile(data.CareerProfile, data.ManagerName, data.CareerSeed, data.SelectedClubName);
        CareerInitialized = data.CareerInitialized;
        WorldSeed = data.WorldSeed;
        CountryPackId = data.CountryPackId;
        AvailableClubs = data.AvailableClubs ?? Array.Empty<string>();
        SelectedClubName = data.SelectedClubName;
        NextFixtureSummary = data.NextFixtureSummary;
        SquadStatusSummary = data.SquadStatusSummary;
        TacticalFormation = data.TacticalFormation;
        PressIntensity = data.PressIntensity;
        Tempo = data.Tempo;
        Width = data.Width;
        Risk = data.Risk;
        CompetitionName = data.CompetitionName;
        CurrentMatchday = data.CurrentMatchday;
        CurrentOpponentName = data.CurrentOpponentName;
        TeamMorale = data.TeamMorale;
        FanSentiment = data.FanSentiment;
        BoardConfidence = data.BoardConfidence;
        CurrentClub = RestoreClubFoundation(data.CurrentClub);
        CurrentDate = DateTime.Parse(data.CurrentDateIso);
        SeasonStartYear = data.SeasonStartYear;
        FormSummary = data.FormSummary;

        _recentResults.Clear();
        if (data.RecentResults != null)
        {
            _recentResults.AddRange(data.RecentResults);
        }

        SquadPlayers = Array.ConvertAll(
            data.SquadPlayers ?? Array.Empty<SaveSlotPlayerData>(),
            player => new SquadPlayer
            {
                PlayerId = player.PlayerId,
                Name = player.Name,
                Position = player.Position,
                Age = player.Age,
                Nationality = string.IsNullOrWhiteSpace(player.Nationality) ? "Novaran" : player.Nationality,
                TrueAbility = player.TrueAbility <= 0 ? 65 : player.TrueAbility,
                TechnicalAttribute = player.TechnicalAttribute <= 0 ? 65 : player.TechnicalAttribute,
                TacticalAttribute = player.TacticalAttribute <= 0 ? 65 : player.TacticalAttribute,
                PhysicalAttribute = player.PhysicalAttribute <= 0 ? 65 : player.PhysicalAttribute,
                MentalAttribute = player.MentalAttribute <= 0 ? 65 : player.MentalAttribute,
                KnownAttributesSummary = string.IsNullOrWhiteSpace(player.KnownAttributesSummary) ? "Known: Form and fitness only." : player.KnownAttributesSummary,
                EstimatedAttributesSummary = string.IsNullOrWhiteSpace(player.EstimatedAttributesSummary) ? "Estimated: Tactical ?-?, Mental ?-?" : player.EstimatedAttributesSummary,
                UnknownAttributesSummary = string.IsNullOrWhiteSpace(player.UnknownAttributesSummary) ? "Unknown: Potential ?, personality depth ?" : player.UnknownAttributesSummary,
                PlayingStyle = string.IsNullOrWhiteSpace(player.PlayingStyle) ? "Balanced player" : player.PlayingStyle,
                Tendencies = string.IsNullOrWhiteSpace(player.Tendencies) ? "Keeps role discipline." : player.Tendencies,
                Traits = string.IsNullOrWhiteSpace(player.Traits) ? "role discipline" : player.Traits,
                Personality = string.IsNullOrWhiteSpace(player.Personality) ? "Professional" : player.Personality,
                TacticalFit = string.IsNullOrWhiteSpace(player.TacticalFit) ? "Partial fit: role comfort needs observation." : player.TacticalFit,
                DevelopmentCurve = string.IsNullOrWhiteSpace(player.DevelopmentCurve) ? "Growth curve: stable." : player.DevelopmentCurve,
                Form = player.Form,
                Morale = player.Morale,
                Fitness = player.Fitness,
                Fatigue = player.Fatigue,
                InjuryRisk = player.InjuryRisk <= 0 ? 12 : player.InjuryRisk,
                Wage = player.Wage <= 0 ? 45000 : player.Wage,
                ContractExpiryYear = player.ContractExpiryYear <= 0 ? 2028 : player.ContractExpiryYear,
                ContractRole = string.IsNullOrWhiteSpace(player.ContractRole) ? "Squad Player" : player.ContractRole,
                Relationship = string.IsNullOrWhiteSpace(player.Relationship) ? "Professional" : player.Relationship,
                PromiseSummary = string.IsNullOrWhiteSpace(player.PromiseSummary) ? "No active promise." : player.PromiseSummary,
                TransferInterest = string.IsNullOrWhiteSpace(player.TransferInterest) ? "No active interest." : player.TransferInterest,
                TacticalFitScore = player.TacticalFitScore <= 0 ? 65 : player.TacticalFitScore,
                PlayerFamiliarity = Math.Clamp(player.PlayerFamiliarity <= 0 ? (player.IsStarting ? 68 : 48) : player.PlayerFamiliarity, 0, 100),
                ScoutingConfidence = Math.Clamp(player.ScoutingConfidence <= 0 ? (player.IsStarting ? 60 : 45) : player.ScoutingConfidence, 0, 100),
                KnownAttributeGroups = string.IsNullOrWhiteSpace(player.KnownAttributeGroups)
                    ? (player.IsStarting ? "form,fitness,technical,physical,current role" : "form,fitness,current role")
                    : player.KnownAttributeGroups,
                EstimatedAttributeGroups = string.IsNullOrWhiteSpace(player.EstimatedAttributeGroups)
                    ? "technical,tactical,physical,mental,potential"
                    : player.EstimatedAttributeGroups,
                UnknownAttributeGroups = string.IsNullOrWhiteSpace(player.UnknownAttributeGroups)
                    ? "pressure response,agent loyalty,future behavior,exact potential"
                    : player.UnknownAttributeGroups,
                IsStarting = player.IsStarting
            });

        LastMatchReport = data.LastMatchReport == null
            ? null
            : new MatchReport
            {
                FixtureLabel = data.LastMatchReport.FixtureLabel,
                Scoreline = data.LastMatchReport.Scoreline,
                ResultLabel = data.LastMatchReport.ResultLabel,
                ConsequenceSummary = data.LastMatchReport.ConsequenceSummary,
                TableImpactSummary = data.LastMatchReport.TableImpactSummary,
                TacticalSummary = data.LastMatchReport.TacticalSummary,
                PressureSummary = data.LastMatchReport.PressureSummary,
                CauseSummary = string.IsNullOrWhiteSpace(data.LastMatchReport.CauseSummary)
                    ? "Cause detail unavailable for this saved report."
                    : data.LastMatchReport.CauseSummary,
                StatsSummary = string.IsNullOrWhiteSpace(data.LastMatchReport.StatsSummary)
                    ? "Stats unavailable for this saved report."
                    : data.LastMatchReport.StatsSummary,
                KeyPlayerMoments = string.IsNullOrWhiteSpace(data.LastMatchReport.KeyPlayerMoments)
                    ? "Key player moments unavailable for this saved report."
                    : data.LastMatchReport.KeyPlayerMoments,
                TacticalExplanation = string.IsNullOrWhiteSpace(data.LastMatchReport.TacticalExplanation)
                    ? "Tactical explanation unavailable for this saved report."
                    : data.LastMatchReport.TacticalExplanation,
                TacticalSection = string.IsNullOrWhiteSpace(data.LastMatchReport.TacticalSection)
                    ? "Tactical section unavailable for this saved report."
                    : data.LastMatchReport.TacticalSection,
                PlayerFitSection = string.IsNullOrWhiteSpace(data.LastMatchReport.PlayerFitSection)
                    ? "Player fit section unavailable for this saved report."
                    : data.LastMatchReport.PlayerFitSection,
                FatigueSection = string.IsNullOrWhiteSpace(data.LastMatchReport.FatigueSection)
                    ? "Fatigue section unavailable for this saved report."
                    : data.LastMatchReport.FatigueSection,
                MoraleSection = string.IsNullOrWhiteSpace(data.LastMatchReport.MoraleSection)
                    ? "Morale section unavailable for this saved report."
                    : data.LastMatchReport.MoraleSection,
                BoardReactionSection = string.IsNullOrWhiteSpace(data.LastMatchReport.BoardReactionSection)
                    ? "Board reaction unavailable for this saved report."
                    : data.LastMatchReport.BoardReactionSection,
                FanReactionSection = string.IsNullOrWhiteSpace(data.LastMatchReport.FanReactionSection)
                    ? "Fan reaction unavailable for this saved report."
                    : data.LastMatchReport.FanReactionSection,
                MediaStorySection = string.IsNullOrWhiteSpace(data.LastMatchReport.MediaStorySection)
                    ? "Media story unavailable for this saved report."
                    : data.LastMatchReport.MediaStorySection,
                StaffAnalysisSection = string.IsNullOrWhiteSpace(data.LastMatchReport.StaffAnalysisSection)
                    ? "Staff analysis unavailable for this saved report."
                    : data.LastMatchReport.StaffAnalysisSection,
                DevelopmentNotesSection = string.IsNullOrWhiteSpace(data.LastMatchReport.DevelopmentNotesSection)
                    ? "Development notes unavailable for this saved report."
                    : data.LastMatchReport.DevelopmentNotesSection,
                KeyEvents = data.LastMatchReport.KeyEvents ?? Array.Empty<string>(),
                MoraleDelta = data.LastMatchReport.MoraleDelta,
                FanDelta = data.LastMatchReport.FanDelta,
                BoardDelta = data.LastMatchReport.BoardDelta
            };
        CompetitionTable = Array.ConvertAll(
            data.CompetitionTable ?? Array.Empty<SaveSlotCompetitionRowData>(),
            row => new CompetitionRow
            {
                ClubName = row.ClubName,
                Played = row.Played,
                Won = row.Won,
                Drawn = row.Drawn,
                Lost = row.Lost,
                GoalsFor = row.GoalsFor,
                GoalsAgainst = row.GoalsAgainst,
                Points = row.Points
            });
        CompetitionFixtures = Array.ConvertAll(
            data.CompetitionFixtures ?? Array.Empty<SaveSlotCompetitionFixtureData>(),
            fixture => new CompetitionFixture
            {
                Matchday = fixture.Matchday,
                HomeClubName = fixture.HomeClubName,
                AwayClubName = fixture.AwayClubName,
                IsComplete = fixture.IsComplete,
                Scoreline = fixture.Scoreline,
                ResultSummary = fixture.ResultSummary,
                CompetitionType = string.IsNullOrWhiteSpace(fixture.CompetitionType) ? "League" : fixture.CompetitionType,
                CompetitionName = string.IsNullOrWhiteSpace(fixture.CompetitionName) ? data.CompetitionName : fixture.CompetitionName,
                RoundName = string.IsNullOrWhiteSpace(fixture.RoundName) ? $"Matchday {fixture.Matchday}" : fixture.RoundName
            });
        SelectedPlayerProfileName = data.SelectedPlayerProfileName;
        CurrentMatchResult = null;
        if (CurrentClub == null && !string.IsNullOrWhiteSpace(SelectedClubName))
        {
            CurrentClub = CareerFoundation.BuildFallbackClubFoundation(
                SelectedClubName,
                TeamMorale,
                FanSentiment,
                BoardConfidence,
                CareerProfile,
                WorldSeed);
        }

        CareerProfile.CurrentClubName = SelectedClubName;
        SyncCurrentClubMoraleFromRuntime();
        RestoreStageFoundationState(data.StageFoundations);
        RefreshFixtureContext();
    }

    public void SelectPlayerProfile(string playerName)
    {
        SelectedPlayerProfileName = playerName;
    }

    public SquadPlayer? GetSelectedPlayerProfile()
    {
        if (string.IsNullOrWhiteSpace(SelectedPlayerProfileName))
        {
            return null;
        }

        foreach (var player in SquadPlayers)
        {
            if (player.Name == SelectedPlayerProfileName)
            {
                return player;
            }
        }

        return null;
    }

    public ClubSquadPlayer[] GetClubSquad(string clubName)
    {
        if (!string.IsNullOrWhiteSpace(clubName) && clubName == SelectedClubName)
        {
            var selectedSquad = new ClubSquadPlayer[SquadPlayers.Length];
            for (var index = 0; index < SquadPlayers.Length; index++)
            {
                var player = SquadPlayers[index];
                selectedSquad[index] = new ClubSquadPlayer
                {
                    PlayerId = string.IsNullOrWhiteSpace(player.PlayerId) ? ClubSquadFactory.BuildPlayerId(clubName, player.Name, index) : player.PlayerId,
                    ClubName = clubName,
                    Name = player.Name,
                    Position = player.Position,
                    Age = player.Age,
                    TrueAbility = player.TrueAbility,
                    TacticalFitScore = player.TacticalFitScore,
                    PlayingStyle = player.PlayingStyle,
                    TacticalFit = player.TacticalFit,
                    Form = player.Form,
                    Morale = player.Morale,
                    Fitness = player.Fitness,
                    Fatigue = player.Fatigue,
                    IsStarting = player.IsStarting
                };
            }

            return selectedSquad;
        }

        return TouchlineWorldGenerator.Instance == null
            ? ClubSquadFactory.BuildFallbackSquad(clubName, WorldSeed)
            : TouchlineWorldGenerator.Instance.ResolveClubSquad(clubName, WorldSeed);
    }

    public string ValidateStage1CareerFoundationContract()
    {
        if (!CareerInitialized)
        {
            return "Career foundation is not initialized.";
        }

        if (string.IsNullOrWhiteSpace(ManagerName) ||
            string.IsNullOrWhiteSpace(CurrentRoleName) ||
            string.IsNullOrWhiteSpace(ManagerBackgroundName) ||
            string.IsNullOrWhiteSpace(LicenseName))
        {
            return "Career profile is missing role, background, or license context.";
        }

        if (string.IsNullOrWhiteSpace(SelectedClubName) || CurrentClub == null)
        {
            return "Career foundation is missing selected club foundation state.";
        }

        if (CurrentClub.Staff.Length == 0)
        {
            return "Club foundation is missing starting staff.";
        }

        if (CurrentClub.Objectives.Length == 0)
        {
            return "Club foundation is missing objectives.";
        }

        if (CurrentClub.TransferBudget <= 0 || CurrentClub.WageBudget <= 0)
        {
            return "Club foundation is missing readable budget data.";
        }

        if (!IsInPercentRange(BoardMorale) ||
            !IsInPercentRange(FanMorale) ||
            !IsInPercentRange(SquadMorale) ||
            !IsInPercentRange(JobPressure))
        {
            return "Morale or pressure values are outside 0-100 bounds.";
        }

        if (CareerProfile.Role == ManagerRole.AssistantManager &&
            RoleAuthoritySummary.Contains("Can suggest", StringComparison.Ordinal) &&
            RoleAuthoritySummary.Contains("Cannot finalize", StringComparison.Ordinal))
        {
            return MatchPlaybackContractValidator.PassMessage;
        }

        if (CareerProfile.Role == ManagerRole.HeadCoach &&
            RoleAuthoritySummary.Contains("Controls lineups", StringComparison.Ordinal) &&
            RoleAuthoritySummary.Contains("Does not fully control", StringComparison.Ordinal))
        {
            return MatchPlaybackContractValidator.PassMessage;
        }

        if (CareerProfile.Role == ManagerRole.Manager &&
            RoleAuthoritySummary.Contains("Controls the broad football project", StringComparison.Ordinal) &&
            RoleAuthoritySummary.Contains("Cannot control ownership", StringComparison.Ordinal))
        {
            return MatchPlaybackContractValidator.PassMessage;
        }

        return "Role authority summary does not match the selected role.";
    }

    public string TogglePlayerLineupStatus(string playerName)
    {
        var targetIndex = Array.FindIndex(SquadPlayers, player => player.Name == playerName);
        if (targetIndex < 0)
        {
            return "Selected player is unavailable for lineup changes.";
        }

        var targetPlayer = SquadPlayers[targetIndex];
        if (CareerProfile.Role == ManagerRole.AssistantManager)
        {
            AddNews(
                "Lineup recommendation filed",
                NewsCategory.Club,
                "Internal",
                $"{ManagerName} recommended a lineup change involving {targetPlayer.Name}, but Assistant Manager authority cannot finalize the XI.",
                2);
            return $"Assistant Manager recommendation logged for {targetPlayer.Name}; final lineup authority sits with senior staff.";
        }

        var startingCount = CountStartingPlayers();

        if (targetPlayer.IsStarting)
        {
            var replacementIndex = FindReplacementBenchIndex(targetPlayer.Position);
            if (replacementIndex < 0)
            {
                return "No bench player is available to keep the XI balanced.";
            }

            var replacementPlayer = SquadPlayers[replacementIndex];
            SetPlayerStartingStatus(targetIndex, false);
            SetPlayerStartingStatus(replacementIndex, true);
            return $"{targetPlayer.Name} moves to the bench. {replacementPlayer.Name} steps into the XI.";
        }

        if (startingCount < 11)
        {
            SetPlayerStartingStatus(targetIndex, true);
            return $"{targetPlayer.Name} is promoted into the XI.";
        }

        var playerToBenchIndex = FindStarterToBenchIndex(targetPlayer.Position, targetIndex);
        if (playerToBenchIndex < 0)
        {
            return "A balanced swap could not be found for this lineup move.";
        }

        var benchPlayer = SquadPlayers[playerToBenchIndex];
        SetPlayerStartingStatus(playerToBenchIndex, false);
        SetPlayerStartingStatus(targetIndex, true);
        return $"{targetPlayer.Name} enters the XI for {benchPlayer.Name}.";
    }

    private static CareerProfile RestoreCareerProfile(
        SaveSlotCareerProfileData? data,
        string managerName,
        int careerSeed,
        string? selectedClubName)
    {
        if (data == null)
        {
            var fallbackProfile = CareerFoundation.CreateCareerProfile(
                managerName,
                careerSeed,
                ManagerRole.Manager,
                ManagerBackground.UnknownUpstart,
                ManagerLicense.NationalCLicense);
            fallbackProfile.CurrentClubName = selectedClubName;
            return fallbackProfile;
        }

        var profile = CareerFoundation.CreateCareerProfile(
            string.IsNullOrWhiteSpace(data.ManagerName) ? managerName : data.ManagerName,
            data.CareerSeed == 0 ? careerSeed : data.CareerSeed,
            CareerFoundation.ParseRole(data.RoleName),
            CareerFoundation.ParseBackground(data.BackgroundName),
            CareerFoundation.ParseLicense(data.LicenseName));
        profile.CurrentClubName = string.IsNullOrWhiteSpace(data.CurrentClubName) ? selectedClubName : data.CurrentClubName;
        profile.Reputation = data.Reputation > 0 ? data.Reputation : profile.Reputation;
        profile.BoardTrust = data.BoardTrust > 0 ? data.BoardTrust : profile.BoardTrust;
        profile.PlayerTrust = data.PlayerTrust > 0 ? data.PlayerTrust : profile.PlayerTrust;
        profile.StaffTrust = data.StaffTrust > 0 ? data.StaffTrust : profile.StaffTrust;
        profile.DirectorTrust = data.DirectorTrust > 0 ? data.DirectorTrust : profile.DirectorTrust;
        profile.MediaPressure = data.MediaPressure > 0 ? data.MediaPressure : profile.MediaPressure;
        return profile;
    }

    private Club? RestoreClubFoundation(SaveSlotClubFoundationData? data)
    {
        if (data == null || string.IsNullOrWhiteSpace(data.Name))
        {
            return null;
        }

        var staff = Array.ConvertAll(
            data.Staff ?? Array.Empty<SaveSlotStaffMemberData>(),
            RestoreStaffMember);
        var objectives = Array.ConvertAll(
            data.Objectives ?? Array.Empty<SaveSlotObjectiveData>(),
            RestoreObjective);
        return new Club
        {
            Name = data.Name,
            IdentitySummary = string.IsNullOrWhiteSpace(data.IdentitySummary)
                ? "Club identity context unavailable from save."
                : data.IdentitySummary,
            ExpectationSummary = string.IsNullOrWhiteSpace(data.ExpectationSummary)
                ? "Board line: saved objective context unavailable."
                : data.ExpectationSummary,
            Archetype = CareerFoundation.ParseClubArchetype(data.ArchetypeName),
            BoardPhilosophy = CareerFoundation.ParseBoardPhilosophy(data.BoardPhilosophyName),
            FanCulture = CareerFoundation.ParseFanCulture(data.FanCultureName),
            DirectorOfFootballStyle = CareerFoundation.ParseDirectorStyle(data.DirectorOfFootballStyleName),
            DirectorRelationshipState = CareerFoundation.ParseDirectorRelationship(data.DirectorRelationshipName),
            Staff = staff,
            Objectives = objectives,
            TransferBudget = data.TransferBudget,
            WageBudget = data.WageBudget,
            BoardMorale = data.BoardMorale,
            FanMorale = data.FanMorale,
            SquadMorale = data.SquadMorale,
            JobPressure = data.JobPressure,
            NewsFeed = data.NewsFeed ?? Array.Empty<string>()
        };
    }

    private static StaffMember RestoreStaffMember(SaveSlotStaffMemberData data)
    {
        return new StaffMember
        {
            Name = string.IsNullOrWhiteSpace(data.Name) ? "Unnamed staff member" : data.Name,
            Role = CareerFoundation.ParseStaffRole(data.RoleName),
            Quality = data.Quality,
            InfluenceSummary = string.IsNullOrWhiteSpace(data.InfluenceSummary)
                ? "Influence summary unavailable."
                : data.InfluenceSummary,
            ContractExpiryYear = data.ContractExpiryYear <= 0 ? 2028 : data.ContractExpiryYear,
            Wage = data.Wage <= 0 ? 9000 : data.Wage,
            Reputation = Math.Clamp(data.Reputation <= 0 ? data.Quality : data.Reputation, 0, 100),
            Loyalty = Math.Clamp(data.Loyalty <= 0 ? 55 : data.Loyalty, 0, 100),
            Ambition = Math.Clamp(data.Ambition <= 0 ? 45 : data.Ambition, 0, 100),
            PreferredStyle = string.IsNullOrWhiteSpace(data.PreferredStyle) ? "Balanced" : data.PreferredStyle,
            Relationship = string.IsNullOrWhiteSpace(data.Relationship) ? "Professional" : data.Relationship
        };
    }

    private static Objective RestoreObjective(SaveSlotObjectiveData data)
    {
        return new Objective
        {
            Summary = string.IsNullOrWhiteSpace(data.Summary) ? "Objective summary unavailable." : data.Summary,
            Priority = CareerFoundation.ParseObjectivePriority(data.PriorityName),
            Type = CareerFoundation.ParseObjectiveType(data.TypeName)
        };
    }

    private void SyncCurrentClubMoraleFromRuntime()
    {
        if (CurrentClub == null)
        {
            return;
        }

        CurrentClub.BoardMorale = BoardConfidence;
        CurrentClub.FanMorale = FanSentiment;
        CurrentClub.SquadMorale = TeamMorale;
        CurrentClub.JobPressure = CareerFoundation.CalculateJobPressure(
            CurrentClub.Archetype,
            CurrentClub.BoardPhilosophy,
            CareerProfile.Role,
            CareerProfile.Background,
            CareerProfile.License,
            BoardConfidence,
            FanSentiment,
            TeamMorale);
    }

    private string BuildSquadStatusSummary()
    {
        var averageFitness = 0;
        var averageForm = 0;
        if (SquadPlayers.Length > 0)
        {
            var totalFitness = 0;
            var totalForm = 0;
            foreach (var player in SquadPlayers)
            {
                totalFitness += player.Fitness;
                totalForm += player.Form;
            }

            averageFitness = totalFitness / SquadPlayers.Length;
            averageForm = totalForm / SquadPlayers.Length;
        }

        return $"{SquadPlayers.Length} registered players | avg fitness {averageFitness} | avg form {averageForm} | morale {DescribeLevel(TeamMorale)} | fans {DescribeLevel(FanSentiment)} | board {DescribeLevel(BoardConfidence)}";
    }

    private void RecordCompetitionResults(int homeGoals, int awayGoals)
    {
        var competitionState = CompetitionRuntimeService.ApplyMatchdayResult(
            AvailableClubs,
            CompetitionFixtures,
            CurrentMatchday,
            SelectedClubName,
            homeGoals,
            awayGoals,
            WorldSeed,
            SeasonStartYear);
        CompetitionTable = competitionState.table;
        CompetitionFixtures = competitionState.fixtures;
    }

    private int CountStartingPlayers()
    {
        var count = 0;
        foreach (var player in SquadPlayers)
        {
            if (player.IsStarting)
            {
                count++;
            }
        }

        return count;
    }

    private void SetPlayerStartingStatus(int index, bool isStarting)
    {
        var player = SquadPlayers[index];
        SquadPlayers[index] = player.With(isStarting: isStarting);
    }

    private int FindReplacementBenchIndex(string position)
    {
        var preferredFamily = GetPositionFamily(position);
        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            if (!SquadPlayers[index].IsStarting && GetPositionFamily(SquadPlayers[index].Position) == preferredFamily)
            {
                return index;
            }
        }

        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            if (!SquadPlayers[index].IsStarting)
            {
                return index;
            }
        }

        return -1;
    }

    private int FindStarterToBenchIndex(string position, int excludedIndex)
    {
        var preferredFamily = GetPositionFamily(position);
        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            if (index != excludedIndex && SquadPlayers[index].IsStarting && GetPositionFamily(SquadPlayers[index].Position) == preferredFamily)
            {
                return index;
            }
        }

        for (var index = 0; index < SquadPlayers.Length; index++)
        {
            if (index != excludedIndex && SquadPlayers[index].IsStarting)
            {
                return index;
            }
        }

        return -1;
    }

    private static string GetPositionFamily(string position)
    {
        return position switch
        {
            "GK" => "GK",
            "RB" or "CB" or "LB" => "DEF",
            "CM" or "AM" => "MID",
            _ => "ATT"
        };
    }

    private void RefreshFixtureContext()
    {
        var fixtureContext = CompetitionRuntimeService.ResolveFixtureContext(
            CompetitionFixtures,
            CurrentMatchday,
            SelectedClubName,
            CurrentDateLabel);
        CurrentOpponentName = fixtureContext.currentOpponentName;
        NextFixtureSummary = fixtureContext.nextFixtureSummary;
    }

    private void UpdateFormSummary(int goalDifference)
    {
        var resultToken = goalDifference switch
        {
            > 0 => "W",
            0 => "D",
            _ => "L"
        };

        _recentResults.Insert(0, resultToken);
        if (_recentResults.Count > 5)
        {
            _recentResults.RemoveAt(5);
        }

        FormSummary = $"Form: {string.Join(" ", _recentResults)}";
    }

    private static string DescribeLevel(int value)
    {
        return value switch
        {
            >= 75 => "surging",
            >= 60 => "steady",
            >= 45 => "uneasy",
            _ => "under pressure"
        };
    }

    private static string BuildResultLabel(int goalDifference, string opponentName)
    {
        return goalDifference switch
        {
            > 0 => $"Winning over {opponentName} lifts the mood around the club.",
            0 => $"The draw with {opponentName} leaves the dressing room asking for more control.",
            _ => $"{opponentName} leave with the points and the pressure tightens."
        };
    }

    private static string FormatSignedDelta(int delta)
    {
        return delta >= 0 ? $"+{delta}" : delta.ToString();
    }

    private static string[] ExtractTeamNames(MatchPlaybackResult playback, string teamName)
    {
        if (playback.Timeline.Frames.Length == 0)
        {
            return Array.Empty<string>();
        }

        var names = new List<string>();
        foreach (var player in playback.Timeline.Frames[0].PlayerStates)
        {
            if (player.Team == teamName)
            {
                names.Add(player.Name);
            }
        }

        return names.ToArray();
    }

    private static bool ContainsOldHardcodedAwayName(string[] names)
    {
        var oldNames = new[]
        {
            "Roman Ivic",
            "Maksym Hale",
            "Victor Salcedo",
            "Pavel Drago",
            "Nico Barros",
            "Ilyas Cherif",
            "Samir Gashi",
            "Tom Bisset",
            "Leandro Pires",
            "Bruno Keita",
            "Yuri Markovic"
        };

        foreach (var name in names)
        {
            foreach (var oldName in oldNames)
            {
                if (name == oldName)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool ContainsSeededOpponentName(string[] playbackNames, ClubSquadPlayer[] expectedSquad)
    {
        foreach (var playbackName in playbackNames)
        {
            foreach (var expectedPlayer in expectedSquad)
            {
                if (playbackName == expectedPlayer.Name)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool StringArraysMatch(string[] first, string[] second)
    {
        if (first.Length != second.Length)
        {
            return false;
        }

        for (var index = 0; index < first.Length; index++)
        {
            if (first[index] != second[index])
            {
                return false;
            }
        }

        return true;
    }

    private static int CountDistinctActionKinds(MatchPlaybackResult playback)
    {
        var kinds = new List<MatchActionKind>();
        foreach (var action in playback.Timeline.Actions)
        {
            if (!kinds.Contains(action.Kind))
            {
                kinds.Add(action.Kind);
            }
        }

        return kinds.Count;
    }

    private static int CountDistinctPassLanes(MatchPlaybackResult playback)
    {
        var lanes = new List<int>();
        foreach (var action in playback.Timeline.Actions)
        {
            if (action.Kind != MatchActionKind.Pass)
            {
                continue;
            }

            var lane = (int)MathF.Round(action.ToPosition.Y * 10.0f);
            if (!lanes.Contains(lane))
            {
                lanes.Add(lane);
            }
        }

        return lanes.Count;
    }

    private static string BuildActionSignature(MatchPlaybackResult playback)
    {
        var segments = new List<string>();
        var limit = Math.Min(18, playback.Timeline.Actions.Length);
        for (var index = 0; index < limit; index++)
        {
            var action = playback.Timeline.Actions[index];
            segments.Add($"{action.Kind}:{action.Team}:{action.ToPosition.X:0.00}:{action.ToPosition.Y:0.00}");
        }

        return string.Join("|", segments);
    }

    private static string[] ExtractRecentEvents(MatchPlaybackResult result)
    {
        var count = Math.Min(4, result.EventFeed.Length);
        var recentEvents = new string[count];

        for (var index = 0; index < count; index++)
        {
            recentEvents[index] = result.EventFeed[result.EventFeed.Length - count + index].Summary;
        }

        return recentEvents;
    }

    private int CountCompletedFixtures()
    {
        var count = 0;
        foreach (var fixture in CompetitionFixtures)
        {
            if (fixture.IsComplete)
            {
                count++;
            }
        }

        return count;
    }

    private int CountOpenFixturesForMatchday(int matchday)
    {
        var count = 0;
        foreach (var fixture in CompetitionFixtures)
        {
            if (fixture.Matchday == matchday && !fixture.IsComplete)
            {
                count++;
            }
        }

        return count;
    }

    private string CompleteCurrentSeason()
    {
        var startingSeasonYear = SeasonStartYear;
        var seasonLength = CompetitionRuntimeService.GetSeasonMatchdayCount(CompetitionFixtures);
        for (var guard = 0; guard < seasonLength + 2 && SeasonStartYear == startingSeasonYear; guard++)
        {
            if (!IsCurrentClubFixtureComplete())
            {
                ApplyMatchResult(PrepareCurrentMatchResult(true));
            }

            if (TouchlineCalendarSystem.Instance == null)
            {
                return "Calendar system unavailable during season completion validation.";
            }

            if (!TouchlineCalendarSystem.Instance.AdvanceCareerDate())
            {
                return TouchlineCalendarSystem.Instance.LastStatusMessage;
            }
        }

        return SeasonStartYear == startingSeasonYear + 1
            ? MatchPlaybackContractValidator.PassMessage
            : "Season rollover did not trigger after completing the fixture list.";
    }

    private static bool IsPlayerValueInBounds(SquadPlayer player)
    {
        return IsInPercentRange(player.Form) &&
            IsInPercentRange(player.Morale) &&
            IsInPercentRange(player.Fitness);
    }

    private static bool IsInPercentRange(int value)
    {
        return value >= 0 && value <= 100;
    }

    private string ResolveDifferentClub(string? selectedClubName)
    {
        foreach (var clubName in AvailableClubs)
        {
            if (clubName != selectedClubName)
            {
                return clubName;
            }
        }

        return string.Empty;
    }

    private int GetClubTablePosition(string clubName)
    {
        return CompetitionRuntimeService.GetClubTablePosition(CompetitionTable, clubName);
    }

    private string BuildTableImpactSummary(int previousPosition, int currentPosition)
    {
        return CompetitionRuntimeService.BuildTableImpactSummary(
            CompetitionTable,
            SelectedClubName,
            previousPosition,
            currentPosition);
    }

    private CompetitionFixture? GetCurrentClubFixture()
    {
        return CompetitionRuntimeService.GetCurrentClubFixture(CompetitionFixtures, CurrentMatchday, SelectedClubName);
    }

    private static bool IsCupFixture(CompetitionFixture? fixture)
    {
        return fixture != null && fixture.CompetitionType.Equals("Cup", StringComparison.OrdinalIgnoreCase);
    }

    private CompetitionRow? GetCompetitionRow(string clubName)
    {
        return CompetitionRuntimeService.GetCompetitionRow(CompetitionTable, clubName);
    }
}

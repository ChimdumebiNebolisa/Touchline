using System;
using System.Collections.Generic;

public enum ManagerRole
{
    AssistantManager,
    HeadCoach,
    Manager
}

public enum ManagerLicense
{
    GrassrootsLicense,
    NationalCLicense,
    NationalBLicense,
    NationalALicense,
    ProLicense
}

public enum ManagerBackground
{
    FormerClubLegend,
    UnknownUpstart,
    AssistantManagerPromotion,
    YouthAcademyCoach,
    FormerPlayer,
    TacticalSpecialist,
    CrisisInterim
}

public enum ClubArchetype
{
    TitleContender,
    FallenGiant,
    MidTableStabilizer,
    RelegationFighter,
    YouthAcademyClub,
    SellingClub,
    FinanciallyRestrictedClub,
    AmbitiousNewMoneyClub,
    ChaoticClub,
    CommunityClub
}

public enum BoardPhilosophy
{
    WinNowBoard,
    PatientLongTermBoard,
    FinanciallyStrictBoard,
    YouthDevelopmentBoard,
    CommercialGrowthBoard,
    DataDrivenBoard,
    TraditionalistBoard,
    TriggerHappyBoard
}

public enum FanCulture
{
    ResultsFirst,
    AttackingFootball,
    DefensiveGrit,
    AcademyLoyalists,
    StarPowerFans,
    AntiSellingFans,
    DerbyObsessed,
    UnderdogLoyalists,
    TraditionalIdentityFans
}

public enum DirectorOfFootballStyle
{
    TalentTrader,
    StarChaser,
    AcademyBuilder,
    DataOperator,
    BargainHunter,
    ControlFreak,
    ClubLoyalist,
    PoliticalSurvivor
}

public enum DirectorRelationshipState
{
    Ally,
    Supportive,
    Neutral,
    Tense,
    Hostile
}

public enum StaffRole
{
    AssistantManager,
    FirstTeamCoach,
    GoalkeepingCoach,
    FitnessCoach,
    Physio,
    YouthCoach,
    Scout,
    HeadOfRecruitment,
    DataAnalyst,
    MediaOfficer
}

public enum ObjectivePriority
{
    Critical,
    Important,
    Preferred,
    Optional
}

public enum ObjectiveType
{
    LeagueObjective,
    CupObjective,
    StyleObjective,
    SquadObjective,
    FinancialObjective,
    ReputationObjective
}

public sealed class CareerProfile
{
    public required string ManagerName { get; set; }
    public required int CareerSeed { get; set; }
    public required ManagerRole Role { get; set; }
    public required ManagerBackground Background { get; set; }
    public required ManagerLicense License { get; set; }
    public string? CurrentClubName { get; set; }
    public required int Reputation { get; set; }
    public required int BoardTrust { get; set; }
    public required int PlayerTrust { get; set; }
    public required int StaffTrust { get; set; }
    public required int DirectorTrust { get; set; }
    public required int MediaPressure { get; set; }
}

public sealed class Club
{
    public required string Name { get; init; }
    public required string IdentitySummary { get; init; }
    public required string ExpectationSummary { get; init; }
    public required ClubArchetype Archetype { get; init; }
    public required BoardPhilosophy BoardPhilosophy { get; init; }
    public required FanCulture FanCulture { get; init; }
    public required DirectorOfFootballStyle DirectorOfFootballStyle { get; init; }
    public required DirectorRelationshipState DirectorRelationshipState { get; init; }
    public required StaffMember[] Staff { get; init; }
    public required Objective[] Objectives { get; init; }
    public required int TransferBudget { get; init; }
    public required int WageBudget { get; init; }
    public required int BoardMorale { get; set; }
    public required int FanMorale { get; set; }
    public required int SquadMorale { get; set; }
    public required int JobPressure { get; set; }
    public required string[] NewsFeed { get; set; }
}

public sealed class StaffMember
{
    public required string Name { get; init; }
    public required StaffRole Role { get; init; }
    public required int Quality { get; init; }
    public required string InfluenceSummary { get; init; }
    public int ContractExpiryYear { get; init; } = 2028;
    public int Wage { get; init; } = 9000;
    public int Reputation { get; init; } = 50;
    public int Loyalty { get; init; } = 55;
    public int Ambition { get; init; } = 45;
    public string PreferredStyle { get; init; } = "Balanced";
    public string Relationship { get; init; } = "Professional";
}

public sealed class Objective
{
    public required string Summary { get; init; }
    public required ObjectivePriority Priority { get; init; }
    public required ObjectiveType Type { get; init; }
}

public static class CareerFoundation
{
    public static readonly string[] RoleDisplayNames =
    {
        "Assistant Manager",
        "Head Coach",
        "Manager"
    };

    public static readonly string[] BackgroundDisplayNames =
    {
        "Former Club Legend",
        "Unknown Upstart",
        "Assistant Manager Promotion",
        "Youth Academy Coach",
        "Former Player",
        "Tactical Specialist",
        "Crisis Interim"
    };

    public static readonly string[] LicenseDisplayNames =
    {
        "Grassroots License",
        "National C License",
        "National B License",
        "National A License",
        "Pro License"
    };

    public static CareerProfile CreateCareerProfile(
        string managerName,
        int careerSeed,
        ManagerRole role,
        ManagerBackground background,
        ManagerLicense license)
    {
        var backgroundEffect = GetBackgroundEffect(background);
        var licenseTrustModifier = GetLicenseTrustModifier(license);
        return new CareerProfile
        {
            ManagerName = managerName,
            CareerSeed = careerSeed,
            Role = role,
            Background = background,
            License = license,
            Reputation = Math.Clamp(backgroundEffect.reputation + GetLicenseReputationModifier(license), 0, 100),
            BoardTrust = Math.Clamp(backgroundEffect.boardTrust + licenseTrustModifier, 0, 100),
            PlayerTrust = Math.Clamp(backgroundEffect.playerTrust + licenseTrustModifier, 0, 100),
            StaffTrust = Math.Clamp(backgroundEffect.staffTrust + licenseTrustModifier, 0, 100),
            DirectorTrust = Math.Clamp(backgroundEffect.directorTrust + licenseTrustModifier, 0, 100),
            MediaPressure = Math.Clamp(backgroundEffect.mediaPressure + GetRoleAccountabilityModifier(role), 0, 100)
        };
    }

    public static Club BuildClubFoundation(
        string clubName,
        string identitySummary,
        string expectationSummary,
        int squadMorale,
        int fanMorale,
        int boardMorale,
        CareerProfile profile,
        int worldSeed)
    {
        var template = ResolveClubTemplate(clubName);
        var staff = BuildStaff(template.archetype, template.directorStyle, worldSeed);
        var objectives = BuildObjectives(template.archetype, template.boardPhilosophy, expectationSummary);
        return new Club
        {
            Name = clubName,
            IdentitySummary = identitySummary,
            ExpectationSummary = expectationSummary,
            Archetype = template.archetype,
            BoardPhilosophy = template.boardPhilosophy,
            FanCulture = template.fanCulture,
            DirectorOfFootballStyle = template.directorStyle,
            DirectorRelationshipState = template.directorRelationship,
            Staff = staff,
            Objectives = objectives,
            TransferBudget = template.transferBudget,
            WageBudget = template.wageBudget,
            BoardMorale = boardMorale,
            FanMorale = fanMorale,
            SquadMorale = squadMorale,
            JobPressure = CalculateJobPressure(
                template.archetype,
                template.boardPhilosophy,
                profile.Role,
                profile.Background,
                profile.License,
                boardMorale,
                fanMorale,
                squadMorale),
            NewsFeed = BuildOpeningNews(clubName, profile, template.archetype, template.boardPhilosophy, template.fanCulture)
        };
    }

    public static Club BuildFallbackClubFoundation(
        string clubName,
        int squadMorale,
        int fanMorale,
        int boardMorale,
        CareerProfile profile,
        int worldSeed)
    {
        return BuildClubFoundation(
            clubName,
            "Loaded career club context. Full identity text was not stored in this save.",
            "Board line: continue the current season plan from the loaded save.",
            squadMorale,
            fanMorale,
            boardMorale,
            profile,
            worldSeed);
    }

    public static int CalculateJobPressure(
        ClubArchetype archetype,
        BoardPhilosophy boardPhilosophy,
        ManagerRole role,
        ManagerBackground background,
        ManagerLicense license,
        int boardMorale,
        int fanMorale,
        int squadMorale)
    {
        var moraleDrag = 100 - ((boardMorale * 2 + fanMorale + squadMorale) / 4);
        var pressure = moraleDrag +
            GetClubPressureModifier(archetype) +
            GetBoardPressureModifier(boardPhilosophy) +
            GetRoleAccountabilityModifier(role) +
            GetBackgroundPressureModifier(background) +
            GetLicensePressureModifier(license);
        return Math.Clamp(pressure, 0, 100);
    }

    public static string GetRoleAuthoritySummary(ManagerRole role)
    {
        return role switch
        {
            ManagerRole.AssistantManager =>
                "Can suggest tactics, training, substitutions, player development, and opposition notes. Cannot finalize lineups, tactics, transfers, contracts, staff hiring, or board objectives.",
            ManagerRole.HeadCoach =>
                "Controls lineups, tactics, training focus, player roles, substitutions, and football-side decisions. Can influence transfers, contracts, sales, staff changes, youth promotions, and scouting priorities. Does not fully control fees, wages, Director of Football decisions, or board budget.",
            _ =>
                "Controls the broad football project. Can control lineups, tactics, training, transfers within limits, contracts within limits, scouting priorities, staff recommendations, squad planning, and media direction. Cannot control ownership, board membership, Director of Football hiring/firing, or absolute budgets."
        };
    }

    public static string BuildObjectivesSummary(Club? club)
    {
        if (club == null || club.Objectives.Length == 0)
        {
            return "Main objectives unavailable until a club is selected.";
        }

        var summaries = new List<string>();
        foreach (var objective in club.Objectives)
        {
            summaries.Add($"{GetDisplayName(objective.Priority)} {GetDisplayName(objective.Type)}: {objective.Summary}");
        }

        return string.Join("\n", summaries);
    }

    public static string BuildStaffSummary(Club? club)
    {
        if (club == null || club.Staff.Length == 0)
        {
            return "Staff foundation unavailable until a club is selected.";
        }

        var summaries = new List<string>();
        foreach (var staff in club.Staff)
        {
            summaries.Add($"{staff.Name}, {GetDisplayName(staff.Role)} ({staff.Quality}) - {staff.InfluenceSummary} | Contract {staff.ContractExpiryYear}, wage {FormatMoney(staff.Wage)}, reputation {staff.Reputation}, loyalty {staff.Loyalty}, ambition {staff.Ambition}, style {staff.PreferredStyle}, relationship {staff.Relationship}");
        }

        return string.Join("\n", summaries);
    }

    public static string BuildNewsSummary(Club? club)
    {
        if (club == null || club.NewsFeed.Length == 0)
        {
            return "News feed unavailable until a club is selected.";
        }

        return string.Join("\n", club.NewsFeed);
    }

    public static string BuildBudgetSummary(Club? club)
    {
        if (club == null)
        {
            return "Budgets unavailable until a club is selected.";
        }

        return $"Transfer budget {FormatMoney(club.TransferBudget)} | Wage budget {FormatMoney(club.WageBudget)}";
    }

    public static string GetDisplayName(ManagerRole value)
    {
        return value switch
        {
            ManagerRole.AssistantManager => "Assistant Manager",
            ManagerRole.HeadCoach => "Head Coach",
            _ => "Manager"
        };
    }

    public static string GetDisplayName(ManagerLicense value)
    {
        return value switch
        {
            ManagerLicense.GrassrootsLicense => "Grassroots License",
            ManagerLicense.NationalCLicense => "National C License",
            ManagerLicense.NationalBLicense => "National B License",
            ManagerLicense.NationalALicense => "National A License",
            _ => "Pro License"
        };
    }

    public static string GetDisplayName(ManagerBackground value)
    {
        return value switch
        {
            ManagerBackground.FormerClubLegend => "Former Club Legend",
            ManagerBackground.AssistantManagerPromotion => "Assistant Manager Promotion",
            ManagerBackground.YouthAcademyCoach => "Youth Academy Coach",
            ManagerBackground.FormerPlayer => "Former Player",
            ManagerBackground.TacticalSpecialist => "Tactical Specialist",
            ManagerBackground.CrisisInterim => "Crisis Interim",
            _ => "Unknown Upstart"
        };
    }

    public static string GetDisplayName(ClubArchetype value)
    {
        return value switch
        {
            ClubArchetype.TitleContender => "Title Contender",
            ClubArchetype.FallenGiant => "Fallen Giant",
            ClubArchetype.RelegationFighter => "Relegation Fighter",
            ClubArchetype.YouthAcademyClub => "Youth Academy Club",
            ClubArchetype.SellingClub => "Selling Club",
            ClubArchetype.FinanciallyRestrictedClub => "Financially Restricted Club",
            ClubArchetype.AmbitiousNewMoneyClub => "Ambitious New-Money Club",
            ClubArchetype.ChaoticClub => "Chaotic Club",
            ClubArchetype.CommunityClub => "Community Club",
            _ => "Mid-table Stabilizer"
        };
    }

    public static string GetDisplayName(BoardPhilosophy value)
    {
        return value switch
        {
            BoardPhilosophy.WinNowBoard => "Win-Now Board",
            BoardPhilosophy.FinanciallyStrictBoard => "Financially Strict Board",
            BoardPhilosophy.YouthDevelopmentBoard => "Youth Development Board",
            BoardPhilosophy.CommercialGrowthBoard => "Commercial Growth Board",
            BoardPhilosophy.DataDrivenBoard => "Data-Driven Board",
            BoardPhilosophy.TraditionalistBoard => "Traditionalist Board",
            BoardPhilosophy.TriggerHappyBoard => "Trigger-Happy Board",
            _ => "Patient Long-Term Board"
        };
    }

    public static string GetDisplayName(FanCulture value)
    {
        return value switch
        {
            FanCulture.ResultsFirst => "Results First",
            FanCulture.DefensiveGrit => "Defensive Grit",
            FanCulture.AcademyLoyalists => "Academy Loyalists",
            FanCulture.StarPowerFans => "Star Power Fans",
            FanCulture.AntiSellingFans => "Anti-Selling Fans",
            FanCulture.DerbyObsessed => "Derby Obsessed",
            FanCulture.UnderdogLoyalists => "Underdog Loyalists",
            FanCulture.TraditionalIdentityFans => "Traditional Identity Fans",
            _ => "Attacking Football"
        };
    }

    public static string GetDisplayName(DirectorOfFootballStyle value)
    {
        return value switch
        {
            DirectorOfFootballStyle.TalentTrader => "Talent Trader",
            DirectorOfFootballStyle.StarChaser => "Star Chaser",
            DirectorOfFootballStyle.AcademyBuilder => "Academy Builder",
            DirectorOfFootballStyle.BargainHunter => "Bargain Hunter",
            DirectorOfFootballStyle.ControlFreak => "Control Freak",
            DirectorOfFootballStyle.ClubLoyalist => "Club Loyalist",
            DirectorOfFootballStyle.PoliticalSurvivor => "Political Survivor",
            _ => "Data Operator"
        };
    }

    public static string GetDisplayName(DirectorRelationshipState value)
    {
        return value switch
        {
            DirectorRelationshipState.Ally => "Ally",
            DirectorRelationshipState.Supportive => "Supportive",
            DirectorRelationshipState.Tense => "Tense",
            DirectorRelationshipState.Hostile => "Hostile",
            _ => "Neutral"
        };
    }

    public static string GetDisplayName(StaffRole value)
    {
        return value switch
        {
            StaffRole.AssistantManager => "Assistant Manager",
            StaffRole.FirstTeamCoach => "First-Team Coach",
            StaffRole.GoalkeepingCoach => "Goalkeeping Coach",
            StaffRole.FitnessCoach => "Fitness Coach",
            StaffRole.Physio => "Physio",
            StaffRole.YouthCoach => "Youth Coach",
            StaffRole.Scout => "Scout",
            StaffRole.HeadOfRecruitment => "Head of Recruitment",
            StaffRole.DataAnalyst => "Data Analyst",
            _ => "Media Officer"
        };
    }

    public static string GetDisplayName(ObjectivePriority value)
    {
        return value switch
        {
            ObjectivePriority.Critical => "Critical",
            ObjectivePriority.Important => "Important",
            ObjectivePriority.Preferred => "Preferred",
            _ => "Optional"
        };
    }

    public static string GetDisplayName(ObjectiveType value)
    {
        return value switch
        {
            ObjectiveType.CupObjective => "Cup objective",
            ObjectiveType.StyleObjective => "Style objective",
            ObjectiveType.SquadObjective => "Squad objective",
            ObjectiveType.FinancialObjective => "Financial objective",
            ObjectiveType.ReputationObjective => "Reputation objective",
            _ => "League objective"
        };
    }

    public static ManagerRole ParseRole(string value)
    {
        return value switch
        {
            "Assistant Manager" => ManagerRole.AssistantManager,
            "Head Coach" => ManagerRole.HeadCoach,
            _ => ManagerRole.Manager
        };
    }

    public static ManagerBackground ParseBackground(string value)
    {
        return value switch
        {
            "Former Club Legend" => ManagerBackground.FormerClubLegend,
            "Assistant Manager Promotion" => ManagerBackground.AssistantManagerPromotion,
            "Youth Academy Coach" => ManagerBackground.YouthAcademyCoach,
            "Former Player" => ManagerBackground.FormerPlayer,
            "Tactical Specialist" => ManagerBackground.TacticalSpecialist,
            "Crisis Interim" => ManagerBackground.CrisisInterim,
            _ => ManagerBackground.UnknownUpstart
        };
    }

    public static ManagerLicense ParseLicense(string value)
    {
        return value switch
        {
            "Grassroots License" => ManagerLicense.GrassrootsLicense,
            "National B License" => ManagerLicense.NationalBLicense,
            "National A License" => ManagerLicense.NationalALicense,
            "Pro License" => ManagerLicense.ProLicense,
            _ => ManagerLicense.NationalCLicense
        };
    }

    public static ClubArchetype ParseClubArchetype(string value)
    {
        return value switch
        {
            "Title Contender" => ClubArchetype.TitleContender,
            "Fallen Giant" => ClubArchetype.FallenGiant,
            "Relegation Fighter" => ClubArchetype.RelegationFighter,
            "Youth Academy Club" => ClubArchetype.YouthAcademyClub,
            "Selling Club" => ClubArchetype.SellingClub,
            "Financially Restricted Club" => ClubArchetype.FinanciallyRestrictedClub,
            "Ambitious New-Money Club" => ClubArchetype.AmbitiousNewMoneyClub,
            "Chaotic Club" => ClubArchetype.ChaoticClub,
            "Community Club" => ClubArchetype.CommunityClub,
            _ => ClubArchetype.MidTableStabilizer
        };
    }

    public static BoardPhilosophy ParseBoardPhilosophy(string value)
    {
        return value switch
        {
            "Win-Now Board" => BoardPhilosophy.WinNowBoard,
            "Financially Strict Board" => BoardPhilosophy.FinanciallyStrictBoard,
            "Youth Development Board" => BoardPhilosophy.YouthDevelopmentBoard,
            "Commercial Growth Board" => BoardPhilosophy.CommercialGrowthBoard,
            "Data-Driven Board" => BoardPhilosophy.DataDrivenBoard,
            "Traditionalist Board" => BoardPhilosophy.TraditionalistBoard,
            "Trigger-Happy Board" => BoardPhilosophy.TriggerHappyBoard,
            _ => BoardPhilosophy.PatientLongTermBoard
        };
    }

    public static FanCulture ParseFanCulture(string value)
    {
        return value switch
        {
            "Results First" => FanCulture.ResultsFirst,
            "Defensive Grit" => FanCulture.DefensiveGrit,
            "Academy Loyalists" => FanCulture.AcademyLoyalists,
            "Star Power Fans" => FanCulture.StarPowerFans,
            "Anti-Selling Fans" => FanCulture.AntiSellingFans,
            "Derby Obsessed" => FanCulture.DerbyObsessed,
            "Underdog Loyalists" => FanCulture.UnderdogLoyalists,
            "Traditional Identity Fans" => FanCulture.TraditionalIdentityFans,
            _ => FanCulture.AttackingFootball
        };
    }

    public static DirectorOfFootballStyle ParseDirectorStyle(string value)
    {
        return value switch
        {
            "Talent Trader" => DirectorOfFootballStyle.TalentTrader,
            "Star Chaser" => DirectorOfFootballStyle.StarChaser,
            "Academy Builder" => DirectorOfFootballStyle.AcademyBuilder,
            "Bargain Hunter" => DirectorOfFootballStyle.BargainHunter,
            "Control Freak" => DirectorOfFootballStyle.ControlFreak,
            "Club Loyalist" => DirectorOfFootballStyle.ClubLoyalist,
            "Political Survivor" => DirectorOfFootballStyle.PoliticalSurvivor,
            _ => DirectorOfFootballStyle.DataOperator
        };
    }

    public static DirectorRelationshipState ParseDirectorRelationship(string value)
    {
        return value switch
        {
            "Ally" => DirectorRelationshipState.Ally,
            "Supportive" => DirectorRelationshipState.Supportive,
            "Tense" => DirectorRelationshipState.Tense,
            "Hostile" => DirectorRelationshipState.Hostile,
            _ => DirectorRelationshipState.Neutral
        };
    }

    public static StaffRole ParseStaffRole(string value)
    {
        return value switch
        {
            "Assistant Manager" => StaffRole.AssistantManager,
            "First-Team Coach" => StaffRole.FirstTeamCoach,
            "Goalkeeping Coach" => StaffRole.GoalkeepingCoach,
            "Fitness Coach" => StaffRole.FitnessCoach,
            "Physio" => StaffRole.Physio,
            "Youth Coach" => StaffRole.YouthCoach,
            "Scout" => StaffRole.Scout,
            "Head of Recruitment" => StaffRole.HeadOfRecruitment,
            "Data Analyst" => StaffRole.DataAnalyst,
            _ => StaffRole.MediaOfficer
        };
    }

    public static ObjectivePriority ParseObjectivePriority(string value)
    {
        return value switch
        {
            "Critical" => ObjectivePriority.Critical,
            "Important" => ObjectivePriority.Important,
            "Preferred" => ObjectivePriority.Preferred,
            _ => ObjectivePriority.Optional
        };
    }

    public static ObjectiveType ParseObjectiveType(string value)
    {
        return value switch
        {
            "Cup objective" => ObjectiveType.CupObjective,
            "Style objective" => ObjectiveType.StyleObjective,
            "Squad objective" => ObjectiveType.SquadObjective,
            "Financial objective" => ObjectiveType.FinancialObjective,
            "Reputation objective" => ObjectiveType.ReputationObjective,
            _ => ObjectiveType.LeagueObjective
        };
    }

    private static (ClubArchetype archetype, BoardPhilosophy boardPhilosophy, FanCulture fanCulture, DirectorOfFootballStyle directorStyle, DirectorRelationshipState directorRelationship, int transferBudget, int wageBudget) ResolveClubTemplate(string clubName)
    {
        return clubName switch
        {
            "Northbridge City" => (
                ClubArchetype.TitleContender,
                BoardPhilosophy.WinNowBoard,
                FanCulture.ResultsFirst,
                DirectorOfFootballStyle.StarChaser,
                DirectorRelationshipState.Neutral,
                12000000,
                425000),
            "Harbor County" => (
                ClubArchetype.RelegationFighter,
                BoardPhilosophy.TraditionalistBoard,
                FanCulture.DefensiveGrit,
                DirectorOfFootballStyle.BargainHunter,
                DirectorRelationshipState.Supportive,
                2200000,
                145000),
            "Eastvale Rovers" => (
                ClubArchetype.YouthAcademyClub,
                BoardPhilosophy.YouthDevelopmentBoard,
                FanCulture.AcademyLoyalists,
                DirectorOfFootballStyle.AcademyBuilder,
                DirectorRelationshipState.Ally,
                3200000,
                155000),
            _ => (
                ClubArchetype.MidTableStabilizer,
                BoardPhilosophy.PatientLongTermBoard,
                FanCulture.AttackingFootball,
                DirectorOfFootballStyle.DataOperator,
                DirectorRelationshipState.Supportive,
                4500000,
                180000)
        };
    }

    private static StaffMember[] BuildStaff(ClubArchetype archetype, DirectorOfFootballStyle directorStyle, int worldSeed)
    {
        var seedOffset = Math.Abs(HashCode.Combine(worldSeed, archetype, directorStyle));
        var tacticalCoach = archetype == ClubArchetype.YouthAcademyClub ? "Mara Ilic" : "Leon Vale";
        var scout = directorStyle switch
        {
            DirectorOfFootballStyle.AcademyBuilder => "Evan Cho",
            DirectorOfFootballStyle.BargainHunter => "Nadia Soren",
            DirectorOfFootballStyle.StarChaser => "Bruno Cardoso",
            _ => "Iris Kavanagh"
        };

        return new[]
        {
            new StaffMember
            {
                Name = "Elias Rowe",
                Role = StaffRole.AssistantManager,
                Quality = 62 + seedOffset % 8,
                InfluenceSummary = "Supports daily preparation and reports on dressing-room mood."
            },
            new StaffMember
            {
                Name = tacticalCoach,
                Role = StaffRole.FirstTeamCoach,
                Quality = 64 + seedOffset % 10,
                InfluenceSummary = "Shapes training quality and tactical preparation."
            },
            new StaffMember
            {
                Name = "Toma Varga",
                Role = StaffRole.FitnessCoach,
                Quality = 60 + seedOffset % 9,
                InfluenceSummary = "Tracks fitness risk and recovery workload."
            },
            new StaffMember
            {
                Name = scout,
                Role = StaffRole.Scout,
                Quality = 61 + seedOffset % 11,
                InfluenceSummary = "Provides recruitment context and early opposition notes."
            },
            new StaffMember
            {
                Name = "Nico Baird",
                Role = StaffRole.DataAnalyst,
                Quality = 59 + seedOffset % 12,
                InfluenceSummary = "Adds tactical analysis and report clarity."
            },
            new StaffMember
            {
                Name = "Sana Okafor",
                Role = StaffRole.MediaOfficer,
                Quality = 58 + seedOffset % 10,
                InfluenceSummary = "Reduces avoidable media risk and frames club messaging."
            }
        };
    }

    private static Objective[] BuildObjectives(
        ClubArchetype archetype,
        BoardPhilosophy boardPhilosophy,
        string expectationSummary)
    {
        var leagueObjective = archetype switch
        {
            ClubArchetype.TitleContender => "Stay in the title conversation through the winter review.",
            ClubArchetype.RelegationFighter => "Stay clear of the bottom places and keep the dressing room together.",
            ClubArchetype.YouthAcademyClub => "Show visible league progress while keeping young players involved.",
            _ => "Finish in the top half and avoid a long dip in form."
        };

        var styleObjective = boardPhilosophy switch
        {
            BoardPhilosophy.WinNowBoard => "Impose games early and keep rivals chasing.",
            BoardPhilosophy.YouthDevelopmentBoard => "Use academy pathway minutes without collapsing results.",
            BoardPhilosophy.TraditionalistBoard => "Make home matches physically difficult and organized.",
            _ => "Build a stable tactical identity that supporters can recognize."
        };

        return new[]
        {
            new Objective
            {
                Summary = leagueObjective,
                Priority = ObjectivePriority.Critical,
                Type = ObjectiveType.LeagueObjective
            },
            new Objective
            {
                Summary = styleObjective,
                Priority = ObjectivePriority.Important,
                Type = ObjectiveType.StyleObjective
            },
            new Objective
            {
                Summary = expectationSummary.Replace("Board line: ", string.Empty),
                Priority = ObjectivePriority.Preferred,
                Type = ObjectiveType.ReputationObjective
            }
        };
    }

    private static string[] BuildOpeningNews(
        string clubName,
        CareerProfile profile,
        ClubArchetype archetype,
        BoardPhilosophy boardPhilosophy,
        FanCulture fanCulture)
    {
        return new[]
        {
            $"Official: {clubName} appoint {profile.ManagerName} as {GetDisplayName(profile.Role)}.",
            $"Board news: {GetDisplayName(boardPhilosophy)} frames the opening brief around {GetDisplayName(archetype).ToLowerInvariant()} expectations.",
            $"Fan reaction: {GetDisplayName(fanCulture)} supporters want the role authority to be visible quickly."
        };
    }

    private static (int reputation, int boardTrust, int playerTrust, int staffTrust, int directorTrust, int mediaPressure) GetBackgroundEffect(ManagerBackground background)
    {
        return background switch
        {
            ManagerBackground.FormerClubLegend => (68, 62, 70, 58, 54, 58),
            ManagerBackground.AssistantManagerPromotion => (46, 64, 62, 66, 60, 42),
            ManagerBackground.YouthAcademyCoach => (42, 58, 55, 63, 58, 38),
            ManagerBackground.FormerPlayer => (55, 54, 66, 56, 52, 48),
            ManagerBackground.TacticalSpecialist => (50, 58, 54, 60, 56, 44),
            ManagerBackground.CrisisInterim => (38, 50, 52, 56, 48, 64),
            _ => (32, 48, 46, 48, 46, 50)
        };
    }

    private static int GetLicenseTrustModifier(ManagerLicense license)
    {
        return license switch
        {
            ManagerLicense.GrassrootsLicense => -7,
            ManagerLicense.NationalBLicense => 3,
            ManagerLicense.NationalALicense => 6,
            ManagerLicense.ProLicense => 10,
            _ => 0
        };
    }

    private static int GetLicenseReputationModifier(ManagerLicense license)
    {
        return license switch
        {
            ManagerLicense.GrassrootsLicense => -5,
            ManagerLicense.NationalBLicense => 4,
            ManagerLicense.NationalALicense => 8,
            ManagerLicense.ProLicense => 12,
            _ => 0
        };
    }

    private static int GetLicensePressureModifier(ManagerLicense license)
    {
        return license switch
        {
            ManagerLicense.GrassrootsLicense => 6,
            ManagerLicense.NationalBLicense => -2,
            ManagerLicense.NationalALicense => -4,
            ManagerLicense.ProLicense => -6,
            _ => 0
        };
    }

    private static int GetRoleAccountabilityModifier(ManagerRole role)
    {
        return role switch
        {
            ManagerRole.AssistantManager => -8,
            ManagerRole.HeadCoach => 3,
            _ => 8
        };
    }

    private static int GetBackgroundPressureModifier(ManagerBackground background)
    {
        return background switch
        {
            ManagerBackground.FormerClubLegend => 7,
            ManagerBackground.CrisisInterim => 10,
            ManagerBackground.UnknownUpstart => 5,
            ManagerBackground.AssistantManagerPromotion => -3,
            ManagerBackground.YouthAcademyCoach => -2,
            _ => 0
        };
    }

    private static int GetClubPressureModifier(ClubArchetype archetype)
    {
        return archetype switch
        {
            ClubArchetype.TitleContender => 12,
            ClubArchetype.RelegationFighter => 8,
            ClubArchetype.ChaoticClub => 15,
            ClubArchetype.FallenGiant => 10,
            ClubArchetype.YouthAcademyClub => -2,
            ClubArchetype.CommunityClub => -4,
            _ => 2
        };
    }

    private static int GetBoardPressureModifier(BoardPhilosophy boardPhilosophy)
    {
        return boardPhilosophy switch
        {
            BoardPhilosophy.WinNowBoard => 10,
            BoardPhilosophy.TriggerHappyBoard => 14,
            BoardPhilosophy.FinanciallyStrictBoard => 5,
            BoardPhilosophy.PatientLongTermBoard => -4,
            BoardPhilosophy.YouthDevelopmentBoard => -3,
            _ => 0
        };
    }

    private static string FormatMoney(int amount)
    {
        return amount >= 1000000
            ? $"${amount / 1000000.0:0.0}m"
            : $"${amount / 1000}k";
    }
}

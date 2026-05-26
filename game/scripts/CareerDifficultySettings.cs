using System;

public enum StrictRealismSetting
{
    Relaxed,
    Balanced,
    Strict
}

public enum DramaFrequencySetting
{
    Low,
    Balanced,
    High
}

public enum ScoutingDifficultySetting
{
    Generous,
    Balanced,
    Strict
}

public enum SackingStrictnessSetting
{
    Forgiving,
    Balanced,
    Harsh
}

public enum TransferDifficultySetting
{
    Easier,
    Balanced,
    Hard
}

public enum HiddenInfoSetting
{
    LowUncertainty,
    BalancedUncertainty,
    HighUncertainty
}

public enum MatchRandomnessSetting
{
    Low,
    Balanced,
    High
}

public enum FinanceDifficultySetting
{
    Forgiving,
    Balanced,
    Strict
}

public sealed class CareerDifficultyProfile
{
    public StrictRealismSetting StrictRealism { get; init; } = StrictRealismSetting.Balanced;
    public DramaFrequencySetting DramaFrequency { get; init; } = DramaFrequencySetting.Balanced;
    public ScoutingDifficultySetting ScoutingDifficulty { get; init; } = ScoutingDifficultySetting.Balanced;
    public SackingStrictnessSetting SackingStrictness { get; init; } = SackingStrictnessSetting.Balanced;
    public TransferDifficultySetting TransferDifficulty { get; init; } = TransferDifficultySetting.Balanced;
    public HiddenInfoSetting HiddenInfo { get; init; } = HiddenInfoSetting.BalancedUncertainty;
    public MatchRandomnessSetting MatchRandomness { get; init; } = MatchRandomnessSetting.Balanced;
    public FinanceDifficultySetting FinanceDifficulty { get; init; } = FinanceDifficultySetting.Balanced;

    public string Summary =>
        $"Difficulty | realism {GetDisplayName(StrictRealism)} | drama {GetDisplayName(DramaFrequency)} | scouting {GetDisplayName(ScoutingDifficulty)} | sacking {GetDisplayName(SackingStrictness)} | transfers {GetDisplayName(TransferDifficulty)} | hidden info {GetDisplayName(HiddenInfo)} | match randomness {GetDisplayName(MatchRandomness)} | finance {GetDisplayName(FinanceDifficulty)}";

    public static CareerDifficultyProfile BalancedDefaults() => new();

    public static string GetDisplayName(StrictRealismSetting value) => value switch
    {
        StrictRealismSetting.Relaxed => "Relaxed",
        StrictRealismSetting.Strict => "Strict",
        _ => "Balanced"
    };

    public static string GetDisplayName(DramaFrequencySetting value) => value switch
    {
        DramaFrequencySetting.Low => "Low",
        DramaFrequencySetting.High => "High",
        _ => "Balanced"
    };

    public static string GetDisplayName(ScoutingDifficultySetting value) => value switch
    {
        ScoutingDifficultySetting.Generous => "Generous",
        ScoutingDifficultySetting.Strict => "Strict",
        _ => "Balanced"
    };

    public static string GetDisplayName(SackingStrictnessSetting value) => value switch
    {
        SackingStrictnessSetting.Forgiving => "Forgiving",
        SackingStrictnessSetting.Harsh => "Harsh",
        _ => "Balanced"
    };

    public static string GetDisplayName(TransferDifficultySetting value) => value switch
    {
        TransferDifficultySetting.Easier => "Easier",
        TransferDifficultySetting.Hard => "Hard",
        _ => "Balanced"
    };

    public static string GetDisplayName(HiddenInfoSetting value) => value switch
    {
        HiddenInfoSetting.LowUncertainty => "Low uncertainty",
        HiddenInfoSetting.HighUncertainty => "High uncertainty",
        _ => "Balanced uncertainty"
    };

    public static string GetDisplayName(MatchRandomnessSetting value) => value switch
    {
        MatchRandomnessSetting.Low => "Low",
        MatchRandomnessSetting.High => "High",
        _ => "Balanced"
    };

    public static string GetDisplayName(FinanceDifficultySetting value) => value switch
    {
        FinanceDifficultySetting.Forgiving => "Forgiving",
        FinanceDifficultySetting.Strict => "Strict",
        _ => "Balanced"
    };

    public static StrictRealismSetting ParseStrictRealism(string value) => value switch
    {
        "Relaxed" => StrictRealismSetting.Relaxed,
        "Strict" => StrictRealismSetting.Strict,
        _ => StrictRealismSetting.Balanced
    };

    public static DramaFrequencySetting ParseDramaFrequency(string value) => value switch
    {
        "Low" => DramaFrequencySetting.Low,
        "High" => DramaFrequencySetting.High,
        _ => DramaFrequencySetting.Balanced
    };

    public static ScoutingDifficultySetting ParseScoutingDifficulty(string value) => value switch
    {
        "Generous" => ScoutingDifficultySetting.Generous,
        "Strict" => ScoutingDifficultySetting.Strict,
        _ => ScoutingDifficultySetting.Balanced
    };

    public static SackingStrictnessSetting ParseSackingStrictness(string value) => value switch
    {
        "Forgiving" => SackingStrictnessSetting.Forgiving,
        "Harsh" => SackingStrictnessSetting.Harsh,
        _ => SackingStrictnessSetting.Balanced
    };

    public static TransferDifficultySetting ParseTransferDifficulty(string value) => value switch
    {
        "Easier" => TransferDifficultySetting.Easier,
        "Hard" => TransferDifficultySetting.Hard,
        _ => TransferDifficultySetting.Balanced
    };

    public static HiddenInfoSetting ParseHiddenInfo(string value) => value switch
    {
        "Low uncertainty" => HiddenInfoSetting.LowUncertainty,
        "High uncertainty" => HiddenInfoSetting.HighUncertainty,
        _ => HiddenInfoSetting.BalancedUncertainty
    };

    public static MatchRandomnessSetting ParseMatchRandomness(string value) => value switch
    {
        "Low" => MatchRandomnessSetting.Low,
        "High" => MatchRandomnessSetting.High,
        _ => MatchRandomnessSetting.Balanced
    };

    public static FinanceDifficultySetting ParseFinanceDifficulty(string value) => value switch
    {
        "Forgiving" => FinanceDifficultySetting.Forgiving,
        "Strict" => FinanceDifficultySetting.Strict,
        _ => FinanceDifficultySetting.Balanced
    };

    public int KnowledgeScoreModifier() =>
        (HiddenInfoModifier() + ScoutingKnowledgeModifier()) / 2;

    public int HiddenInfoModifier() => HiddenInfo switch
    {
        HiddenInfoSetting.LowUncertainty => 14,
        HiddenInfoSetting.HighUncertainty => -18,
        _ => 0
    };

    public int ScoutingKnowledgeModifier() => ScoutingDifficulty switch
    {
        ScoutingDifficultySetting.Generous => 10,
        ScoutingDifficultySetting.Strict => -14,
        _ => 0
    };

    public int ScoutingReportDelayModifier() => ScoutingDifficulty switch
    {
        ScoutingDifficultySetting.Generous => -1,
        ScoutingDifficultySetting.Strict => 2,
        _ => 0
    };

    public int ObjectivePressureModifier() =>
        StrictRealismModifier() + SackingPressureModifier() + FinancePressureModifier();

    public int StrictRealismModifier() => StrictRealism switch
    {
        StrictRealismSetting.Relaxed => -10,
        StrictRealismSetting.Strict => 12,
        _ => 0
    };

    public int SackingPressureModifier() => SackingStrictness switch
    {
        SackingStrictnessSetting.Forgiving => -14,
        SackingStrictnessSetting.Harsh => 14,
        _ => 0
    };

    public int WarningThresholdModifier() => SackingStrictness switch
    {
        SackingStrictnessSetting.Forgiving => 6,
        SackingStrictnessSetting.Harsh => -8,
        _ => 0
    };

    public int UltimatumThresholdModifier() => SackingStrictness switch
    {
        SackingStrictnessSetting.Forgiving => 8,
        SackingStrictnessSetting.Harsh => -10,
        _ => 0
    };

    public int TransferFrictionModifier() =>
        StrictRealismModifier() + TransferDifficulty switch
        {
            TransferDifficultySetting.Easier => -6,
            TransferDifficultySetting.Hard => 10,
            _ => 0
        };

    public int FinancePressureModifier() => FinanceDifficulty switch
    {
        FinanceDifficultySetting.Forgiving => -10,
        FinanceDifficultySetting.Strict => 12,
        _ => 0
    };

    public int FinanceBudgetModifierPercent() => FinanceDifficulty switch
    {
        FinanceDifficultySetting.Forgiving => 8,
        FinanceDifficultySetting.Strict => -10,
        _ => 0
    };

    public int MatchRandomnessSpread() => MatchRandomness switch
    {
        MatchRandomnessSetting.Low => 0,
        MatchRandomnessSetting.High => 2,
        _ => 1
    };

    public int DramaEventChanceModifier() => DramaFrequency switch
    {
        DramaFrequencySetting.Low => -2,
        DramaFrequencySetting.High => 2,
        _ => 0
    };
}

public static class CareerDifficultyOptions
{
    public static readonly string[] StrictRealism = { "Relaxed", "Balanced", "Strict" };
    public static readonly string[] DramaFrequency = { "Low", "Balanced", "High" };
    public static readonly string[] ScoutingDifficulty = { "Generous", "Balanced", "Strict" };
    public static readonly string[] SackingStrictness = { "Forgiving", "Balanced", "Harsh" };
    public static readonly string[] TransferDifficulty = { "Easier", "Balanced", "Hard" };
    public static readonly string[] HiddenInfo = { "Low uncertainty", "Balanced uncertainty", "High uncertainty" };
    public static readonly string[] MatchRandomness = { "Low", "Balanced", "High" };
    public static readonly string[] FinanceDifficulty = { "Forgiving", "Balanced", "Strict" };
}

/// <summary>
/// Central balance baselines for the post-implementation tuning pass.
/// Values are intentionally modest so difficulty settings and context still dominate outcomes.
/// </summary>
public static class TouchlineBalanceConstants
{
    public const int ObjectiveWarningThreshold = 68;
    public const int ObjectiveUltimatumThreshold = 84;
    public const int ObjectiveSackingThreshold = 96;

    public const int MoraleSwingPerMatchMax = 8;
    public const int TrustSwingPerWeekMax = 2;
    public const int ReputationSwingPerMajorEventMax = 5;

    public const int TransferFrictionBase = 12;
    public const int DevelopmentWeeklyGainCap = 3;
    public const int InjuryRiskWeeklyCap = 6;

    public const int MatchGoalVarianceBalanced = 1;
    public const int MatchGoalVarianceHigh = 2;
}

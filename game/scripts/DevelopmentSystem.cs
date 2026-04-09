using System;

public static class DevelopmentSystem
{
    public static GameState.SquadPlayer[] ApplySeasonRollover(
        GameState.SquadPlayer[] squadPlayers,
        int worldSeed,
        int seasonStartYear)
    {
        return Array.ConvertAll(
            squadPlayers,
            player =>
            {
                var nextAge = player.Age + 1;
                var rng = new Random(Math.Abs(HashCode.Combine(worldSeed, seasonStartYear, player.Name, player.Position)));
                var formDelta = CalculateFormDelta(nextAge, rng);
                var fitnessDelta = CalculateFitnessDelta(nextAge, rng);
                var moraleDelta = formDelta > 0 ? 2 : formDelta < 0 ? -2 : 0;

                return new GameState.SquadPlayer
                {
                    Name = player.Name,
                    Position = player.Position,
                    Age = nextAge,
                    Form = Math.Clamp(player.Form + formDelta, 55, 90),
                    Morale = Math.Clamp(player.Morale + moraleDelta, 50, 90),
                    Fitness = Math.Clamp(player.Fitness + fitnessDelta, 72, 95),
                    IsStarting = player.IsStarting
                };
            });
    }

    private static int CalculateFormDelta(int age, Random rng)
    {
        if (age <= 22)
        {
            return 1 + rng.Next(0, 3);
        }

        if (age <= 28)
        {
            return rng.Next(-1, 2);
        }

        return -1 - rng.Next(0, 3);
    }

    private static int CalculateFitnessDelta(int age, Random rng)
    {
        if (age <= 22)
        {
            return rng.Next(0, 2);
        }

        if (age <= 28)
        {
            return rng.Next(-1, 2);
        }

        return -1 - rng.Next(0, 2);
    }
}

namespace StringEvolution.Models;

public sealed class GeneticAlgorithmSettings
{
    public int PopulationSize { get; init; } = 200;

    public double MutationProbability { get; init; } = 0.01;

    public int TournamentSize { get; init; } = 3;

    public string Alphabet { get; init; }
        = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        + "abcdefghijklmnopqrstuvwxyz"
        + "0123456789"
        + " .,!?-;:\"'";

    public int DelayMilliseconds { get; init; } = 500;

    public int StepsPerFrame { get; init; } = 1;
}

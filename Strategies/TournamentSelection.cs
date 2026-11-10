using System;

namespace StringEvolution.Strategies;

public sealed class TournamentSelection(int tournamentSize) : ISelectionStrategy
{
    private readonly int _tournamentSize = tournamentSize;

    public Individual Select(Individual[] population, Random random)
    {
        Individual? best = null;
        for (int i = 0; i < _tournamentSize; ++i)
        {
            var candidate = population[random.Next(population.Length)];
            if (best is null || candidate.Fitness > best.Fitness)
            {
                best = candidate;
            }
        }
        return best!;
    }
}

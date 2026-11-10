using System;

namespace StringEvolution.Infrastructure;

public static class GeneticAlgorithmFactory
{
    public static GeneticAlgorithm Create(string target, GeneticAlgorithmSettings settings)
    {
        var fitnessEvaluator = new StringFitnessEvaluator(target);
        var selectionStrategy = new TournamentSelection(settings.TournamentSize);
        var crossoverStrategy = new UniformCrossover();
        var mutationStrategy = new PointMutation(settings.MutationProbability, settings.Alphabet);

        var population = new Population(settings.PopulationSize, fitnessEvaluator);

        Individual CreateRandomIndividual()
        {
            var genes = new char[target.Length];
            for (int i = 0; i < genes.Length; i++)
            {
                genes[i] = settings.Alphabet[Random.Shared.Next(settings.Alphabet.Length)];
            }
            return new(genes);
        }

        return new
        (
            population,
            selectionStrategy,
            crossoverStrategy,
            mutationStrategy,
            CreateRandomIndividual
        );
    }
}

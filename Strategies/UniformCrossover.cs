using System;

namespace StringEvolution.Strategies;

public sealed class UniformCrossover : ICrossoverStrategy
{
    public Individual Crossover(Individual parentOne, Individual parentTwo, Random random)
    {
        var genes = new char[parentOne.Genes.Length];
        for (int i = 0; i < genes.Length; ++i)
        {
            genes[i] = random.Next(2) == 0 ? parentOne.Genes[i] : parentTwo.Genes[i];
        }
        return new(genes);
    }
}

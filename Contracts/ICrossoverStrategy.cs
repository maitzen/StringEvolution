using System;

namespace StringEvolution.Contracts;

public interface ICrossoverStrategy
{
    Individual Crossover(Individual parentOne, Individual parentTwo, Random random);
}

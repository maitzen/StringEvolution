using System;

namespace StringEvolution.Contracts;

public interface ISelectionStrategy
{
    Individual Select(Individual[] population, Random random);
}

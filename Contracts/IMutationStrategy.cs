using System;

namespace StringEvolution.Contracts;

public interface IMutationStrategy
{
    void Mutate(Individual individual, Random random);
}

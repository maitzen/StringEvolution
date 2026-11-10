using System;

namespace StringEvolution.Strategies;

public sealed class StringFitnessEvaluator(string target) : IFitnessEvaluator
{
    private readonly string _target = target ?? throw new ArgumentNullException(nameof(target));

    public int Evaluate(Individual individual)
    {
        int fitness = 0;
        for (int i = 0; i < _target.Length; ++i)
        {
            if (individual.Genes[i] == _target[i])
            {
                ++fitness;
            }
        }
        return fitness;
    }
}

using System;

namespace StringEvolution.Strategies;

public sealed class PointMutation(double mutationProbability, string alphabet) : IMutationStrategy
{
    private readonly double _mutationProbability = mutationProbability;

    private readonly string _alphabet = alphabet ?? throw new ArgumentNullException(nameof(alphabet));

    public void Mutate(Individual individual, Random random)
    {
        for (int i = 0; i < individual.Genes.Length; ++i)
        {
            if (random.NextDouble() < _mutationProbability)
            {
                individual.Genes[i] = _alphabet[random.Next(_alphabet.Length)];
            }
        }
    }
}

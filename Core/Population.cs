using System;
using System.Linq;

namespace StringEvolution.Core;

public sealed class Population(int size, IFitnessEvaluator fitnessEvaluator)
{
    private readonly IFitnessEvaluator _fitnessEvaluator = fitnessEvaluator
        ?? throw new ArgumentNullException(nameof(fitnessEvaluator));

    public Individual[] Individuals { get; private set; } = new Individual[size];

    public int Size => Individuals.Length;

    public Individual Best => Individuals.OrderByDescending(i => i.Fitness).First();

    public double AverageFitness => Individuals.Average(i => i.Fitness);

    public void Initialize(Func<Individual> individualFactory)
    {
        for (int i = 0; i < Individuals.Length; ++i)
        {
            Individuals[i] = individualFactory();
        }
        EvaluateAll();
    }

    public void ReplaceWith(Individual[] newIndividuals)
    {
        Individuals = newIndividuals;
        EvaluateAll();
    }

    private void EvaluateAll()
    {
        foreach (var individual in Individuals)
        {
            individual.Fitness = _fitnessEvaluator.Evaluate(individual);
        }
    }
}

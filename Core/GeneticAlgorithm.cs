using System;

namespace StringEvolution.Core;

public sealed class GeneticAlgorithm
{
    private readonly Population _population;

    private readonly ISelectionStrategy _selectionStrategy;

    private readonly ICrossoverStrategy _crossoverStrategy;

    private readonly IMutationStrategy _mutationStrategy;

    private readonly Func<Individual> _individualFactory;

    private readonly Random _random = Random.Shared;

    public GeneticAlgorithm
    (
        Population population,
        ISelectionStrategy selectionStrategy,
        ICrossoverStrategy crossoverStrategy,
        IMutationStrategy mutationStrategy,
        Func<Individual> individualFactory
    )
    {
        _population = population
            ?? throw new ArgumentNullException(nameof(population));

        _selectionStrategy = selectionStrategy
            ?? throw new ArgumentNullException(nameof(selectionStrategy));

        _crossoverStrategy = crossoverStrategy
            ?? throw new ArgumentNullException(nameof(crossoverStrategy));

        _mutationStrategy = mutationStrategy
            ?? throw new ArgumentNullException(nameof(mutationStrategy));

        _individualFactory = individualFactory
            ?? throw new ArgumentNullException(nameof(individualFactory));

        Generation = 0;
        _population.Initialize(_individualFactory);
        BestEver = FindNewBestEverOrGetOldBestEver();
    }

    public int Generation { get; private set; }

    public Individual BestEver { get; private set; }

    public Individual CurrentBest => _population.Best;

    public double AverageFitness => _population.AverageFitness;

    public bool IsSolved =>
        BestEver is not null
        && BestEver.Fitness == _population.Individuals[0].Genes.Length;

    public void Evolve()
    {
        if (IsSolved)
        {
            return;
        }
        var newPopulation = new Individual[_population.Size];
        for (int i = 0; i < _population.Size; i++)
        {
            var parent1 = _selectionStrategy.Select(_population.Individuals, _random);
            var parent2 = _selectionStrategy.Select(_population.Individuals, _random);
            var child = _crossoverStrategy.Crossover(parent1, parent2, _random);
            _mutationStrategy.Mutate(child, _random);
            newPopulation[i] = child;
        }
        _population.ReplaceWith(newPopulation);
        BestEver = FindNewBestEverOrGetOldBestEver();
        ++Generation;
    }

    private Individual FindNewBestEverOrGetOldBestEver()
    {
        var currentBest = _population.Best;
        if (BestEver is null || currentBest.Fitness > BestEver.Fitness)
        {
            return currentBest;
        }
        return BestEver;
    }
}

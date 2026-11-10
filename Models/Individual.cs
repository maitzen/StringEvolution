using System;

namespace StringEvolution.Models;

public sealed class Individual(char[] genes)
{
    public char[] Genes { get; } = genes ?? throw new ArgumentNullException(nameof(genes));

    public int Fitness { get; set; }

    public string GenesToString() => new(Genes);
}

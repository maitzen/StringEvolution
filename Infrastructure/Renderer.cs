using System;
using System.Collections.Generic;
using System.Text;

namespace StringEvolution.Infrastructure;

public sealed class Renderer(string target)
{
    private readonly string _target = target;

    private readonly List<(int generation, string text, int fitness)> _improvementLog = [];

    private int _lastBestFitness = -1;

    public void Draw(GeneticAlgorithm algorithm, double generationsPerSecond)
    {
        var best = algorithm.CurrentBest;

        if (best.Fitness > _lastBestFitness)
        {
            _improvementLog.Add((algorithm.Generation, best.GenesToString(), best.Fitness));
            _lastBestFitness = best.Fitness;
        }

        var buffer = new StringBuilder();
        Console.SetCursorPosition(0, 0);

        buffer.Append("ЦЕЛЬ            ");
        buffer.AppendLine(_target);

        buffer.Append("ЛУЧШАЯ          ");
        buffer.AppendLine(best.GenesToString());

        buffer.AppendLine();
        buffer.AppendLine($"ПОКОЛЕНИЕ     {algorithm.Generation}");
        buffer.AppendLine($"ФИТНЕС        {best.Fitness}/{_target.Length}");
        buffer.AppendLine($"СРЕДНИЙ       {algorithm.AverageFitness:F3}");
        buffer.AppendLine($"СКОРОСТЬ      {generationsPerSecond:F3}    ");

        buffer.AppendLine();
        buffer.AppendLine("ЭВОЛЮЦИЯ");
        foreach (var (generation, text, fitness) in _improvementLog)
        {
            buffer.AppendLine($"#{generation:D3}{fitness,5}{text,20}");
        }

        Console.Write(buffer.ToString());

        int bestLineIndex = 1;
        Console.SetCursorPosition("ЛУЧШАЯ          ".Length, bestLineIndex);
        for (int i = 0; i < _target.Length; i++)
        {
            Console.ForegroundColor = best.Genes[i] == _target[i]
                ? ConsoleColor.Green : ConsoleColor.Red;
            Console.Write(best.Genes[i]);
        }
        Console.ResetColor();
    }

    public static void DrawFinal(GeneticAlgorithm algorithm)
    {
        Console.Clear();
        Console.WriteLine("Эволюция завершена!");
        Console.WriteLine($"Потребовалось поколений: {algorithm.Generation}.");
        Console.WriteLine($"Результат: {algorithm.BestEver.GenesToString()}.");
    }
}

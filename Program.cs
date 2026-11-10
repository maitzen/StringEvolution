using System;
using System.Threading;

namespace StringEvolution;

public static class Program
{
    public static void Main()
    {
        Console.Title = "String Evolution";
        Console.CursorVisible = false;

        Console.Write("Введите целевую фразу (или нажмите Enter для 'Hello, World!'): ");
        var target = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(target))
        {
            target = "Hello, World!";
        }

        var settings = new GeneticAlgorithmSettings();
        var algorithm = GeneticAlgorithmFactory.Create(target, settings);
        var renderer = new Renderer(target);

        Console.Clear();

        var startTime = DateTime.UtcNow;
        int generationsSinceStart = 0;

        while (!algorithm.IsSolved)
        {
            for (int i = 0; i < settings.StepsPerFrame; ++i)
            {
                algorithm.Evolve();
                ++generationsSinceStart;
                if (algorithm.IsSolved)
                {
                    break;
                }
            }

            var elapsedSeconds = (DateTime.UtcNow - startTime).TotalSeconds;

            var generationsPerSecond = elapsedSeconds > 0
                ? generationsSinceStart / elapsedSeconds : 0;

            renderer.Draw(algorithm, generationsPerSecond);
            Thread.Sleep(settings.DelayMilliseconds);
        }

        Renderer.DrawFinal(algorithm);
        Console.Write("\nНажмите любую клавишу...");
        Console.CursorVisible = true;
        Console.ReadKey(intercept: true);
    }
}

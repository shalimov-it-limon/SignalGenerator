using System;
using core;

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите тип сигнала: 1 - Синусоида, 2 - Меандр");

        var choice = Console.ReadLine();
        Console.Write("Амплитуда: ");
        double amplitude = double.Parse(Console.ReadLine());

        Console.Write("Частота: ");
        double frequency = double.Parse(Console.ReadLine());

        Console.Write("Количество точек: ");
        int points = int.Parse(Console.ReadLine());

        ISignalGenerator generator = choice == "1"
            ? new SineSignalGenerator()
            : new SquareSignalGenerator();

        var signal = generator.Generate(amplitude, frequency, points);

        Console.WriteLine($"Max: {SignalProcessor.Max(signal)}");
        Console.WriteLine($"Min: {SignalProcessor.Min(signal)}");
        Console.WriteLine($"Avg: {SignalProcessor.Average(signal)}");
        Console.WriteLine($"Zero crossings: {SignalProcessor.ZeroCrossings(signal)}");

        new FileSignalRepository().Save(choice == "1" ? "sine" : "square", amplitude, frequency, signal);

        Console.WriteLine("Файл сохранен.");
    }
}

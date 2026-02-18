using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace core
{
    //Модель точки сигнала
    public class SignalPoint
    {
        public double Time { get; }
        public double Value { get; }

        public SignalPoint(double time, double value)
        {
            Time = time;
            Value = value;
        }
    }

    //Интерфейс генератора
    public interface ISignalGenerator
    {
        List<SignalPoint> Generate(double amplitude, double frequency, int points);
    }

    //Генератор синусоиды
    public class SineSignalGenerator : ISignalGenerator
    {
        public List<SignalPoint> Generate(double amplitude, double frequency, int points)
        {
            Validate(points);

            var result = new List<SignalPoint>();
            double step = 1.0 / points;

            for (int i = 0; i < points; i++)
            {
                double time = i * step;
                double value = amplitude * Math.Sin(2 * Math.PI * frequency * time);
                result.Add(new SignalPoint(time, value));
            }

            return result;
        }

        private void Validate(int points)
        {
            if (points < 100 || points > 10000)
                throw new ArgumentException("Количество точек должно быть от 100 до 10000.");
        }
    }

    //Генератор меандра
    public class SquareSignalGenerator : ISignalGenerator
    {
        public List<SignalPoint> Generate(double amplitude, double frequency, int points)
        {
            if (points < 100 || points > 10000)
                throw new ArgumentException("Количество точек должно быть от 100 до 10000.");

            var result = new List<SignalPoint>();
            double step = 1.0 / points;

            for (int i = 0; i < points; i++)
            {
                double time = i * step;
                double value = Math.Sin(2 * Math.PI * frequency * time) >= 0
                    ? amplitude
                    : -amplitude;

                result.Add(new SignalPoint(time, value));
            }

            return result;
        }
    }

    //Обработка сигналов
    public static class SignalProcessor
    {
        public static double Max(List<SignalPoint> points) =>
            points.Max(p => p.Value);

        public static double Min(List<SignalPoint> points) =>
            points.Min(p => p.Value);

        public static double Average(List<SignalPoint> points) =>
            points.Average(p => p.Value);

        public static int ZeroCrossings(List<SignalPoint> points)
        {
            int count = 0;

            for (int i = 1; i < points.Count; i++)
            {
                if (points[i - 1].Value * points[i].Value < 0)
                    count++;
            }

            return count;
        }
    }

    public class FileSignalRepository
    {
        public void Save(string type, double amplitude, double frequency, List<SignalPoint> points)
        {
            string fileName = $"{type}_A{amplitude}_F{frequency}_{DateTime.Now:yyyyMMddHHmmss}.txt";

            using var writer = new StreamWriter(fileName);
            writer.WriteLine($"Type: {type}, Amplitude: {amplitude}, Frequency: {frequency}");
            writer.WriteLine("Time,Value");

            foreach (var p in points)
                writer.WriteLine($"{p.Time},{p.Value}");
        }
    }



}

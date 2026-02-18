using System;
using Xunit;
using core;
using ConsoleApp;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    public class SineGeneratorTests
    {
        [Fact]
        public void Generate_ShouldReturnCorrectCount()
        {
            var generator = new SineSignalGenerator();
            var result = generator.Generate(5, 1, 1000);

            Assert.Equal(1000, result.Count);
        }

        [Fact]
        public void Generate_InvalidPoints_ShouldThrow()
        {
            var generator = new SineSignalGenerator();
            Assert.Throws<ArgumentException>(() => generator.Generate(1, 1, 10));
        }
    }

    public class ProcessorTests
    {
        [Fact]
        public void Max_ShouldReturnCorrectValue()
        {
            var points = new List<SignalPoint>
        {
            new SignalPoint(0, -1),
            new SignalPoint(1, 5),
            new SignalPoint(2, 2)
        };

            Assert.Equal(5, SignalProcessor.Max(points));
        }
    }

    public class FileTests
    {
        [Fact]
        public void Save_ShouldCreateFile()
        {
            var repo = new FileSignalRepository();
            var points = new List<SignalPoint>
        {
            new SignalPoint(0, 1)
        };

            repo.Save("test", 1, 1, points);

            var file = Directory.GetFiles(Directory.GetCurrentDirectory(), "test_*.txt");
            Assert.NotEmpty(file);
        }
    }



}

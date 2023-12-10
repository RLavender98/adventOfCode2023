namespace AOC2023;

public static class DayNine
{

    private static List<int> CalculateNextLayer(List<int> previousLayer)
    {
        return previousLayer.Skip(1).Select((val, index) => val - previousLayer[index]).ToList();
    }

    private static bool AllZero(List<int> layer)
    {
        return layer.All(val => val == 0);
    }

    public static void Run()
    {
        var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day9.txt";
        var lines = File.ReadLines(fileLocation).Select(line => line.Split(" "));
        var histories = lines.Select(valArray => valArray.Select(entry => int.Parse(entry)).ToList()).ToList();
        
        var sum = 0;
        foreach (var history in histories)
        {
            var layers = new List<List<int>>();
            layers.Add(history);

            var shouldContinue = true;
            while (shouldContinue)
            {
                layers.Add(CalculateNextLayer(layers[^1]));
                shouldContinue = !AllZero(layers[^1]);
            }

            foreach (var layer in layers)
            {
                layer.Reverse();
            }
            for (var i = layers.Count - 2; i >= 0; i--)
            {
                layers[i].Add(layers[i][^1] - layers[i + 1][^1]);
            }

            sum += layers[0][^1];
        }
        
        Console.WriteLine(sum);
    }
}
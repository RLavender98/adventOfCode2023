using System.Text.RegularExpressions;

namespace AOC2023;

public static class DayOne
{
    public static void Run()
    {
        var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day1.txt";
        var lines = File.ReadLines(fileLocation).ToList();

        var numbers = new List<string>()
        {
            "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
        };
        Console.WriteLine(numbers);
        var digitRegex = new Regex(@"\d|one|two|three|four|five|six|seven|eight|nine");
        var calibrationValues = lines.Select(line => digitRegex.Matches(line))
            .Select(digits => digits.Select(digit => $"{digit}".Length > 1 ? $"{numbers.IndexOf($"{digit}") + 1}" : $"{digit}").ToList())
            .Select(digits => int.Parse($"{digits[0]}{digits[^1]}"));
        Console.WriteLine(calibrationValues.Sum(x => x));
    }
}
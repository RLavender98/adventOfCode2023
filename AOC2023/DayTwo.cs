using System.Text.RegularExpressions;

namespace AOC2023;

// 12 red cubes, 13 green cubes, and 14 blue cubes
public static class DayTwo
{
    // Part One
    // public static void Run()
    // {
    //     var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day2.txt";
    //     var lines = File.ReadLines(fileLocation).ToList();
    //
    //     var idRegex = new Regex(@"(\d+)");
    //
    //     var splitLines = lines.Select(line => line.Split(":"));
    //     var idSum = 0;
    //     foreach (var splitLine in splitLines)
    //     {
    //         var id = int.Parse($"{idRegex.Match(splitLine[0]).Groups[1]}");
    //         var pulls = splitLine[1].Split(";");
    //         var include = true;
    //         foreach (var pull in pulls)
    //         {
    //             var colours = pull.Split(",");
    //             foreach (var colour in colours)
    //             {
    //                 if (pull.Contains("red"))
    //                 {
    //                     var red = int.Parse($"{new Regex(@"(\d+) red").Match(pull).Groups[1]}");
    //                     if (red > 12)
    //                     {
    //                         include = false;
    //                         break;
    //                     }
    //                 }
    //                 if (pull.Contains("green"))
    //                 {
    //                     var green = int.Parse($"{new Regex(@"(\d+) green").Match(pull).Groups[1]}");
    //                     if (green > 13)
    //                     {
    //                         include = false;
    //                         break;
    //                     }
    //                 }
    //                 if (pull.Contains("blue"))
    //                 {
    //                     var blue = int.Parse($"{new Regex(@"(\d+) blue").Match(pull).Groups[1]}");
    //                     if (blue > 14)
    //                     {
    //                         include = false;
    //                         break;
    //                     }
    //                 }
    //             }
    //         }
    //
    //         if (include == true)
    //         {
    //             idSum += id;
    //         }
    //     }
    //     Console.WriteLine(idSum);
    // }
    
    // Part Two
    public static void Run()
    {
        var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day2.txt";
        var lines = File.ReadLines(fileLocation).ToList();

        var idRegex = new Regex(@"(\d+)");

        var splitLines = lines.Select(line => line.Split(":"));
        var powerSum = 0;
        foreach (var splitLine in splitLines)
        {
            var id = int.Parse($"{idRegex.Match(splitLine[0]).Groups[1]}");
            var pulls = splitLine[1].Split(";");
            var redMax = 0;
            var blueMax = 0;
            var greeNMAX = 0;
            foreach (var pull in pulls)
            {
                var colours = pull.Split(",");
                foreach (var colour in colours)
                {
                    if (pull.Contains("red"))
                    {
                        var red = int.Parse($"{new Regex(@"(\d+) red").Match(pull).Groups[1]}");
                        if (red > redMax)
                        {
                            redMax = red;
                        }
                    }
                    if (pull.Contains("green"))
                    {
                        var green = int.Parse($"{new Regex(@"(\d+) green").Match(pull).Groups[1]}");
                        if (green > greeNMAX)
                        {
                            greeNMAX = green;
                        }
                    }
                    if (pull.Contains("blue"))
                    {
                        var blue = int.Parse($"{new Regex(@"(\d+) blue").Match(pull).Groups[1]}");
                        if (blue > blueMax)
                        {
                            blueMax = blue;
                        }
                    }
                }
            }

            var power = redMax * greeNMAX * blueMax;
            powerSum += power;
        }
        Console.WriteLine(powerSum);
    }
}
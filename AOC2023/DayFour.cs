using System.Numerics;
using System.Text.RegularExpressions;

namespace AOC2023;

public static class DayFour
{
    // public static void Run()
    // {
    //     var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day4.txt";
    //     var lines = File.ReadLines(fileLocation).ToList();
    //
    //     var numberRegex = new Regex(@"\d+");
    //     var parsed = lines.Select(line => numberRegex.Matches(line).Select(match => match.Value).Skip(1));
    //
    //     var pointsTotal = 0;
    //     foreach (var par in parsed)
    //     {
    //         var points = 0;
    //         var winningNumbers = par.Take(10).ToList();
    //         var otherNumbers = par.Skip(10).ToList();
    //
    //         foreach (var onum in otherNumbers)
    //         {
    //             if(winningNumbers.Contains(onum))
    //             {
    //                 if (points == 0)
    //                 {
    //                     points = 1;
    //                 }
    //                 else
    //                 {
    //                     points *= 2;
    //                 }
    //             }
    //         }
    //
    //         pointsTotal += points;
    //     }
    //     
    //     Console.WriteLine(pointsTotal);
    // }

    public class ScratchCard
    {
        public int Copies { get; set; }
        public int Points { get; set; }
    }
    
    public static void Run()
    {
        var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day4.txt";
        var lines = File.ReadLines(fileLocation).ToList();

        var numberRegex = new Regex(@"\d+");
        var parsed = lines.Select(line => numberRegex.Matches(line).Select(match => match.Value)).ToList();
        var scDict = parsed.ToDictionary(line => line.ToList()[0], line => new ScratchCard() { Copies = 1 });

        foreach (var par in parsed)
        {
            var points = 0;
            var numbers = par.ToList();
            var id = numbers.Take(1).ToList()[0];
            var winningNumbers = numbers.Skip(1).Take(10).ToList();
            var otherNumbers = numbers.Skip(11).ToList();

            foreach (var onum in otherNumbers)
            {
                if(winningNumbers.Contains(onum))
                {
                    points += 1;
                }
            }

            scDict[id].Points = points;
            var idInt = int.Parse(id);
            var copies = scDict[id].Copies;
            var toVal = points + idInt > 201 ? points - (points + idInt - 201) : points;
            for (var i = 1; i <= toVal; i++)
            {
                scDict[(idInt + i).ToString()].Copies += copies;
            }
        }

        var totalCards = 0;
        foreach (var pair in scDict)
        {
            totalCards += pair.Value.Copies;
        }
        
        Console.WriteLine(totalCards);
    }
}
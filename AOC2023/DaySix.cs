using System.Text.RegularExpressions;

namespace AOC2023;

public static class DaySix
{
    // public static void Run()
    // {
    //     var races = new List<List<long>>()
    //     {
    //         new() { 45, 295 },
    //         new() { 98, 1734 },
    //         new() { 83, 1278 },
    //         new() { 73, 1210 },
    //     };
    //     
    //     // distance = t * (T-t)
    //     // d = tT - t*2
    //     // t*2 = tT - d
    //     // t^2 - Tt + d = 0
    //     // t = (T +- sqr(T^2 - 4d))/2
    //
    //     double answer = 1;
    //     foreach (var race in races)
    //     {
    //         var rootA = (race[0] + Math.Sqrt(race[0] * race[0] - 4 * race[1])) / 2;
    //         var rootB = (race[0] - Math.Sqrt(race[0] * race[0] - 4 * race[1])) / 2;
    //
    //         var minWin = Math.Ceiling(rootB);
    //         var maxWin = Math.Floor(rootA);
    //
    //         answer *= maxWin + 1 - minWin;
    //     }
    //     Console.WriteLine(answer);
    // }
    
    public static void Run()
    {
        var races = new List<List<long>>()
        {
            new() { 45988373, 295173412781210 }
        };
        
        // distance = t * (T-t)
        // d = tT - t*2
        // t*2 = tT - d
        // t^2 - Tt + d = 0
        // t = (T +- sqr(T^2 - 4d))/2

        double answer = 1;
        foreach (var race in races)
        {
            var rootA = (race[0] + Math.Sqrt(race[0] * race[0] - 4 * race[1])) / 2;
            var rootB = (race[0] - Math.Sqrt(race[0] * race[0] - 4 * race[1])) / 2;

            var minWin = Math.Ceiling(rootB);
            var maxWin = Math.Floor(rootA);

            answer *= maxWin + 1 - minWin;
        }
        Console.WriteLine(answer);
    }
}
namespace AOC2023;

public class Number
{
    public string number { get; set; }
    public int startIndex { get; set; }
}

public static class DayThree
{
    // public static void Run()
    // {
    //     var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day3.txt";
    //     var lines = File.ReadLines(fileLocation).ToList();
    //
    //     var sum = 0;
    //     
    //     for (var lineIndex = 0; lineIndex < lines.Count; lineIndex++)
    //     {
    //         var line = lines[lineIndex];
    //         var numbers = new List<Number>();
    //         
    //         // Fetch numbers in line and their start indices
    //         var n = "";
    //         var startIndex = 0;
    //         for (var index = 0; index < line.Length; index++)
    //         {
    //             var charac = line[index];
    //             if (charac is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9')
    //             {
    //                 if (n.Length == 0)
    //                 {
    //                     startIndex = index;
    //                 }
    //                 n += charac;
    //             }
    //             else
    //             {
    //                 if (n.Length > 0)
    //                 {
    //                     numbers.Add(new Number()
    //                     {
    //                         number = n,
    //                         startIndex = startIndex
    //                     });
    //                     n = "";
    //                 }
    //             }
    //         }
    //         if (n.Length > 0)
    //         {
    //             numbers.Add(new Number()
    //             {
    //                 number = n,
    //                 startIndex = startIndex
    //             });
    //         }
    //         
    //         // for each number check for adjacent symbols
    //         foreach (var num in numbers)
    //         {
    //             Console.WriteLine(num.number);
    //             var boxStartLine = Math.Max(lineIndex - 1, 0);
    //             var boxStopLine = Math.Min(lineIndex + 1, lines.Count - 1);
    //             var boxStartChar = Math.Max(num.startIndex - 1, 0);
    //             var boxStopChar = Math.Min(num.startIndex + num.number.Length, line.Length - 1);
    //             
    //             Console.WriteLine(boxStartLine);
    //             Console.WriteLine(boxStopLine);
    //             Console.WriteLine(boxStartChar);
    //             Console.WriteLine(boxStopChar);
    //
    //             var shouldCount = false;
    //             for (var j = boxStartLine; j <= boxStopLine; j++)
    //             {
    //                 for (var i = boxStartChar; i <= boxStopChar; i++)
    //                 {
    //                     if (lines[j][i] is not ('0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9' or '.'))
    //                     {
    //                         shouldCount = true;
    //                     }
    //                 }
    //             }
    //
    //             if (shouldCount)
    //             {
    //                 Console.WriteLine(num.number);
    //                 sum += int.Parse(num.number);
    //             }
    //         }
    //     }
    //     
    //     Console.WriteLine(sum);
    // }

    public class GearLocation
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public List<int> Numbers { get; set; }
    }
    public static void Run()
    {
        var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day3.txt";
        var lines = File.ReadLines(fileLocation).ToList();
        var GearLocations = new Dictionary<string, List<int>>();
        
        for (var lineIndex = 0; lineIndex < lines.Count; lineIndex++)
        {
            var line = lines[lineIndex];
            var numbers = new List<Number>();
            
            // Fetch numbers in line and their start indices
            var n = "";
            var startIndex = 0;
            for (var index = 0; index < line.Length; index++)
            {
                var charac = line[index];
                if (charac is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9')
                {
                    if (n.Length == 0)
                    {
                        startIndex = index;
                    }
                    n += charac;
                }
                else
                {
                    if (n.Length > 0)
                    {
                        numbers.Add(new Number()
                        {
                            number = n,
                            startIndex = startIndex
                        });
                        n = "";
                    }
                }
            }
            if (n.Length > 0)
            {
                numbers.Add(new Number()
                {
                    number = n,
                    startIndex = startIndex
                });
            }
            
            // for each number check for adjacent gears
            
            foreach (var num in numbers)
            {
                Console.WriteLine(num.number);
                var boxStartLine = Math.Max(lineIndex - 1, 0);
                var boxStopLine = Math.Min(lineIndex + 1, lines.Count - 1);
                var boxStartChar = Math.Max(num.startIndex - 1, 0);
                var boxStopChar = Math.Min(num.startIndex + num.number.Length, line.Length - 1);
                
                Console.WriteLine(boxStartLine);
                Console.WriteLine(boxStopLine);
                Console.WriteLine(boxStartChar);
                Console.WriteLine(boxStopChar);

                var shouldCount = false;
                for (var j = boxStartLine; j <= boxStopLine; j++)
                {
                    for (var i = boxStartChar; i <= boxStopChar; i++)
                    {
                        if (lines[j][i] is '*')
                        {
                            var locationString = $"{j};{i}";
                            if (GearLocations.ContainsKey(locationString))
                            {
                                GearLocations[locationString].Add(int.Parse(num.number));
                            }
                            else
                            {
                                GearLocations.Add(locationString, new List<int>()
                                {
                                    int.Parse(num.number)
                                });
                            }
                        }
                    }
                }
            }
        }

        var sum = 0;
        foreach (var gear in GearLocations)
        {
            if (gear.Value.Count == 2)
            {
                sum += gear.Value[0] * gear.Value[1];
            }
        }
        
        Console.WriteLine(sum);
    }
}

// tried:
// 550934
using System.Numerics;
using System.Xml;

namespace AOC2023;

public static class DayFive
{
    // public static void Run()
    // {
    //     var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day5.txt";
    //     var lines = File.ReadLines(fileLocation).ToList();
    //     var seeds = lines[0];
    //
    //     // Parse sections
    //     var sections = new List<List<string>>();
    //     var section = new List<string>();
    //     foreach (var line in lines.Skip(2))
    //     {
    //         if (line == "")
    //         {
    //             sections.Add(section);
    //             section = new List<string>();
    //         }
    //         else
    //         {
    //             section.Add(line);
    //         }
    //     }
    //     sections.Add(section);
    //     
    //     // 
    //     var values = seeds.Split(" ").Skip(1).Select(number => BigInteger.Parse(number)).ToList();
    //     
    //     foreach (var sect in sections)
    //     {
    //         values = MapValuesToNextValues(values, sect);
    //     }
    //     
    //     Console.WriteLine(values.Min());
    // }
    //
    //
    // private static List<BigInteger> MapValuesToNextValues(List<BigInteger> values, List<string> sect)
    // {
    //     var newValues = new List<BigInteger>();
    //     var usedValues = new List<BigInteger>();
    //     values.Sort();
    //     foreach (var s in sect.Skip(1))
    //     {
    //         var rangeArray = s.Split(" ").Select(number => BigInteger.Parse(number)).ToList();
    //         foreach (var value in values)
    //         {
    //             if (value >= rangeArray[1] && value < rangeArray[1] + rangeArray[2])
    //             {
    //                 newValues.Add(rangeArray[0] + (value - rangeArray[1]));
    //                 usedValues.Add(value);
    //             }
    //         }
    //     }
    //
    //     foreach (var value in values)
    //     {
    //         if (!usedValues.Contains(value))
    //         {
    //             newValues.Add(value);
    //         }
    //     }
    //
    //     return newValues;
    // }

    public class Range
    {
        public BigInteger start;
        public BigInteger end;
    }
    
    public class MappingRange
    {
        public BigInteger start;
        public BigInteger length;
        public BigInteger destinationStart;
    }
    
    public static void Run()
    {
        var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day5.txt";
        var lines = File.ReadLines(fileLocation).ToList();
        var seeds = lines[0];
    
        // Parse sections
        var sections = new List<List<string>>();
        var section = new List<string>();
        foreach (var line in lines.Skip(2))
        {
            if (line == "")
            {
                sections.Add(section);
                section = new List<string>();
            }
            else
            {
                section.Add(line);
            }
        }
        sections.Add(section);
        
        var parsedSeedLine = seeds.Split(" ").Skip(1).Select(number => BigInteger.Parse(number)).ToList();
        var valueRanges = new List<Range>();
        for (int i = 0; i < parsedSeedLine.Count - 1; i += 2)
        {
            valueRanges.Add(new Range(){ start = parsedSeedLine[i], end = parsedSeedLine[i] + parsedSeedLine[i+1] - 1});
        }

        valueRanges = MapValuesToNextValues(valueRanges, sections[0]);
        valueRanges = MapValuesToNextValues(valueRanges, sections[1]);
        valueRanges = MapValuesToNextValues(valueRanges, sections[2]);
        valueRanges = MapValuesToNextValues(valueRanges, sections[3]);
        valueRanges = MapValuesToNextValues(valueRanges, sections[4]);
        valueRanges = MapValuesToNextValues(valueRanges, sections[5]);
        valueRanges = MapValuesToNextValues(valueRanges, sections[6]);


        // foreach (var sect in sections)
        // {
        //     valueRanges = MapValuesToNextValues(valueRanges, sect);
        //     // valueRanges = ConsolidateRanges(valueRanges);
        // }

        foreach (var valueRange in valueRanges)
        {
            Console.WriteLine(valueRange.start);
            Console.WriteLine(valueRange.end);
        }
        Console.WriteLine(valueRanges.Select(range => range.start).ToList().Min());
    }
    
    
    private static List<Range> MapValuesToNextValues(List<Range> valueRanges, List<string> sect)
    {
        Console.WriteLine("MAPPING VALUES");
        // Console.WriteLine(valueRanges.Count);
        var newValues = new List<Range>();
        var mappingRanges = new List<MappingRange>();
        foreach (var s in sect.Skip(1))
        {
            var mapArrayParsed = s.Split(" ").Select(number => BigInteger.Parse(number)).ToList();
            mappingRanges.Add(new MappingRange()
            {
                start = mapArrayParsed[1],
                length = mapArrayParsed[2],
                destinationStart = mapArrayParsed[0]
            });
        }
        mappingRanges.Sort((r1, r2) => r1.start < r2.start ? -1 : 1);
        // foreach (var r in mappingRanges)
        // {
        //     Console.WriteLine(r.start);
        //     Console.WriteLine(r.length);
        //     Console.WriteLine(r.destinationStart);
        // }
    
        foreach (var valueRange in valueRanges)
        {
            var lastPointInRangeMapped = valueRange.start - 1;
            foreach (var mappingRange in mappingRanges)
            {
                if (lastPointInRangeMapped + 1 < mappingRange.start && mappingRange.start <= valueRange.end)
                {
                    newValues.Add(new Range()
                    {
                        start = lastPointInRangeMapped + 1,
                        end = mappingRange.start - 1
                    });
                }
                // valueRange completely contained
                if (mappingRange.start <= valueRange.start && mappingRange.start + mappingRange.length - 1 >= valueRange.end)
                {
                    // map entire range
                    newValues.Add(new Range()
                    {
                        start = mappingRange.destinationStart + (valueRange.start - mappingRange.start),
                        end = mappingRange.destinationStart + (valueRange.end - mappingRange.start)
                    });
                    lastPointInRangeMapped = valueRange.end;
                }
                
                // valueRange overlaps the start
                if (mappingRange.start <= valueRange.start &&
                    mappingRange.start + mappingRange.length - 1 >= valueRange.start &&
                    mappingRange.start + mappingRange.length - 1 < valueRange.end)
                {
                    // map start of range to end of mapping range
                    newValues.Add(new Range()
                    {
                        start = mappingRange.destinationStart + (valueRange.start - mappingRange.start),
                        end = mappingRange.destinationStart + mappingRange.length - 1
                    });
                    lastPointInRangeMapped = mappingRange.start + mappingRange.length - 1;
                }
                
                // valueRange overlaps the end
                if (mappingRange.start > valueRange.start
                    && mappingRange.start <= valueRange.end
                    && mappingRange.start + mappingRange.length - 1 >= valueRange.end)
                {
                    // map start of mapping range to end of range
                    newValues.Add(new Range()
                    {
                        start = mappingRange.destinationStart,
                        end = mappingRange.destinationStart + (valueRange.end - mappingRange.start)
                    });
                    lastPointInRangeMapped = valueRange.end;
                }
                
                // valueRange contains mapping range
                if (mappingRange.start > valueRange.start &&
                    mappingRange.start + mappingRange.length - 1 < valueRange.end)
                {
                    // map mapping range
                    newValues.Add(new Range()
                    {
                        start = mappingRange.destinationStart,
                        end = mappingRange.destinationStart + mappingRange.length - 1
                    });
                    lastPointInRangeMapped = mappingRange.start + mappingRange.length - 1;
                }
            }
    
            if (lastPointInRangeMapped + 1 < valueRange.end)
            {
                newValues.Add(new Range()
                {
                    start = lastPointInRangeMapped + 1,
                    end = valueRange.end
                });
            }
        }
        
        return newValues;
    }
    
    // private static List<Range> ConsolidateRanges(List<Range> ranges)
    // {
    //     ranges.Sort((r1, r2) => r1.start < r2.start ? -1 : 1);
    //     var go = true;
    //     var consolidated = new List<Range>();
    //     consolidated.Add(new Range()
    //     {
    //         start = ranges[0].start,
    //         end = ranges[0].end
    //     });
    //     foreach (var range in ranges.Skip(1))
    //     {
    //         if (consolidated[^1].end > range.start && consolidated[^1].end <= range.end)
    //         {
    //             consolidated[^1].end = range.end;
    //         }
    //         else if (consolidated[^1].end > range.start && consolidated[^1].end > range.end)
    //         {
    //             // do nothing
    //         }
    //         else
    //         {
    //             consolidated.Add(new Range()
    //             {
    //                 start = range.start,
    //                 end = range.end
    //             });
    //         }
    //     }
    //
    //     return consolidated;
    // }
}
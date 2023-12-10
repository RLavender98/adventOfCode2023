namespace AOC2023;

public class Coord
{
    public int Row;
    public int Column;
}

public class Path
{
    public char Direction;
    public Coord Coord;

    public void TakeAStep(List<List<char>> map, Dictionary<char, Dictionary<char, char>> pipes)
    {
        if (Direction == 'N')
        {
            Coord.Row -= 1;
        }
        if (Direction == 'S')
        {
            Coord.Row += 1;
        }
        if (Direction == 'E')
        {
            Coord.Column += 1;
        }
        if (Direction == 'W')
        {
            Coord.Column -= 1;
        }

        var nextPipe = pipes[map[Coord.Row][Coord.Column]];
        Direction = nextPipe[Direction];
    }
}

public static class DayTen
{

    public static Dictionary<char, Dictionary<char, char>> Pipes = new Dictionary<char, Dictionary<char, char>>()
    {
        { 'F', new Dictionary<char, char>() { {'N', 'E'}, {'W', 'S'} } },
        { 'J', new Dictionary<char, char>() { { 'E', 'N' }, {'S', 'W'} } },
        { '7', new Dictionary<char, char>() { { 'N', 'W' }, {'E', 'S'} } },
        { 'L', new Dictionary<char, char>() { { 'S', 'E' }, {'W', 'N'} } },
        { '|', new Dictionary<char, char>() { { 'S', 'S' }, { 'N', 'N'} } },
        { '-', new Dictionary<char, char>() { { 'W', 'W' }, {'E', 'E'} } }
    };

    private static Coord GetStartCoord(List<List<char>> map)
    {
        for (var i = 0; i < map.Count; i++)
        {
            for (var j = 0; j < map[0].Count; j++)
            {
                if (map[i][j] == 'S')
                {
                    return new Coord()
                    {
                        Row = i,
                        Column = j
                    };
                }
            }
        }

        return new Coord();
    }

    // public static void Run()
    // {
    //     var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day10.txt";
    //     var map = File.ReadLines(fileLocation).Select(line => line.ToList()).ToList();
    //
    //     var startCoord = GetStartCoord(map);
    //
    //     var startDirections = Pipes['J'].Values.ToList();
    //     var pathOne = new Path()
    //     {
    //         Direction = startDirections[0],
    //         Coord = new Coord()
    //         {
    //             Row = startCoord.Row,
    //             Column = startCoord.Column
    //         }
    //     };
    //     var pathTwo = new Path()
    //     {
    //         Direction = startDirections[1],
    //         Coord = new Coord()
    //         {
    //             Row = startCoord.Row,
    //             Column = startCoord.Column
    //         }
    //     };
    //
    //     var shouldContinue = true;
    //     var stepCount = 0;
    //     while (shouldContinue)
    //     {
    //         stepCount++;
    //         pathOne.TakeAStep(map, Pipes);
    //         pathTwo.TakeAStep(map, Pipes);
    //         shouldContinue = !(pathOne.Coord.Row == pathTwo.Coord.Row && pathOne.Coord.Column == pathTwo.Coord.Column);
    //     }
    //     Console.WriteLine(stepCount);
    // }
    
    private static int AreaContainedInLine(List<Coord> line, List<List<char>> map)
    {
        var isInside = false;
        var area = 0;
        char? hStart = null;
        for (var i = 0; i < line.Count - 1; i++)
        {
            var iPipe = map[line[i].Row][line[i].Column];

            if (iPipe == '|')
            {
                isInside = !isInside;
            }

            if (iPipe == 'F' || iPipe == 'L')
            {
                hStart = iPipe;
            }

            if (iPipe == '7')
            {
                if (hStart == 'L')
                {
                    isInside = !isInside;
                }
            }
            
            if (iPipe == 'J')
            {
                if (hStart == 'F')
                {
                    isInside = !isInside;
                }
            }

            if (isInside)
            {
                area += line[i + 1].Column - line[i].Column - 1;
            }
        }

        return area;
    }
    
    public static void Run()
    {
        var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day10.txt";
        var map = File.ReadLines(fileLocation).Select(line => line.ToList()).ToList();

        var startCoord = GetStartCoord(map);

        var startDirections = Pipes['J'].Values.ToList();
        map[startCoord.Row][startCoord.Column] = 'J';
        var pathOne = new Path()
        {
            Direction = startDirections[0],
            Coord = new Coord()
            {
                Row = startCoord.Row,
                Column = startCoord.Column
            }
        };
        var pathTwo = new Path()
        {
            Direction = startDirections[1],
            Coord = new Coord()
            {
                Row = startCoord.Row,
                Column = startCoord.Column
            }
        };

        var shouldContinue = true;
        var pipeCoords = new List<Coord>()
        {
            new Coord()
            {
                Row = startCoord.Row,
                Column = startCoord.Column
            }
        };

        while (shouldContinue)
        {
            pathOne.TakeAStep(map, Pipes);
            pathTwo.TakeAStep(map, Pipes);
            shouldContinue = !(pathOne.Coord.Row == pathTwo.Coord.Row && pathOne.Coord.Column == pathTwo.Coord.Column);
            pipeCoords.Add(new Coord()
            {
                Row = pathOne.Coord.Row,
                Column = pathOne.Coord.Column
            });
            if (shouldContinue)
            {
                pipeCoords.Add(new Coord()
                {
                    Row = pathTwo.Coord.Row,
                    Column = pathTwo.Coord.Column
                });
            }
        }

        pipeCoords.Sort((coord, coord1) => coord.Row < coord1.Row ? -1 : (coord.Row > coord1.Row ? 1 : (coord.Column < coord1.Column ? -1 : 1)));
        var groupedCoords = new List<List<Coord>>();
        var tempList = new List<Coord>();
        foreach (var coord in pipeCoords)
        {
            if (tempList.Count == 0)
            {
                tempList.Add(coord);
                continue;
            }

            if (tempList[0].Row == coord.Row)
            {
                tempList.Add(coord);
                continue;
            }
            
            groupedCoords.Add(tempList);
            tempList = new List<Coord>()
            {
                coord
            };
        }
        groupedCoords.Add(tempList);

        var area = 0;
        foreach (var line in groupedCoords.Skip(1).Take(groupedCoords.Count-2))
        {
            area += AreaContainedInLine(line, map);
        }
        
        Console.WriteLine(area);
    }
}
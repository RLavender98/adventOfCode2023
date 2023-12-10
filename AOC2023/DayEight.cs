namespace AOC2023;

public class Node
{
    public string Left;
    public string Right;
}

public static class DayEight
{
    // public static void Run()
    // {
    //     var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day8.txt";
    //     var lines = File.ReadLines(fileLocation).ToList();
    //
    //     var instruc = lines[0].ToList();
    //     var nodes = lines.Skip(2).ToDictionary(line => line.Substring(0,3), line => new Node
    //     {
    //         Left = line.Substring(7,3),
    //         Right = line.Substring(12,3)
    //     });
    //
    //     var currentNodeKey = "AAA";
    //     var instrucIndex = 0;
    //     var pathCount = 0;
    //
    //     while (currentNodeKey != "ZZZ")
    //     {
    //         pathCount++;
    //         if (instruc[instrucIndex] == 'R')
    //         {
    //             currentNodeKey = nodes[currentNodeKey].Right;
    //         }
    //         if (instruc[instrucIndex] == 'L')
    //         {
    //             currentNodeKey = nodes[currentNodeKey].Left;
    //         }
    //
    //         instrucIndex++;
    //         if (instrucIndex >= instruc.Count)
    //         {
    //             instrucIndex = 0;
    //         }
    //     }
    //
    //     Console.WriteLine(pathCount);
    // }


    private static List<string> GetAllStartNodes(Dictionary<string, Node> nodes)
    {
        var startNodes = new List<string>();
        foreach (var keyValuePair in nodes)
        {
            if (keyValuePair.Key.EndsWith("A"))
            {
                startNodes.Add(keyValuePair.Key);
            }
        }

        return startNodes;
    }

    private static bool AllNodesEndInZ(List<string> nodes)
    {
        foreach (var n in nodes)
        {
            if (!n.EndsWith("Z"))
            {
                return false;
            }
        }
        return true;
    }
    
    
    public static void Run()
    {
        var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day8.txt";
        var lines = File.ReadLines(fileLocation).ToList();

        var instruc = lines[0].ToList();
        var nodes = lines.Skip(2).ToDictionary(line => line.Substring(0,3), line => new Node
        {
            Left = line.Substring(7,3),
            Right = line.Substring(12,3)
        });

        var startNodes = GetAllStartNodes(nodes);
        foreach (var no in startNodes)
        {
            var currentNode = no;
            var instrucIndex = 0;
            var pathCount = 0;
            
            while (!currentNode.EndsWith("Z"))
            {
                pathCount++;
                if (instruc[instrucIndex] == 'R')
                {
                    currentNode = nodes[currentNode].Right;
                }
                if (instruc[instrucIndex] == 'L')
                {
                    currentNode = nodes[currentNode].Left;
                }

                instrucIndex++;
                if (instrucIndex >= instruc.Count)
                {
                    instrucIndex = 0;
                }
            }

            Console.WriteLine(pathCount);
        }
    }
}
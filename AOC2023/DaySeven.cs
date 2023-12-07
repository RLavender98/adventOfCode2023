using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace AOC2023;

public static class DaySeven
{
    // private static readonly List<char> Order = new List<char>()
    // {
    //     '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A',
    // };
    //
    // private static List<int> Groups(string hand)
    // {
    //     var distinct = hand.ToList().Distinct();
    //     return distinct.Select(card =>
    //     {
    //         var count = 0;
    //         foreach (var c in hand.ToList())
    //         {
    //             if (c == card)
    //             {
    //                 count++;
    //             }
    //         }
    //
    //         return count;
    //     }).ToList();
    // }
    //
    // private static int Compare(string hand1, string hand2)
    // {
    //     var hand1Groups = Groups(hand1);
    //     var hand2Groups = Groups(hand2);
    //     hand1Groups.Sort();
    //     hand2Groups.Sort();
    //     hand1Groups.Reverse();
    //     hand2Groups.Reverse();
    //
    //     for (var i = 0; i < Math.Min(hand1Groups.Count, hand2Groups.Count); i++)
    //     {
    //         if (hand1Groups[i] < hand2Groups[i])
    //         {
    //             return -1;
    //         }
    //         if (hand1Groups[i] > hand2Groups[i])
    //         {
    //             return 1;
    //         }
    //     }
    //
    //     for (var i = 0; i < 5; i++)
    //     {
    //         if (Order.IndexOf(hand1[i]) < Order.IndexOf(hand2[i]))
    //         {
    //             return -1;
    //         }
    //         if (Order.IndexOf(hand1[i]) > Order.IndexOf(hand2[i]))
    //         {
    //             return 1;
    //         }
    //     }
    //
    //     return 0;
    // }
    //
    // public static void Run()
    // {
    //     var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day7.txt";
    //     var lines = File.ReadLines(fileLocation).ToList();
    //
    //     var handsAndBids = lines.Select(line => line.Split(" ")).ToList();
    //     handsAndBids.Sort((handBid1, handBid2) => Compare(handBid1[0], handBid2[0]));
    //     // foreach (var handAndBid in handsAndBids)
    //     // {
    //     //     Console.WriteLine(handAndBid[1]);
    //     // }
    //
    //     var sum = 0;
    //     for (var i = 0; i < handsAndBids.Count; i++)
    //     {
    //         sum += (i + 1) * int.Parse(handsAndBids[i][1]);
    //     }
    //     Console.WriteLine(sum);
    // }
    
    private static readonly List<char> Order = new List<char>()
    { 
        'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A',
    };

    private static List<int> Groups(string hand)
    {
        var distinct = hand.ToList().Distinct();
        return distinct.Select(card =>
        {
            if (card == 'J')
            {
                return 0;
            }
            var count = 0;
            foreach (var c in hand.ToList())
            {
                if (c == card)
                {
                    count++;
                }
            }

            return count;
        }).ToList();
    }

    private static int CountJokers(string hand)
    {
        var count = 0;
        foreach (var c in hand.ToList())
        {
            if (c == 'J')
            {
                count++;
            }
        }

        return count;
    }

    private static int Compare(string hand1, string hand2)
    {
        var hand1Groups = Groups(hand1);
        var hand2Groups = Groups(hand2);
        hand1Groups.Sort();
        hand2Groups.Sort();
        hand1Groups.Reverse();
        hand2Groups.Reverse();

        var hand1Jokers = CountJokers(hand1);
        var hand2Jokers = CountJokers(hand2);

        hand1Groups[0] += hand1Jokers;
        hand2Groups[0] += hand2Jokers;

        for (var i = 0; i < Math.Min(hand1Groups.Count, hand2Groups.Count); i++)
        {
            if (hand1Groups[i] < hand2Groups[i])
            {
                return -1;
            }
            if (hand1Groups[i] > hand2Groups[i])
            {
                return 1;
            }
        }

        for (var i = 0; i < 5; i++)
        {
            if (Order.IndexOf(hand1[i]) < Order.IndexOf(hand2[i]))
            {
                return -1;
            }
            if (Order.IndexOf(hand1[i]) > Order.IndexOf(hand2[i]))
            {
                return 1;
            }
        }

        return 0;
    }

    public static void Run()
    {
        var fileLocation = "/Users/rublav/Work/AdventOfCode/AOC2023/AOC2023/input_day7.txt";
        var lines = File.ReadLines(fileLocation).ToList();

        var handsAndBids = lines.Select(line => line.Split(" ")).ToList();
        handsAndBids.Sort((handBid1, handBid2) => Compare(handBid1[0], handBid2[0]));
        // foreach (var handAndBid in handsAndBids)
        // {
        //     Console.WriteLine(handAndBid[1]);
        // }

        var sum = 0;
        for (var i = 0; i < handsAndBids.Count; i++)
        {
            sum += (i + 1) * int.Parse(handsAndBids[i][1]);
        }
        Console.WriteLine(sum);
    }
}
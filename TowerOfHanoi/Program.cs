using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var hs = new HanoiSolver(discs: 3);

        hs.start();

        Console.ReadKey();
    }
}

class HanoiSolver
{
    private int TotalDiscs { get; } = 0;
    private Stack<int> FirstPeg { get; } = new Stack<int>();
    private Stack<int> SecondPeg { get; } = new Stack<int>();
    private Stack<int> ThirdPeg { get; } = new Stack<int>();

    public HanoiSolver(int discs = 3)
    {
        TotalDiscs = discs;

        //Create list of items (discs)
        var discList = Enumerable.Range(1, TotalDiscs).Reverse();

        //Add items (discs) to first peg
        foreach (var d in discList)
        {
            FirstPeg.Push(d);
        }
    }

    public void start()
    {
        PrintPegs();

        Move(TotalDiscs, FirstPeg, ThirdPeg, SecondPeg);
    }

    private void Move(
        int discs,
        Stack<int> fromPeg,
        Stack<int> toPeg,
        Stack<int> otherPeg)
    {
        if (discs == 1)
        {
            toPeg.Push(fromPeg.Pop());
            return;
        }

        Move(discs - 1, fromPeg, otherPeg, toPeg);

        toPeg.Push(fromPeg.Pop());

        Move(discs - 1, otherPeg, toPeg, fromPeg);

        Console.WriteLine();
        PrintPegs();
    }

    private void PrintPegs()
    {
        var fp = FirstPeg.Select(x => x).ToList();
        var sp = SecondPeg.Select(x => x).ToList();
        var tp = ThirdPeg.Select(x => x).ToList();

        var result = "";

        for (var i = 0; i < TotalDiscs; i++)
        {
            result += WriteSingleLine(fp, i) + "   ";
            result += WriteSingleLine(sp, i) + "   ";
            result += WriteSingleLine(tp, i) + "   ";
            result += "\n";
        }

        Console.WriteLine(result);
    }

    private static string WriteSingleLine(List<int> sp, int i)
    {
        var result = "";
        if (sp.Count - 1 < i)
        {
            result += "#";
        }
        else
        {
            result += new string('=', sp[i]) + "#" + new string('=', sp[i]);
        }
        return result;
    }

    private string PrintPeg(Stack<int> peg)
    {
        var p = peg.Select(x => x).ToList();

        var result = "";

        foreach (var r in p)
        {
            for (int i = 0; i < r; i++)
            {
                result += "=";
            }
            result += "#";
            for (int i = 0; i < r; i++)
            {
                result += "=";
            }
        }

        return result;
    }
}
using Simulator.Maps;
using Simulator;
using System.Text;

namespace Simulator;

public class LogVisualizer
{
    SimulationHistory Log { get; }
    public LogVisualizer(SimulationHistory log)
    {
        Log = log ?? throw new ArgumentNullException(nameof(log));
    }
    public void Draw(int turnIndex)
    {
        var turnLog = Log.TurnLogs[turnIndex];

        Console.OutputEncoding = Encoding.UTF8;

        Console.Write(Box.TopLeft);
        for (int x = 0; x < Log.SizeX - 1; x++)
        {
            Console.Write($"{Box.Horizontal}{Box.TopMid}");
        }
        Console.WriteLine($"{Box.Horizontal}{Box.TopRight}");
        for (int y = Log.SizeY - 1; y >= 0; y--)
        {
            Console.Write(Box.Vertical);
            for (int x = 0; x < Log.SizeX; x++)
            {
                var position = new Point(x, y);
                if (turnLog.Symbols.ContainsKey(position))
                {
                    var symbol = turnLog.Symbols[position];
                    Console.Write(symbol);
                }
                else
                {
                    Console.Write(" ");
                }
                Console.Write(Box.Vertical);
            }
            Console.WriteLine();
            if (y > 0)
            {
                Console.Write(Box.MidLeft);
                for (int x = 0; x < Log.SizeX - 1; x++)
                {
                    Console.Write($"{Box.Horizontal}{Box.Cross}");
                }
                Console.WriteLine($"{Box.Horizontal}{Box.MidRight}");
            }
        }
        Console.Write(Box.BottomLeft);
        for (int x = 0; x < Log.SizeX - 1; x++)
        {
            Console.Write($"{Box.Horizontal}{Box.BottomMid}");
        }
        Console.WriteLine($"{Box.Horizontal}{Box.BottomRight}");
        Console.WriteLine();
    }
}

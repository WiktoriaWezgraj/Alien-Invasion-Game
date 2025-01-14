using System;
using System.Collections.Generic;
using System.Drawing;
using Simulator;

namespace Simulator.Maps
{
    public class Program
    {
        public static void Main()
        {
            // Tworzenie mapy
            Map map = new BigBounceMap(8, 6);

            // Lista obiektów poruszających się po mapie
            List<IMappable> mappables = new()
            {
                new Player("Elandor"),
                new Ufo("Gorbag"),
                new Aliens { Description = "Rabbits", Size = 2 },
                new FlyingAliens { Description = "Ostriches", Size = 1, CanFly = false },
                new FlyingAliens { Description = "Eagles", Size = 1, CanFly = true }
            };

            // Początkowe pozycje obiektów
            List<Point> positions = new()
            {
                new Point(1, 2),
                new Point(2, 5),
                new Point(3, 1),
                new Point(4, 5),
                new Point(0, 0)
            };

            // Ciąg ruchów
            string moves = "llllluuuuuuuurrrrrrrrrrrrrrrrrrrd";

            // Inicjalizacja symulacji i historii
            Simulation simulation = new(map, mappables, positions, moves);
            SimulationHistory history = new(simulation);
            LogVisualizer logVisualizer = new LogVisualizer(history);
            
            logVisualizer.Draw(5);
            logVisualizer.Draw(10);
            logVisualizer.Draw(15);
            logVisualizer.Draw(20);
            Console.WriteLine("Simulation Finished!");
        }
    }
}


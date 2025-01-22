using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simulator.Maps;
using Simulator;
using Microsoft.AspNetCore.Http;
using System.Numerics;
using System.Net.WebSockets;
namespace SimWeb.Pages
{
    //kazde nacisniecie strzalki jako posta
    //w interfejsie 4 przyciski i klawiature dopinamy do guzikow
    //przerobic klasy po orkach i animalsach, dodac punkty do playera i zeby wyswietlaly się na stronie
    public class PrivacyModel : PageModel
    {
        public string? GreetingMessage { get; private set; }
        public string UserMoves { get; private set; } = "";

        public SimulationHistory GenerateSimulationHistory(string moves)
        {
            if (string.IsNullOrWhiteSpace(moves))
            {
                moves = "dlrludluddlrulrdlurl"; // Ustaw domyślne ruchy
            }

            Map map = new BigBounceMap(8, 6);

            // Lista obiektów poruszających się po mapie
            List<IMappable> creatures = new()
            {
                new Player("Ryland Grace") {Points = 0},
                new Ufo("Strangers"),
                new Aliens {Description = "X Æ A-Xii", Size = 1},
                new Aliens {Description = "Exa Dark Sideræl", Size = 2},
                new Aliens {Description = "Tau Techno Mechanicus", Size = 2},
                new FlyingAliens { Description = "Griffin", Size = 1, FastFlying = true },
                new FlyingAliens { Description = "Vivian", Size = 1, FastFlying = true },
                new FlyingAliens { Description = "Saxon", Size = 1, FastFlying = false },
                new FlyingAliens { Description = "Strider", Size = 1, FastFlying = false },
                new FlyingAliens { Description = "Azure", Size = 1, FastFlying = false }
            };

            // Początkowe pozycje obiektów
            List<Point> positions = new()
            {
                new Point(1, 2),
                new Point(2, 5),
                new Point(2, 1),
                new Point(4, 1),
                new Point(5, 1),
                new Point(1, 3),
                new Point(2, 4),
                new Point(6, 5),
                new Point(7, 1),
                new Point(6, 1)
            };

            // Ciąg ruchów
            //string moves = "dlrludluddlrulrdlurl";

            Simulation sim = new(map, creatures, positions, moves);
            return new SimulationHistory(sim);
        }

        public string GenerateGridHTML(SimulationHistory history, int turn)
        {
            string output = "";
            for (int i = 0; i < history.SizeY; i++)
            {
                for (int j = 0; j < history.SizeX; j++)
                {
                    string symbol = history.TurnLogs[turn].Symbols.ContainsKey(new Point(j, i))
                        ? history.TurnLogs[turn].Symbols[new Point(j, i)].ToString()
                        : "";

                    symbol = symbol switch
                    {
                        "A" => "Al",
                        "F" => "Fast",
                        "" => "Blank",
                        _ => symbol
                    };

                    string image = $"<img src=\"/css/{symbol}.png\">";
                    output += $"<div class=\"grid-item\">{image}</div>";
                }
            }
            return output;
        }

        public string GridHTML { get; private set; } = "";
        public SimulationHistory? SimHistory { get; private set; }
        public int Turn { get; private set; }
        public string Info { get; private set; } = "";


        public void OnGet()
        { 
        
            UserMoves = HttpContext.Session.GetString("Moves") ?? "dlrludluddlrulrdlurl";
            Turn = HttpContext.Session.GetInt32("Turn") ?? 0;

            SimHistory = GenerateSimulationHistory(UserMoves);

            GridHTML = GenerateGridHTML(SimHistory, Turn);
        }

        public void OnPost()
        {

            string? movesInput = Request.Form["userMoves"];
            if (!string.IsNullOrWhiteSpace(movesInput))
            {
                UserMoves = movesInput;
                HttpContext.Session.SetString("Moves", UserMoves);
            }
            else
            {
                UserMoves = HttpContext.Session.GetString("Moves") ?? "dlrludluddlrulrdlurl";
            }

            Turn = HttpContext.Session.GetInt32("Turn") ?? 0;

            string action = Request.Form["turnchange"];
            Turn = action switch
            {
                "prev" => Math.Max(0, Turn - 1),
                "next" => Math.Min(20, Turn + 1),
                _ => Turn
            };

            HttpContext.Session.SetInt32("Turn", Turn);

            SimHistory = GenerateSimulationHistory(UserMoves);
            GridHTML = GenerateGridHTML(SimHistory, Turn);
        }
    }
}
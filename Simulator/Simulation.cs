using Simulator.Maps;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace Simulator;

public class Simulation
{
    private int currentMappableIndex = 0;

    /// <summary>
    /// Simulation's map.
    /// </summary>
    public Map Map { get; }

    /// <summary>
    /// IMappables moving on the map.
    /// </summary>
    public List<IMappable> Mappables { get; }

    /// <summary>
    /// Starting positions of mappables.
    /// </summary>
    public List<Point> Positions { get; }

    /// <summary>
    /// Cyclic list of mappables moves. 
    /// Bad moves are ignored - use DirectionParser.
    /// First move is for first mappable, second for second and so on.
    /// When all mappables make moves, 
    /// next move is again for first mappable and so on.
    /// </summary>
    public string Moves { get; }

    /// <summary>
    /// Has all moves been done?
    /// </summary>
    public bool Finished = false;

    /// <summary>
    /// IMappable which will be moving current turn.
    /// </summary>
    public IMappable CurrentMappable
    {
        get
        {
            return Mappables[currentMappableIndex % Mappables.Count];
        }
    }

    /// <summary>
    /// Lowercase name of direction which will be used in current turn.
    /// </summary>
    public string CurrentMoveName
    {
        get
        {
            if (string.IsNullOrEmpty(Moves)) return string.Empty;

            var direction = DirectionParser.Parse(Moves[currentMappableIndex % Moves.Length].ToString());
            if (direction.Any()) return direction[0].ToString().ToLower();

            return string.Empty;
        }
    }


    private HashSet<char> validMove = new HashSet<char> { 'l', 'r', 'u', 'd' };
    private string ValidateMoves(string moves) => new(moves.Where(c => validMove.Contains(Char.ToLower(c))).ToArray());

    /// <summary>
    /// Simulation constructor.
    /// Throw errors:
    /// if mappables' list is empty,
    /// if number of mappables differs from 
    /// number of starting positions.
    /// </summary>
    public Simulation(Map map, List<IMappable> mappables, List<Point> positions, string moves)
    {
        if (mappables == null || mappables.Count == 0)
            throw new ArgumentException("IMappables list cannot be empty.");
        if (positions == null || positions.Count != mappables.Count)
            throw new ArgumentException("Number of positions must match number of mappables.");

        Map = map ?? throw new ArgumentNullException(nameof(map));
        Mappables = mappables;
        Positions = positions;

        // Zapewnienie, że Moves nie jest puste
        Moves = string.IsNullOrEmpty(moves) ? "lrud" : ValidateMoves(moves);

        for (int i = 0; i < mappables.Count; i++)
        {
            Mappables[i].InitMapAndPosition(Map, Positions[i]);
        }
    }


    /// <summary>
    /// Makes one move of current mappable in current direction.
    /// Throw error if simulation is finished.
    /// </summary>
    public void Turn()
    {
        if (Finished)
        {
            throw new InvalidOperationException("Simulation is already finished.");
        }

        if (string.IsNullOrEmpty(Moves))
        {
            throw new InvalidOperationException("No moves available for simulation.");
        }

        var direction = DirectionParser.Parse(Moves[currentMappableIndex % Moves.Length].ToString())[0];
        CurrentMappable.Go(direction);

        currentMappableIndex++;
        if (currentMappableIndex >= Moves.Length) Finished = true;
    }

}


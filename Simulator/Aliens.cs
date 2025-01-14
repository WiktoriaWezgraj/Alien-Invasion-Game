using Simulator.Maps;
using System;
using System.ComponentModel.DataAnnotations;

namespace Simulator;
public class Aliens : IMappable
{
    public string _description = "Unknown description";
    public required string Description
    {
        get => _description;
        init
        {
            _description = Validator.Shortener(value, 3, 15, '#');

        }
    }

    public Point Position { get; protected set; }
    public virtual char Symbol => 'A';
    public Map? Map { get; private set; }

    public uint Size { get; set; } = 3;

    public virtual string Info => $"{Description} <{Size}>";

    public virtual void Go(Direction direction)
    {
        if (Map == null) throw new InvalidOperationException("Aliens cannot move since it's not on the map!");

        var newPosition = Map.Next(Position, direction);

        Map.Move(this, Position, newPosition);
        Position = newPosition;
    }

    public void InitMapAndPosition(Map map, Point point)
    {
        if (map == null) throw new ArgumentNullException(nameof(map));
        if (Map != null) throw new InvalidOperationException("Alien is already on a map.");
        if (!map.Exist(point)) throw new ArgumentException("Position does not exist");

        Map = map;
        Position = point;
        map.Add(this, point);
    }

    protected virtual Point GetNewPosition(Direction direction)
    {
        return Map.Next(Position, direction);
    }

    public override string ToString() => $"{GetType().Name.ToUpper()}: {Info}";

}


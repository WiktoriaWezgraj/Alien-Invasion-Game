using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator.Maps;

internal static class MapMovement
{
    public static Point WallNext(Map m, Point p, Direction d)
    {
        var moved = p.Next(d);
        if (!m.Exist(moved)) return p;
        return moved;
    }

    public static Point WallNextDiagonal(Map m, Point p, Direction d)
    {
        var moved = p.NextDiagonal(d);
        if (!m.Exist(moved)) return p;
        return moved;
    }

    //bouncey
    public static Point BounceNext(Map m, Point p, Direction d)
    {
        Point position = p.Next(d);

        if (m.Exist(position))
            return position;

        Direction reflectedDirection = ReflectDirection(d);
        position = p.Next(reflectedDirection);

        if (m.Exist(position))
            return position;

        return p;

    }

    public static Point BounceNextDiagonal(Map m, Point p, Direction d)
    {
        Point position = p.NextDiagonal(d);

        if (m.Exist(position))
            return position;

        Direction reflectedDirection = ReflectDirection(d);
        position = p.NextDiagonal(reflectedDirection);

        if (m.Exist(position))
            return position;

        return p;
    }
    private static Direction ReflectDirection(Direction d)
    {
        switch (d)
        {
            case Direction.Left:
                return Direction.Right;

            case Direction.Right:
                return Direction.Left;

            case Direction.Up:
                return Direction.Down;

            case Direction.Down:
                return Direction.Up;
        }
        return d;
    }

    //big torus
    public static Point BigTorusNext(Map m, Point p, Direction d)
    {
        var nextPoint = m.Next(p, d);
        return new Point((nextPoint.X + m.SizeX) % m.SizeX, (nextPoint.Y + m.SizeY) % m.SizeY);
    }

    public static Point BigTorusNextDiagonal(Map m, Point p, Direction d)
    {
        var nextDiagonalPoint = m.NextDiagonal(p, d);
        return new Point((nextDiagonalPoint.X + m.SizeX) % m.SizeX, (nextDiagonalPoint.Y + m.SizeY) % m.SizeY);
    }
}



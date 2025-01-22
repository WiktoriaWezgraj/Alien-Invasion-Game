using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;

namespace Simulator.Maps;

internal static class MapMovement
{
    public static Point WallNext(Map m, Point p, Direction d)
    {
        var moved = p.Next(d);
        if (!m.Exist(moved)) return p;
        return moved;
    }

    public static Point BounceNext(Map m, Point p, Direction d)
    {
        var moved = p.Next(d);
        if (!m.Exist(moved))
        {
            var reversed = p.Next(DirectionParser.ReflectDirection(d));
            if (m.Exist(reversed)) return reversed;
            return p;
        }
        return moved;
    }

    //big torus
    public static Point BigTorusNext(Map m, Point p, Direction d)
    {
        var nextPoint = m.Next(p, d);
        return new Point((nextPoint.X + m.SizeX) % m.SizeX, (nextPoint.Y + m.SizeY) % m.SizeY);
    }
}



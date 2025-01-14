using System;
using System.Collections.Generic;
using System.Drawing;

namespace Simulator.Maps;

public class BigMap : Map
{
    public BigMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        if (sizeX > 1000 || sizeY > 1000)
        {
            throw new ArgumentOutOfRangeException("Map size exceeds allowed dimensions (1000x1000).");
        }
    }
}


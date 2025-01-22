using System;

namespace Simulator;

public class FlyingAliens : Aliens
{
    public override char Symbol => FastFlying ? 'F' : 'f';
    public bool FastFlying { get; set; } = true;

    public override string Info
    {
        get
        {
            string flyStatus = FastFlying ? "fly+" : "fly-";
            return $"{Description} ({flyStatus}) <{Size}>";
        }
    }

    protected override Point GetNewPosition(Direction direction) => FastFlying
    ? Map.Next(Map.Next(Position, direction), direction)
    : Map.Next(Map.Next(Position, direction), direction);


}

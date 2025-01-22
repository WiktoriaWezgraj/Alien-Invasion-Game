using System;
using System.Text.Json.Serialization;

namespace Simulator;

public class Ufo : Creature
{
    public int _rage = 0;
    public int Rage
    {
        get => _rage;
        init => _rage = Validator.Limiter(value, 0, 10);
    }

    public override string Info => $"{Name} [{Level}][{Rage}]";

    public override char Symbol => 'U';

    private int huntCount = 0;

    public Ufo() : base() { }

    public Ufo(string name, int level = 1, int rage = 1)
        : base(name, level)
    {
        Rage = rage;
        huntCount = 0;
    }

    public void Hunt()
    {
        huntCount++;

        if (huntCount % 2 == 0 && _rage < 10)
        {
            _rage++;
            huntCount = 0;
        }
    }
    public override string Greeting() => $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}.";
}

    


using System;
using System.Text.Json.Serialization;

namespace Simulator;

public class Player : Creature
{
    
    public int _points = 0;
    public int Points
    {
        get => _points;
        init => _points = Validator.Limiter(value, 0, 10);
    }

    public override string Info => $"{Name} [{Level}][{Points}]";

    public override char Symbol => 'P';

    private int moveCount = 0;

    public Player() : base() { }

    public Player(string name, int level= 1, int points=0) : base(name, level)
    {
        moveCount = 0;
        Points = points;
    }

    public void AddPoints(int points)
    {
        _points += points;
    }

    //div w cshtml
    //to do modelu strony .cs
    //interfejs w cshtml
    public override string Greeting() => $"Hi, I'm {Name}.";
}

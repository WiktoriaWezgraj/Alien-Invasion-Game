using System;
using System.Text.Json.Serialization;

namespace Simulator;

public class Player : Creature
{
    
    public int _hp = 0;
    public int HP
    {
        get => _hp;
        init => _hp = Validator.Limiter(value, 0, 10);
    }
    [JsonIgnore]
    public override int Power => 8 * Level + 2 * HP;

    [JsonIgnore]
    public override string Info => $"{Name} [{Level}][{HP}]";

    [JsonIgnore]
    public override char Symbol => 'P';

    private int moveCount = 0;

    public Player() : base() { }

    public Player(string name, int level= 1, int hp=0) : base(name, level)
    {
        moveCount = 0;
        HP = hp;
    }

    public void Move()
    {
        moveCount++;

        if (moveCount % 3 == 0 && _hp < 10)
        {
            _hp++;
            moveCount = 0;
        }
    }

    public override string Greeting() => $"Hi, I'm {Name}, my level is {Level}, my HP is {HP}.";

}

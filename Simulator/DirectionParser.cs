namespace Simulator
{
    public static class DirectionParser
    {
        public static List<Direction> Parse(string letters)
        {
            var directions = new List<Direction>();

            foreach (var c in letters.ToUpper())
            {
                switch (c)
                {
                    case 'U':
                        directions.Add(Direction.Up);
                        break;
                    case 'R':
                        directions.Add(Direction.Right);
                        break;
                    case 'D':
                        directions.Add(Direction.Down);
                        break;
                    case 'L':
                        directions.Add(Direction.Left);
                        break;
                }
            }

            return directions;
        }
        public static Direction ReflectDirection(Direction d)
        {
            return d switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                _ => throw new ArgumentException("Invalid direction")
            };
        }
    }
}


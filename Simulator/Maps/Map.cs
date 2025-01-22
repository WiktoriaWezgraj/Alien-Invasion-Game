using System.Drawing;

namespace Simulator.Maps;

public abstract class Map
{
    private readonly Dictionary<Point, List<IMappable>> _fields;
    private readonly Rectangle _map;

    protected Func<Map, Point, Direction, Point>? FNext; //z zewnatrz nam nie popsują

    public int SizeX { get; }
    public int SizeY { get; }

    protected Map(int sizeX, int sizeY)
    {
        if (sizeX < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeX), "Too narrow");
        }

        if (sizeY < 5)
        {
            throw new ArgumentOutOfRangeException(nameof(sizeY), "Too short");
        }
        SizeX = sizeX;
        SizeY = sizeY;
        _map = new Rectangle(0, 0, SizeX - 1, SizeY - 1);
        _fields = new Dictionary<Point, List<IMappable>>();
    }

    public virtual bool Exist(Point p) => _map.Contains(p);

    public virtual void Add(IMappable mappable, Point position)
    {
        if (!Exist(position))
            throw new ArgumentException("Position is outside the map boundaries.");
        if (!_fields.ContainsKey(position))
            _fields[position] = new List<IMappable>();
        _fields[position].Add(mappable);
    }

    public virtual void Remove(IMappable mappable, Point position)
    {
        if (_fields.ContainsKey(position))
        {
            _fields[position].Remove(mappable);
            if (_fields[position].Count == 0)
                _fields.Remove(position);
        }
    }

    public virtual List<IMappable>? At(Point point)
        => _fields.TryGetValue(point, out var mappables) ? mappables : null;

    public virtual List<IMappable>? At(int x, int y) => At(new Point(x, y));

    public Point Next(Point p, Direction d) => FNext?.Invoke(this, p, d) ?? p;

    public virtual void Move(IMappable mappable, Point from, Point to)
    {
        if (!Exist(from) || !Exist(to))
            throw new ArgumentException("One or both positions are outside the map boundaries.");

        Remove(mappable, from);
        Add(mappable, to);
    }
}


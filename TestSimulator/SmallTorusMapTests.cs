using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SmallTorusMapTests
{
    [Fact]
    public void Constructor_ValidSize_ShouldSetSize()
    {
        int sizeX = 10, sizeY = 15;
        var map = new SmallTorusMap(sizeX, sizeY);
        Assert.Equal(sizeX, map.SizeX);
        Assert.Equal(sizeY, map.SizeY);
    }

    [Theory]
    [InlineData(-1, -1, 20, 19, 19)]
    [InlineData(25, 25, 20, 5, 5)]
    public void Exist_ShouldHandleModuloCorrectly(int x, int y, int size, int expectedX, int expectedY)
    {
        var map = new SmallTorusMap(size, size);
        var point = new Point(x, y);
        var wrappedPoint = new Point((point.X + size) % size, (point.Y + size) % size);
        Assert.Equal(new Point(expectedX, expectedY), wrappedPoint);
    }

    [Theory]
    [InlineData(5, 10, Direction.Up, 5, 11)]
    [InlineData(0, 0, Direction.Down, 0, 19)]
    [InlineData(0, 8, Direction.Left, 19, 8)]
    [InlineData(19, 10, Direction.Right, 0, 10)]
    public void Next_ShouldReturnCorrectNextPoint(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        var map = new SmallTorusMap(20, 20);
        var point = new Point(x, y);
        var nextPoint = map.Next(point, direction);
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }

    [Theory]
    [InlineData(5, 10, Direction.Up, 6, 11)]
    [InlineData(0, 0, Direction.Down, 19, 19)]
    [InlineData(0, 8, Direction.Left, 19, 9)]
    [InlineData(19, 10, Direction.Right, 0, 9)]
    public void NextDiagonal_ShouldReturnCorrectNextPoint(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        var map = new SmallTorusMap(20, 20);
        var point = new Point(x, y);
        var nextPoint = map.NextDiagonal(point, direction);
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }
}

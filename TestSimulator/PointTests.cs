using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;

namespace TestSimulator;

public class PointTests
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(100, -20)]
    [InlineData(-1, -1)]
    [InlineData(50, 50)]
    public void Constructor_ShouldSetCorrectData(int x, int y)
    {
        var point = new Point(x, y);
        Assert.Equal(x, point.X);
        Assert.Equal(y, point.Y);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectData()
    {
        int x = 7;
        int y = 8;

        var point = new Point(x, y);
        var result = point.ToString();
        Assert.Equal("(7, 8)", result);

    }

    [Theory]
    [InlineData(0, 0, Direction.Up, 0, 1)]
    [InlineData(0, 0, Direction.Down, 0, -1)]
    [InlineData(0, 0, Direction.Left, -1, 0)]
    [InlineData(0, 0, Direction.Right, 1, 0)]
    public void Next_ShouldReturnCorrectPoint(int x, int y, Direction direction, int x2, int y2)
    {
        var point = new Point(x, y);
        var result = point.Next(direction);

        var point2 = new Point(x2, y2);
        Assert.Equal(point2, result);
    }

    [Theory]
    [InlineData(0, 0, Direction.Up, 1, 1)]
    [InlineData(-1, -1, Direction.Down, -2, -2)]
    [InlineData(0, 0, Direction.Down, -1, -1)]
    [InlineData(0, 0, Direction.Left, -1, 1)]
    [InlineData(0, 0, Direction.Right, 1, -1)]

    public void NextDiagonal_ShouldReturnCorrectPoint(int x, int y, Direction direction, int x2, int y2)
    {
        var point = new Point(x, y);
        var nextPoint = point.NextDiagonal(direction);

        Assert.Equal(x2, nextPoint.X);
        Assert.Equal(y2, nextPoint.Y);
    }

    [Fact]
    public void Next_ShouldHandleUnknownDirection()
    {
        var point = new Point(5, 5);
        var nextPoint = point.Next((Direction)100); 

        Assert.Equal(point.X, nextPoint.X);
        Assert.Equal(point.Y, nextPoint.Y);
    }

    [Fact]
    public void NextDiagonal_ShouldHandleUnknownDirection()
    {
        var point = new Point(5, 5);
        var nextPoint = point.NextDiagonal((Direction)100); 
        Assert.Equal(point.X, nextPoint.X);
        Assert.Equal(point.Y, nextPoint.Y);
    }
}

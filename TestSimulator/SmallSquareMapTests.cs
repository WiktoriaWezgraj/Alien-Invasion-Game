using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simulator;
using Simulator.Maps;

namespace TestSimulator;

public class SmallSquareMapTests
{
    [Fact]
    public void Constructor_ValidSize_ShouldInitializeCorrectly()
    {
        int size = 10;
        var map = new SmallSquareMap(size);

        Assert.Equal(size, map.SizeX);
        Assert.Equal(size, map.SizeY);
    } 

    [Theory]
    [InlineData(4)]   
    [InlineData(21)]  
    [InlineData(-1)]  
    public void Constructor_ShouldThrowExceptionForInvalidSize(int size)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(size));
    }

    [Theory]
    [InlineData(-1, 5, false)]  
    [InlineData(5, -1, false)]  
    [InlineData(10, 5, false)]  
    [InlineData(5, 10, false)]
    [InlineData(10, 10, false)]
    [InlineData(5, 5, true)]    
    [InlineData(0, 0, true)]    
    [InlineData(9, 9, true)]    

    public void Exist_ShouldReturnExpectedResult(int x, int y, bool expected)
    {
        var map = new SmallSquareMap(10);
        var point = new Point(x, y);

        Assert.Equal(expected, map.Exist(point));
    }

    [Theory]
    // inside
    [InlineData(5, 5, Direction.Up, 5, 6)]
    [InlineData(5, 5, Direction.Down, 5, 4)]
    [InlineData(5, 5, Direction.Left, 4, 5)]
    [InlineData(5, 5, Direction.Right, 6, 5)]

    // boundary
    [InlineData(0, 0, Direction.Left, 0, 0)]
    [InlineData(0, 0, Direction.Down, 0, 0)]
    [InlineData(9, 9, Direction.Right, 9, 9)]
    [InlineData(9, 9, Direction.Up, 9, 9)]
    public void Next_ShouldReturnExpectedPoint(int startX, int startY, Direction direction, int expectedX, int expectedY)
    {
        var map = new SmallSquareMap(10);
        var start = new Point(startX, startY);

        var result = map.Next(start, direction);

        Assert.Equal(new Point(expectedX, expectedY), result);
    }

    [Theory]
    // inside
    [InlineData(5, 5, Direction.Up, 6, 6)]
    [InlineData(5, 5, Direction.Down, 4, 4)]
    [InlineData(5, 5, Direction.Left, 4, 6)]
    [InlineData(5, 5, Direction.Right, 6, 4)]

    // boundary- should not move outside the boundaries
    [InlineData(0, 0, Direction.Left, 0, 0)]
    [InlineData(0, 0, Direction.Down, 0, 0)]
    [InlineData(9, 9, Direction.Right, 9, 9)]
    [InlineData(9, 9, Direction.Up, 9, 9)]
    public void NextDiagonal_ShouldReturnExpectedPoint(int startX, int startY, Direction direction, int expectedX, int expectedY)
    {
        var map = new SmallSquareMap(10);
        var start = new Point(startX, startY);

        var result = map.NextDiagonal(start, direction);

        Assert.Equal(new Point(expectedX, expectedY), result);
    }

}

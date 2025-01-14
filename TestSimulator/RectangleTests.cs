using Simulator;
namespace TestSimulator;

public class RectangleTests
{
    [Fact]
    public void Constructor_ValidCoordinates_ShouldInitializeCorrectly()
    {
        int x1 = 1, y1 = 2, x2 = 5, y2 = 6;
        var rectangle = new Rectangle(x1, y1, x2, y2);

        Assert.Equal(1, rectangle.X1);
        Assert.Equal(2, rectangle.Y1);
        Assert.Equal(5, rectangle.X2);
        Assert.Equal(6, rectangle.Y2);
    }

    [Theory]
    [InlineData(6, 7, 2, 3, 2, 3, 6, 7)]
    [InlineData(3, 4, 1, 2, 1, 2, 3, 4)]
    [InlineData(1, 2, 3, 4, 1, 2, 3, 4)]
    public void Constructor_SwappedCoordinates_ShouldNormalizeCorrectly(
        int x1, int y1, int x2, int y2, int expectedX1, int expectedY1, int expectedX2, int expectedY2)
    {
        var rectangle = new Rectangle(x1, y1, x2, y2);

        Assert.Equal(expectedX1, rectangle.X1);
        Assert.Equal(expectedY1, rectangle.Y1);
        Assert.Equal(expectedX2, rectangle.X2);
        Assert.Equal(expectedY2, rectangle.Y2);
    }

    [Theory]
    [InlineData(0, 0, 0, 2)] 
    [InlineData(0, 2, 0, 2)] 
    public void Constructor_InvalidCoordinates_ShouldThrowArgumentException(int x1, int y1, int x2, int y2)
    { 
       Assert.Throws<ArgumentException>(() => new Rectangle(x1, y1, x2, y2));
 
    }

    [Theory]
    [InlineData(0, 0, true)]  
    [InlineData(1, 3, true)] 
    [InlineData(3, 3, true)]  
    [InlineData(6, 6, false)] 
    [InlineData(-1, 6, false)] 
    public void Contains_ShouldReturnCorrectResult(int x1, int y1, bool expected)
    {

        var rectangle = new Rectangle(0, 0, 5, 5);
        var point = new Point(x1, y1);
        var result = rectangle.Contains(point);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectValue()
    {
        string result = "(1, 1):(2, 2)";
        Rectangle r = new(1, 1, 2, 2);

        Assert.Equal(result, r.ToString());
    }

    [Fact]
    public void Constructor_ReverseCoordinates_ShouldSetCorrectCoordinates()
    {

        var rectangle = new Rectangle(3, 4, 1, 2);
        var result = rectangle.ToString();

        Assert.Equal("(1, 2):(3, 4)", result);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Simulator;

namespace TestSimulator;

public class ValidatorTests
{
    [Theory]
    [InlineData(15, 10, 20, 15)]
    [InlineData(25, 10, 20, 20)]
    [InlineData(5, 10, 20, 10)]
    [InlineData(10, 10, 10, 10)]
    [InlineData(-10, -20, -5, -10)]
    [InlineData(-30, -20, -5, -20)]
    [InlineData(0, 5, 15, 5)]
    public void Limiter_ShouldReturnCorrectValue(int value, int min, int max, int expected)
    {
        int result = Validator.Limiter(value, min, max);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("a    b", 3, 5, '#', "A##")]
    [InlineData("TestName", 3, 10, '#', "TestName")]       
    [InlineData("my example", 5, 7, '#', "My exam")]           
    [InlineData("  space test ", 4, 8, '*', "Space te")]   
    [InlineData("Short", 10, 15, '#', "Short#####")]       
    [InlineData("lowercase", 5, 15, 'x', "Lowercase")]     
    [InlineData("    ", 3, 6, '*', "***")]                 
    [InlineData("", 4, 8, 'x', "Xxxx")]                    
    [InlineData("single", 1, 1, '-', "S")]
    [InlineData(null, 2, 5, '#', "##")]                    
    [InlineData("   ", 4, 8, '*', "****")]                 
    [InlineData("X", 2, 2, '-', "X-")]                     
    [InlineData("Trim Test   ", 5, 10, '_', "Trim Test")]      
    [InlineData("String", 10, 10, '!', "String!!!!")]
    [InlineData("Whitespace", 15, 20, ' ', "Whitespace     ")]
    public void Shortener_ShouldReturnCorrectValue(string value, int min, int max, char placeholder, string expected)
    {
        string result = Validator.Shortener(value, min, max, placeholder);

        Assert.Equal(expected, result);
    }
}
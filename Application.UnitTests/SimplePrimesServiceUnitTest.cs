using Xunit;

namespace Application.UnitTests;

public class SimplePrimesServiceUnitTest
{

    private readonly SimplePrimesCalculator _systemUnderTest = new();
    
    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(23)]
    [InlineData(11587)]
    public void IsPrime_ShouldReturnTrue_WhenNumberIsPrime(int number)
    {
        // Arrange  + Act
        var result = _systemUnderTest.IsPrime(number);
        // Assert
        Assert.True(result);
        
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(6)]
    [InlineData(24)]
    [InlineData(11588)]
    [InlineData(11589)]
    public void IsPrime_ShouldReturnFalse_WhenNumberIsPrime(int number)
    {
        // Arrange  + Act
        var result = _systemUnderTest.IsPrime(number);
        // Assert
        Assert.False(result);
        
    }
}


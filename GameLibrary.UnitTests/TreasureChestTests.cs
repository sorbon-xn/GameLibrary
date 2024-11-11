using System;

namespace GameLibrary.UnitTests;

public class TreasureChestTests
{
    // Antipattern: A test should have no logic
    [Theory]
    [InlineData(true, true)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    [InlineData(false, false)]
    public void CanOpen_WhenCalled_ReturnsExpectedOutcome2(bool isLocked, bool hasKey)
    {
        // Arrange
        var sut = new TreasureChest(isLocked);

        // Act
        var result = sut.CanOpen(hasKey);
        
        // Assert
        if ((isLocked && hasKey) || !isLocked)
        {
            Assert.True(result);
        }
        else
        {
            Assert.False(result);
        }
    }
}

using System;
using FluentAssertions;

namespace GameLibrary.UnitTests;

public class QuestRewardCalculatorTests
{
    [Theory]
    [InlineData(1, 1.0, 100)]
    [InlineData(2, 1.0, 200)]
    [InlineData(3, 2.0, 600)]
    public void CalculateQuestReward_GivenDifficultyAndMultiplier_ReturnsExpectedReward(
        int difficulty,
        double multiplier,
        int expectedReward
    )
    {
        // Arrange
        var sut = new QuestRewardCalculator(multiplier);

        // Act
        var reward = sut.CalculateQuestReward(difficulty);

        // Assert
        reward.Should().Be(expectedReward);
    }
}

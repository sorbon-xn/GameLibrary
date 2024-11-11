using FluentAssertions;
using NSubstitute;

namespace GameLibrary.UnitTests;

public class GameMatchFactoryTests
{
    [Fact]
    public void CreateMatch_WhenOnWeekend_CreatesMatch()
    {
        // Arrange
        var now = new DateTimeOffset(2024, 11, 10, 0, 0, 0, TimeSpan.Zero); // A Sunday
        var timeProviderStub = Substitute.For<TimeProvider>();
        timeProviderStub.GetUtcNow().Returns(now);

        var sut = new GameMatchFactory(timeProviderStub);

        // Act
        GameMatch match = sut.CreateMatch("player1");

        // Assert
        match.Should().NotBeNull();
    }

    [Fact]
    public void CreateMatch_WhenOnWeekday_Throws()
    {
        // Arrange
        var now = new DateTimeOffset(2024, 11, 11, 0, 0, 0, TimeSpan.Zero); // A Monday
        var timeProviderStub = Substitute.For<TimeProvider>();
        timeProviderStub.GetUtcNow().Returns(now);

        var sut = new GameMatchFactory(timeProviderStub);

        // Act
        Action act = () => sut.CreateMatch("player1");

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}


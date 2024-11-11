using FluentAssertions;

namespace GameLibrary.UnitTests;

public class PlayerTests
{
    readonly Player sut;
    public PlayerTests()
    {
        sut = new("Alice", 1, DateTime.Now);
    }

    [Fact]
    public void IncreaseLevel_WhenCalled_HasExpectedLevel()
    {
        // Arrange
        // Player sut = new("Alice", 1, DateTime.Now);

        // Act
        sut.IncreaseLevel();

        // Assert
        sut.Level.Should().Be(2);
        sut.Level.Should().BeGreaterThan(1);
        sut.Level.Should().BeGreaterThanOrEqualTo(2);
        sut.Level.Should().BePositive();
        sut.Level.Should().NotBe(1);
        sut.Level.Should().BeInRange(2, 100);
    }

    [Fact]
    public void Greet_ValidGreeting_ReturnsGreetingWithName()
    {
        // Arrange
        // var sut = new Player("Alice", 1, DateTime.Now);

        // Act
        var result = sut.Greet("Hello");

        // Assert
        result.Should().Be("Hello, Alice!");
        result.Should().Contain("Alice");
        result.Should().EndWith("Alice!");
        result.Should().NotBeNullOrWhiteSpace();
    }    

    [Fact]
    public void Constructor_OnNewInstance_SetsJoinDate ()
    {
        // Arrange
        var currentDate = DateTime.Now;

        // Act
        var sut2 = new Player("Alice", 1, currentDate);

        // Assert
        sut2.JoinDate.Should().Be(currentDate);
        sut2.JoinDate.Should().BeCloseTo(currentDate, TimeSpan.FromMilliseconds(500));
        sut2.JoinDate.Should().BeWithin(TimeSpan.FromMilliseconds(500)).Before(currentDate);
    }    

    [Fact]
    public void AddItemToInventory_WithValidItem_AddsTheItem()
    {
        // Arrange
        // var sut = new Player("Alice", 1, DateTime.Now);
        var item = new InventoryItem(101, "Sword", "A sharp blade.");        

        // Act
        sut.AddItemToInventory(item);

        // Assert
        sut.InventoryItems.Should().HaveCount(1);
        sut.InventoryItems.Should().NotBeEmpty();
        sut.InventoryItems.Should().Contain(item);
        sut.InventoryItems.Should().ContainSingle(
            item => item.Id == 101 && item.Name == "Sword");
    }  

    [Fact]
    public void Greet_NullOrEmptyGreeting_ThrowsArgumentException()
    {
        // Arrange
        // var sut = new Player("Alice", 1, DateTime.Now);

        // Act
        Action act = () => sut.Greet("");

        // Assert
        act.Should().Throw<ArgumentException>();
    }   

    [Fact]
    public void IncreaseLevel_WhenCalled_RaisesLevelUpEvent()
    {
        // Arrange
        // var sut = new Player("Alice", 1, DateTime.Now);
        using var monitoredSut = sut.Monitor();
        
        // Act
        sut.IncreaseLevel();

        // Assert
        monitoredSut.Should().Raise(nameof(sut.LevelUp));
    }

    // Antipattern: Do not have multiple AAA sections
    [Fact]
    public void GrantExperienceAndIncreaseLevel_WhenCalled_IncreaseExperienceAndLevel()
    {
        // Arrange
        // var sut = new Player("Alice", 1, DateTime.Now);
        var initialExperiencePoints = sut.ExperiencePoints;

        // Act
        sut.GrantExperiencePoints(8, 10);

        // Assert
        sut.ExperiencePoints.Should().BeGreaterThan(initialExperiencePoints);

        // Act
        sut.IncreaseLevel();

        // Assert
        sut.Level.Should().Be(2);
    }

    // Antipattern: A test should have no logic
    [Fact]
    public void IncreaseLevel_RandomOfTimes_LevelIncreasesCorrectly()
    {
        // Arrang
        // var sut = new Player("Alice",1, new DateTime(2020, 1, 1));
        int randomIncreases = Random.Shared.Next(1, 100);

        // Act
        for (int i = 0; i < randomIncreases; i++ )
        {
            sut.IncreaseLevel();
        }

        // Assert
        sut.Level.Should().Be(1+randomIncreases);
    }
}


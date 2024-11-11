using System;
using FluentAssertions;
using NSubstitute;

namespace GameLibrary.UnitTests;

public class QuestMasterTests
{

    [Fact]
    public void AssignQuest_ValidInput_CreatesQuestAndNotifiesPlayerWithCorrectMessage()
    {
        // Arrange
        var questGenerator = new QuestGenerator();
        var notificationServiceMock = Substitute.For<INotificationService>();

        var sut  = new QuestMaster(questGenerator,notificationServiceMock);
        var PlayerContact = new PlayerContact("Alice", "alice@mail.com");

        // Act
        var quest = sut.AssignQuest(PlayerContact, 5);
    
        // Assert
        quest.Should().NotBeNull();
        quest.PlayerName.Should().Be("Alice");
        quest.Reward.Should().BePositive();
        notificationServiceMock.Received()
                        .NotifyPlayer(PlayerContact, Arg.Any<string>());
    }
}

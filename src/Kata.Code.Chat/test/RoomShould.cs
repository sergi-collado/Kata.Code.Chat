using System;
using System.Collections;
using Xunit;
using FluentAssertions;
using NSubstitute;

namespace Kata.Code.Chat.UnitTests
{
    public class RoomShould
    {
        [Fact]
        public void ShowWelcomeMessage()
        {
            var utc = Substitute.For<IUtc>();
            var room = new Room(utc);
            const int countExpected = 1;
            var messageExpected = new Message(new DateTime(1900, 1, 1), "System", "Welcome to chat!");

            room.Messages.Should().HaveCount(countExpected).And.ContainSingle(m => m == messageExpected);
        }

        [Fact]
        public void AddNewMessage()
        {
            var utc = Substitute.For<IUtc>();
            utc.Now().Returns(new DateTime(2019, 11, 5, 18, 0, 0));
            Room room = new Room(utc);

            string newMessageContent = "Any new message";
            string newMessageUser = "Any new user";
            var expectedMessages = new[]
            {
                new Message(new DateTime(1900, 1, 1), "System", "Welcome to chat!"),
                new Message(utc.Now(), newMessageUser, newMessageContent)
            };

            room.AddMessage(newMessageUser, newMessageContent);

            room.Messages.Should()
                    .HaveCount(expectedMessages.Length)
                    .And.BeEquivalentTo(expectedMessages);
        }
    }
}

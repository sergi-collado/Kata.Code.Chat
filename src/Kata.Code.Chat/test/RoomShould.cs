using System;
using System.Collections;
using Xunit;
using FluentAssertions;

namespace Kata.Code.Chat.UnitTests
{
    public class RoomShould
    {
        [Fact]
        public void ShowWelcomeMessage()
        {
            var room = new Room();
            const int countExpected = 1;
            var messageExpected = new Message(new DateTime(1900, 1, 1), "System", "Welcome to chat!");

            room.Messages.Should().HaveCount(countExpected).And.ContainSingle(m => m == messageExpected);
        }
    }
}

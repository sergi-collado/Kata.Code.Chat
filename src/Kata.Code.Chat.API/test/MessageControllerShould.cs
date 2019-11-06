using System;
using System.Collections;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using Kata.Code.Chat.API.Controllers;
using Kata.Code.Chat.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Kata.Code.Chat.API.UnitTests
{
    public class MessageControllerShould
    {
        [Theory, AutoData]
        public async void ReceiveMessage(DateTime dte, string user, string content)
        {
            //Arrange
            var room = Substitute.For<IRoom>();
            var log = Substitute.For<ILogger<MessageController>>();
            room.Messages.Returns(new List<Message> {new Message(dte, user, content)});
            var expectedMessage = new V1.Message(){ MessageDateTime = dte, User = user, Content = content};
            var controller = new MessageController(log, room);

            //Act
            var result = await controller.Get();

            //Assert
            var typedResult = (IEnumerable<V1.Message>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(1).And.ContainSingle(m => m == expectedMessage);
        }
    }
}

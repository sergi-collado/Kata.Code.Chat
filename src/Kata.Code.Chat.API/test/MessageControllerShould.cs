using System;
using System.Collections;
using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using Kata.Code.Chat.API.Controllers;
using Kata.Code.Chat.API.Models;
using Kata.Code.Chat.API.Services;
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
            var repository = Substitute.For<IMessageRepository>();
            var controller = new MessageController(log, room, repository);

            //Act
            var result = await controller.Get();

            //Assert
            var typedResult = (IEnumerable<V1.Message>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(1).And.ContainSingle(m => m == expectedMessage);
        }

        [Theory, AutoData]
        public async void ReturnAllMessages(DateTime dte, string user, string content)
        {
            //Arrange
            var log = Substitute.For<ILogger<MessageController>>();
            var messages = new List<Message>(){ new Message(dte, user, content)};
            var repository = Substitute.For<IMessageRepository>();
            repository.GetMessages().Returns(messages);
            var expectedMessages = new List<V1.Message>()
            {
                new Message(new DateTime(1900, 1, 1), "System", "Welcome to chat!").ToDto()
            };
            expectedMessages.AddRange(messages.ConvertAll(m => m.ToDto()));
            var room = new Room(new utc());
            var controller = new MessageController(log, room, repository);

            //Act
            var result = await controller.Get();

            //Assert
            var typedResult = (IEnumerable<V1.Message>)Assert.IsType<OkObjectResult>(result).Value;
            typedResult.Should().HaveCount(2).And.BeEquivalentTo(expectedMessages);
        }

        [Theory, AutoData]
        public async void RegisterNewMessage(string user, string content)
        {
            //Arrange
            var log = Substitute.For<ILogger<MessageController>>();
            var repository = Substitute.For<IMessageRepository>();
            var utc = Substitute.For<IUtc>();
            utc.Now().Returns(new DateTime(2019, 11, 29, 10, 0, 0));
            var room = new Room(utc);
            var controller = new MessageController(log, room, repository);
            var message = new V1.Message() { MessageDateTime = utc.Now(), User = user, Content = content };
            //Act
            await controller.Post(message);

            //Assert
            await repository.Received().SaveMessage(Arg.Is<Message>(m => m == message.ToEntity()));
        }
    }
}

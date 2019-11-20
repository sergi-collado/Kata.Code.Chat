using System;
using System.Collections.Generic;

namespace Kata.Code.Chat
{
    public class Room : IRoom
    {
        private readonly IUtc utc;
        public List<Message> Messages { get; }

        public Room(IUtc utc)
        {
            this.utc = utc;
            Messages = new List<Message>
            {
                new Message(new DateTime(1900, 1, 1), "System", "Welcome to chat!")
            };
        }

        public void AddMessage(string user, string content)
        {
            Messages.Add(new Message(utc.Now(), user, content));
        }

        public void AddMessages(List<Message> messages)
        {
            Messages.AddRange(messages);
        }
    }
}
using System;
using System.Collections.Generic;

namespace Kata.Code.Chat
{
    public class Room : IRoom
    {
        public List<Message> Messages { get; }

        public Room()
        {
            Messages = new List<Message>
            {
                new Message(new DateTime(1900, 1, 1), "System", "Welcome to chat!")
            };
        }
    }
}
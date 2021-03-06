﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kata.Code.Chat.API.Models
{
    public static class Extensions
    {
        public static V1.Message ToDto(this Message message)
        {
            return new V1.Message()
            {
                MessageDateTime = message.dateTime,
                User = message.user,
                Content = message.message
            };
        }

        public static Message ToEntity(this V1.Message message)
        {
            return new Message(message.MessageDateTime, message.User, message.Content);
        }
    }
}

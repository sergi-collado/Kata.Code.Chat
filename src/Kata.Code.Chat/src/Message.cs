using System;
using System.Collections.Generic;

namespace Kata.Code.Chat
{
    public class Message : IEquatable<Message>
    {
        public readonly DateTime dateTime;
        public readonly string user;
        public readonly string message;

        public Message(DateTime dateTime, string user, string message)
        {
            this.dateTime = dateTime;
            this.user = user;
            this.message = message;
        }

        public bool Equals(Message other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return dateTime.Equals(other.dateTime) && string.Equals(user, other.user) && string.Equals(message, other.message);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Message) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = dateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ (user != null ? user.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (message != null ? message.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Message left, Message right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Message left, Message right)
        {
            return !Equals(left, right);
        }
    }
}
using System;

namespace Kata.Code.Chat.API.Models
{
    public static partial class V1
    {
        public class Message : IEquatable<Message>
        {
            public DateTime MessageDateTime { get; set; }
            public string User { get; set; }
            public string Content { get; set; }

            public bool Equals(Message other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return MessageDateTime.Equals(other.MessageDateTime) && string.Equals(User, other.User) && string.Equals(Content, other.Content);
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
                    int hashCode = MessageDateTime.GetHashCode();
                    hashCode = (hashCode * 397) ^ (User != null ? User.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (Content != null ? Content.GetHashCode() : 0);
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
}
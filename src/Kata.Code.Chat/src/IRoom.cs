using System.Collections.Generic;

namespace Kata.Code.Chat
{
    public interface IRoom
    {
        List<Message> Messages { get; }
    }
}
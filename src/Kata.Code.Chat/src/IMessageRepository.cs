using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kata.Code.Chat
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessages();
    }
}
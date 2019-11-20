using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kata.Code.Chat.DataAccess.Repositories
{
    class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext chatContext;

        public MessageRepository(ChatDbContext chatContext)
        {
            this.chatContext = chatContext;
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            return await chatContext.Messages.ToListAsync();
        }
    }
}

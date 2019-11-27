using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Kata.Code.Chat.DataAccess.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext chatContext;
        private readonly DatabaseSettings databaseSettings;

        public MessageRepository(ChatDbContext chatContext, DatabaseSettings databaseSettings)
        {
            this.chatContext = chatContext;
            this.databaseSettings = databaseSettings;
        }

        public async Task<IEnumerable<Message>> GetMessages()
        {
            //return await chatContext.Messages.ToListAsync();
            const string sqlQuery = @"
            SELECT
                ""dateTime"" as dateTime,
                ""user"" as user,
                ""message"" as message
            FROM public.""Messages""
            ";

            using (var connection = new NpgsqlConnection(databaseSettings.ChatConnection))
            {
                return await connection.QueryAsync<Message>(sqlQuery);
            }
        }

        public async Task SaveMessage(Message message)
        {
            chatContext.Messages.Add(message);
            await chatContext.SaveChangesAsync();
        }
    }
}

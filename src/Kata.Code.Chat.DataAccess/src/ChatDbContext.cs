using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Kata.Code.Chat.DataAccess
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

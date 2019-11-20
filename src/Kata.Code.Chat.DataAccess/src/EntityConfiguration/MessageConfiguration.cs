using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kata.Code.Chat.DataAccess.EntityConfiguration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(m => m.dateTime).HasName("Message_PK");
            builder.Property(m => m.dateTime).IsRequired().HasColumnType("timestamp");
            builder.Property(m => m.message).IsRequired().HasMaxLength(512);
            builder.Property(m => m.user).IsRequired().HasMaxLength(40);
        }
    }
}

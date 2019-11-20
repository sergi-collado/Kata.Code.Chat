﻿// <auto-generated />
using System;
using Kata.Code.Chat.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Kata.Code.Chat.DataAccess.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    partial class ChatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Kata.Code.Chat.Message", b =>
                {
                    b.Property<DateTime>("dateTime")
                        .HasColumnType("timestamp");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasMaxLength(512);

                    b.Property<string>("user")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("dateTime")
                        .HasName("Message_PK");

                    b.ToTable("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}

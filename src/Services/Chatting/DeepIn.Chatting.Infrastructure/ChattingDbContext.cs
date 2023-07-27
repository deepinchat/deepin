using DeepIn.Chatting.Domain.ChatAggregate;
using DeepIn.Chatting.Infrastructure.EntityTypeConfigurations;
using DeepIn.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Chatting.Infrastructure
{
    public class ChattingDbContext : DbContextBase<ChattingDbContext>
    {
        public ChattingDbContext(DbContextOptions<ChattingDbContext> options, IMediator mediator) : base(options, mediator) { }

        public const string DEFAULT_SCHEMA = "chatting";
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMember> ChatMembers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ChatEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMemberEntityTypeConfiguration());
        }
    }
}

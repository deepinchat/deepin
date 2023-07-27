using DeepIn.Chatting.Domain.ChatAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Chatting.Infrastructure.EntityTypeConfigurations
{
    public class ChatMemberEntityTypeConfiguration : IEntityTypeConfiguration<ChatMember>
    {
        public void Configure(EntityTypeBuilder<ChatMember> builder)
        {
            builder.ToTable(ChatMember.TableName, ChattingDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.ChatId).HasMaxLength(64).HasColumnName("chat_id");
            builder.Property(x => x.UserId).IsRequired().HasMaxLength(64).HasColumnName("user_id");
            builder.Property(x => x.Alias).HasMaxLength(16).HasColumnName("alias");

            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.IsOwner).HasColumnName("is_owner");
            builder.Property(x => x.IsAdmin).HasColumnName("is_admin");
            builder.Property(x => x.IsBot).HasColumnName("is_bot");
        }
    }
}

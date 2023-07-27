using DeepIn.Chatting.Domain.ChatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepIn.Chatting.Infrastructure.EntityTypeConfigurations
{
    public class ChatEntityTypeConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable(Chat.TableName, ChattingDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(64).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(x => x.IsPrivate).HasColumnName("is_private");
            builder.Property(x => x.Name).HasMaxLength(32).HasColumnName("name");
            builder.Property(x => x.Link).HasMaxLength(32).HasColumnName("link");
            builder.Property(x => x.AvatarId).HasMaxLength(64).HasColumnName("avatar_id");
            builder.Property(x => x.Description).HasMaxLength(256).HasColumnName("description");

            builder.Property(x => x.CreatedBy).HasMaxLength(64).HasColumnName("created_by");
            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
            builder.Property(x => x.IsDeleted).HasColumnName("is_deleted");
            builder.Property(x => x.Type).HasColumnName("type");

            builder.HasMany(x => x.ChatMembers).WithOne().HasForeignKey(x => x.ChatId);
        }
    }
}

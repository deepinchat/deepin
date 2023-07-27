using DeepIn.Contacts.API.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Contacts.API.Infrastructure.EntityTypeConfigurations
{
    public class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable(Contact.TableName, ContactDbContext.DEFAULT_SCHEMA);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.CreatedBy).IsRequired().HasMaxLength(64).HasColumnName("created_by");
            builder.Property(x => x.UserId).IsRequired().HasMaxLength(64).HasColumnName("user_id");
            builder.Property(x => x.FirstName).HasMaxLength(16).HasColumnName("first_name");
            builder.Property(x => x.LastName).HasMaxLength(16).HasColumnName("last_name");
            builder.Property(x => x.DisplayName).HasMaxLength(16).HasColumnName("display_name");
            builder.Property(x => x.PhoneNumber).HasMaxLength(16).HasColumnName("phone_number");
            builder.Property(x => x.Email).HasMaxLength(16).HasColumnName("email");

            builder.Property(x => x.CreatedAt).HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).HasColumnName("updated_at");
        }
    }
}

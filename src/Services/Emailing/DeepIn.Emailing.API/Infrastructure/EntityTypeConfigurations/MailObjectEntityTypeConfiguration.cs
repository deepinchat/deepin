using DeepIn.Emailing.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeepIn.Emailing.API.Infrastructure.EntityTypeConfigurations
{
    internal class MailObjectEntityTypeConfiguration : IEntityTypeConfiguration<MailObject>
    {
        public void Configure(EntityTypeBuilder<MailObject> builder)
        {
            builder.ToTable("mail_object", EmailingDbContext.DEFAULT_SCHEMA);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.From).IsRequired(true).HasMaxLength(256);
            builder.Property(s => s.To).IsRequired(true).HasMaxLength(2048);
            builder.Property(s => s.CC).HasMaxLength(2048);
            builder.Property(s => s.Subject).HasMaxLength(1024);
        }
    } 
}

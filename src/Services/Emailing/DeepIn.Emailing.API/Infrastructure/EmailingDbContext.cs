using DeepIn.Emailing.API.Domain.Entities;
using DeepIn.Emailing.API.Infrastructure.EntityTypeConfigurations;
using DeepIn.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Emailing.API.Infrastructure
{
    public class EmailingDbContext : DbContextBase<EmailingDbContext>
    {
        public const string DEFAULT_SCHEMA = "emailing";
        public EmailingDbContext(DbContextOptions<EmailingDbContext> options, IMediator? mediator = null) : base(options, mediator) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MailObjectEntityTypeConfiguration());
        }
        public DbSet<MailObject> MailObjects { get; set; }
    }
}
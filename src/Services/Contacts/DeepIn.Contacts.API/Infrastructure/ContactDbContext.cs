using DeepIn.Contacts.API.Domain.Entities;
using DeepIn.Contacts.API.Infrastructure.EntityTypeConfigurations;
using DeepIn.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Contacts.API.Infrastructure
{
    public class ContactDbContext : DbContextBase<ContactDbContext>
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options, IMediator mediator) : base(options, mediator) { }

        public const string DEFAULT_SCHEMA = "contacts";
        public DbSet<Contact> Contacts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactEntityTypeConfiguration());
        }
    }
}

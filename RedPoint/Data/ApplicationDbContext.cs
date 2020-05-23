using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;

namespace RedPoint.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<HubGroupIdentifier> UniqueIdentifiers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
        }
    }
}
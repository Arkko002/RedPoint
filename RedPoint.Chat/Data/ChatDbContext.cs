using Microsoft.EntityFrameworkCore;
using RedPoint.Chat.Models;

namespace RedPoint.Data
{
    /// <inheritdoc/>
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options)
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
            builder.Entity<ChatUser>().ToTable("Users");
        }
    }
}
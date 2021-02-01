using Microsoft.EntityFrameworkCore;
using RedPoint.Chat.Models.Chat;

namespace RedPoint.Chat.Data
{
    /// <inheritdoc/>
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ChatUser> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Server> Servers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ChatUser>().ToTable("Users");
        }
    }
}
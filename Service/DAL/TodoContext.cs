using System.Data.Entity;

namespace Service.DAL
{
    public class TodoContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<PendingUser> PendingUsers { get; set; }
        public DbSet<PendingPasswordReset> PendingPasswordResets { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public TodoContext()
            : base("name=TodoConnectionString")
        {
        }
    }
}
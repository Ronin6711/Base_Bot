using Microsoft.EntityFrameworkCore;

namespace Discord_Bot.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; } = null!;
    }

    // UserEntity.cs
    public class UserEntity
    {
        public ulong Id { get; set; }
        public string? Name { get; set; }
        public int BasePoints { get; set; }
        public string? Status { get; set; }
    }
}

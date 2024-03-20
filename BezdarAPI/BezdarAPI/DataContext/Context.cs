using BezdarAPI.DataContext.Model;
using Microsoft.EntityFrameworkCore;

namespace BezdarAPI.DataContext
{
    public class Context(DbContextOptions<Context> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Shop> Shops { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using WebRedisEx.Models.Data;

namespace WebRedisEx.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        // 添加无参构造函数
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}

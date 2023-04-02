using Microsoft.EntityFrameworkCore;
using WebApplication1;

namespace NET6MiddleWareStudy
{

    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        //這個跟 model 層綁太緊了，要在 web 端 直接實作？//不要實作 web層的東西。
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> IdentityRoles { get; set; }
    }
}

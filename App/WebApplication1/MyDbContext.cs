using Microsoft.EntityFrameworkCore;
using WebApplication1;

namespace NET6MiddleWareStudy
{

    public class MyDbContext : DbContext
    {
        public static Dictionary<string, object> Tables; 
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            Tables = new Dictionary<string, object>();
            Tables.Add("ApplicationUser", ApplicationUsers);
            Tables.Add("ApplicationRole", IdentityRoles);
        }

        //這個跟 model 層綁太緊了，要在 web 端 直接實作？
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> IdentityRoles { get; set; }
    }
}

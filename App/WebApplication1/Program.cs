using App_NET6.Controllers;
using Domain_Train.dev;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NET6MiddleWareStudy;
using Train;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //# Add services to the container.

            //builder.Services.AddDistributedMemoryCache();//奇怪的耦合，差點中風
            //builder.Services.AddSession(options =>
            //{
            //    options.Cookie.Name = ".MyApp.Session";
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);
            //});

            builder.Services.AddControllers();
            builder.Services.AddScoped<IdentityUser, ApplicationUser>();
            builder.Services.AddScoped<IdentityRole, ApplicationRole>();
            builder.Services.AddScoped<IUserStore<IdentityUser>, CustomUserStore>();
            builder.Services.AddScoped<IRoleStore<IdentityRole>, CustomRoleStore>();
            builder.Services.AddScoped<UserManager<IdentityUser>>();
            builder.Services.AddScoped<SignInManager<IdentityUser>>();
            builder.Services.AddScoped<ITrainPersistant, FakeTrainDb>();
            //builder.Services.AddScoped<RoleManager<IdentityRole>>();


            builder.Services.AddDbContext<MyDbContext>(o=>o.UseInMemoryDatabase(databaseName: "MyDb"));
            //builder.Services.AddScoped<IUserStore<ApplicationUser>, UserStore<ApplicationUser, ApplicationRole, MyDbContext>>();
            //builder.Services.AddScoped<IRoleStore<ApplicationRole>, RoleStore<ApplicationRole, MyDbContext>>();
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddDefaultTokenProviders();


            var app = builder.Build();

            //# Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseMiddleware<SpaMiddleware>();
            //app.UseSession();

            //app.MapControllers();
            app.UseRouting();
            app.UseEndpoints(e => {
                e.MapControllers();
                e.MapFallbackToFile("index.html");
            });
            app.Run();
        }
    }
}
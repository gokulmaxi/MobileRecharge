using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MobileRecharge.Areas.Identity.Data;
namespace MobileRecharge
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("MobileRechargeContextConnection") ?? throw new InvalidOperationException("Connection string 'MobileRechargeContextConnection' not found.");

            builder.Services.AddDbContext<MobileRechargeContext>(options =>
options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<MobileRechargeUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MobileRechargeContext>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); ;

            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //seeding user roles
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Manger" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            //seeding admin user
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<MobileRechargeUser>>();
                string mail = "admin@presidio.com";
                string pass = "Verystr0ngpass";
                if(await userManager.FindByEmailAsync(mail) == null)
                {
                    Console.WriteLine("seeding admin user");
                    var user = new MobileRechargeUser();
                    user.Email = mail;
                    user.UserName = "admin";
                    userManager.CreateAsync(user, pass);
                    //await userManager.AddToRoleAsync(user, "Admin");

                }
            }
            app.Run();
        }
    }
}
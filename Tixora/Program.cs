using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tixora.Models;
using Tixora.Models.Context;
using Tixora.Repositories;
using Tixora.Repositories.Interfaces;
using Tixora.Services;
using Tixora.Services.Interface;
using Tixora.Services.Interfaces;

namespace Tixora
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);



            // Add services to the container.




            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<TixoraContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<TixoraContext>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IVenueService, VenueService>();


            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<IVenueRepository, VenueRepository>();
            builder.Services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            



            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}

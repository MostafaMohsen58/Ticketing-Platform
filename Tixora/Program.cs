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

            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddScoped<ITicketCategoryRepository, TicketCategoryRepository>();
            builder.Services.AddScoped<IOrganizerRepository, OrganizerRepository>();

            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IEventRepository, EventRepository>();

            builder.Services.AddScoped<IEventsService, EventsService>();


            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IVenueRepository, VenueRepository>();

            builder.Services.AddScoped<IVenueService, VenueService>();
            builder.Services.AddScoped<IOrganizerService, OrganizerService>();
            builder.Services.AddScoped<ITicketCategoryService, TicketCategoryService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddSingleton<FileService>();

            builder.Services.AddAuthentication().AddGoogle(op =>
            {
                op.ClientId = builder.Configuration["Auth:Google:ClientId"];
                op.ClientSecret = builder.Configuration["Auth:Google:ClientSecret"];
            });

            var app = builder.Build();

            //Remove Expired Bookings
            var timer = new System.Timers.Timer(30 * 60 * 1000);
            timer.Elapsed += async (sender, e) =>
            {
                using (var scope = app.Services.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<TixoraContext>();
                    var expiredBookings = context.Bookings.Where(b => b.PaymentStatus == "Pending" && b.BookedAt < DateTime.UtcNow.AddMinutes(-30))
                    .ToList();
                    if (expiredBookings.Any())
                    {
                        context.Bookings.RemoveRange(expiredBookings);
                        await context.SaveChangesAsync();
                    }
                }
            };
            timer.Start();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            test();
            SeedAdminUserAsync(app).GetAwaiter().GetResult();

            app.Run();

        }
        public static async Task SeedAdminUserAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";
            string adminRole = "Admin";

            // Create role if it doesn't exist
            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // Create user if it doesn't exist
            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = "User",
                    EmailConfirmed = true,
                    Gender=Gender.Male,
                    Address = "Admin Address",
                    City = "Admin City",
                    PhoneNumber = "01000000000",
                    NationalId = "00000000000000",
                    DateOfBrith = new DateOnly(1990, 1, 1)
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, adminRole);
                    Console.WriteLine("? Admin user created successfully.");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"? Error creating admin user: {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("?? Admin user already exists.");
            }
        }

        public static void test()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            var user = new IdentityUser();
            string passwordHash = hasher.HashPassword(user, "Admin@123");
            Console.WriteLine(passwordHash);
        }
    }
}

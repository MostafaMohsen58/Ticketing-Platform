using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tixora.Models.Context
{
    public class TixoraContext:IdentityDbContext<ApplicationUser>
    {
        public TixoraContext(DbContextOptions<TixoraContext> options) : base(options)
        {}
        
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var comedyDate = new DateTime(2025, 6, 15, 20, 0, 0);
            var footballDate = new DateTime(2025, 7, 1, 19, 0, 0);
            var techDate = new DateTime(2025, 7, 15, 10, 0, 0);
            var scienceDate = new DateTime(2025, 8, 1, 18, 0, 0);

            modelBuilder.Entity<Organizer>().HasData(
                new Organizer { Id = 1, Name = "Comedy Club Egypt", ContactEmail = "info@comedyclub.com", ContactPhone = "01032567599", LogoUrl = "comedyLogo.jpg" },
                new Organizer { Id = 2, Name = "Egyptian Football Association", ContactEmail = "info@efa.com", ContactPhone = "0223456789", LogoUrl = "footlogo.jpg" },
                new Organizer { Id = 3, Name = "Tech Summit Org", ContactEmail = "contact@techsummit.org", ContactPhone = "01012345678", LogoUrl = "TechLogo.jpg" },
                new Organizer { Id = 4, Name = "Science Foundation", ContactEmail = "info@science.org", ContactPhone = "0229876543", LogoUrl = "SciLogo.jpg" }
            );

            modelBuilder.Entity<Venue>().HasData(
                new Venue { Id = 1, Name = "Cairo House", Address = "Cairo, Egypt", Capacity = 2000 },
                new Venue { Id = 2, Name = "Cairo International Stadium", Address = "Nasr City, Cairo", Capacity = 75000 },
                new Venue { Id = 3, Name = "Bibliotheca Alexandrina", Address = "Alexandria", Capacity = 1500 },
                new Venue { Id = 4, Name = "American University in Cairo", Address = "New Cairo", Capacity = 500 }
            );

            modelBuilder.Entity<TicketCategory>().HasData(
                new TicketCategory { Id = 1, Name = "VIP", Description = "VIP seating with complimentary drinks", PriceMultiplier = 2.0f },
                new TicketCategory { Id = 2, Name = "Regular", Description = "Regular seating", PriceMultiplier = 1.0f },
                new TicketCategory { Id = 3, Name = "Student", Description = "Discounted ticket for students", PriceMultiplier = 0.5f }
            );

            modelBuilder.Entity<Event>().HasData(
                // Stand-Up Comedy
                new Event
                {
                    Id = 1,
                    Title = "Stand-Up Comedy Night",
                    Description = "An evening with Egypt's top comedians",
                    StartDate = comedyDate,
                    ImageUrl = "images/comedy-night.jpg",
                    OrganizerId = 1,
                    VenueId = 1,
                    Category = "Entertainment"
                },
                // Football Match
                new Event
                {
                    Id = 2,
                    Title = "Al Ahly vs Zamalek Derby",
                    Description = "The classic Cairo football derby",
                    StartDate = footballDate,
                    ImageUrl = "images/AlahlyVsZam.jpg",
                    OrganizerId = 2,
                    VenueId = 2,
                    Category = "Matches"
                },
                // Tech Conference
                new Event
                {
                    Id = 3,
                    Title = "Tech Trends 2025",
                    Description = "Annual technology conference",
                    StartDate = techDate,
                    ImageUrl = "images/Trends.jpg",
                    OrganizerId = 3,
                    VenueId = 3,
                    Category = "Entertainment"
                },
                // Science Lecture
                new Event
                {
                    Id = 4,
                    Title = "Future of AI",
                    Description = "Scientific lecture on artificial intelligence",
                    StartDate = scienceDate,
                    ImageUrl = "images/Future.jpg",
                    OrganizerId = 4,
                    VenueId = 4,
                    Category = "Entertainment"
                },
                // Football Match
                new Event
                {
                    Id = 5,
                    Title = "Al Ahly vs FC Porto",
                    Description = "The classic Cairo football derby",
                    StartDate = footballDate,
                    ImageUrl = "images/PortovsAlahly.jpeg",
                    OrganizerId = 2,
                    VenueId = 2,
                    Category = "Matches"
                },
                // Football Match
                new Event
                {
                    Id = 6,
                    Title = "Liverpool vs chelsea",
                    Description = "The classic Cairo football derby",
                    StartDate = footballDate,
                    ImageUrl = "images/LivvsChe.jpg",
                    OrganizerId = 2,
                    VenueId = 2,
                    Category = "Matches"
                },
                // Football Match
                new Event
                {
                    Id = 7,
                    Title = "Barcelona vs Real Madrid",
                    Description = "The classic Cairo football derby",
                    StartDate = footballDate,
                    ImageUrl = "images/barcavsreal.jpeg",
                    OrganizerId = 2,
                    VenueId = 2,
                    Category = "Matches"
                },
                // Football Match
                new Event
                {
                    Id = 8,
                    Title = "Manchester United vs Manchester City",
                    Description = "The classic Cairo football derby",
                    StartDate = footballDate,
                    ImageUrl = "images/Manvscity.jpeg",
                    OrganizerId = 2,
                    VenueId = 2,
                    Category = "Matches"
                }
            );
            modelBuilder.Entity<Ticket>().HasData(
                // Comedy Event Tickets
                new Ticket { Id = 1, EventId = 1, TicketCategoryId = 1, Price = 500, AvailableQuantity = 50, Status = 0 },
                new Ticket { Id = 2, EventId = 1, TicketCategoryId = 2, Price = 300, AvailableQuantity = 150, Status = 0 },
                new Ticket { Id = 3, EventId = 1, TicketCategoryId = 3, Price = 150, AvailableQuantity = 100, Status = 0 },

                // Football Match Tickets
                new Ticket { Id = 4, EventId = 2, TicketCategoryId = 1, Price = 1000, AvailableQuantity = 500, Status = 0 },
                new Ticket { Id = 5, EventId = 2, TicketCategoryId = 2, Price = 500, AvailableQuantity = 5000, Status = 0 },
                new Ticket { Id = 6, EventId = 2, TicketCategoryId = 3, Price = 250, AvailableQuantity = 2000, Status = 0 },

                // Tech Conference Tickets
                new Ticket { Id = 7, EventId = 3, TicketCategoryId = 1, Price = 800, AvailableQuantity = 30, Status = 0 },
                new Ticket { Id = 8, EventId = 3, TicketCategoryId = 2, Price = 400, AvailableQuantity = 200, Status = 0 },
                new Ticket { Id = 9, EventId = 3, TicketCategoryId = 3, Price = 280, AvailableQuantity = 100, Status = 0 },

                // Science Lecture Tickets
                new Ticket { Id = 10, EventId = 4, TicketCategoryId = 2, Price = 200, AvailableQuantity = 300, Status = 0 },
                new Ticket { Id = 11, EventId = 4, TicketCategoryId = 3, Price = 100, AvailableQuantity = 150, Status = 0 },

                // Science Lecture Tickets
                new Ticket { Id = 12, EventId = 5, TicketCategoryId = 1, Price = 1000, AvailableQuantity = 500, Status = 0 },
                new Ticket { Id = 13, EventId = 5, TicketCategoryId = 2, Price = 500, AvailableQuantity = 5000, Status = 0 },
                new Ticket { Id = 14, EventId = 5, TicketCategoryId = 3, Price = 250, AvailableQuantity = 2000, Status = 0 },

                // Science Lecture Tickets
                new Ticket { Id = 15, EventId = 6, TicketCategoryId = 1, Price = 1000, AvailableQuantity = 500, Status = 0 },
                new Ticket { Id = 16, EventId = 6, TicketCategoryId = 2, Price = 500, AvailableQuantity = 5000, Status = 0 },
                new Ticket { Id = 17, EventId = 6, TicketCategoryId = 3, Price = 250, AvailableQuantity = 2000, Status = 0 },

                // Science Lecture Tickets
                new Ticket { Id = 18, EventId = 7, TicketCategoryId = 1, Price = 1000, AvailableQuantity = 500, Status = 0 },
                new Ticket { Id = 19, EventId = 7, TicketCategoryId = 2, Price = 500, AvailableQuantity = 5000, Status = 0 },
                new Ticket { Id = 20, EventId = 7, TicketCategoryId = 3, Price = 250, AvailableQuantity = 2000, Status = 0 },

                // Science Lecture Tickets
                new Ticket { Id = 21, EventId = 8, TicketCategoryId = 1, Price = 1000, AvailableQuantity = 500, Status = 0 },
                new Ticket { Id = 22, EventId = 8, TicketCategoryId = 2, Price = 500, AvailableQuantity = 5000, Status = 0 },
                new Ticket { Id = 23, EventId = 8, TicketCategoryId = 3, Price = 250, AvailableQuantity = 2000, Status = 0 }
            );

        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tixora.Models.Context
{
    public class TixoraContext:IdentityDbContext<ApplicationUser>
    {
        public TixoraContext(DbContextOptions<TixoraContext> options) : base(options)
        {

        }
        
        public DbSet<Orginzier> Orginziers { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

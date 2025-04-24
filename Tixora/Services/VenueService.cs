using Tixora.Models;
using Tixora.Repositories;
using Tixora.Repositories.Interfaces;
using Tixora.Services.Interfaces;
using Tixora.ViewModels;

namespace Tixora.Services
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository venueRepository;

        public VenueService(IVenueRepository venueRepository)
        {
            this.venueRepository = venueRepository;
        }
        public void Create(Venue venue)
        {
            venueRepository.AddAsync(venue);
            venueRepository.SaveAsync();
        }
        public Venue? CheckVenueExistWithSameName(string name)
        {
            var venue = venueRepository.GetAll().FirstOrDefault(v => v.Name == name);
            return venue;
        }

        public void Update(Venue venue)
        {
            var venueFromDb = GetById(venue.Id);
            if(venueFromDb != null)
            {
                venueFromDb.Name = venue.Name;
                venueFromDb.Address = venue.Address;
                venueFromDb.Capacity = venue.Capacity;

                venueRepository.UpdateAsync(venueFromDb);
                venueRepository.SaveAsync();
            }
        }
        public void Delete(int id)
        {
            var venueFromDb = GetById(id);
            if (venueFromDb != null)
            {
                venueRepository.Delete(venueFromDb);
                venueRepository.SaveAsync();
            }
            
        }
        public Venue GetById(int id)
        {
            Venue venue =venueRepository.GetById(id);
            if (venue == null)
            {
                return null;
            }
            return venue;
        }
        public List<Venue> GetAll()
        {
            List<Venue> venues= venueRepository.GetAll();
            return venues;
        }

        

    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task Create(Venue venue)
        {
            await venueRepository.AddAsync(venue);
            await venueRepository.SaveAsync();
        }
        public async Task<Venue?> CheckVenueExistWithSameName(string name)
        {
            var venue = await venueRepository.GetAll();
            //.FirstOrDefault(v => v.Name == name);
            foreach (var v in venue)
            {
                if (v.Name == name)
                {
                    return v;
                }
            }
            return null;
        }

        public async Task Update(Venue venue)
        {
            var venueFromDb =await GetById(venue.Id);
            if(venueFromDb != null)
            {
                venueFromDb.Name = venue.Name;
                venueFromDb.Address = venue.Address;
                venueFromDb.Capacity = venue.Capacity;

                await venueRepository.UpdateAsync(venueFromDb);
                await venueRepository.SaveAsync();
            }
        }
        public async Task Delete(int id)
        {
            var venueFromDb =await GetById(id);

            if (venueFromDb != null)
            {
                venueRepository.Delete(venueFromDb);
                var x = venueRepository.Save();
                
            }
            
        }
        public async Task<Venue?> GetById(int id)
        {
            Venue venue = await venueRepository.GetById(id);
            if (venue == null)
            {
                return null;
            }
            return venue;
        }
        public async Task<List<Venue>> GetAll()
        {
            List<Venue> venues=await venueRepository.GetAll();
            return venues;
        }
        public List<SelectListItem> Venues()
        {
            return venueRepository.GetVenues();
        }


    }
}

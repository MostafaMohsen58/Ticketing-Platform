using System;
using System.Collections.Generic;
using Tixora.Models;
namespace Tixora.ViewModels.EventViewModel
{
    public class MatchesViewModel
    {
        public List<Event> Events { get; set; }
        public List<Venue> Venues { get; set; }
        public string SelectedCategory { get; set; }
        public int? SelectedVenueId { get; set; }
        public DateTime? SelectedDate { get; set; }
        public string SearchTerm { get; set; }

        // Add predefined categories for matches/events display
        public List<CategoryItem> Categories { get; set; } = new List<CategoryItem>
        {
            new CategoryItem { Name = "All", Icon = "calendar-alt", ColorClass = "primary" },
            new CategoryItem { Name = "Matches", Icon = "futbol", ColorClass = "success" },
            new CategoryItem { Name = "Concerts", Icon = "music", ColorClass = "danger" },
            new CategoryItem { Name = "Theater", Icon = "theater-masks", ColorClass = "warning" },
            new CategoryItem { Name = "Exhibitions", Icon = "building", ColorClass = "info" }
        };
    }

    public class CategoryItem
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string ColorClass { get; set; }
    }
}
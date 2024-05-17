using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingSystem.Models
{
    public class EventUserViewModel
    {
        public List<Event>? Events { get; set; }
        public SelectList? UserIds { get; set; }
        public string? EventUser {  get; set; }
        public string? SearchString { get; set; }
    }
}

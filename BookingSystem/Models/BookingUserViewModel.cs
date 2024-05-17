using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingSystem.Models
{
    public class BookingUserViewModel
    {
        public List<Booking>? Bookings { get; set; }
        public SelectList? BookingEvents { get; set; }
        public SelectList? BookingEventsIds { get; set; }
        public SelectList? BookingUsers { get; set; }
        public int? BookingEventId { get; set; }
        public string? BookingUser { get; set; }
        public string? SearchString { get; set; }
    }
}

using BookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Models
{
    public class SeedBookingData
    {
        public static void InitializeBooking(IServiceProvider serviceProvider)
        {
            using (var context = new BookingSystemContext(serviceProvider.GetRequiredService<DbContextOptions<BookingSystemContext>>()))
            {

                if (context.Booking.Any())
                {
                    return;
                }
                context.Booking.AddRange(
                    new Booking
                    {
                        User = context.User.Find(5),
                        Event = context.Event.Find(16),
                        StartTime = DateTime.Parse("12:30"),
                        EndTime = DateTime.Parse("15:30"),
                        IsBooked = true
                    },
                    new Booking
                    {
                        User = context.User.Find(6),
                        Event = context.Event.Find(14),
                        StartTime = DateTime.Parse("05:30"),
                        EndTime = DateTime.Parse("07:30"),
                        IsBooked = true
                    },
                    new Booking
                    {
                        User = context.User.Find(8),
                        Event = context.Event.Find(13),
                        StartTime = DateTime.Parse("15:30"),
                        EndTime = DateTime.Parse("17:30"),
                        IsBooked = true
                    },
                    new Booking
                    {
                        User = context.User.Find(7),
                        Event = context.Event.Find(18),
                        StartTime = DateTime.Parse("21:30"),
                        EndTime = DateTime.Parse("22:30"),
                        IsBooked = true
                    }
                    
                );
                context.SaveChanges();
            }
        }
    }
}

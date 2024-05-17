using BookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Models
{
    public class SeedEventData
    {
        public static void InitializeEvent(IServiceProvider serviceProvider)
        {
            using (var context = new BookingSystemContext(serviceProvider.GetRequiredService<DbContextOptions<BookingSystemContext>>()))
            {
                
                if (context.Event.Any())
                    {
                    return;
                    }
                context.Event.AddRange(
                    new Event
                    {
                        UserId = 5,
                        UserFirstName = context.User?.Find(5)!.FirstName,
                        UserLastName = context.User?.Find(5)!.LastName,
                        Title = "Programmering",
                        Description = "Arbeta med programmering",
                        Date = DateTime.Parse("2024-05-11"),
                        StartTime = DateTime.Parse("15:30"),
                        EndTime = DateTime.Parse("17:30")
                    },
                    new Event
                    {
                        UserId = 6,
                        UserFirstName = context.User?.Find(6)!.FirstName,
                        UserLastName = context.User?.Find(6)!.LastName,
                        Title = "Psykologi",
                        Description = "Arbeta med psykologi",
                        Date = DateTime.Parse("2024-05-11"),
                        StartTime = DateTime.Parse("05:30"),
                        EndTime = DateTime.Parse("07:30")
                    },
                    new Event
                    {
                        UserId = 7,
                        UserFirstName = context.User?.Find(7)!.FirstName,
                        UserLastName = context.User?.Find(7)!.LastName,
                        Title = "Envariabelsanalys",
                        Description = "Arbeta med differential ekvationer",
                        Date = DateTime.Parse("2024-05-11"),
                        StartTime = DateTime.Parse("09:30"),
                        EndTime = DateTime.Parse("11:30")
                    },
                    new Event
                    {
                        UserId = 8,
                        UserFirstName = context.User?.Find(8)!.FirstName,
                        UserLastName = context.User?.Find(8)!.LastName,
                        Title = "Fysik",
                        Description = "Arbeta med fysik",
                        Date = DateTime.Parse("2024-05-11"),
                        StartTime = DateTime.Parse("11:30"),
                        EndTime = DateTime.Parse("12:30")
                    }
                    
                );
                context.SaveChanges();
            }
        }
    }
}

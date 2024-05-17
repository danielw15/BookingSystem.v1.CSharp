using BookingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Models
{
    public class SeedUserData
    {
        public static void InitializeUser(IServiceProvider serviceProvider)
        {
            using (var context = new BookingSystemContext(serviceProvider.GetRequiredService<DbContextOptions<BookingSystemContext>>()))
            {
                if (context.User.Any())
                    {
                    return;
                    }
                context.User.AddRange(
                    new User
                    {
                        FirstName = "Jeanette",
                        LastName = "Andersson",
                        Email = "jeanette@andersson.com",
                        Password = "1233123123",
                        PhoneNumber = "0722363125",
                        CreationDate = DateTime.Parse("2023-03-01"),
                        Role = "CFO"
                    },
                    new User
                    {
                        FirstName = "Olof",
                        LastName = "Nilsson",
                        Email = "olof@nilsson.us",
                        Password = "1234123123",
                        PhoneNumber = "0721727432",
                        CreationDate = DateTime.Parse("2023-06-17"),
                        Role = "CEO"
                    },
                    new User
                    {
                        FirstName = "David",
                        LastName = "Grönblad",
                        Email = "David@gronblad.se",
                        Password = "1231123123",
                        PhoneNumber = "0722363125",
                        CreationDate = DateTime.Parse("2024-03-25"),
                        Role = "Worker"
                    },
                    new User
                    {
                        FirstName = "Ronald",
                        LastName = "Livbom",
                        Email = "Ronald@livbom.com",
                        Password = "1123123123",
                        PhoneNumber = "0771111111",
                        CreationDate = DateTime.Parse("2024-03-21"),
                        Role = "Dog"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}

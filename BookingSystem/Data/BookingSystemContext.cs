using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Models;

namespace BookingSystem.Data
{
    public class BookingSystemContext : DbContext
    {
        public BookingSystemContext (DbContextOptions<BookingSystemContext> options)
            : base(options)
        {
        }

        public DbSet<BookingSystem.Models.User> User { get; set; } = default!;
        public DbSet<BookingSystem.Models.Event> Event { get; set; } = default!;
        public DbSet<BookingSystem.Models.Booking> Booking { get; set; } = default!;


    }
}

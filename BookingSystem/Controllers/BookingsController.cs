using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Data;
using BookingSystem.Models;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;

namespace BookingSystem.Controllers
{
    public class BookingsController : Controller
    {
        private readonly BookingSystemContext _context;

        public BookingsController(BookingSystemContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(int bookingEventId = 0, string searchString = "")
        {
            // delete all rows in db: await _context.Booking.ExecuteDeleteAsync();

            if (_context.Booking == null)
            {
                return Problem("Entity set 'BookingSystemContext.Booking' is null.");
            }

            var bookings = _context.Booking
                .Include(b => b.User)
                .Include(b => b.Event)
                .Where(b => (searchString == "" 
                || (b.User.FirstName.Contains(searchString) || b.User.LastName.Contains(searchString)))
                && (bookingEventId == 0 || b.Event.Id == bookingEventId));

            IQueryable<Booking> bookingQuery = _context.Booking;

            var bookingUserVM = new BookingUserViewModel
            {
                BookingEventsIds = new SelectList(await bookingQuery.Select(b => b.Event!.Id).Distinct().ToListAsync()),
                BookingUsers = new SelectList(await bookingQuery.Select(b => b.User).ToListAsync()),
                BookingEvents = new SelectList(await bookingQuery.Select(b => b.Event).ToListAsync()),
                Bookings = await bookings.ToListAsync()
            };
            return View(bookingUserVM);

        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }


        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,EventId,StartTime,EndTime,IsBooked")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,EventId,StartTime,EndTime,IsBooked")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Events/CheckBookingEventIdExists/9
        public async Task<IActionResult> CheckBookingEventIdExists(int id)
        {
            var bookingEventExists = await BookingEventExists(id);
            var eventObject = _context.Event.Find(id);
            var dateObject = DateTime.Now;
            string? eventTitle = null;
            string? eventDescription = null;
            string? eventDate = null;
            string? eventStartTime = null;
            string? eventEndTime = null;
            if (bookingEventExists && eventObject != null)
            {
                dateObject = eventObject.Date;
                eventTitle = eventObject.Title;
                eventDescription = eventObject.Description;
                eventDate = eventObject.Date.ToString("yyyy/MM/dd");
                eventStartTime = eventObject.StartTime.ToString("HH:mm");
                eventEndTime = eventObject.EndTime.ToString("HH:mm");
            }
            return Ok(new
            {
                exists = bookingEventExists,
                date = dateObject,
                EventTitle = eventTitle,
                EventDescription = eventDescription,
                EventDate = eventDate,
                EventStartTime = eventStartTime,
                EventEndTime = eventEndTime
            });
        }

        // GET: Events/CheckBookingUserIdExists/8
        public async Task<IActionResult> CheckBookingUserIdExists(int id)
        {
            var bookingUserExists = await BookingUserExists(id);
            var user = _context.User.Find(id);
            string? userFirstName = null;
            string? userLastName = null;
            if (bookingUserExists && user != null)
            {
                userFirstName = user.FirstName;
                userLastName = user.LastName;
            }
            return Ok(new { exists = bookingUserExists, FullName = userFirstName + " " + userLastName });
        }

        //will implement Checks so objects cannot be created with invalid FKs
        // GET: Events/CheckIfEventAndUserExists/9
        private async Task<bool> CheckIfEventAndUserExists(int idEvent, int idUser)
        {
            if(await BookingUserExists(idUser) && await BookingEventExists(idEvent))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        private async Task<bool> BookingUserExists(int id)
        {
            return await _context.User.AnyAsync(e => e.Id == id);
        }
        private async Task<bool> BookingEventExists(int id)
        {
            return await _context.Event.AnyAsync(e => e.Id == id);
        }
        
        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.Id == id);
        }


    }
}

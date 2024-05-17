using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingSystem.Data;
using BookingSystem.Models;

namespace BookingSystem.Controllers
{
    public class EventsController : Controller
    {
        private readonly BookingSystemContext _context;

        public EventsController(BookingSystemContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index(int eventUser, string searchString)
        {
            // delete db : await _context.Event.ExecuteDeleteAsync();
            if (_context.User == null)
            {
                return Problem("Entity set 'BookingSystemContext.Event' is null.");
            }
            IQueryable<int> eventQuery = from m in _context.Event
                                            orderby m.UserId
                                            select m.UserId;
            var events = from m in _context.Event
                         select m;

            if(!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.Title!.Contains(searchString));
            }
            if (eventUser != null && eventUser != 0)
            {
                events = events.Where(x => x.UserId == eventUser);
            }
            var eventUserVM = new EventUserViewModel
            {
                UserIds = new SelectList(await eventQuery.Distinct().ToListAsync()),
                Events = await events.ToListAsync()
            };
            return View(eventUserVM);

        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create/
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Title,Description,Date,StartTime,EndTime,")] Event @event)
        {
            if(_context.User.Find(@event.UserId) != null)
            {
                @event.UserFirstName = _context.User?.Find(@event.UserId)!.FirstName;
                @event.UserLastName = _context.User?.Find(@event.UserId)!.LastName;
                if (ModelState.IsValid)
                {
                    _context.Add(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,UserFirstNamem,UserLastName,Description,Date,StartTime,EndTime")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event != null)
            {
                _context.Event.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Events/CheckEventUserExists/7
        public async Task<IActionResult> CheckEventUserExists(int id)
        {
            var eventUserExists = await EventUserExists(id);
            var user = _context.User.Find(id);
            string? userFirstName = null;
            string? userLastName = null;
            if (eventUserExists && user != null)
            {
                userFirstName = user.FirstName;
                userLastName = user.LastName;
            }
            return Ok(new { exists = eventUserExists, FullName = userFirstName + " " + userLastName });
        }

        private async Task<bool> EventUserExists(int id)
        {
            return await _context.User.AnyAsync(e => e.Id == id);
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }


    }
}

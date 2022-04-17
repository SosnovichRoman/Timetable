using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timetable.Data;
using Timetable.Models;

namespace Timetable.Controllers
{

    public class DaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Days
        public async Task<IActionResult> Index()
        {
            var days = _context.Day.Include(d => d.Group);
            return View(await days.ToListAsync());
        }

        // GET: Days/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _context.Day
                .Include(d => d.Group)
                .FirstOrDefaultAsync(m => m.id == id);
            if (day == null)
            {
                return NotFound();
            }

            return View(day);
        }

        // GET: Days/Create
        public IActionResult Create(string groupId)
        { 
            ViewData["GroupId"] = groupId;
            return View();
        }

        // POST: Days/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,dayOfWeek,GroupId")] Day day)
        {
            if (ModelState.IsValid)
            {
                _context.Add(day);
                await _context.SaveChangesAsync();
                return RedirectToAction("ShowDay", new { GroupId = day.GroupId });
            }
            ViewData["GroupId"] = new SelectList(_context.Set<Group>(), "id", "id", day.GroupId);
            return View(day);
        }

        // GET: Days/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _context.Day.FindAsync(id);
            if (day == null)
            {
                return NotFound();
            }
            ViewData["WeekId"] = new SelectList(_context.Set<Group>(), "id", "id", day.GroupId);
            return View(day);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,WeekId")] Day day)
        {
            if (id != day.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(day);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayExists(day.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ShowDay", new { DayId = day.GroupId });
            }
            ViewData["WeekId"] = new SelectList(_context.Set<Group>(), "id", "id", day.GroupId);
            return View(day);
        }

        // GET: Days/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var day = await _context.Day
                .Include(d => d.Group)
                .FirstOrDefaultAsync(m => m.id == id);
            if (day == null)
            {
                return NotFound();
            }

            return View(day);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var day = await _context.Day.FindAsync(id);
            _context.Day.Remove(day);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayExists(int id)
        {
            return _context.Day.Any(e => e.id == id);
        }
        public IActionResult ShowDay(int GroupId)
        {
            var days = _context.Day.ToList();
            var classs = _context.Group.ToList();
            var itemss = _context.Lesson.ToList();

            ViewData["GroupId"] = GroupId;
            var showday = _context.Day.Where(c => c.GroupId == GroupId).ToList();
            return View(showday);
            
        }
        public IActionResult ShowTimetable(string GroupName)
        {
            var days = _context.Day.ToList();

            var classs = _context.Group.ToList();
            var itemss = _context.Lesson.ToList();

            var _groupId = _context.Group.Where(g => g.name == GroupName).FirstOrDefault().id;
            ViewData["GroupId"] = _groupId;
            var showTimetable = _context.Day.Where(c => c.GroupId == _groupId).ToList();
            return View(showTimetable);

        }
    }
}

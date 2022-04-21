using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timetable.Data;
using Timetable.Models;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text;

namespace Timetable.Controllers
{

    public class GroupController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Group.ToListAsync());
        }

        // GET: Clas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clas = await _context.Group
                .FirstOrDefaultAsync(m => m.id == id);
            if (clas == null)
            {
                return NotFound();
            }

            return View(clas);
        }

        // GET: Clas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name")] Group group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(group);
        }

        // GET: Clas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clas = await _context.Group.FindAsync(id);
            if (clas == null)
            {
                return NotFound();
            }
            return View(clas);
        }

        // POST: Clas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name")] Group clas)
        {
            if (id != clas.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClasExists(clas.id))
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
            return View(clas);
        }

        // GET: Clas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clas = await _context.Group
                .FirstOrDefaultAsync(m => m.id == id);
            if (clas == null)
            {
                return NotFound();
            }

            return View(clas);
        }

        // POST: Clas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clas = await _context.Group.FindAsync(id);
            _context.Group.Remove(clas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClasExists(int id)
        {

            return _context.Group.Any(e => e.id == id);
        }


        public IActionResult ShowXML(int GroupID)
        {

            return View( _context.Group.ToList());

        }
        public FileResult GetJSON()
        {

            string xml = JsonSerializer.Serialize(_context.Group.ToList());
            
            byte[] mas = Encoding.ASCII.GetBytes(xml);
            string file_type = "application/json";
            string file_name = "xml.json";
            return File(mas, file_type, file_name);
        }

    }
}

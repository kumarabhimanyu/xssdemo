using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XSSDemo.Data;
using XSSDemo.Models;

namespace XSSDemo.Controllers
{
    public class PersonController : Controller
    {
        private readonly XSSDemoContext _context;

        public PersonController(XSSDemoContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
              return _context.PersonInfo != null ? 
                          View(await _context.PersonInfo.ToListAsync()) :
                          Problem("Entity set 'XSSDemoContext.PersonInfo'  is null.");
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonInfo == null)
            {
                return NotFound();
            }

            var personInfo = await _context.PersonInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personInfo == null)
            {
                return NotFound();
            }

            return View(personInfo);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Bio")] PersonInfo personInfo)
        {
            if (ModelState.IsValid)
            {
                StringBuilder sbBio = new StringBuilder();

                string? encodedBio = HttpUtility.HtmlEncode(personInfo.Bio);
                sbBio.Append(HttpUtility.HtmlEncode(encodedBio));
                sbBio.Replace("&amp;lt;b&amp;gt;", "<b>");
                sbBio.Replace("&amp;lt;/b&amp;gt;", "</b>");

                personInfo.Bio = sbBio.ToString();
                _context.Add(personInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personInfo);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonInfo == null)
            {
                return NotFound();
            }

            var personInfo = await _context.PersonInfo.FindAsync(id);
            if (personInfo == null)
            {
                return NotFound();
            }
            return View(personInfo);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Bio")] PersonInfo personInfo)
        {
            if (id != personInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonInfoExists(personInfo.Id))
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
            return View(personInfo);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonInfo == null)
            {
                return NotFound();
            }

            var personInfo = await _context.PersonInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personInfo == null)
            {
                return NotFound();
            }

            return View(personInfo);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonInfo == null)
            {
                return Problem("Entity set 'XSSDemoContext.PersonInfo'  is null.");
            }
            var personInfo = await _context.PersonInfo.FindAsync(id);
            if (personInfo != null)
            {
                _context.PersonInfo.Remove(personInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonInfoExists(int id)
        {
          return (_context.PersonInfo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

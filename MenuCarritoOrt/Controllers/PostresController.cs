using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MenuCarritoOrt.Datos;
using MenuCarritoOrt.Models;

namespace MenuCarritoOrt.Controllers
{
    public class PostresController : Controller
    {
        private readonly BaseDatos _context;

        public PostresController(BaseDatos context)
        {
            _context = context;
        }

        // GET: Postres
        public async Task<IActionResult> Index()
        {
            return View(await _context.Postres.ToListAsync());
        }

        // GET: Postres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postre = await _context.Postres
                .FirstOrDefaultAsync(m => m.id == id);
            if (postre == null)
            {
                return NotFound();
            }

            return View(postre);
        }

        // GET: Postres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Postres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Precio,Descripcion,id")] Postre postre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postre);
        }

        // GET: Postres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postre = await _context.Postres.FindAsync(id);
            if (postre == null)
            {
                return NotFound();
            }
            return View(postre);
        }

        // POST: Postres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nombre,Precio,Descripcion,id")] Postre postre)
        {
            if (id != postre.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostreExists(postre.id))
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
            return View(postre);
        }

        // GET: Postres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postre = await _context.Postres
                .FirstOrDefaultAsync(m => m.id == id);
            if (postre == null)
            {
                return NotFound();
            }

            return View(postre);
        }

        // POST: Postres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postre = await _context.Postres.FindAsync(id);
            _context.Postres.Remove(postre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostreExists(int id)
        {
            return _context.Postres.Any(e => e.id == id);
        }
    }
}

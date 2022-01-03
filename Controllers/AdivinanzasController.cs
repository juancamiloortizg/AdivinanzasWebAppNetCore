using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdivinanzasWebApp.Data;
using AdivinanzasWebApp.Models;

namespace AdivinanzasWebApp.Controllers
{
    public class AdivinanzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdivinanzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adivinanzas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Adivinanza.ToListAsync());
        }
        // GET: Adivinanzas/MostrarBuscador
        public async Task<IActionResult> MostrarBuscador()
        {
            return View();
        }
        // GET: Adivinanzas/MostrarResultados
        public async Task<IActionResult> MostrarResultados(string BuscarTermino)
        {
            return View("Index", await _context.Adivinanza.Where( a => a.Pregunta.Contains(BuscarTermino)).ToListAsync());
        }

        // GET: Adivinanzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adivinanza = await _context.Adivinanza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adivinanza == null)
            {
                return NotFound();
            }

            return View(adivinanza);
        }

        // GET: Adivinanzas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adivinanzas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Pregunta,Respuesta")] Adivinanza adivinanza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adivinanza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adivinanza);
        }

        // GET: Adivinanzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adivinanza = await _context.Adivinanza.FindAsync(id);
            if (adivinanza == null)
            {
                return NotFound();
            }
            return View(adivinanza);
        }

        // POST: Adivinanzas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pregunta,Respuesta")] Adivinanza adivinanza)
        {
            if (id != adivinanza.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adivinanza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdivinanzaExists(adivinanza.Id))
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
            return View(adivinanza);
        }

        // GET: Adivinanzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adivinanza = await _context.Adivinanza
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adivinanza == null)
            {
                return NotFound();
            }

            return View(adivinanza);
        }

        // POST: Adivinanzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adivinanza = await _context.Adivinanza.FindAsync(id);
            _context.Adivinanza.Remove(adivinanza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdivinanzaExists(int id)
        {
            return _context.Adivinanza.Any(e => e.Id == id);
        }
    }
}

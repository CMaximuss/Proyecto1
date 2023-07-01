using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;

namespace MVCBasico
{
    public class ReservasController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        Usuario usuario;

        public ReservasController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var escuelaDatabaseContext = _context.Reserva.Include(r => r.Comercio).Include(r => r.Usuario);
            return View(await escuelaDatabaseContext.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Comercio)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // GET: Reservas/Create
        public IActionResult Create()
        {
            ViewData["ComercioId"] = new SelectList(_context.Comercios, "Id", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Mail");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,ComercioId,UsuarioId")] Reserva reserva)
        {
            int id = reserva.UsuarioId;
            
            if (ModelState.IsValid)
            {

                _context.Add(reserva);
                await _context.SaveChangesAsync();

                return RedirectToAction("ReservasPorId", "Reservas", new { id = id});
                //return RedirectToAction(nameof(Index)); return anterior
            }

            ViewData["Comercio"] = new SelectList(_context.Comercios,"Id","Nombre", reserva.Comercio.Nombre);
            ViewData["Usuario"] = new SelectList(_context.Usuarios,"Id","Mail", reserva.Usuario.Mail);

            return View(reserva);        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["ComercioId"] = new SelectList(_context.Comercios, "Id", "Nombre", reserva.ComercioId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Mail", reserva.UsuarioId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,ComercioId,UsuarioId")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reserva);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["ComercioId"] = new SelectList(_context.Comercios, "Id", "Nombre", reserva.ComercioId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Mail", reserva.UsuarioId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _context.Reserva
                .Include(r => r.Comercio)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _context.Reserva.Any(e => e.Id == id);
        }
    

        //funciona
        public async Task<IActionResult> ReservasPorId(int id)
        {
            ViewBag.Id = id;
            
            var listaReservas = _context.Reserva.Include(r => r.Comercio).Include( r => r.Usuario).Where(r => r.UsuarioId == id);

            return View(await listaReservas.ToListAsync());
        }

        

       


    }
}

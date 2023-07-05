using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCBasico.Context;
using MVCBasico.Models;

namespace MVCBasico.Controllers
{
    public class ComercioController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        private const string _fotoComercioDefault= "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSLQbPGPISqJ4cxwwcVwi9Gbal41u692aVNag&usqp=CAU";

        public ComercioController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        // GET: Comercio
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comercios.ToListAsync());
        }

        // GET: Comercio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("MensajeError");
            }

            var comercio = await _context.Comercios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comercio == null)
            {
                return View("MensajeError");
            }

            return View(comercio);
        }

        // GET: Comercio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comercio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Mail,Contrasenia,Telefono,Direccion,FotoComercio")] Comercio comercio)
        {
            if (ModelState.IsValid)
            {
                if (await ComercioDuplicado(comercio.Mail))
                {
                    return View("MensajeError");
                }
                else
                {        
                    if (comercio.FotoComercio == null)
                    {
                        comercio.FotoComercio = _fotoComercioDefault;
                    }

                    if (await VerificarComercioFotoDuplicado(comercio.FotoComercio))
                    {
                        return View("MensajeError");
                    }
                }
            
                _context.Add(comercio);
                await _context.SaveChangesAsync();
                var comercioAux = await _context.Comercios.Where(c => c.Mail == comercio.Mail).FirstOrDefaultAsync();
                return RedirectToAction("ReservasPorComercio", "Reservas", new { id = comercioAux.Id });
                // return RedirectToAction(nameof(Index));
            }
            return View(comercio);
        }

        // GET: Comercio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("MensajeError");
            }

            var comercio = await _context.Comercios.FindAsync(id);
            if (comercio == null)
            {
                return View("MensajeError");
            }
            return View(comercio);
        }

        // POST: Comercio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Mail,Contrasenia,Telefono,Direccion,FotoComercio")] Comercio comercio)
        {
            if (id != comercio.Id)
            {
                return View("MensajeError");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comercio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComercioExists(comercio.Id))
                    {
                        return View("MensajeError");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comercio);
        }

        // GET: Comercio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("MensajeError");
            }

            var comercio = await _context.Comercios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comercio == null)
            {
                return View("MensajeError");
            }

            return View(comercio);
        }

        // POST: Comercio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comercio = await _context.Comercios.FindAsync(id);
            _context.Comercios.Remove(comercio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComercioExists(int id)
        {
            return _context.Comercios.Any(e => e.Id == id);
        }

        private async Task<bool> VerificarComercioFotoDuplicado(string foto)
        {
            var comercio = await _context.Comercios.Where(c => c.FotoComercio == foto).FirstOrDefaultAsync();

            if(comercio == null || comercio.FotoComercio == _fotoComercioDefault)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ComercioDuplicado(string correo)
        {
            var Comercio = await _context.Comercios.Where(c => c.Mail == correo).FirstOrDefaultAsync();

            if (Comercio == null)
            {
                return false;
            }
            return true;
        }

        //Falta agregar el metodo de validacion al iniciar sesion


        public async Task<IActionResult> ListaComercio()
        {
           
            return View(await _context.Comercios.OrderBy(c => c.Nombre).ToListAsync());
        }

       
        public IActionResult InicioSesion()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> IniciarSesion([Bind("Id,Mail,Contrasenia")] Comercio comercio)
        {
            var comercioAux = await _context.Comercios.Where(u => u.Mail == comercio.Mail).FirstOrDefaultAsync();
            DateTime fechaHoy = DateTime.Today;

            if (comercioAux == null)
            {
                return RedirectToAction("MensajeError");
            }
            else
            {
                if (comercioAux.Contrasenia == comercio.Contrasenia)
                {
                    AutoLimpieza(fechaHoy);
                    return RedirectToAction("ReservasPorComercio", "Reservas", new { id = comercioAux.Id });
                }
                else
                {
                    return RedirectToAction("MensajeError");
                }
            }
        }

        public IActionResult MensajeError()
        {
           return View();
        }

        public void AutoLimpieza(DateTime fecha)
        {
            var lista = _context.Reserva.ToList();


            foreach (var item in lista)
            {
                if (item.Fecha < fecha)
                {
                    _context.Reserva.Remove(item);
                    _context.SaveChanges();
                }
            }

        }


    }
}

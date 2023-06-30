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
    public class UsuarioController : Controller
    {
        private readonly EscuelaDatabaseContext _context;

        public UsuarioController(EscuelaDatabaseContext context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Dni,Mail,Contrasenia")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (await UsuarioDuplicado(usuario.Mail))
                {
                    return RedirectToAction("MensajeError", "Home");
                }

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListaReserva));
            }
            return View(ListaReserva(usuario.Id));
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,Dni,Mail,Contrasenia")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }

        public async Task<IActionResult> IniciarSesion(string correo, string contrasenia)
        {
            var usuario = await _context.Usuarios.Where(u => u.Mail == correo).FirstOrDefaultAsync();

            if (usuario == null)
            {
                return RedirectToAction("MensajeError", "Home");
            }
            else
            {
                if (usuario.Contrasenia == contrasenia)
                {

                    return View("Index");
                }
                else
                {
                    return RedirectToAction("MensajeError", "Home");
                }
            }
        }



        private async Task<bool> UsuarioDuplicado(string correo)
        {
            var usuario = await _context.Usuarios.Where(u => u.Mail == correo).FirstOrDefaultAsync();

            if (usuario == null)
            {
                return false;
            }
            return true;
        }

        public IActionResult InicioSesion()
        {
            return View();
        }


        /*  public async Task<IActionResult> MisReservas(Usuario usuario)
          {
              var listaUsuarios = await _context.Usuarios.ToListAsync();
              var listaMisReservas = usuario.ReservasUsuario;

              foreach (Usuario var in listaUsuarios)
              {
                  if(var == usuario)
                  {
                      listaMisReservas.Add(var.)
                  }

              }


              return  
          }*/

        /* public async Task<IActionResult> MisReservas(Usuario usuario)
         {
             var listaDeReservas = await _context.Reserva.ToListAsync();
             var misReservas = usuario.ReservasUsuario;

             foreach (var item in listaDeReservas)
             {
                 if (item.UsuarioId == usuario.Id)
                 {
                     misReservas.Add(item);
                 }
             }
             return View(misReservas);
         }
        */
        /*
         [HttpGet("Usuario/ListaReserva")]
         public async Task<IActionResult> ListaReserva()
         {
             var listaDeReservas = await _context.Reserva.ToListAsync();

             return View(listaDeReservas);
         }
        */

        public ActionResult ListaReserva(int usuarioId)
        {
            // Obtener las reservas del usuario desde la base de datos
            var reservas = _context.Reserva.Where(r => r.UsuarioId == usuarioId).ToList();

            // Realizar cualquier otra lógica o procesamiento necesario

            return View(reservas);
        }

    }
}

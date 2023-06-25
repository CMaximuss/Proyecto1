using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCBasico.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MVCBasico.Context
{
    public class EscuelaDatabaseContext : IdentityDbContext
                                         //sin el identity estaba:  DbContext

    {

        public
                EscuelaDatabaseContext(DbContextOptions<EscuelaDatabaseContext> options): base(options)
        {
        }
        public DbSet<MVCBasico.Models.Usuario> Usuarios { get; set; }
        public DbSet<MVCBasico.Models.Comercio> Comercios { get; set; }
        public DbSet<MVCBasico.Models.Servicio> Servicios { get; set; }
        public DbSet<MVCBasico.Models.Reserva> Reserva { get; set; }
    }



}


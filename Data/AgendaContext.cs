using Entidades;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {

        }

        public virtual DbSet<Contacto> Contactos { get; set; }
        public virtual DbSet<ContactoTelefono> ContactoTelefonos { get; set; }
        public virtual DbSet<ContactoCorreoElectronico> ContactoCorreosElectornico { get; set; }

    }
}

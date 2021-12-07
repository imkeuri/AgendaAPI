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
        public virtual DbSet<ContactoCorreoElectronico> ContactoCorreosElectronico { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Contacto>()
                .HasIndex(c => c.Cedula)
                .IsUnique(true);

        }
    }
}

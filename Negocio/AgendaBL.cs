using Data;
using DTOs;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Negocio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class AgendaBL : IAgendaBL
    {
        private readonly AgendaContext _agendaContext;

        public AgendaBL(AgendaContext agendaContext)
        {
            _agendaContext = agendaContext;
        }
        public async Task CrearContacto(ContactoDTO contacto)
        {
            var transaccion = _agendaContext.Database.BeginTransaction();

            try
            {

                Contacto obj_contacto = new()
                {
                    Nombre = contacto.Nombre,
                    Apellido = contacto.Apellido,
                    Cedula = contacto.Cedula,
                };

                await _agendaContext.Contactos.AddAsync(obj_contacto);

                contacto.CorreoElectronicos.ForEach(async contacto =>
                {
                    contacto.Contacto = obj_contacto;
                    await _agendaContext.ContactoCorreosElectornico.AddAsync(contacto);

                });

                contacto.Telefonos.ForEach(async telefono =>
                {
                    telefono.Contacto = obj_contacto;
                    await _agendaContext.ContactoTelefonos.AddAsync(telefono);

                });

                await _agendaContext.SaveChangesAsync();
                await transaccion.CommitAsync();

            }
            catch (ArgumentNullException)
            {
                await transaccion.RollbackAsync();
                throw new ArgumentNullException("Hay un problema con algunos de los datos digitados");
            }
            catch (Exception)
            {
                await transaccion.RollbackAsync();

                throw new Exception("Hubo un error");
            }

        }

        public async Task<List<ContactoDTO>> GetListaContactos()
        {
            List<ContactoDTO> lst_rows = await (from c in _agendaContext.Contactos
                                                select new ContactoDTO
                                                {
                                                    Nombre = c.Nombre,
                                                    Apellido = c.Apellido,
                                                    Cedula = c.Cedula,
                                                    Telefonos = (_agendaContext.ContactoTelefonos.Where(x => x.Id_Contacto == c.Id).Select(ct => new ContactoTelefono
                                                    { NumeroTelefono = ct.NumeroTelefono })).ToList(),
                                                    CorreoElectronicos = (_agendaContext.ContactoCorreosElectornico.Where(x => x.Id_Contacto == c.Id).Select(ct => new ContactoCorreoElectronico
                                                    { Correo = ct.Correo })).ToList()

                                                }).ToListAsync();

            return lst_rows;
        }
    }
}

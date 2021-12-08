using Data;
using DTOs;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Negocio.Contratos;
using Negocio.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
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
            var transaccion = await _agendaContext.Database.BeginTransactionAsync();

            try
            {

                Contacto obj_contacto = new()
                {
                    Nombre = contacto.Nombre,
                    Apellido = contacto.Apellido,
                    Cedula = contacto.Cedula,
                    Direccion = contacto.Direccion
                };

                await _agendaContext.Contactos.AddAsync(obj_contacto);

                if (contacto.CorreoElectronicos != null)
                {
                    contacto.CorreoElectronicos.ForEach(async correo =>
                    {
                        ContactoCorreoElectronico contactoCorreoElectronico = new();

                        contactoCorreoElectronico.Contacto = obj_contacto;
                        contactoCorreoElectronico.Correo = correo.CorreoElectronico;


                        await _agendaContext.ContactoCorreosElectronico.AddAsync(contactoCorreoElectronico);

                    });
                }

                if (contacto.Telefonos != null)
                {
                    contacto.Telefonos.ForEach(async telefono =>
                    {
                        ContactoTelefono contactoTelefono = new();

                        contactoTelefono.Contacto = obj_contacto;
                        contactoTelefono.NumeroTelefono = telefono.Telefono;


                        await _agendaContext.ContactoTelefonos.AddAsync(contactoTelefono);

                    });
                }
                await _agendaContext.SaveChangesAsync();
                await transaccion.CommitAsync();

            }
            catch (ConstraintException)
            {
                await transaccion.RollbackAsync();
                throw new ServiceException("No puede insertar duplicados");

            }
            catch (ArgumentNullException)
            {
                await transaccion.RollbackAsync();
                throw new ServiceException("Hubo un problema con algunos de los campos");
            }
            catch (Exception)
            {
                await transaccion.RollbackAsync();
                throw new ServiceException("Hubo un  problema al procesar la solicitud");
            }

        }

        public async Task ActualizarContacto(ContactoDTO contacto)
        {
            var transaccion = await _agendaContext.Database.BeginTransactionAsync();

            try
            {
                var obj_contacto = await _agendaContext.Contactos.Where(c => c.Id == contacto.Id).FirstOrDefaultAsync();

                if (obj_contacto == null)
                    throw new UnvalidArgumentException("Algunos de los telefono no existen");



                obj_contacto.Cedula = contacto.Cedula;
                obj_contacto.Nombre = contacto.Nombre;
                obj_contacto.Apellido = contacto.Apellido;
                obj_contacto.Direccion = contacto.Direccion;



                _agendaContext.Contactos.Update(obj_contacto);

                if (contacto.CorreoElectronicos != null)
                {

                    contacto.CorreoElectronicos.ForEach(correo =>
                  {
                      ContactoCorreoElectronico contactoCorreo = _agendaContext.ContactoCorreosElectronico.
                      Where(c => c.Id == correo.Id).FirstOrDefault();

                      if (contactoCorreo == null)
                          throw new UnvalidArgumentException("Algunos de los correos no existen");

                      contactoCorreo.Correo = correo.CorreoElectronico;

                      _agendaContext.ContactoCorreosElectronico.Update(contactoCorreo);


                  });

                }

                if (contacto.Telefonos != null)
                {
                    contacto.Telefonos.ForEach(telefono =>
                   {
                       ContactoTelefono contactoTelefono = _agendaContext.ContactoTelefonos.
                      Where(c => c.Id == telefono.Id).FirstOrDefault();

                       if (contactoTelefono == null)
                           throw new UnvalidArgumentException("Algunos de los correos no existen");

                       contactoTelefono.NumeroTelefono = telefono.Telefono;

                       _agendaContext.ContactoTelefonos.Update(contactoTelefono);


                   });


                }
                await _agendaContext.SaveChangesAsync();
                await transaccion.CommitAsync();

            }
            catch (UnvalidArgumentException)
            {
                await transaccion.RollbackAsync();
                throw new ServiceException("Algunos de los datos ingresados no existen, no hubo cambios");
            }
            catch (ConstraintException)
            {
                await transaccion.RollbackAsync();
                throw new ServiceException("No puede insertar duplicados, no hubo cambios");

            }
            catch (ArgumentNullException)
            {
                await transaccion.RollbackAsync();
                throw new ServiceException("Hubo un problema con algunos de los campos, no hubo cambios");
            }
            catch (Exception)
            {
                await transaccion.RollbackAsync();
                throw new ServiceException("Hubo un  problema al procesar la solicitud, no hubo cambios");
            }
        }


        public async Task<List<ContactoDTO>> GetListaContactos()
        {
            try
            {

                List<ContactoDTO> lst_rows = await (from c in _agendaContext.Contactos
                                                    select new ContactoDTO
                                                    {
                                                        Id = c.Id,
                                                        Nombre = c.Nombre,
                                                        Apellido = c.Apellido,
                                                        Cedula = c.Cedula,
                                                        Direccion = c.Direccion,
                                                        Telefonos = (_agendaContext.ContactoTelefonos.Where(x => x.Id_Contacto == c.Id).Select(ct => new ContactoTelefonoDTO
                                                        { Telefono = ct.NumeroTelefono })).ToList(),
                                                        CorreoElectronicos = (_agendaContext.ContactoCorreosElectronico.Where(x => x.Id_Contacto == c.Id).Select(ct => new ContactoCorreoElectronicoDTO
                                                        { CorreoElectronico = ct.Correo })).ToList()

                                                    }).ToListAsync();

                return lst_rows;
            }
            catch (Exception)
            {

                throw new ServiceException("Al parecer el servicio no se encuentra disponible");
            }
        }

        public async Task<ContactoDTO> GetContacto(int id)
        {
            try
            {
                ContactoDTO obj_contacto = await (from c in _agendaContext.Contactos
                                                  select new ContactoDTO
                                                  {
                                                      Id = c.Id,
                                                      Nombre = c.Nombre,
                                                      Apellido = c.Apellido,
                                                      Cedula = c.Cedula,
                                                      Direccion = c.Direccion,
                                                      Telefonos = (_agendaContext.ContactoTelefonos.Where(x => x.Id_Contacto == c.Id).Select(ct => new ContactoTelefonoDTO
                                                      {
                                                          Id = ct.Id,
                                                          Telefono = ct.NumeroTelefono
                                                      })).ToList(),
                                                      CorreoElectronicos = (_agendaContext.ContactoCorreosElectronico.Where(x => x.Id_Contacto == c.Id).Select(ct => new ContactoCorreoElectronicoDTO
                                                      {
                                                          Id = ct.Id,
                                                          CorreoElectronico = ct.Correo
                                                      })).ToList()

                                                  }).Where(c => c.Id == id).FirstOrDefaultAsync();


                return obj_contacto;
            }
            catch (Exception)
            {

                throw new ServiceException("Al parecer el servicio no se encuentra disponible");
            }
        }

        public async Task DeleteContacto(int id)
        {
            var transaccion = await _agendaContext.Database.BeginTransactionAsync();

            Contacto obj_contacto = await (_agendaContext.Contactos.Where(x => x.Id == id).FirstOrDefaultAsync());

            try
            {
                if (obj_contacto == null)
                    throw new UnvalidArgumentException("No es posible eliminar un contacto no existente");


                await _agendaContext.ContactoTelefonos.Where(ct => ct.Id_Contacto == id).ForEachAsync(ct =>
                {
                    _agendaContext.ContactoTelefonos.Remove(ct);

                });

                await _agendaContext.ContactoCorreosElectronico.Where(cc => cc.Id_Contacto == id).ForEachAsync(cc =>
                {
                    _agendaContext.ContactoCorreosElectronico.Remove(cc);

                });

                _agendaContext.Contactos.Remove(obj_contacto);

                await _agendaContext.SaveChangesAsync();
                await transaccion.CommitAsync();


            }
            catch (UnvalidArgumentException ex)
            {
                await transaccion.RollbackAsync();
                throw new ServiceException(ex.Message);
            }
            catch (Exception)
            {
                await transaccion.RollbackAsync();
                throw new ServiceException("No fue posible procesar la solicitud");
            }

        }
    }
}

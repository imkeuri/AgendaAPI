using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Contratos;
using Negocio.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly IAgendaBL _agendaBl;


        public ContactosController(IAgendaBL agendaBl)
        {
            _agendaBl = agendaBl;
        }

        [HttpGet("GetListContactos")]
        public async Task<ActionResult<List<ContactoDTO>>> GetContactos()
        {
            try
            {
                var lst_contactos = await _agendaBl.GetListaContactos();
                return Ok(new { Status = 1, contactos = lst_contactos });
            }
            catch (ServiceException ex)
            {
                return Ok(new { Status = -1, contactos = new List<ContactoDTO>(), ex.Message }); ;
            }

        }


        [HttpGet("GetContacto/{id}")]
        public async Task<ActionResult<ContactoDTO>> GetContactos(int id)
        {
            try
            {
                var contacto = await _agendaBl.GetContacto(id);
                return Ok(new { Status = 1, contacto = contacto });
            }
            catch (ServiceException ex)
            {
                return Ok(new { Status = -1, contactos = new ContactoDTO(), ex.Message }); ;
            }

        }

        [HttpPost("CreateContacto")]
        public async Task<ActionResult> PostContacto([FromBody] ContactoDTO contacto)
        {
            try
            {
                await _agendaBl.CrearContacto(contacto);

                return Ok(new { Status = 1, Message = "Contacto creado correctamente" });
            }
            catch (ServiceException ex)
            {
                return Ok(new { Status = -1, ex.Message }); ;
            }
        }


        [HttpPut("UpdateContacto")]
        public async Task<IActionResult> PutContacto([FromBody] ContactoDTO contacto, int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != contacto.Id)
                return BadRequest();

            try
            {
                await _agendaBl.ActualizarContacto(contacto);
                return Ok(new { Status = 1, Message = "Contacto actualizado correctamente" });
            }
            catch (ServiceException ex)
            {
                return Ok(new { Status = -1, ex.Message }); ;
            }
        }

        [HttpDelete("DeleteContacto")]
        public async Task<IActionResult> DeleteContacto(int id)
        {
            try
            {
                await _agendaBl.DeleteContacto(id);
                return Ok(new { Status = 1, Message = "Contacto eliminado correctamente" });
            }
            catch (ServiceException ex)
            {
                return Ok(new { Status = -1, ex.Message }); ;
            }

        }




    }
}

using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Negocio.Contratos
{
    public interface IAgendaBL
    {
        Task<List<ContactoDTO>> GetListaContactos();

        Task<ContactoDTO> GetContacto(int id);

        Task CrearContacto(ContactoDTO contacto);

        Task ActualizarContacto(ContactoDTO contacto);

        Task DeleteContacto(int id);

    }
}

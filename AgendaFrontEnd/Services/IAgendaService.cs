using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaFrontEnd.Services
{
    public interface IAgendaService<T>
    {

        Task<List<T>> GetAllAsync(string requestUri);
        Task<T> GetByIdAsync(string requestUri, int Id);
        Task<T> PostAsync(string requestUri, T obj);
        Task<T> PutAsync(string requestUri, int Id, T obj);
        Task<T> DeleteAsync(string requestUri, int Id);
        Task<T> GetAsync(string requestUri);

    }
}

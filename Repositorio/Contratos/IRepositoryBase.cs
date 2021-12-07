using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Contratos
{
    public interface IRepositoryBase<T>
    {

        public Task Create(T entity);

        public Task Delete(T entity);

        public Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expresion);

        public Task<T> FindById(int id);

        public Task<IQueryable<T>> GetAll();

        public Task Update(T entity);


    }
}

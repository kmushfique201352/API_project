using System.Collections.Generic;
using System.Linq.Expressions;

namespace API_project.Repositories
{
    public interface IGenericRepository
    {
        public interface IGenericRepository<T> where T : class
        {
            IEnumerable<T> GetAll();
            T GetById(int id);
            void Add(T entity);
            void Update(T entity);
            void Delete(int id);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPerson.DataAccess.Read.Repositories
{
    public interface IReadRepository<T> where T : class
    {
        IQueryable<T> GetAllQuery();
        Task<List<T>> GetAll();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using MvcPerson.DataAccess.Model;

namespace MvcPerson.DataAccess.Write.Repositories
{
    public interface IWriteRepository<T> where T : class
    {
        Task Create(T person);
        Task CreateBulk(IEnumerable<T> itemCollection);
    }
}

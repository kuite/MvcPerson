using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcPerson.DataAccess.Model;
using MvcPerson.DTO;

namespace MvcPerson.DataAccess.Read.Facade
{
    public interface IReadeFacade
    {
        Task<IEnumerable<PersonDetails>> GetAllPeople();
        Task<bool> Exist(PersonDetails details);
    }
}

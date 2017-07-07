using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcPerson.DTO;

namespace MvcPerson.DataAccess.Write.Facade
{
    public interface IWriteFacade
    {
        Task CreatePerson(PersonDetails details);
        Task CreatePersonBulk(IEnumerable<PersonDetails> details);
    }
}

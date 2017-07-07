using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MvcPerson.DataAccess.Model;

namespace MvcPerson.DataAccess.Read.Repositories
{
    public class DbReadsRepository : IReadRepository<Person>
    {
        private readonly PersonContext context;

        public DbReadsRepository(PersonContext context)
        {
            this.context = context;
        }

        public IQueryable<Person> GetAllQuery()
        {
            return context.Person;
        }

        public async Task<List<Person>> GetAll()
        {
            return await GetAllQuery().ToListAsync();
        }
    }
}

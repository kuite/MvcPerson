using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MvcPerson.DataAccess.Model;

namespace MvcPerson.DataAccess.Write.Repositories
{
    public class DbWriteRepository : IWriteRepository<Person>
    {
        private readonly PersonContext context;

        public DbWriteRepository(PersonContext context)
        {
            this.context = context;
        }

        public async Task Create(Person person)
        {
            context.Person.Add(person);
            await context.SaveChangesAsync();
        }

        public async Task CreateBulk(IEnumerable<Person> itemCollection)
        {
            context.Configuration.AutoDetectChangesEnabled = false;
            foreach (Person person in itemCollection)
            {
                context.Person.Add(person);
            }
            context.Configuration.AutoDetectChangesEnabled = true;
            await context.SaveChangesAsync();
        }
    }
}

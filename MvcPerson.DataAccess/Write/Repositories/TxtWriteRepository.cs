using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MvcPerson.DataAccess.Model;
using NLog;

namespace MvcPerson.DataAccess.Write.Repositories
{
    public class TxtWriteRepository : IWriteRepository<Person>
    {
        private readonly Logger logger;

        public TxtWriteRepository(Logger logger)
        {
            this.logger = logger;
        }

        public async Task Create(Person item)
        {
            await Task.Run(() =>
            {
                LogPerson(item);
            });
        }

        public async Task CreateBulk(IEnumerable<Person> itemCollection)
        {
            await Task.Run(() =>
            {
                foreach (Person person in itemCollection)
                {
                    LogPerson(person);
                }
            });
        }

        private void LogPerson(Person person)
        {
            logger.Info($"Person{{ Id: {person.Id}, Firstname: {person.Firstname}, Lastname: {person.Lastname} }}");
        }
    }
}

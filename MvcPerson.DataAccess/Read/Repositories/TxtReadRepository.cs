using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcPerson.DataAccess.Model;
using NLog;

namespace MvcPerson.DataAccess.Read.Repositories
{
    public class TxtReadRepository : IReadRepository<Person>
    {
        private readonly string logPath;

        public TxtReadRepository(string logPath)
        {
            this.logPath = logPath;
        }

        public IQueryable<Person> GetAllQuery()
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> GetAll()
        {
            return Task.Run(() =>
            {
                List<Person> ppl = new List<Person>();
                if (!File.Exists(logPath))
                {
                    return ppl;
                }

                var json = File.ReadAllText(logPath);

                return ppl;
            });
        }
    }
}

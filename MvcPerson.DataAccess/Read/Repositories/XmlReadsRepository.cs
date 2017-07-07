using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MvcPerson.DataAccess.Model;

namespace MvcPerson.DataAccess.Read.Repositories
{
    public class XmlReadsRepository : IReadRepository<Person>
    {
        private readonly string xmlPath;

        public XmlReadsRepository(string xmlPath)
        {
            this.xmlPath = xmlPath;
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
                if (!File.Exists(xmlPath))
                {
                    return ppl;
                }
                XDocument xDocument = XDocument.Load(xmlPath);
                XElement root = xDocument.Element("Poeple");
                IEnumerable<XElement> rows = root.Descendants("Person");
                foreach (XElement row in rows)
                {
                    ppl.Add(new Person
                    {
                        Firstname = row.Element("Firstname").Value,
                        Lastname = row.Element("Lastname").Value,
                    });
                }

                return ppl;
            });
        }
    }
}

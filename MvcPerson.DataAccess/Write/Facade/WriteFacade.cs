using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using MvcPerson.DataAccess.Model;
using MvcPerson.DataAccess.Write.Repositories;
using MvcPerson.DTO;

namespace MvcPerson.DataAccess.Write.Facade
{
    public class WriteFacade : IWriteFacade
    {
        private readonly IMapper mapper;
        private readonly IWriteRepository<Person> dbWrite;
        private readonly IWriteRepository<Person> txtWrite;
        private readonly IWriteRepository<Person> xmlWrite;

        public WriteFacade(
            IMapper mapper,
            IWriteRepository<Person> dbWrite,
            IWriteRepository<Person> txtWrite,
            IWriteRepository<Person> xmlWrite)
        {
            this.mapper = mapper;
            this.dbWrite = dbWrite;
            this.txtWrite = txtWrite;
            this.xmlWrite = xmlWrite;
        }

        public async Task CreatePerson(PersonDetails details)
        {
            var person = new Person{ Id = Guid.NewGuid() };
            mapper.Map(details, person);
            Task t1 = null;
            Task t2 = null;
            Task t3 = null;
            try
            {
                t1 = dbWrite.Create(person);
                t2 = txtWrite.Create(person);
                t3 = xmlWrite.Create(person);
            }
            catch (Exception ex)
            {
                if (ex is XmlException || 
                    ex is EntityException)
                {
                    //log ex
                    throw;
                }

            }
            finally
            {
                await Task.WhenAll(t1, t2, t3);
            }
        }

        public async Task CreatePersonBulk(IEnumerable<PersonDetails> details)
        {
            List<Person> ppl = mapper.Map<IEnumerable<PersonDetails>, List<Person>>(details);

            Task t1 = null;
            Task t2 = null;
            Task t3 = null;
            try
            {
                t1 = dbWrite.CreateBulk(ppl);
                t2 = txtWrite.CreateBulk(ppl);
                t3 = xmlWrite.CreateBulk(ppl);
            }
            catch (Exception ex)
            {
                if (ex is XmlException ||
                    ex is EntityException)
                {
                    //log ex
                    throw;
                }
                //one of data storage is corrupted
                throw;
            }
            finally
            {
                await Task.WhenAll(t1, t2, t3);
            }
        }
    }
}

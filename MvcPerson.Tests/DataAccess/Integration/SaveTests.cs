using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MvcPerson.DataAccess.Model;
using MvcPerson.DataAccess.Read.Repositories;
using MvcPerson.DataAccess.Write.Repositories;
using MvcPerson.Tests.Utilis;
using NUnit.Framework;

namespace MvcPerson.Tests.DataAccess.Integration
{
    public class SaveTests
    {
        private IWriteRepository<Person> xmlWriter;
        private IWriteRepository<Person> dbWriter;

        private IReadRepository<Person> xmlReader;
        private IReadRepository<Person> dbReader;

        [OneTimeSetUp]
        public void Setup()
        {
            xmlWriter = new XmlWriteRepository(ConfigurationManager.AppSettings["xmlPathTest"]);
            xmlReader = new XmlReadsRepository(ConfigurationManager.AppSettings["xmlPathTest"]);

            PersonContext ctx = new PersonContext();
            dbReader = new DbReadsRepository(ctx);
            dbWriter = new DbWriteRepository(ctx);
        }

        [Test]
        public async Task CreatePersonXmlTest()
        {
            var fn = "Ivan";
            var ln = "Saric";
            var person = new Person { Firstname = fn, Lastname = ln };
            await xmlWriter.Create(person);
            List<Person> ppl = xmlReader.GetAll().Result;

            var saved = ppl.FirstOrDefault();
            Assert.True(saved != null);
            Assert.True(saved.Firstname == fn && saved.Lastname == ln);
        }

        [Test]
        public async Task CreatePersonDbTest()
        {
            var fn = "Ivan";
            var ln = "Saric";
            var id = Guid.NewGuid();
            var person = new Person { Id = id, Firstname = fn, Lastname = ln };
            await dbWriter.Create(person);
            Person saved = dbReader.GetAllQuery().FirstOrDefault(p => 
                p.Id == id);

            Assert.True(saved != null);
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            File.Delete(ConfigurationManager.AppSettings["xmlPathTest"]);
            Database.Delete(ConfigurationManager.ConnectionStrings["PersonDataBase"].ConnectionString);
        }
    }
}

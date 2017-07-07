using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using MvcPerson.DataAccess.Model;
using MvcPerson.DataAccess.Read.Facade;
using MvcPerson.DataAccess.Read.Repositories;
using MvcPerson.DTO;
using MvcPerson.Tests.Utilis;
using NUnit.Framework;

namespace MvcPerson.Tests.DataAccess
{
    public class ReadFacadeTests
    {
        private ReadFacade reader;
        private List<Person> people;

        [OneTimeSetUp]
        public void Setup()
        {
            people = new List<Person>
            {
                new Person {Firstname = "Michail", Lastname = "Bialkov"},
                new Person {Firstname = "Eric", Lastname = "Hansen"}
            };
            var ppl = people.AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IDbAsyncEnumerable<Person>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Person>(ppl.GetEnumerator()));

            mockSet.As<IQueryable<Person>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Person>(ppl.Provider));

            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(ppl.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(ppl.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(ppl.GetEnumerator());

            IMapper mapper = Helpers.GetMapperConfig();
            Mock<IReadRepository<Person>> dbRepository = new Mock<IReadRepository<Person>>();
            dbRepository.Setup(x => x.GetAllQuery()).Returns(mockSet.Object);

            reader = new ReadFacade(mapper, dbRepository.Object);
        }

        [Test]
        public void ExistTest()
        {
            var person = new PersonDetails { Firstname = "Eric", Lastname = "Hansen" };
            
            Assert.True(reader.Exist(person).Result);
        }

        [OneTimeTearDown]
        public void Dispose()
        {

        }
    }
}

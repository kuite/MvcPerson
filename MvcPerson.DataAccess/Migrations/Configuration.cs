using MvcPerson.DataAccess.Model;

namespace MvcPerson.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PersonContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PersonContext context)
        {
            context.Person.Add(new Person
            {
                Id = Guid.NewGuid(),
                Firstname = "Jan",
                Lastname = "Kowalski"
            });

            context.Person.Add(new Person
            {
                Id = Guid.NewGuid(),
                Firstname = "John",
                Lastname = "Snow"
            });
        }
    }
}

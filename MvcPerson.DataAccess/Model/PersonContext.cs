using System.Data.Entity;

namespace MvcPerson.DataAccess.Model
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> Person { get; set; }

        public PersonContext() : base("name=PersonDataBase")
        {
            Database.Initialize(true);
        }
    }
}

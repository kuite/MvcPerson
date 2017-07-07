using System;

namespace MvcPerson.DataAccess.Model
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}

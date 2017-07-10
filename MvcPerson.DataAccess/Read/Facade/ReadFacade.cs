using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MvcPerson.DataAccess.Model;
using MvcPerson.DataAccess.Read.Repositories;
using MvcPerson.DTO;

namespace MvcPerson.DataAccess.Read.Facade
{
    public class ReadFacade : IReadeFacade
    {
        private readonly IMapper mapper;
        private readonly IReadRepository<Person> dbRepo;

        public ReadFacade(
            IMapper mapper,
            IReadRepository<Person> dbRepo)
        {
            this.mapper = mapper;
            this.dbRepo = dbRepo;
        }

        public async Task<bool> Exist(PersonDetails details)
        {
            bool existingDetails = false;
            if (dbRepo != null)
            {
                existingDetails = await dbRepo.GetAllQuery().Where(d =>
                    d.Lastname.Equals(details.Lastname, StringComparison.InvariantCultureIgnoreCase) &&
                    d.Firstname.Equals(details.Firstname, StringComparison.InvariantCultureIgnoreCase)).AnyAsync();

            }
            return existingDetails;
        }

        public async Task<IEnumerable<PersonDetails>> GetAllPeople()
        {
            IEnumerable<PersonDetails> detailsList = new List<PersonDetails>();
            if (dbRepo != null && mapper != null)
            {
                List<Person> ppl = await dbRepo.GetAll();
                detailsList = mapper.Map<List<Person>, List<PersonDetails>>(ppl);
            }
            

            return detailsList;
        }
    }
}

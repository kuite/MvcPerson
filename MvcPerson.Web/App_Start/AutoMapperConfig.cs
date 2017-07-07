using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MvcPerson.DataAccess.Model;
using MvcPerson.DTO;

namespace MvcPerson
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PersonDetails, Person>();
                cfg.CreateMap<Person, PersonDetails>();
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }

    }
}
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using MvcPerson.DataAccess.Write.Facade;
using MvcPerson.DataAccess.Write.Repositories;
using Unity.Mvc5;
using AutoMapper;
using MvcPerson.DataAccess.Model;
using MvcPerson.DataAccess.Read.Facade;
using MvcPerson.DataAccess.Read.Repositories;
using System.Configuration;
using NLog;

namespace MvcPerson
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            IMapper mapper = AutoMapperConfig.GetMapperConfig();

            var container = new UnityContainer();
            container.RegisterType<IReadRepository<Person>, DbReadsRepository>();
            container.RegisterType<IWriteRepository<Person>, DbWriteRepository>("db");
            container.RegisterType<IWriteRepository<Person>, TxtWriteRepository>
                ("txt", new InjectionConstructor(LogManager.GetCurrentClassLogger()));
            container.RegisterType<IWriteRepository<Person>, XmlWriteRepository>
                ("xml", new InjectionConstructor(ConfigurationManager.AppSettings["xmlPath"]));
            container.RegisterInstance<IMapper>(mapper);

            container.RegisterType<IWriteFacade, WriteFacade>(
                new InjectionConstructor(
                 new ResolvedParameter<IMapper>(),
                 new ResolvedParameter<IWriteRepository<Person>>("db"), 
                 new ResolvedParameter<IWriteRepository<Person>>("txt"),
                 new ResolvedParameter<IWriteRepository<Person>>("xml")
                 ));

            container.RegisterType<IReadeFacade, ReadFacade>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
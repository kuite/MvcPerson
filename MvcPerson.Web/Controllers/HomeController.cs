using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MvcPerson.DataAccess.Read.Facade;
using MvcPerson.DTO;

namespace MvcPerson.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReadeFacade reader;

        public HomeController(IReadeFacade reader)
        {
            this.reader = reader;
        }

        public async Task<ActionResult> Index()
        {
            return View(await reader.GetAllPeople());
        }
    }
}
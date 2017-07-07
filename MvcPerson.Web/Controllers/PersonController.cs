using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcPerson.DataAccess.Read.Facade;
using MvcPerson.DataAccess.Write.Facade;
using MvcPerson.DTO;

namespace MvcPerson.Controllers
{
    public class PersonController : Controller
    {
        private readonly IWriteFacade writer;
        private readonly IReadeFacade reader;

        public PersonController(IWriteFacade writer, IReadeFacade reader)
        {
            this.writer = writer;
            this.reader = reader;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPerson(PersonDetails person)
        {
            var exist = await reader.Exist(person);

            if (exist)
            {
                return View("Error", null ,"Person already exist");
            }

            await writer.CreatePerson(person);
            return View("../Home/Index", await reader.GetAllPeople());
        }

        public ActionResult AddPerson()
        {
            return View();
        }

    }
}
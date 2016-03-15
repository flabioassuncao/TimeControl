using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using TimeControl.Models;

namespace TimeControl.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        // GET: /<controller>/
        [Route("Person/Index")]
        public IActionResult Index()
        {
            var persons = new PersonDatabase();
            return View(persons);
        }
    }
}
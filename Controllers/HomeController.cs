using Microsoft.AspNet.Mvc;
 
namespace TimeControl.Controllers
{
    public class HomeController : Controller
    {
        
        [HttpGet]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
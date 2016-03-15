using Microsoft.AspNet.Mvc;
 
namespace TimeControl.Controllers
{
    public class InitialController : Controller
    {
        [HttpGet]
        [Route("initial/timer")]
        public IActionResult timer()
        {
            return View();
        }
        
        [HttpGet]
        [Route("initial/projects")]
        public IActionResult projects()
        {
            return View();   
        }
    }
}
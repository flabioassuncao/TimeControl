using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace TimeControl.Controllers
{
    [Authorize]
    public class InitialController : Controller
    {
        [HttpGet]
        [Route("initial/timer")]
        public IActionResult timer()
        {
            if(User.Identity.IsAuthenticated)
            {
                ViewBag.Nome = User.Identity.Name;
                return View();
            }
            return View();
        }
        
        [HttpGet]
        [Route("initial/projects")]
        public IActionResult projects()
        {
            if(User.Identity.IsAuthenticated)
            {
                ViewBag.Nome = User.Identity.Name;
                return View();
            }
            return View();  
        }
    }
}
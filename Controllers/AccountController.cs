using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using TimeControl.Models;

namespace TimeControl.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _securityManager;
        private readonly SignInManager<ApplicationUser> _loginManager;
        //2
        public AccountController(UserManager<ApplicationUser> secMgr, SignInManager<ApplicationUser> loginManager)
        {
            _securityManager = secMgr;
            _loginManager = loginManager;
        }
        
        //3
        [HttpGet]
        [AllowAnonymous]
        [Route("Account/Register")]
        public IActionResult Register()
        {
            return View();
        }
 
        //4
  
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Account/Register")]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await _securityManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _loginManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
             
            return View(model);
        }
 
        //5
        [HttpGet]
        [AllowAnonymous]
        [Route("Account/Login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
 
        //6
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("Account/Login")]
        public async Task<IActionResult> Login(Login model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _loginManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return new HttpOkObjectResult(returnUrl);
                }
            }
 
              
            return View(model);
        }
 
        //7
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Account/LogOff")]
        public async Task<IActionResult> LogOff()
        {
            await _loginManager.SignOutAsync();
             
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        
        //8
        [HttpGet]
        [AllowAnonymous]
        [Route("Account/RedirectToReturnUrl")]
        private IActionResult RedirectToReturnUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
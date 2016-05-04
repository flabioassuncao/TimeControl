using System;
using System.Security.Claims;
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
        
        public AccountController(UserManager<ApplicationUser> secMgr, SignInManager<ApplicationUser> loginManager)
        {
            _securityManager = secMgr;
            _loginManager = loginManager;
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("Account/Register")]
        public async Task<IActionResult> Register([FromBodyAttribute]Register model)
        { 
           if (ModelState.IsValid)
           {
                try
                {
                    if(model.Password == model.ConfirmPassword){
                        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                        var result = await _securityManager.CreateAsync(user, model.Password);
                        if (!result.Succeeded)
                            throw new Exception(result.ToString());          
                    }else
                        return HttpBadRequest("Different passwords");
                    
                    return Ok();                    
                }
                catch (System.Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);                   
                }
           }
            return HttpBadRequest("Password must contain numbers, special characters, uppercase and lowercase");
        }
 
        [HttpPost]
        [AllowAnonymous]
        [Route("Account/Login")]
        public async Task<IActionResult> Login([FromBodyAttribute]Login model, [FromQueryAttribute]string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _loginManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                    if (!result.Succeeded)
                        throw new Exception("Could not make the Sign. Verify that the information provided is correct.");          
                    
                    return new HttpOkObjectResult(returnUrl);          
                }
                catch (System.Exception e)
                {
                    ModelState.AddModelError("Error", e.Message);                   
                }
            }
            return HttpBadRequest(ModelState);
        }
        
        [HttpPost]
        [Route("Account/LogOff")]
        public async Task<IActionResult> LogOff()
        {
            try
            {
                await _loginManager.SignOutAsync();    
                return new HttpOkObjectResult(string.Empty);
            }
            catch (System.Exception)
            {                
               return HttpBadRequest();                
            } 
        }
    }
}
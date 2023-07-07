using LoginForm.Models;
using LoginForm.Services;
using Microsoft.AspNetCore.Mvc;

namespace LoginForm.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProcessLogin(UserModel user)
        {
            SecurityService securityService = new SecurityService();

            if (securityService.IsValid(user))
            return View("LoginSuccess", user);
            else
            return View("LoginFailure", user);
            //return View();
        }

    }
}

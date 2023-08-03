using CST_350_Milestone.Models;
using CST_350_Milestone.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CST_350_Milestone.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ProcessLogin(UserModel user)
		{
			var securityService = new SecurityService();

			if (securityService.IsValid(user))
			{
				int userId = securityService.GetUserIdByUsername(user.Username);
				if (userId != 0) // if user found
				{
					HttpContext.Session.SetInt32("UserId", userId); // Store the user's ID in a session variable
				}

				return RedirectToAction("MinesweeperBoard", "Minesweeper");
			}

			return View("LoginFailure", user);
		}
	}
}
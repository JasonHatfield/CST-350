using CST_350_Milestone.Models;
using CST_350_Milestone.Services;
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
				return RedirectToAction("MinesweeperBoard", "Minesweeper");

			return View("LoginFailure", user);
		}
	}
}
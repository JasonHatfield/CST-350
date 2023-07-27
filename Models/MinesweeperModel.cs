using Microsoft.AspNetCore.Mvc;

namespace CST_350_Milestone.Models
{
    public class MinesweeperModel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

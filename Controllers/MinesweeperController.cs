using Microsoft.AspNetCore.Mvc;

namespace CST_350_Milestone.Controllers
{
    [Route("Minesweeper")]
    public class MinesweeperController : Controller
    {
        [Route("MinesweeperBoard")]
        public IActionResult MinesweeperBoard()
        {
            return View();
        }
    }
}
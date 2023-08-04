using CST_350_Milestone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CST_350_Milestone.Services;

namespace CST_350_Milestone.Controllers
{
    [ApiController]
    [Route("api")]
    public class MinesweeperApiController : ControllerBase
    {
        private readonly SavedGamesDAO _savedGamesDAO;

        public MinesweeperApiController(SavedGamesDAO savedGamesDAO)
        {
            _savedGamesDAO = savedGamesDAO;
        }

        [HttpGet]
        [Route("showAllSavedGames")] // Updated route
        public IActionResult ShowAllSavedGames()
        {
            var savedGames = _savedGamesDAO.GetAllSavedGames();
            return Ok(savedGames);
        }

        [HttpGet]
        [Route("showSavedGames/{gameId}")]
        public IActionResult ShowSavedGame(int gameId)
        {
            var savedGame = _savedGamesDAO.GetSavedGameById(gameId);
            if (savedGame == null)
            {
                return NotFound();
            }
            return Ok(savedGame);
        }

        [HttpDelete]
        [Route("deleteOneGame/{gameId}")]
        public IActionResult DeleteOneGame(int gameId)
        {
            var deletedGame = _savedGamesDAO.DeleteGameById(gameId);
            if (deletedGame == null)
            {
                return NotFound();
            }
            return Ok(deletedGame);
        }
    }
}

using CST_350_Milestone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
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
        [Route("showSavedGames")]
        public IActionResult ShowSavedGames()
        {
            var savedGames = _savedGamesDAO.GetSavedGamesByUserId(HttpContext.Session.GetInt32("UserId") ?? -1);
            return Ok(savedGames);
        }

        [HttpGet]
        [Route("showSavedGames/{gameId}")]
        public IActionResult ShowSavedGame(int gameId)
        {
            var savedGame = _savedGamesDAO.GetSavedGamesByUserId(gameId);
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
            var deletedGame = _savedGamesDAO.GetSavedGamesByUserId(gameId);
            if (deletedGame == null)
            {
                return NotFound();
            }
            return Ok(deletedGame);
        }
    }
}


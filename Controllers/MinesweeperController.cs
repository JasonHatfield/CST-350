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
    [Route("Minesweeper")]
    public class MinesweeperController : Controller
    {
        private readonly string _connectionString;
        private readonly SavedGamesDAO _savedGamesDAO;

        public MinesweeperController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [Route("MinesweeperBoard")]
        public IActionResult MinesweeperBoard()
        {
            ViewData["UserId"] = HttpContext.Session.GetInt32("UserId");
            return View();
        }

        [HttpPost]
        [Route("SaveGame")]
        public IActionResult SaveGame(string userId, string timestamp, string gameState)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO SavedGames (UserId, Timestamp, GameState) VALUES (@UserId, @Timestamp, @GameState)";

                using var command = new SqlCommand(query, connection);
                Debug.WriteLine("userId in SaveGame method: " + userId);

                // Set parameter values
                command.Parameters.AddWithValue("@UserId", int.Parse(userId));
                command.Parameters.AddWithValue("@Timestamp", DateTime.Parse(timestamp));
                command.Parameters.AddWithValue("@GameState", gameState);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return Ok();
        }

        [Route("SavedGames")]
        public IActionResult SavedGames()
        {
            List<SavedGameModel> savedGames = new List<SavedGameModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM SavedGames WHERE UserId = @UserId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", HttpContext.Session.GetInt32("UserId"));

                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int gameId = reader.GetInt32(0);
                        int userId = reader.GetInt32(1);
                        string timestamp = reader.GetDateTime(2).ToString();
                        string gameState = reader.GetString(3);

                        SavedGameModel savedGame = new SavedGameModel(gameId, userId, timestamp, gameState);
                        savedGames.Add(savedGame);
                    }
                }
            }
            return View(savedGames);
        }

        [Route("LoadGame")]
        public IActionResult LoadGame(int gameId)
        {
            SavedGameModel savedGame;

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM SavedGames WHERE Id = @GameId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GameId", gameId);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int userId = reader.GetInt32(1);
                        string timestamp = reader.GetDateTime(2).ToString();
                        string gameState = reader.GetString(3);

                        savedGame = new SavedGameModel(gameId, userId, timestamp, gameState);
                        ViewBag.UserId = userId; // Pass UserId to the view using ViewBag
                    }
                    else
                    {
                        // Handle case when the game is not found
                        return NotFound();
                    }
                }
            }

            // Pass the saved game state as JSON string to the view to render the game board
            return View("MinesweeperBoard", savedGame);
        }

        [HttpPost]
        [Route("DeleteGame")]
        public IActionResult DeleteGame(int gameId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "DELETE FROM SavedGames WHERE Id = @GameId";

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@GameId", gameId);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    TempData["DeleteSuccessMessage"] = "Game deleted successfully.";
                }
                else
                {
                    TempData["DeleteErrorMessage"] = "Game not found or already deleted.";
                }
            }

            return RedirectToAction("SavedGames");
        }
    }
}
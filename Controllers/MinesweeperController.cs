using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace CST_350_Milestone.Controllers
{
    [Route("Minesweeper")]
    public class MinesweeperController : Controller
    {
		private readonly string _connectionString;

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

				using (var command = new SqlCommand(query, connection))
				{
					Debug.WriteLine("userId in SaveGame method: " + userId);

					// Set parameter values
					command.Parameters.AddWithValue("@UserId", int.Parse(userId));
					command.Parameters.AddWithValue("@Timestamp", DateTime.Parse(timestamp));
					command.Parameters.AddWithValue("@GameState", gameState);

					// Open the connection and execute the query
					connection.Open();
					command.ExecuteNonQuery();
				}
			}

			return Ok();
		}
	}
}
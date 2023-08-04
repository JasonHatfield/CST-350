using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CST_350_Milestone.Models;

namespace CST_350_Milestone.Services
{
    public class SavedGamesDAO
    {
        private readonly string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UserManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<SavedGameModel> GetSavedGamesByUserId(int userId)
        {
            List<SavedGameModel> savedGames = new List<SavedGameModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM SavedGames WHERE UserId = @UserId";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int gameId = reader.GetInt32(0);
                        int UserId = reader.GetInt32(1);
                        string timestamp = reader.GetDateTime(2).ToString();
                        string gameState = reader.GetString(3);

                        SavedGameModel savedGame = new SavedGameModel(gameId, UserId, timestamp, gameState);
                        savedGames.Add(savedGame);
                    }
                }
            }

            return savedGames;
        }

        public SavedGameModel GetSavedGameById(int gameId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM SavedGames WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", gameId);

                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        int userId = reader.GetInt32(1);
                        string timestamp = reader.GetDateTime(2).ToString();
                        string gameState = reader.GetString(3);

                        return new SavedGameModel(gameId, userId, timestamp, gameState);
                    }
                }
            }

            return null;
        }

        public SavedGameModel DeleteGameById(int gameId)
        {
            var savedGame = GetSavedGameById(gameId);

            if (savedGame != null)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "DELETE FROM SavedGames WHERE Id = @GameId"; // Use 'Id' column name

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@GameId", gameId);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }

            return savedGame;
        }

        public List<SavedGameModel> GetAllSavedGames()
        {
            List<SavedGameModel> savedGames = new List<SavedGameModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "SELECT * FROM SavedGames";

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int gameId = reader.GetInt32(0);
                        int UserId = reader.GetInt32(1);
                        string timestamp = reader.GetDateTime(2).ToString();
                        string gameState = reader.GetString(3);

                        SavedGameModel savedGame = new SavedGameModel(gameId, UserId, timestamp, gameState);
                        savedGames.Add(savedGame);
                    }
                }
            }

            return savedGames;
        }

    }
}

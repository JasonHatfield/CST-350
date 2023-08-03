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
                            string timestamp = reader.GetDateTime(1).ToString();
                            string gameState = reader.GetString(2);

                            SavedGameModel savedGame = new SavedGameModel(gameId, userId, timestamp, gameState);
                            savedGames.Add(savedGame);
                        }
                    }
                }

                return savedGames;
            }
        }
    }

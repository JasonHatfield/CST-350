namespace CST_350_Milestone.Models
{
    public class SavedGameModel
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string Timestamp { get; set; }
        public string GameState { get; set; }

        public SavedGameModel(int gameId, int userId, string timestamp, string gameState)
        {
            GameId = gameId;
            UserId = userId;
            Timestamp = timestamp;
            GameState = gameState;
        }
    }
}

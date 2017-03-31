using UnityEngine;
using UnityEngine.SceneManagement;

namespace VRStandardAssets.Common
{
   /*
    * This class is used to keep score during a game and save
    * the highscores to PlayerPrefs.
    */
    public static class SessionData
    {
        // This enum shows all the types of games that use scores.
        public enum GameType
        {
            FLYER,
            MAZE,
        };

        private const string k_FlyerData = "flyerData";             // These are the names given to PlayerPrefs based on game type.
        private const string k_MazeData = "MazeData";
        private static int s_HighScore;                             // Used to store the highscore for the current game type.
        private static int s_Score;                                 // Used to store the current game's score.
        private static string s_CurrentGame;                        // The name of the current game type.

        // returns the original higscore to be stored
        public static int HighScore
        {
            get
            {
                return s_HighScore;
            }
        }
        // returns the current score to be stored
        public static int Score
        {
            get
            {
                return s_Score;
            }
        }

        public static void SetGameType(GameType gameType)
        {
            // Set the name of the current game based on the game type.
            switch (gameType)
            {
                case GameType.FLYER:
                    s_CurrentGame = k_FlyerData;
                    break;

                case GameType.MAZE:
                    s_CurrentGame = k_MazeData;
                    break;

                default:
                    Debug.LogError("Invalid GameType");
                    break;
            }
            Restart();
        }

        public static void Restart()
        {
            // Reset the current score and get the highscore from player prefs.
            s_Score = 0;
            s_HighScore = GetHighScore();
        }

        public static void AddScore(int score)
        {
            // Add to the current score and check if the high score needs to be set.
            s_Score += score;
            CheckHighScore();
        }

        public static int GetHighScore()
        {
            // Get the value of the highscore from the game name.
            return PlayerPrefs.GetInt(s_CurrentGame, 0);
        }

        static int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        private static void CheckHighScore()
        {
            // If the current score is greater than the high score then set the high score.
            if (s_Score > s_HighScore)
                SetHighScore();
       }

        private static void SetHighScore()
        {
            // Make sure the name of the current game has been set.
            if (string.IsNullOrEmpty(s_CurrentGame))
                Debug.LogError("m_CurrentGame not set");

            // The high score is now equal to the current score.
            s_HighScore = s_Score;

            // Set the high score for the current game's name and save it.
            PlayerPrefs.SetInt(s_CurrentGame, s_Score);
            PlayerPrefs.Save();
        }
       
        private static void CheckScore()
        {

        }
    }
}
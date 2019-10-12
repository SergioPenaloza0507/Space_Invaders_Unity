using System;

namespace Game_Management
{
    /// <summary>
    /// Class made for handling global game behavour
    /// </summary>
    public static class Game
    {
        public static bool Paused;

        public static void Pause()
        {
            Paused = true;
        }

        public static void Resume()
        {
            Paused = false;
        }

        public static void EndGame(GameCase gameCase)
        {
            
        }
    }
}

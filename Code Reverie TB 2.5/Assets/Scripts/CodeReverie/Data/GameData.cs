namespace CodeReverie
{
    public class GameData
    {
        public string path;
        public bool newGame;
        public int dataSlot;
        
        public GameData()
        {
            // currentScene = "Town";
            // playerPosition = Vector2.zero;
            // deathCount = 0;
            newGame = true;
            path = $"{dataSlot}/SaveFile.es3";
        }
    }
}
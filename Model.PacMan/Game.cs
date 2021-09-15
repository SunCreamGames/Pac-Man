namespace Model.PacMan
{
    using System.Security.Cryptography;

    public class Game
    {
        public Graph map;

        private MockMazeCreator mapGen;

        public Game()
        {
            mapGen = new MockMazeCreator();
            var matrix = mapGen.GenerateMap();
            map = new Graph(matrix);
        }
        
        
    }
}
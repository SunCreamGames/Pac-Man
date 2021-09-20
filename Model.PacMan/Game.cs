namespace Model.PacMan
{
    using System;
    using System.Data;
    using System.Security.Cryptography;

    public class Game
    {
        public Graph map;
        private int frameCounter;
        private IMazeCreator mapGen;
        public event Action<Graph, Pacman, Ghost, Ghost, Ghost, Ghost, int> DrawCall;
        public event Action OnLevelCompleted, OnLevelFailed;
        public event Action<int, int> OnCoinEaten;

        private Pacman player;
        private Blinky red;
        private Twinky blue;
        private Pinky pink;
        private Clyde orange;

        private Direction inputDir;

        public Game(IMazeCreator mapGenerator)
        {
            frameCounter = 1;
            mapGen = mapGenerator;
            var matrix = mapGen.GenerateMap(10, 10);
            map = new Graph(matrix);
            map.OnCoinEaten += CoinEaten;
            inputDir = Direction.Left;

            player = new Pacman(map);

            red = new Blinky(map, map.Vertices[11, 8]);
            blue = new Twinky(map, map.Vertices[11, 9]);
            pink = new Pinky(map, map.Vertices[8, 9]);
            orange = new Clyde(map, map.Vertices[12, 9]);
        }

        private void CoinEaten(int xCor, int yCor)
        {
            OnCoinEaten?.Invoke(xCor, yCor);
            CheckWin();
        }


        public void UpdateFrame(Direction input)
        {
            // CheckLose();
            MakeDecisions(input);
            MoveAll();
            frameCounter++;
            frameCounter %= 10;
            DrawCall?.Invoke(map, player, red, orange, pink, blue, frameCounter);
        }

        private void CheckLose()
        {
            var ghosts = new Ghost[] {red, blue, orange, pink};
            foreach (var ghost in ghosts)
            {
                if (ghost.Position.X == player.Position.X &&
                    Math.Abs(ghost.Position.Y - player.Position.Y) < 12)
                {
                    OnLevelFailed?.Invoke();
                    return;
                }

                if (ghost.Position.Y == player.Position.Y &&
                    Math.Abs(ghost.Position.X - player.Position.X) < 12)
                {
                    OnLevelFailed?.Invoke();
                    return;
                }
            }
        }

        private void CheckWin()
        {
            if (map.CoinsLeft == 0)
            {
                OnLevelCompleted?.Invoke();
            }
        }

        private void MoveAll()
        {
            pink.UpdatePosition();
            blue.UpdatePosition();
            red.UpdatePosition();
            orange.UpdatePosition();
            player.UpdatePosition();
        }

        private void MakeDecisions(Direction input)
        {
         
            player.MakeDecision(input);
        }


        public class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
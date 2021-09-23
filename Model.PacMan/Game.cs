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
        public event Action OnLevelCompleted;
        public event Action<int> OnPacmanDie;
        public event Action<int, int> OnCoinEaten;
        public int LivesCount => livesCount;
        private int livesCount;
        private Pacman player;
        private Blinky red;
        private Twinky blue;
        private Pinky pink;
        private Clyde orange;
        private Direction inputDir;
        public int CurrentScore { get; private set; }
        public Vertex PlayerCell => player.currentVertex;

        public Vertex[] GhostCells => new Vertex[]
            {red.CurrentVertex, pink.CurrentVertex, blue.CurrentVertex, orange.CurrentVertex};


        public Game(IMazeCreator mapGenerator)
        {
            livesCount = 3;
            frameCounter = 1;
            mapGen = mapGenerator;
            var matrix = mapGen.GenerateMap(10, 10);
            map = new Graph(matrix);
            map.OnCoinEaten += CoinEaten;
            inputDir = Direction.Left;
            CurrentScore = 0;
            SpawnActors();
        }

        public void RestartLevel()
        {
            livesCount = 3;
            CurrentScore = 0;

            foreach (var vertex in map.Vertices)
            {
                if (vertex.IsWalkable == Walkablitity.Walkable)
                {
                    vertex.SetCoin();
                }
            }

            SpawnActors();
        }

        public void SpawnActors()
        {
            player = new Pacman(map);

            red = new Blinky(map, map.Vertices[11, 8]);
            blue = new Twinky(map, map.Vertices[11, 9]);
            pink = new Pinky(map, map.Vertices[9, 9]);
            orange = new Clyde(map, map.Vertices[12, 9]);
        }

        private void CoinEaten(int xCor, int yCor)
        {
            CurrentScore += 50;
            OnCoinEaten?.Invoke(xCor, yCor);
            CheckWin();
        }


        public void UpdateFrame(Direction input)
        {
            MakeDecisions(input);
            MoveAll();
            frameCounter++;
            frameCounter %= 10;
            DrawCall?.Invoke(map, player, red, orange, pink, blue, frameCounter);
            CheckLose();
        }

        private void CheckLose()
        {
            var ghosts = new Ghost[] {red, blue, orange, pink};
            foreach (var ghost in ghosts)
            {
                if (ghost.Position.X == player.Position.X &&
                    Math.Abs(ghost.Position.Y - player.Position.Y) < 12)
                {
                    livesCount--;
                    OnPacmanDie?.Invoke(livesCount);
                    return;
                }

                if (ghost.Position.Y == player.Position.Y &&
                    Math.Abs(ghost.Position.X - player.Position.X) < 12)
                {
                    livesCount--;
                    OnPacmanDie?.Invoke(livesCount);
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
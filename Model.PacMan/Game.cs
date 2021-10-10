namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Security.Cryptography;

    public class Game
    {
        public Graph map;
        public int FrameCounter { get; private set; }
        private IMazeCreator mapGen;
        public event Action<Graph, Pacman, Ghost, Ghost, Ghost, Ghost, int> DrawCall;
        public event Action OnLevelCompleted;
        public event Action<int> OnPacmanDie;
        public event Action<int, int> OnCoinEaten;
        public event Action<List<Vertex>> DrawThePath;

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

        private IPacmanDecisionMaker pacmanAi;

        public Game(IMazeCreator mapGenerator)
        {
            livesCount = 8;
            FrameCounter = 1;
            mapGen = mapGenerator;
            var matrix = mapGen.GenerateMap(10, 10);
            map = new Graph(matrix,
                new List<IPathFinder>() {new BfsPathFinder(), new DfsPathFinder(), new UnInformPathFinder()});
            map.OnCoinEaten += CoinEaten;

            // TODO: Injecting ai
            inputDir = Direction.Left;
            CurrentScore = 0;
            SpawnActors();
        }

        private void DrawPath(List<Vertex> obj)
        {
            DrawThePath?.Invoke(obj);
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
            pacmanAi = new PacmanDecisionMaker(map, player, new AStarPathfinder());

            red = new Blinky(map, map.Vertices[11, 8], new RandomGhostDecisionMaker());
            blue = new Twinky(map, map.Vertices[11, 9], new RandomGhostDecisionMaker());
            pink = new Pinky(map, map.Vertices[9, 9], new RandomGhostDecisionMaker());
            orange = new Clyde(map, map.Vertices[12, 9], new RandomGhostDecisionMaker());
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
            FrameCounter++;
            FrameCounter %= 30;
            DrawCall?.Invoke(map, player, red, orange, pink, blue, FrameCounter);
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
            // player.SetInputDirection(input);
            pacmanAi.SetPositions(GhostCells, PlayerCell);
            pacmanAi.MakeDecision();
        }


        public class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
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
        public event Action<Graph, Pacman, Ghost[], int> DrawCall;
        public event Action OnLevelCompleted, OnLevelFailed;

        private Pacman player;
        private Blinky red;
        private Twinky blue;
        private Pinky pink;
        private Clyde orange;

        private Direction inputDir;

        public Game()
        {
            frameCounter = 1;
            mapGen = new MazeGenerator();
            var matrix = mapGen.GenerateMap(10,10);
            map = new Graph(matrix);
            inputDir = Direction.Left;

            player = new Pacman(map);
            red = new Blinky(map);
            blue = new Twinky(map);
            pink = new Pinky(map);
            orange = new Clyde(map);
        }


        public void UpdateFrame(Direction input)
        {
            // CheckWin();
            // CheckLose();
            MakeDecisions(input);
            MoveAll();
            frameCounter++;
            frameCounter %= 10;
            DrawCall?.Invoke(map, player, new Ghost[] {red, blue, orange, pink}, frameCounter);
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
            // pink.UpdatePosition();
            // blue.UpdatePosition();
            // red.UpdatePosition();
            // orange.UpdatePosition();
            player.UpdatePosition();
        }

        private void MakeDecisions(Direction input)
        {
            // pink.MakeDecision();
            // blue.MakeDecision();
            // red.MakeDecision();
            // orange.MakeDecision();
            player.MakeDecision(input);
        }


        public class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
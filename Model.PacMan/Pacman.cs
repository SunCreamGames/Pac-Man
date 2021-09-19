namespace Model.PacMan
{
    using System;
    using System.IO;

    public class Pacman : IDisposable
    {
        private Graph map;
        public Vertex currentVertex { get; private set; }

        public Game.Position Position { get; private set; }
        public Direction curentDirection { get; private set; }
        private Direction nextDirection;
        StreamWriter logger;

        public Pacman(Graph map)
        {
            this.map = map;

            currentVertex = map.GetRandomWalkableVertex();
            Position = new Game.Position()
            {
                X = 8 + currentVertex.Coordinate.Item1 * 16,
                Y = 8 + currentVertex.Coordinate.Item2 * 16
            };
            curentDirection = Direction.None;
            nextDirection = Direction.None;
            logger = new StreamWriter("C:\\Users\\Богдан\\Desktop\\logs.txt");
        }

        public void UpdatePosition()
        {
            switch (curentDirection)
            {
                case Direction.Left:
                    Position.X -= 4;
                    break;
                case Direction.Right:
                    Position.X += 4;
                    break;
                case Direction.Up:
                    Position.Y -= 4;
                    break;
                case Direction.Down:
                    Position.Y += 4;
                    break;
            }

            UpdateCurrentVertex();
        }

        private void UpdateCurrentVertex()
        {
            if (Position.Y % 16 != 8 || Position.X % 16 != 8)
            {
                return;
            }

            currentVertex = map.Vertices[Position.Y / 16, Position.X / 16];
            if (currentVertex.HasCoin)
            {
                currentVertex.DestroyCoin();
            }


            logger.WriteLine(
                $" Position :   {currentVertex.Coordinate.Item1}:{currentVertex.Coordinate.Item2}");
            logger.WriteLine(
                $" L :   {currentVertex.LVertex.Coordinate.Item1}:{currentVertex.LVertex.Coordinate.Item2}:{currentVertex.LVertex.IsWalkable}");
            logger.WriteLine(
                $" U :   {currentVertex.UVertex.Coordinate.Item1}:{currentVertex.UVertex.Coordinate.Item2}:{currentVertex.UVertex.IsWalkable}");
            logger.WriteLine(
                $" R :   {currentVertex.RVertex.Coordinate.Item1}:{currentVertex.RVertex.Coordinate.Item2}:{currentVertex.RVertex.IsWalkable}");
            logger.WriteLine(
                $" D :   {currentVertex.DVertex.Coordinate.Item1}:{currentVertex.DVertex.Coordinate.Item2}:{currentVertex.DVertex.IsWalkable}");


            switch (nextDirection)
            {
                case Direction.Left:
                    if (currentVertex.LVertex.IsWalkable != Walkablitity.Walkable) nextDirection = Direction.None;
                    break;
                case Direction.Right:
                    if (currentVertex.RVertex.IsWalkable != Walkablitity.Walkable) nextDirection = Direction.None;
                    break;
                case Direction.Up:
                    if (currentVertex.UVertex.IsWalkable != Walkablitity.Walkable) nextDirection = Direction.None;
                    break;
                case Direction.Down:
                    if (currentVertex.DVertex.IsWalkable != Walkablitity.Walkable) nextDirection = Direction.None;
                    break;
            }

            switch (curentDirection)
            {
                case Direction.Left:
                    if (currentVertex.LVertex.IsWalkable != Walkablitity.Walkable) curentDirection = Direction.None;
                    break;
                case Direction.Right:
                    if (currentVertex.RVertex.IsWalkable != Walkablitity.Walkable) curentDirection = Direction.None;
                    break;
                case Direction.Up:
                    if (currentVertex.UVertex.IsWalkable != Walkablitity.Walkable) curentDirection = Direction.None;
                    break;
                case Direction.Down:
                    if (currentVertex.DVertex.IsWalkable != Walkablitity.Walkable) curentDirection = Direction.None;
                    break;
            }

            if (nextDirection != Direction.None)
            {
                curentDirection = nextDirection;
                nextDirection = Direction.None;
            }

            logger.WriteLine();
            logger.Flush();
        }

        public void MakeDecision(Direction inputDir)
        {
            nextDirection = inputDir;
        }

        public void Dispose()
        {
            logger.Close();
            logger?.Dispose();
        }
    }
}
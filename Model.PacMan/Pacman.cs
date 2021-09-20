namespace Model.PacMan
{
    using System;
    using System.IO;

    public class Pacman
    {
        private Graph map;
        public Vertex currentVertex { get; private set; }

        public Game.Position Position { get; private set; }
        public Direction CurrentDirection { get; private set; }
        private Direction nextDirection;

        public Pacman(Graph map)
        {
            this.map = map;

            currentVertex = map.Vertices[map.Vertices.GetLength(1) / 2, map.Vertices.GetLength(0) - 2];
            while (currentVertex.IsWalkable != Walkablitity.Walkable)
            {
                currentVertex = currentVertex.UVertex;
            }

            Position = new Game.Position()
            {
                X = 8 + currentVertex.Coordinate.Item1 * 16,
                Y = 8 + currentVertex.Coordinate.Item2 * 16
            };
            UpdateCurrentVertex();

            CurrentDirection = Direction.None;
            nextDirection = Direction.None;
        }

        public void UpdatePosition()
        {
            switch (CurrentDirection)
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

            switch (CurrentDirection)
            {
                case Direction.Left:
                    if (currentVertex.LVertex.IsWalkable != Walkablitity.Walkable) CurrentDirection = Direction.None;
                    break;
                case Direction.Right:
                    if (currentVertex.RVertex.IsWalkable != Walkablitity.Walkable) CurrentDirection = Direction.None;
                    break;
                case Direction.Up:
                    if (currentVertex.UVertex.IsWalkable != Walkablitity.Walkable) CurrentDirection = Direction.None;
                    break;
                case Direction.Down:
                    if (currentVertex.DVertex.IsWalkable != Walkablitity.Walkable) CurrentDirection = Direction.None;
                    break;
            }

            if (nextDirection != Direction.None)
            {
                CurrentDirection = nextDirection;
                nextDirection = Direction.None;
            }
        }

        public void MakeDecision(Direction inputDir)
        {
            nextDirection = inputDir;
        }
    }
}
namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Ghost
    {
        private Graph map;
        private Vertex currentVertex;
        public Direction curentDirection { get; private set; }
        public Game.Position Position { get; }

        public Ghost(Graph map)
        {
            this.map = map;
            currentVertex = map.GetRandomWalkableVertex();
            Position = new Game.Position()
            {
                X = 8 + currentVertex.Coordinate.Item1 * 16,
                Y = 8 + currentVertex.Coordinate.Item2 * 16
            };
        }

        public void UpdatePosition()
        {
            switch (curentDirection)
            {
                case Direction.Left:
                    Position.X -= 2;
                    break;
                case Direction.Right:
                    Position.X += 2;
                    break;
                case Direction.Up:
                    Position.Y += 2;
                    break;
                case Direction.Down:
                    Position.Y -= 2;
                    break;
            }

            UpdateCurrentVertex();
        }

        private void UpdateCurrentVertex()
        {
            if (Position.X % 16 == 8 && Position.Y % 16 == 8)
            {
                currentVertex = map.Vertices[Position.X / 16, Position.Y / 16];
            }
        }

        public void MakeDecision()
        {
            var r = new Random();
            switch (curentDirection)
            {
                case Direction.Left:
                    if (currentVertex.LVertex.IsWalkable != Walkablitity.Wall)
                    {
                        var possibleMoves = new List<Vertex>()
                            {currentVertex.DVertex, currentVertex.RVertex, currentVertex.UVertex};
                        possibleMoves = possibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == currentVertex.DVertex)
                        {
                            curentDirection = Direction.Down;
                        }
                        else if (nextDir == currentVertex.RVertex)
                        {
                            curentDirection = Direction.Right;
                        }
                        else
                        {
                            curentDirection = Direction.Up;
                        }
                    }

                    break;
                case Direction.Right:
                    if (currentVertex.RVertex.IsWalkable != Walkablitity.Wall)
                    {
                        var possibleMoves = new List<Vertex>()
                            {currentVertex.DVertex, currentVertex.LVertex, currentVertex.UVertex};
                        possibleMoves = possibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == currentVertex.DVertex)
                        {
                            curentDirection = Direction.Down;
                        }
                        else if (nextDir == currentVertex.LVertex)
                        {
                            curentDirection = Direction.Left;
                        }
                        else
                        {
                            curentDirection = Direction.Up;
                        }
                    }

                    break;
                case Direction.Up:
                    if (currentVertex.UVertex.IsWalkable != Walkablitity.Wall)
                    {
                        var possibleMoves = new List<Vertex>()
                            {currentVertex.DVertex, currentVertex.LVertex, currentVertex.RVertex};
                        possibleMoves = possibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == currentVertex.DVertex)
                        {
                            curentDirection = Direction.Down;
                        }
                        else if (nextDir == currentVertex.LVertex)
                        {
                            curentDirection = Direction.Left;
                        }
                        else
                        {
                            curentDirection = Direction.Right;
                        }
                    }

                    break;
                case Direction.Down:
                    if (currentVertex.DVertex.IsWalkable != Walkablitity.Wall)
                    {
                        var possibleMoves = new List<Vertex>()
                            {currentVertex.UVertex, currentVertex.LVertex, currentVertex.RVertex};
                        possibleMoves = possibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == currentVertex.UVertex)
                        {
                            curentDirection = Direction.Up;
                        }
                        else if (nextDir == currentVertex.LVertex)
                        {
                            curentDirection = Direction.Left;
                        }
                        else
                        {
                            curentDirection = Direction.Right;
                        }
                    }

                    break;
            }
        }
    }

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        None
    }
}
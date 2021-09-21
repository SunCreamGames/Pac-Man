namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Ghost
    {
        private Graph map;
        private Vertex currentVertex;
        public Direction CurrentDirection { get; protected set; }
        public Game.Position Position { get; private set; }
        protected Random r;

        public Ghost(Graph map, Vertex startPoint)
        {
            this.map = map;
            currentVertex = startPoint;
            r = new Random();

            Position = new Game.Position()
            {
                X = 8 + currentVertex.Coordinate.Item1 * 16,
                Y = 8 + currentVertex.Coordinate.Item2 * 16
            };
            CurrentDirection = Direction.Up;
            MakeDecision();
        }

        public void SetCurrentVertex(Vertex v)
        {
            currentVertex = v;
            Position = new Game.Position()
            {
                X = 8 + currentVertex.Coordinate.Item1 * 16,
                Y = 8 + currentVertex.Coordinate.Item2 * 16
            };
            CurrentDirection = Direction.Up;
            MakeDecision();
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
            var c = currentVertex;
            if (Position.Y % 16 == 8 && Position.X % 16 == 8)
            {
                currentVertex = map.Vertices[Position.Y / 16, Position.X / 16];
            }

            if (c != currentVertex)
            {
                TryMakeDecision();
            }
        }

        private void TryMakeDecision()
        {
            MakeDecision();
        }

        public void MakeDecision()
        {
            List<Vertex> possibleMoves;
            switch (CurrentDirection)
            {
                case Direction.Left:
                    if (currentVertex.LVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = new List<Vertex>()
                            {currentVertex.DVertex, currentVertex.RVertex, currentVertex.UVertex};
                        possibleMoves = possibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == currentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == currentVertex.RVertex)
                        {
                            CurrentDirection = Direction.Right;
                        }
                        else
                        {
                            CurrentDirection = Direction.Up;
                        }
                    }
                    else
                    {
                        possibleMoves = new List<Vertex>()
                        {
                            currentVertex.LVertex, currentVertex.DVertex, currentVertex.RVertex, currentVertex.UVertex
                        }.Where(v => v != null && v.IsWalkable != Walkablitity.Wall).ToList();
                        if (possibleMoves.Count <= 2)
                        {
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];

                        if (nextDir == currentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == currentVertex.RVertex)
                        {
                            CurrentDirection = Direction.Right;
                        }
                        else if (nextDir == currentVertex.LVertex)
                        {
                            CurrentDirection = Direction.Left;
                        }
                        else
                        {
                            CurrentDirection = Direction.Up;
                        }
                    }

                    return;
                case Direction.Right:
                    if (currentVertex.RVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = new List<Vertex>()
                            {currentVertex.DVertex, currentVertex.LVertex, currentVertex.UVertex};
                        possibleMoves = possibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == currentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == currentVertex.LVertex)
                        {
                            CurrentDirection = Direction.Left;
                        }
                        else
                        {
                            CurrentDirection = Direction.Up;
                        }
                    }
                    else
                    {
                        possibleMoves = new List<Vertex>()
                        {
                            currentVertex.LVertex, currentVertex.DVertex, currentVertex.RVertex, currentVertex.UVertex
                        }.Where(v => v != null && v.IsWalkable != Walkablitity.Wall).ToList();
                        if (possibleMoves.Count <= 2)
                        {
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];

                        if (nextDir == currentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == currentVertex.RVertex)
                        {
                            CurrentDirection = Direction.Right;
                        }
                        else if (nextDir == currentVertex.LVertex)
                        {
                            CurrentDirection = Direction.Left;
                        }
                        else
                        {
                            CurrentDirection = Direction.Up;
                        }
                    }

                    return;

                case Direction.Up:
                    if (currentVertex.UVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = new List<Vertex>()
                            {currentVertex.DVertex, currentVertex.LVertex, currentVertex.RVertex};
                        possibleMoves = possibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == currentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == currentVertex.LVertex)
                        {
                            CurrentDirection = Direction.Left;
                        }
                        else
                        {
                            CurrentDirection = Direction.Right;
                        }
                    }
                    else
                    {
                        possibleMoves = new List<Vertex>()
                        {
                            currentVertex.LVertex, currentVertex.DVertex, currentVertex.RVertex, currentVertex.UVertex
                        }.Where(v => v != null && v.IsWalkable != Walkablitity.Wall).ToList();
                        if (possibleMoves.Count <= 2)
                        {
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];

                        if (nextDir == currentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == currentVertex.RVertex)
                        {
                            CurrentDirection = Direction.Right;
                        }
                        else if (nextDir == currentVertex.LVertex)
                        {
                            CurrentDirection = Direction.Left;
                        }
                        else
                        {
                            CurrentDirection = Direction.Up;
                        }
                    }

                    return;

                case Direction.Down:
                    if (currentVertex.DVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = new List<Vertex>()
                            {currentVertex.UVertex, currentVertex.LVertex, currentVertex.RVertex};
                        possibleMoves = possibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == currentVertex.UVertex)
                        {
                            CurrentDirection = Direction.Up;
                        }
                        else if (nextDir == currentVertex.LVertex)
                        {
                            CurrentDirection = Direction.Left;
                        }
                        else
                        {
                            CurrentDirection = Direction.Right;
                        }
                    }
                    else
                    {
                        possibleMoves = new List<Vertex>()
                        {
                            currentVertex.LVertex, currentVertex.DVertex, currentVertex.RVertex, currentVertex.UVertex
                        }.Where(v => v != null && v.IsWalkable != Walkablitity.Wall).ToList();
                        if (possibleMoves.Count <= 2)
                        {
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];

                        if (nextDir == currentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == currentVertex.RVertex)
                        {
                            CurrentDirection = Direction.Right;
                        }
                        else if (nextDir == currentVertex.LVertex)
                        {
                            CurrentDirection = Direction.Left;
                        }
                        else
                        {
                            CurrentDirection = Direction.Up;
                        }
                    }

                    return;
            }
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
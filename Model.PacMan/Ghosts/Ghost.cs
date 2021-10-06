namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Ghost
    {
        private Graph map;
        public Vertex CurrentVertex { get; private set; }
        public Direction CurrentDirection { get; protected set; }
        public Game.Position Position { get; private set; }
        protected Random r;

        public Ghost(Graph map, Vertex startPoint)
        {
            this.map = map;
            CurrentVertex = startPoint;
            r = new Random();

            Position = new Game.Position()
            {
                X = 8 + CurrentVertex.Coordinate.Item1 * 16,
                Y = 8 + CurrentVertex.Coordinate.Item2 * 16
            };
            CurrentDirection = Direction.Up;
            MakeDecision();
        }

        public void SetCurrentVertex(Vertex v)
        {
            CurrentVertex = v;
            Position = new Game.Position()
            {
                X = 8 + CurrentVertex.Coordinate.Item1 * 16,
                Y = 8 + CurrentVertex.Coordinate.Item2 * 16
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
            var c = CurrentVertex;
            if (Position.Y % 16 == 8 && Position.X % 16 == 8)
            {
                CurrentVertex = map.Vertices[Position.Y / 16, Position.X / 16];
            }

            if (c != CurrentVertex)
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
            List<Vertex> possibleMoves = new List<Vertex>()
                {CurrentVertex.DVertex, CurrentVertex.RVertex, CurrentVertex.UVertex, CurrentVertex.LVertex};

            var startPossibleMoves = new List<Vertex>()
                {CurrentVertex.DVertex, CurrentVertex.RVertex, CurrentVertex.UVertex, CurrentVertex.LVertex};
            switch (CurrentDirection)
            {
                case Direction.Left:
                    if (CurrentVertex.LVertex.IsWalkable == Walkablitity.Wall)
                    {
                       
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == CurrentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == CurrentVertex.RVertex)
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
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves =startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        if (possibleMoves.Count <= 2)
                        {
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];

                        if (nextDir == CurrentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == CurrentVertex.RVertex)
                        {
                            CurrentDirection = Direction.Right;
                        }
                        else if (nextDir == CurrentVertex.LVertex)
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
                    if (CurrentVertex.RVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves =startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        if (possibleMoves.Count == 0)
                        {
                            
                        }
                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == CurrentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == CurrentVertex.LVertex)
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
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves =startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        if (possibleMoves.Count <= 2)
                        {
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];

                        if (nextDir == CurrentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == CurrentVertex.RVertex)
                        {
                            CurrentDirection = Direction.Right;
                        }
                        else if (nextDir == CurrentVertex.LVertex)
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
                    if (CurrentVertex.UVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves =startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == CurrentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == CurrentVertex.LVertex)
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
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves =startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }
                        if (possibleMoves.Count <= 2)
                        {
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];

                        if (nextDir == CurrentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == CurrentVertex.RVertex)
                        {
                            CurrentDirection = Direction.Right;
                        }
                        else if (nextDir == CurrentVertex.LVertex)
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
                    if (CurrentVertex.DVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves =startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        if (nextDir == CurrentVertex.UVertex)
                        {
                            CurrentDirection = Direction.Up;
                        }
                        else if (nextDir == CurrentVertex.LVertex)
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
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves =startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        if (possibleMoves.Count <= 2)
                        {
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];

                        if (nextDir == CurrentVertex.DVertex)
                        {
                            CurrentDirection = Direction.Down;
                        }
                        else if (nextDir == CurrentVertex.RVertex)
                        {
                            CurrentDirection = Direction.Right;
                        }
                        else if (nextDir == CurrentVertex.LVertex)
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
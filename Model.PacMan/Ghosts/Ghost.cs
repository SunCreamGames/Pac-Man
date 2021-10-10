namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Runtime.Remoting.Channels;

    public class Ghost
    {
        private Graph map;
        private IGhostDecisionMaker ghostDecisionMaker;
        public Vertex CurrentVertex { get; private set; }
        public Direction CurrentDirection { get; protected set; }
        public Game.Position Position { get; private set; }

        public Ghost(Graph map, Vertex startPoint, IGhostDecisionMaker ghostDecisionMaker)
        {
            this.map = map;
            CurrentVertex = startPoint;
            this.ghostDecisionMaker = ghostDecisionMaker;

            Position = new Game.Position()
            {
                X = 8 + CurrentVertex.Coordinate.Item1 * 16,
                Y = 8 + CurrentVertex.Coordinate.Item2 * 16
            };
            CurrentDirection = Direction.Up;
            CurrentDirection = GetDirectionToTheVertex(ghostDecisionMaker.MakeDecision(map, CurrentVertex, null));
        }

        private Direction GetDirectionToTheVertex(List<Vertex> targetPath)
        {
            if (!targetPath.Contains(CurrentVertex))
            {
                throw new Exception("Path doesn't contain curVer");
            }

            var indexOfCurrentVertex = targetPath.IndexOf(CurrentVertex);

            if (targetPath.Last() == CurrentVertex)
            {
                targetPath = ghostDecisionMaker.MakeDecision(map, CurrentVertex, null);
                return GetDirectionToTheVertex(targetPath);
            }

            if (targetPath[indexOfCurrentVertex + 1] == CurrentVertex.LVertex)
            {
                return Direction.Left;
            }

            if (targetPath[indexOfCurrentVertex + 1] == CurrentVertex.RVertex)
            {
                return Direction.Right;
            }

            if (targetPath[indexOfCurrentVertex + 1] == CurrentVertex.UVertex)
            {
                return Direction.Up;
            }

            if (targetPath[indexOfCurrentVertex + 1] == CurrentVertex.DVertex)
            {
                return Direction.Down;
            }

            throw new Exception("Default case exception");
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
            CurrentDirection = GetDirectionToTheVertex(ghostDecisionMaker.MakeDecision(map, CurrentVertex, null));
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
            CurrentDirection = GetDirectionToTheVertex(ghostDecisionMaker.MakeDecision(map, CurrentVertex, null));
        }
    }
}

public enum Direction
{
    Up,
    Left,
    Right,
    Down,
    None
}
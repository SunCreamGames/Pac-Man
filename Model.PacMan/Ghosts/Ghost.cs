namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Runtime.Remoting.Channels;

    public class Ghost
    {
        protected readonly Graph map;
        protected int close, far;
        private IGhostDecisionMaker ghostDecisionMaker;
        public Vertex CurrentVertex { get; private set; }
        public Direction CurrentDirection { get; protected set; }
        public Game.Position Position { get; private set; }
        protected Random rand;
        private Pacman pacman;
        public Vertex PacmanPosition => pacman.currentVertex;

        public Ghost(Graph map, Vertex startPoint, IGhostDecisionMaker ghostDecisionMaker, Pacman pacman)
        {
            this.map = map;
            close = 10;
            far = 25;
            CurrentVertex = startPoint;
            this.ghostDecisionMaker = ghostDecisionMaker;
            this.pacman = pacman;
            rand = new Random();
            SetCurrentVertex(startPoint);
            var wayToPacMan = ghostDecisionMaker.GetDistanceToPacman(map, CurrentVertex, PacmanPosition);
            if (wayToPacMan < far && wayToPacMan > close)
            {
                CurrentDirection =
                    GetDirectionToTheVertex(ghostDecisionMaker.MakeDecision(map, CurrentVertex, PacmanPosition, rand));
                Position = new Game.Position()
                {
                    X = 8 + CurrentVertex.Coordinate.Item1 * 16,
                    Y = 8 + CurrentVertex.Coordinate.Item2 * 16
                };
                CurrentDirection =
                    GetDirectionToTheVertex(ghostDecisionMaker.MakeDecision(map, CurrentVertex, PacmanPosition, rand));
            }
        }

        private Direction GetDirectionToTheVertex(List<Vertex> targetPath)
        {
            if (!targetPath.Contains(CurrentVertex))
            {
                throw new Exception("Path doesn't contain curVer");
            }

            var indexOfCurrentVertex = targetPath.IndexOf(CurrentVertex);

            if (targetPath.First() == CurrentVertex)
            {
                return CurrentDirection;
            }


            if (targetPath[indexOfCurrentVertex - 1] == CurrentVertex.LVertex)
            {
                return Direction.Left;
            }

            if (targetPath[indexOfCurrentVertex - 1] == CurrentVertex.RVertex)
            {
                return Direction.Right;
            }

            if (targetPath[indexOfCurrentVertex - 1] == CurrentVertex.UVertex)
            {
                return Direction.Up;
            }

            if (targetPath[indexOfCurrentVertex - 1] == CurrentVertex.DVertex)
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
            CurrentDirection =
                GetDirectionToTheVertex(ghostDecisionMaker.MakeDecision(map, CurrentVertex, PacmanPosition, rand));
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
            if (ghostDecisionMaker.GetDistanceToPacman(map, CurrentVertex, PacmanPosition) < close)
            {
                ghostDecisionMaker = new WanderGhostDecisionMaker();
            }
            else
            {
                ghostDecisionMaker = new ChaseGhostDecisionMaker();
            }

            CurrentDirection =
                GetDirectionToTheVertex(ghostDecisionMaker.MakeDecision(map, CurrentVertex, PacmanPosition, rand));
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
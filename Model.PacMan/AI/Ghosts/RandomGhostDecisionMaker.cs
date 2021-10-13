namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RandomGhostDecisionMaker : GhostDecisionMaker
    {
        public override event Action<IDecisionMaker> OnSwitch;

        private Direction currentDirection;
        private Vertex _position;

        public override List<Vertex> MakeDecision(Graph map, Vertex position, Vertex target, Random r)
        {
            _position = position;
            Vertex nextVertex = null;
            List<Vertex> possibleMoves;
            var startPossibleMoves = _position.Neighbours.ToList();
            switch (currentDirection)
            {
                case Direction.Left:
                    nextVertex = _position.LVertex;
                    if (nextVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        nextVertex = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {_position, nextVertex};
                    }
                    else
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        if (possibleMoves.Count <= 2)
                        {
                            return new List<Vertex>() {_position, nextVertex};
                        }

                        nextVertex = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {_position, nextVertex};
                    }

                case Direction.Right:
                    nextVertex = _position.RVertex;
                    if (nextVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }


                        nextVertex = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {_position, nextVertex};
                    }
                    else
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        if (possibleMoves.Count <= 2)
                        {
                            return new List<Vertex>() {_position, nextVertex};
                        }

                        nextVertex = possibleMoves[r.Next(possibleMoves.Count)];

                        return new List<Vertex>() {_position, nextVertex};
                    }


                case Direction.Up:
                    nextVertex = _position.UVertex;
                    if (nextVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        nextVertex = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {_position, nextVertex};
                    }
                    else
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        if (possibleMoves.Count <= 2)
                        {
                            return new List<Vertex>() {_position, nextVertex};
                        }

                        nextVertex = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {_position, nextVertex};
                    }

                case Direction.Down:
                    nextVertex = _position.DVertex;
                    if (nextVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        nextVertex = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {_position, nextVertex};
                    }
                    else
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        if (possibleMoves.Count <= 2)
                        {
                            return new List<Vertex>() {_position, nextVertex};
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {_position, nextDir};
                    }
            }

            return new List<Vertex>() {_position, nextVertex};
        }


        public override void SwitchingDecision(Graph map, Vertex ghost, Vertex pacman)
        {
        }
    }
}
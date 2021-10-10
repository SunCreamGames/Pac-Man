namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RandomGhostDecisionMaker : IGhostDecisionMaker
    {
        private Direction currentDirection;

        public void UpdateCurrentDirection(Direction d)
        {
            currentDirection = d;
        }

        public List<Vertex> MakeDecision(Graph map, Vertex currentVertex, Vertex target)
        {
            Vertex nextVertex = null;
            List<Vertex> possibleMoves = new List<Vertex>()
                {currentVertex.DVertex, currentVertex.RVertex, currentVertex.UVertex, currentVertex.LVertex};
            var r = new Random();
            var startPossibleMoves = new List<Vertex>()
                {currentVertex.DVertex, currentVertex.RVertex, currentVertex.UVertex, currentVertex.LVertex};
            switch (currentDirection)
            {
                case Direction.Left:
                    nextVertex = currentVertex.LVertex;
                    if (currentVertex.LVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {currentVertex, nextDir};
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
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {currentVertex, nextDir};
                    }

                case Direction.Right:
                    nextVertex = currentVertex.RVertex;
                    if (currentVertex.RVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        if (possibleMoves.Count == 0)
                        {
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {currentVertex, nextDir};
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
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];

                        return new List<Vertex>() {currentVertex, nextDir};
                    }


                case Direction.Up:
                    nextVertex = currentVertex.UVertex;
                    if (currentVertex.UVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {currentVertex, nextDir};
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
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {currentVertex, nextDir};
                    }

                case Direction.Down:
                    nextVertex = currentVertex.DVertex;
                    if (currentVertex.DVertex.IsWalkable == Walkablitity.Wall)
                    {
                        possibleMoves = startPossibleMoves.Where(v => v.IsWalkable == Walkablitity.Walkable).ToList();
                        if (possibleMoves.Count == 0)
                        {
                            possibleMoves = startPossibleMoves.Where(v => v.IsWalkable != Walkablitity.Wall).ToList();
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {currentVertex, nextDir};
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
                            break;
                        }

                        var nextDir = possibleMoves[r.Next(possibleMoves.Count)];
                        return new List<Vertex>() {currentVertex, nextDir};
                    }
            }

             return new List<Vertex>() {currentVertex, nextVertex};
        }
    }
}
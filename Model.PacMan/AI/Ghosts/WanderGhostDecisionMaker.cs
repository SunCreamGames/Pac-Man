namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;

    public class WanderGhostDecisionMaker : IGhostDecisionMaker
    {
        int close = 10;
        int far = 30;
        private Vertex _position;
        private List<Vertex> path;

        private Direction decision;


        public List<Vertex> MakeDecision(Graph map, Vertex position, Vertex targetVertex, Random random)
        {
            _position = position;


            if (path == null || path.FirstOrDefault() == null || path.FirstOrDefault() == _position)
            {
                targetVertex = map.GetRandomWalkableVertex();
                path = map.FindPath(_position, targetVertex).Result.Item2;
            }

            return path;
        }

        public int GetDistanceToPacman(Graph map, Vertex ghost, Vertex pacman)
        {
            return map.FindPath(ghost, pacman).Result.Item1;
        }

        private Direction GetDirectionToTheVertex(List<Vertex> targetPath)
        {
            if (!targetPath.Contains(_position))
            {
                throw new Exception("Path doesn't contain curVer");
            }

            var indexOfCurrentVertex = targetPath.IndexOf(_position);

            if (targetPath.First() == _position)
            {
                throw new Exception("Pacman on position");
            }


            if (targetPath[indexOfCurrentVertex - 1] == _position.LVertex)
            {
                return Direction.Left;
            }

            if (targetPath[indexOfCurrentVertex - 1] == _position.RVertex)
            {
                return Direction.Right;
            }

            if (targetPath[indexOfCurrentVertex - 1] == _position.UVertex)
            {
                return Direction.Up;
            }

            if (targetPath[indexOfCurrentVertex - 1] == _position.DVertex)
            {
                return Direction.Down;
            }


            throw new Exception("Default case exception");
        }
    }
}
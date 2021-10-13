namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;

    public class WanderGhostDecisionMaker : GhostDecisionMaker
    {
        public override event Action<IDecisionMaker> OnSwitch;

        int farDist = 50;
        private Vertex _position;
        private List<Vertex> path;

        private Direction decision;


        public override List<Vertex> MakeDecision(Graph map, Vertex position, Vertex targetVertex, Random random)
        {
            _position = position;


            if (path == null || path.FirstOrDefault() == null || path.FirstOrDefault() == _position)
            {
                targetVertex = map.GetRandomWalkableVertex();
                path = map.FindPath(_position, targetVertex).Result.Item2;
            }

            return path;
        }

        public override void SwitchingDecision(Graph map, Vertex ghost, Vertex pacman)
        {
            var distanceToPacman = map.FindPath(ghost, pacman).Result.Item1;
            if (distanceToPacman > farDist)
            {
                OnSwitch?.Invoke(new ChaseGhostDecisionMaker());
            }

            if (path == null)
            {
                MakeDecision(map, ghost, pacman, new Random());
            }

            if (path.FirstOrDefault() == null)
            {
                throw new Exception("Path is empty");
            }

            if (path.FirstOrDefault() == ghost)
            {
                OnSwitch?.Invoke(new ChaseGhostDecisionMaker());
            }
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
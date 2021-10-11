namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;

    public class ChaseGhostDecisionMaker : IGhostDecisionMaker
    {
        public List<Vertex> MakeDecision(Graph map, Vertex _position, Vertex targetVertex, Random random)
        {
            var path = map.FindPath(_position, new[] {targetVertex}).Result.Item2[0].Item2;
            return path;
        }

        public int GetDistanceToPacman(Graph map, Vertex ghost, Vertex pacman)
        {
            return map.FindPath(ghost, new[] {pacman}).Result.Item2[0].Item1;
        }
    }
}
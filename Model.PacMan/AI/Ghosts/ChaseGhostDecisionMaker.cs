namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;

    public class ChaseGhostDecisionMaker : GhostDecisionMaker
    {
        public override event Action<IDecisionMaker> OnSwitch;

        protected int closeDist = 5;

        public override List<Vertex> MakeDecision(Graph map, Vertex _position, Vertex targetVertex, Random random)
        {
            return map.FindPath(_position, targetVertex).Result.Item2;
        }

        public override void SwitchingDecision(Graph map, Vertex ghost, Vertex pacman)
        {
            var pathToPacman = map.FindPath(ghost, pacman).Result.Item1;
            if (pathToPacman < closeDist)
            {
                OnSwitch?.Invoke(new WanderGhostDecisionMaker());
            }
        }
    }
}
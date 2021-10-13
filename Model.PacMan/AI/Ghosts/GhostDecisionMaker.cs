namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public abstract class GhostDecisionMaker : IDecisionMaker
    {
        protected int farDist = 50;
        protected int closeDist = 20;
        public abstract List<Vertex> MakeDecision(Graph map, Vertex _position, Vertex targetVertex, Random random);
        public abstract void SwitchingDecision(Graph map, Vertex ghost, Vertex pacman);
        public abstract event Action<IDecisionMaker> OnSwitch;
    }
}
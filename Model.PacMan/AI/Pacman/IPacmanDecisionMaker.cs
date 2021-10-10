namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;

    public interface IPacmanDecisionMaker : IDecisionMaker
    {
        void SetPositions(Vertex[] ghosts, Vertex position);
        void MakeDecision();
    }
}
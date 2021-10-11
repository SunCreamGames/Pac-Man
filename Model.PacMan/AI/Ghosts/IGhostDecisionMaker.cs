namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public interface IGhostDecisionMaker : IDecisionMaker
    {
        List<Vertex> MakeDecision(Graph map, Vertex _position, Vertex targetVertex, Random random);
        int GetDistanceToPacman(Graph map, Vertex ghost, Vertex pacman);
    }
}
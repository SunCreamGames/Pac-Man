namespace Model.PacMan
{
    using System.Collections.Generic;

    public interface IGhostDecisionMaker : IDecisionMaker
    {
        List<Vertex> MakeDecision(Graph map, Vertex currentVertex, Vertex targetVertex);
    }
}
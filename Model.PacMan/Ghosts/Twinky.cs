namespace Model.PacMan
{
    using System;

    public class Twinky : Ghost
    {
        private readonly Graph map;

        public Twinky(Graph map, Vertex vert, IGhostDecisionMaker ghostDecisionMaker) : base(map , vert, ghostDecisionMaker)
        {
            CurrentDirection = Direction.Up;
            this.map = map;
        }
    }
}
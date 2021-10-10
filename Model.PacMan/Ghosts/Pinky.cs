namespace Model.PacMan
{
    using System;

    public class Pinky : Ghost
    {
        private readonly Graph map;

        public Pinky(Graph map, Vertex vert, IGhostDecisionMaker ghostDecisionMaker) : base(map , vert, ghostDecisionMaker)
        {
            CurrentDirection = Direction.Right;
            this.map = map;
        }
    }
}
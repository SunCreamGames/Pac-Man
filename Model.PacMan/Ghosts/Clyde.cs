namespace Model.PacMan
{
    using System;

    public class Clyde : Ghost
    {
        private readonly Graph map;

        public Clyde(Graph map, Vertex vert, IGhostDecisionMaker ghostDecisionMaker) : base(map , vert, ghostDecisionMaker)
        {
            CurrentDirection = Direction.Left;
            this.map = map;
        }
    }
}
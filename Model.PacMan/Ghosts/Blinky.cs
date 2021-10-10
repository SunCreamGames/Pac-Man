namespace Model.PacMan
{
    using System;
    using System.Dynamic;

    public class Blinky : Ghost
    {
        private readonly Graph map;

        public Blinky(Graph map, Vertex vert, IGhostDecisionMaker ghostDecisionMaker) : base(map, vert, ghostDecisionMaker)
        {
            this.map = map;
            CurrentDirection = Direction.Up;
        }
    }
}
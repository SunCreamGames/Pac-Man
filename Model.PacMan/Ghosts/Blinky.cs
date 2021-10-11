namespace Model.PacMan
{
    using System;
    using System.Dynamic;

    public class Blinky : Ghost
    {
        public Blinky(Graph map, Vertex vert, IGhostDecisionMaker ghostDecisionMaker, Pacman pacman) : base(map, vert,
            ghostDecisionMaker, pacman)
        {
            close = 10;
            far = 40;
            CurrentDirection = Direction.Up;
            rand = new Random(3);
        }
    }
}
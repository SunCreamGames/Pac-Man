namespace Model.PacMan
{
    using System;

    public class Twinky : Ghost
    {
        public Twinky(Graph map, Vertex vert, IGhostDecisionMaker ghostDecisionMaker, Pacman pacman) : base(map, vert,
            ghostDecisionMaker, pacman)
        {
            close = 10;
            far = 15;
            CurrentDirection = Direction.Up;
            rand = new Random(9);
        }
    }
}
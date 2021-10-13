namespace Model.PacMan
{
    using System;

    public class Twinky : Ghost
    {
        public Twinky(Graph map, Vertex vert, GhostDecisionMaker ghostDecisionMaker, Pacman pacman) : base(map, vert,
            ghostDecisionMaker, pacman)
        {
            CurrentDirection = Direction.Up;
            rand = new Random(9);
        }
    }
}
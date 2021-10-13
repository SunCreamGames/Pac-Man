namespace Model.PacMan
{
    using System;
    using System.Dynamic;

    public class Blinky : Ghost
    {
        public Blinky(Graph map, Vertex vert, GhostDecisionMaker ghostDecisionMaker, Pacman pacman) : base(map, vert,
            ghostDecisionMaker, pacman)
        {
            CurrentDirection = Direction.Up;
            rand = new Random(3);
        }
    }
}
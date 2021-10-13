namespace Model.PacMan
{
    using System;

    public class Pinky : Ghost
    {
        public Pinky(Graph map, Vertex vert, GhostDecisionMaker ghostDecisionMaker, Pacman pacman) : base(map, vert,
            ghostDecisionMaker, pacman)
        {
            CurrentDirection = Direction.Right;
            rand = new Random(5);
        }
    }
}
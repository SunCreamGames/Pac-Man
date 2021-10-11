namespace Model.PacMan
{
    using System;

    public class Pinky : Ghost
    {
        public Pinky(Graph map, Vertex vert, IGhostDecisionMaker ghostDecisionMaker, Pacman pacman) : base(map, vert,
            ghostDecisionMaker, pacman)
        {
            close = 5;
            far = 15;
            CurrentDirection = Direction.Right;
            rand = new Random(5);
        }
    }
}
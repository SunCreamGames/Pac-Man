namespace Model.PacMan
{
    using System;

    public class Clyde : Ghost
    {
        public Clyde(Graph map, Vertex vert, IGhostDecisionMaker ghostDecisionMaker, Pacman pacman) : base(map, vert,
            ghostDecisionMaker, pacman)
        {
            close = 15;
            far = 30;
            CurrentDirection = Direction.Left;
            rand = new Random(13);
        }
    }
}
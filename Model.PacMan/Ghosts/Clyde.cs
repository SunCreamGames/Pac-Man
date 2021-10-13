namespace Model.PacMan
{
    using System;

    public class Clyde : Ghost
    {
        public Clyde(Graph map, Vertex vert, GhostDecisionMaker ghostDecisionMaker, Pacman pacman) : base(map, vert,
            ghostDecisionMaker, pacman)
        {
            CurrentDirection = Direction.Left;
            rand = new Random(13);
        }
    }
}
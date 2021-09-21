namespace Model.PacMan
{
    using System;

    public class Twinky : Ghost
    {
        private readonly Graph map;

        public Twinky(Graph map, Vertex vert) : base(map , vert)
        {
            CurrentDirection = Direction.Up;
            this.map = map;
            r = new Random(4);
        }
    }
}
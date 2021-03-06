namespace Model.PacMan
{
    using System;
    using System.Dynamic;

    public class Blinky : Ghost
    {
        private readonly Graph map;

        public Blinky(Graph map, Vertex vert) : base(map, vert)
        {
            this.map = map;
            CurrentDirection = Direction.Up;
            r = new Random(0);
        }
    }
}
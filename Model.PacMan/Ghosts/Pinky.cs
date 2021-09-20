namespace Model.PacMan
{
    public class Pinky : Ghost
    {
        private readonly Graph map;

        public Pinky(Graph map, Vertex vert) : base(map , vert)
        {
            CurrentDirection = Direction.Right;
            this.map = map;
        }
    }
}
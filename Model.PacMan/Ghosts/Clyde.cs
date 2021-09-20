namespace Model.PacMan
{
    public class Clyde : Ghost
    {
        private readonly Graph map;

        public Clyde(Graph map, Vertex vert) : base(map , vert)
        {
            CurrentDirection = Direction.Left;
            this.map = map;
        }
    }
}
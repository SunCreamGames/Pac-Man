namespace Model.PacMan
{
    public class Clyde : Ghost
    {
        private readonly Graph map;

        public Clyde(Graph map) : base(map)
        {
            this.map = map;
        }
    }
}
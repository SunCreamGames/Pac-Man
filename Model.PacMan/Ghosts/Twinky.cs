namespace Model.PacMan
{
    public class Twinky : Ghost
    {
        private readonly Graph map;

        public Twinky(Graph map) : base(map)
        {
            this.map = map;
        }
    }
}
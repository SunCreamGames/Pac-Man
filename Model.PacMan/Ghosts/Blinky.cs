namespace Model.PacMan
{
    public class Blinky : Ghost
    {
        private readonly Graph map;

        public Blinky(Graph map) : base(map)
        {
            this.map = map;
        }
    }
}
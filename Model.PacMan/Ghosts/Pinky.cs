namespace Model.PacMan
{
    public class Pinky : Ghost
    {
        private readonly Graph map;

        public Pinky(Graph map) : base(map)
        {
            this.map = map;
        }
    }
}
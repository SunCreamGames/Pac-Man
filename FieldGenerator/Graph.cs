namespace Model.PacMan
{
    using System.Collections.Generic;

    public class Graph
    {
        private List<Vertex> vertices;
    }

    class Vertex
    {
        private List<Vertex> neighbours;

        public (int, int) Coordinate { get; set; }

        public Vertex()
        {
            neighbours = new List<Vertex>();
        }


        public void AddNeighbour(Vertex neighbour)
        {
            if (neighbours.Contains(neighbour))
            {
                return;
            }

            neighbours.Add(neighbour);
            neighbour.AddNeighbour(this);
        }
    }
}
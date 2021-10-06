namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Vertex
    {
        public Vertex LVertex { get; private set; }
        public Vertex UVertex { get; private set; }
        public Vertex RVertex { get; private set; }
        public Vertex DVertex { get; private set; }

        public event Action<int, int> OnCoinEaten;
        public Walkablitity IsWalkable { get; private set; }
        public bool HasCoin { get; private set; }
        public (int, int) Coordinate { get; set; }

        public Vertex PreviousVertex { get; set; }

        public int Cost { get; set; }


        public Vertex(Walkablitity isWalkable, int x, int y)
        {
            Cost = Int32.MaxValue;
            IsWalkable = isWalkable;
            HasCoin = isWalkable == Walkablitity.Walkable;
            Coordinate = (x, y);
        }

        public void DestroyCoin()
        {
            HasCoin = false;
            OnCoinEaten?.Invoke(Coordinate.Item1, Coordinate.Item2);
        }

        public double GetFinalCost(Vertex vertex)
        {
            return Cost + GetHeuristicCost(vertex);
        }

        public void SetCoin()
        {
            HasCoin = true;
        }

        private double GetHeuristicCost(Vertex target)
        {
            return Math.Sqrt(Math.Pow(Coordinate.Item1 - target.Coordinate.Item1, 2) +
                             Math.Pow(Coordinate.Item2 - target.Coordinate.Item2, 2));
        }

        public void SetNeighbours(Vertex dVertex, Vertex rVertex, Vertex uVertex, Vertex lVertex)
        {
            DVertex = dVertex;
            RVertex = rVertex;
            UVertex = uVertex;
            LVertex = lVertex;
        }
    }
}
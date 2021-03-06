namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Graph
    {
        public Vertex[,] Vertices { get; }
        public int CoinsLeft => walkableVertices.Count(v => v.HasCoin);
        private List<Vertex> walkableVertices;
        public event Action<int, int> OnCoinEaten;

        private int coinsLeft;

        public Graph(int[,] mapMatrix)
        {
            Vertices = new Vertex[mapMatrix.GetLength(0), mapMatrix.GetLength(1)];
            walkableVertices = new List<Vertex>();
            coinsLeft = 0;
            for (int i = 0; i < Vertices.GetLength(0); i++)
            {
                for (int j = 0; j < Vertices.GetLength(1); j++)
                {
                    Walkablitity w = Walkablitity.Walkable;
                    if (mapMatrix[i, j] == 2)
                    {
                        w = Walkablitity.Wall;
                    }
                    else if (mapMatrix[i, j] == 3)
                    {
                        w = Walkablitity.Pen;
                    }

                    Vertices[i, j] = new Vertex(w, i, j);
                    if (Vertices[i, j].IsWalkable == Walkablitity.Walkable)
                    {
                        walkableVertices.Add(Vertices[i, j]);
                        Vertices[i, j].OnCoinEaten += CoinEaten;
                        coinsLeft++;
                    }
                }
            }

            SetNeighbours();
        }

        private void CoinEaten(int x, int y)
        {
            coinsLeft--;
            OnCoinEaten?.Invoke(x, y);
        }

        private void SetNeighbours()
        {
            for (int i = 0; i < Vertices.GetLength(0); i++)
            {
                for (int j = 0; j < Vertices.GetLength(1); j++)
                {
                    Vertex l = null;
                    Vertex u = null;
                    Vertex d = null;
                    Vertex r = null;
                    if (j > 0)
                    {
                        l = Vertices[i, j - 1];
                    }

                    if (i > 0)
                    {
                        u = Vertices[i - 1, j];
                    }

                    if (i < Vertices.GetLength(0) - 1)
                    {
                        d = Vertices[i + 1, j];
                    }

                    if (j < Vertices.GetLength(1) - 1)
                    {
                        r = Vertices[i, j + 1];
                    }

                    Vertices[i, j].SetNeighbours(d, r, u, l);
                }
            }
        }

        public Vertex GetRandomWalkableVertex()
        {
            var r = new Random();
            Vertex vert;
            do
            {
                vert = walkableVertices[r.Next(walkableVertices.Count)];
            } while (vert.IsWalkable != Walkablitity.Walkable);

            return vert;
        }
    }

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


        public Vertex(Walkablitity isWalkable, int x, int y)
        {
            IsWalkable = isWalkable;
            HasCoin = isWalkable == Walkablitity.Walkable;
            Coordinate = (x, y);
        }

        public void DestroyCoin()
        {
            HasCoin = false;
            OnCoinEaten?.Invoke(Coordinate.Item1, Coordinate.Item2);
        }

        public void SetCoin()
        {
            HasCoin = true;
        }

        public void SetNeighbours(Vertex dVertex, Vertex rVertex, Vertex uVertex, Vertex lVertex)
        {
            DVertex = dVertex;
            RVertex = rVertex;
            UVertex = uVertex;
            LVertex = lVertex;
        }
    }

    public enum Walkablitity
    {
        Walkable,
        Wall,
        Pen
    }
}
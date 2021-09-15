namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;

    public class Graph
    {
        private Vertex[,] vertices;

        public Graph(int[,] mapMatrix)
        {
            vertices = new Vertex[mapMatrix.GetLength(0), mapMatrix.GetLength(1)];
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                for (int j = 0; j < vertices.GetLength(1); j++)
                {
                    vertices[i, j] = new Vertex(mapMatrix[i, j] == 1);
                }
            }

            SetNeighbours();
        }

        private void SetNeighbours()
        {
            for (int i = 0; i < vertices.GetLength(0); i++)
            {
                for (int j = 0; j < vertices.GetLength(1); j++)
                {
                    Vertex l = null;
                    Vertex u = null;
                    Vertex d = null;
                    Vertex r = null;
                    if (i > 0)
                    {
                        u = vertices[i - 1, j];
                    }

                    if (j > 0)
                    {
                        l = vertices[i, j - 1];
                    }

                    if (j < vertices.GetLength(1) - 1)
                    {
                        r = vertices[i, j + 1];
                    }

                    if (i < vertices.GetLength(0) - 1)
                    {
                        d = vertices[i + 1, j];
                    }

                    vertices[i, j].SetNeighbours(d, r, u, l);
                }
            }
        }
    }

    class Vertex
    {
        public Vertex LVertex { get; private set; }
        public Vertex UVertex { get; private set; }
        public Vertex RVertex { get; private set; }
        public Vertex DVertex { get; private set; }

        public bool isWalkable { get; private set; }
        public (int, int) Coordinate { get; set; }


        public Vertex(bool isWalkable)
        {
            this.isWalkable = isWalkable;
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
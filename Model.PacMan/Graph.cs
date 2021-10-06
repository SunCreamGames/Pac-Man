namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Runtime.Remoting.Messaging;

    public class Graph
    {
        public Vertex[,] Vertices { get; }
        public int CoinsLeft => walkableVertices.Count(v => v.HasCoin);
        public string PathFindingAlgorithmName => currentPathFinder.GetName();

        private List<Vertex> walkableVertices, notWallVetritices;
        public event Action<int, int> OnCoinEaten;

        private int coinsLeft;
        private IPathFinder currentPathFinder;
        private List<IPathFinder> pathFinders;

        public Graph(int[,] mapMatrix, List<IPathFinder> pathFinders)
        {
            if (pathFinders.Count == 0)
            {
                throw new Exception("There is now pathfinders for constructing graph");
            }

            this.pathFinders = pathFinders;

            currentPathFinder = pathFinders[0];
            Vertices = new Vertex[mapMatrix.GetLength(0), mapMatrix.GetLength(1)];
            walkableVertices = new List<Vertex>();
            notWallVetritices = new List<Vertex>();
            coinsLeft = 0;
            for (int i = 0; i < Vertices.GetLength(0); i++)
            {
                for (int j = 0; j < Vertices.GetLength(1); j++)
                {
                    Walkablitity w = Walkablitity.Walkable;
                    if (mapMatrix[i, j] == 2)
                    {
                        w = Walkablitity.Wall;
                        Vertices[i, j] = new Vertex(w, i, j);
                    }
                    else if (mapMatrix[i, j] == 3)
                    {
                        w = Walkablitity.Pen;
                        Vertices[i, j] = new Vertex(w, i, j);
                        notWallVetritices.Add(Vertices[i, j]);
                    }

                    Vertices[i, j] = new Vertex(w, i, j);
                    if (Vertices[i, j].IsWalkable == Walkablitity.Walkable)
                    {
                        walkableVertices.Add(Vertices[i, j]);
                        notWallVetritices.Add(Vertices[i, j]);
                        Vertices[i, j].OnCoinEaten += CoinEaten;
                        coinsLeft++;
                    }
                }
            }

            SetNeighbours();
        }

        public void UpdatePathFinder()
        {
            var currentPFIndex = pathFinders.IndexOf(currentPathFinder);
            currentPathFinder = pathFinders[(currentPFIndex + 1) % (pathFinders.Count)];
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

        public async Task<(long, List<(int, List<Vertex>)>)> FindPath(Vertex playerCell, Vertex[] ghostCells)
        {
            currentPathFinder.SetPoints(playerCell, ghostCells);
            return await currentPathFinder.FindPath();
        }
    }


    public enum Walkablitity
    {
        Walkable,
        Wall,
        Pen
    }
}
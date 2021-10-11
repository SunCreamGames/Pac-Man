namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AStarPathfinder : IPathFinder
    {
        private const string Name = "A*";

        private Vertex start;
        private Vertex[] end;
        private Graph grid;


        public void SetPoints(Vertex start, Vertex[] end, Graph grid)
        {
            this.grid = grid;
            this.start = start;
            this.end = end;
        }


        public async Task<(long, List<(int, List<Vertex>)>)> FindPath()
        {
            if (start == null || end.Contains(null))
            {
                throw new Exception("Start or target cell is null");
            }


            var timer = System.Diagnostics.Stopwatch.StartNew();

            foreach (var vertex in grid.Vertices)
            {
                vertex.Cost = Int32.MaxValue;
            }

            var availableList = new List<Vertex>();
            var visitedList = new List<Vertex>();
            Vertex curVer;
            start.Cost = 0;
            availableList.Add(start);

            timer.Start();
            while (!end.All(visitedList.Contains))
            {
                curVer = availableList.OrderBy(v => v.Cost + grid.GetHeuristicCost(v, end)).First();
                foreach (var nVertex in curVer.Neighbours.Where(v =>
                    v.IsWalkable != Walkablitity.Wall && !visitedList.Contains(v)))
                {
                    if (nVertex.Cost < curVer.Cost + 1)
                    {
                        availableList.Add(nVertex);
                    }
                    else
                    {
                        nVertex.Cost = curVer.Cost + 1;
                        nVertex.PreviousVertex = curVer;
                        availableList.Add(nVertex);
                    }
                }


                visitedList.Add(curVer);
                availableList.Remove(curVer);
            }

            timer.Stop();

            var ways = new List<(int, List<Vertex>)>();
            foreach (var vertex in end)
            {
                var way = new List<Vertex>();
                way.Add(vertex);
                var v = vertex;
                while (v.PreviousVertex != null && v != start)
                {
                    way.Add(v.PreviousVertex);
                    v = v.PreviousVertex;
                }

                ways.Add((way[0].Cost, way));
            }

            return (timer.ElapsedTicks, ways);
        }

        public string GetName()
        {
            return Name;
        }
    }
}
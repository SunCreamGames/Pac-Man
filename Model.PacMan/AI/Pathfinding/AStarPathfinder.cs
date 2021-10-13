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
        private Vertex end;
        private Graph grid;


        public void SetPoints(Graph grid, Vertex start, Vertex end)
        {
            this.grid = grid;
            this.start = start;
            this.end = end;
        }


        public async Task<(int, List<Vertex>)> FindPath()
        {
            if (start == null || end == null)
            {
                // throw new Exception("Start or target cell is null");
                return await FindPath();
            }

            var distance = 0;
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
            while (!visitedList.Contains(end))
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

            var way = new List<Vertex>();
            curVer = end;
            way.Add(curVer);

            while (curVer != start)
            {
                if (curVer.PreviousVertex == null) break;

                way.Add(curVer.PreviousVertex);
                curVer = curVer.PreviousVertex;
                distance++;
            }


            visitedList = null;
            GC.Collect();
            return (distance, way);
        }

        public string GetName()
        {
            return Name;
        }
    }
}
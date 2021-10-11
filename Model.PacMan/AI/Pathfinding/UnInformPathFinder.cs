namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UnInformPathFinder : IPathFinder
    {
        private const string Name = "UIS";

        private Vertex start;
        private Vertex end;

        public void SetPoints(Graph grid, Vertex start, Vertex end)
        {
            this.start = start;
            this.end = end;
        }

        public async Task<(int, List<Vertex>)> FindPath()
        {
            List<Vertex> visited = new List<Vertex>();
            List<Vertex> available = new List<Vertex>();

            var distance = 0;
            var curVer = start;
            start.Cost = 0;
            available.Add(curVer);

            var timer = System.Diagnostics.Stopwatch.StartNew();
            timer.Start();

            while (!visited.Contains(end))
            {
                if (available.Count == 0)
                {
                    if (visited.Contains(end))
                    {
                        continue;
                    }

                    end.PreviousVertex = end.Neighbours
                        .Where(v => visited.Contains(v)).ToList().FirstOrDefault();
                    if (end.PreviousVertex == null)
                    {
                        throw new Exception("No ways to visit ghost cell");
                    }

                    visited.Add(end);
                }
            }

            timer.Stop();
            curVer = available.Aggregate((curMin, v) => (curMin == null || (v.Cost <
                                                                            curMin.Cost))
                ? v
                : curMin);

            visited.Add(curVer);
            available.Remove(curVer);
            timer.Start();

            var neighbours = new List<Vertex>()
                {curVer.DVertex, curVer.LVertex, curVer.RVertex, curVer.UVertex};
            neighbours = neighbours
                .Where(v => v != null && v.IsWalkable != Walkablitity.Wall && !visited.Contains(v) &&
                            !available.Contains(v)).ToList();
            foreach (var neighbour in neighbours)
            {
                available.Add(neighbour);
                neighbour.PreviousVertex = curVer;
                if (neighbour.Cost < Int32.MaxValue)
                {
                    if (neighbour.Cost > curVer.Cost + 1)
                    {
                        neighbour.Cost = curVer.Cost + 1;
                        neighbour.PreviousVertex = curVer;
                    }
                }
                else
                {
                    neighbour.Cost = curVer.Cost + 1;
                    neighbour.PreviousVertex = curVer;
                }
            }

            timer.Stop();
            var elapsedMs = timer.ElapsedTicks;

            var way = new List<Vertex>();
            curVer = end;
            way.Add(end);
            while (curVer != start)
            {
                way.Add(curVer.PreviousVertex);
                curVer = curVer.PreviousVertex;
                distance++;
            }

            available = null;
            visited = null;
            GC.Collect();
            return (distance, way);
        }

        public string GetName()
        {
            return Name;
        }
    }
}
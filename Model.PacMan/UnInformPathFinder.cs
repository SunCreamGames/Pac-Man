namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UnInformPathFinder : IPathFinder
    {
        public readonly string Name = "UIS";

        private Vertex start;
        private Vertex[] end;

        public void SetPoints(Vertex start, Vertex[] end)
        {
            this.start = start;
            this.end = end;
        }

        public async Task<(long, List<(int, List<Vertex>)>)> FindPath()
        {
            List<Vertex> available = new List<Vertex>() {start};
            List<Vertex> visited = new List<Vertex>();

            var distance = 0;
            var curVer = start;
            start.Cost = 0;


            var timer = System.Diagnostics.Stopwatch.StartNew();
            timer.Start();

            while (!(visited.Contains(end[0]) && visited.Contains(end[1]) && visited.Contains(end[2]) &&
                     visited.Contains(end[3])))
            {
                if (available.Count == 0)
                {
                    foreach (var vertex in end)
                    {
                        if (visited.Contains(vertex))
                        {
                            continue;
                        }

                        vertex.PreviousVertex = new List<Vertex>
                                {vertex.DVertex, vertex.LVertex, vertex.RVertex, vertex.UVertex}
                            .Where(v => visited.Contains(v)).ToList().FirstOrDefault();
                        if (vertex.PreviousVertex == null)
                        {
                            throw new Exception("No ways to visit ghost cell");
                        }

                        visited.Add(vertex);
                    }
                }

                curVer = available.Aggregate((curMin, v) => (curMin == null || (v.Cost <
                    curMin.Cost))
                    ? v
                    : curMin);

                visited.Add(curVer);
                available.Remove(curVer);
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
            }

            timer.Stop();
            var elapsedMs = timer.ElapsedMilliseconds;

            var result = new List<(int, List<Vertex>)>();
            foreach (var vertex in end)
            {
                var way = new List<Vertex>();
                curVer = vertex;
                while (curVer != start)
                {
                    way.Add(curVer);
                    curVer = curVer.PreviousVertex;
                    distance++;
                }

                result.Add((distance, way));
                distance = 0;
            }

            available = null;
            visited = null;
            GC.Collect();
            return (elapsedMs, result);
        }

        public string GetName()
        {
            return Name;
        }
    }
}
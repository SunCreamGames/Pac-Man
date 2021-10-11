namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BfsPathFinder : IPathFinder
    {
        private const string Name = "BFS";

        private Vertex start;
        private Vertex[] end;

        public void SetPoints(Vertex start, Vertex[] end, Graph grid)
        {
            this.start = start;
            this.end = end;
        }

        public async Task<(long, List<(int, List<Vertex>)>)> FindPath()
        {
            if (start == null || end.Contains(null))
            {
                throw new Exception("Start or target cell is null");
            }

            var visited = new List<Vertex>();
            int distance = 0;
            Vertex curVer = start;
            var available = new Queue<Vertex>();
            available.Enqueue(start);

            var timer = System.Diagnostics.Stopwatch.StartNew();
            timer.Start();

            while (!end.All(visited.Contains))
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
                        }
                        else
                        {
                            visited.Add(vertex);
                        }
                    }

                    continue;
                }

                curVer = available.Dequeue();
                var neighbours = new List<Vertex>() {curVer.DVertex, curVer.LVertex, curVer.RVertex, curVer.UVertex};
                neighbours = neighbours
                    .Where(v => v != null && v.IsWalkable != Walkablitity.Wall && !visited.Contains(v)).ToList();
                foreach (var neighbour in neighbours)
                {
                    available.Enqueue(neighbour);
                    neighbour.PreviousVertex = curVer;
                }

                visited.Add(curVer);
            }

            timer.Stop();
            var elapsedMs = timer.ElapsedTicks;

            var result = new List<(int, List<Vertex>)>();
            foreach (var vertex in end)
            {
                var way = new List<Vertex>();
                curVer = vertex;
                way.Add(curVer);
                while (curVer != start)
                {
                    if (curVer.PreviousVertex == null) break;

                    way.Add(curVer.PreviousVertex);
                    curVer = curVer.PreviousVertex;
                    distance++;
                }

                result.Add((distance, way));
                distance = 0;
            }

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
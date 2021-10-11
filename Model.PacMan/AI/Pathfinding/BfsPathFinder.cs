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
        private Vertex end;

        public void SetPoints(Graph grid, Vertex start, Vertex end)

        {
            this.start = start;
            this.end = end;
        }

        public async Task<(int, List<Vertex>)> FindPath()
        {
            if (start == null || end == (null))
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

            while (!visited.Contains(end))
            {
                if (available.Count == 0)
                {
                    if (visited.Contains(end))
                    {
                        continue;
                    }

                    end.PreviousVertex = end.Neighbours.Where(v => visited.Contains(v)).ToList().FirstOrDefault();
                    if (end.PreviousVertex != null)
                    {
                        visited.Add(end);
                    }

                    continue;
                }

                curVer = available.Dequeue();
                var neighbours = curVer.Neighbours.ToList();
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
namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DfsPathFinder : IPathFinder
    {
        private const string Name = "DFS";

        private Vertex start;
        private Vertex end;


        public void SetPoints(Graph grid, Vertex start, Vertex end)
        {
            this.start = start;
            this.end = end;
        }

        public async Task<(int, List<Vertex>)> FindPath()
        {
            if (start == null || end == null)
            {
                throw new Exception("Start or target cell is null");
            }

            var visited = new List<Vertex>();

            var distance = 0;
            var curVer = start;
            var available = new Stack<Vertex>();
            available.Push(start);


            while (!visited.Contains(end))
            {
                if (available.Count == 0)
                {
                    if (visited.Contains(end))
                    {
                        continue;
                    }

                    end.PreviousVertex = new List<Vertex>
                            {end.DVertex, end.LVertex, end.RVertex, end.UVertex}
                        .Where(v => visited.Contains(v)).ToList().FirstOrDefault();
                    if (end.PreviousVertex == null)
                    {
                        throw new Exception("No ways to visit ghost cell");
                    }

                    visited.Add(end);
                }

                curVer = available.Pop();
                var neighbours = new List<Vertex>() {curVer.RVertex, curVer.DVertex, curVer.LVertex, curVer.UVertex};
                neighbours = neighbours
                    .Where(v => v != null && v.IsWalkable != Walkablitity.Wall && !visited.Contains(v) &&
                                !available.Contains(v)).ToList();
                foreach (var neighbour in neighbours)
                {
                    available.Push(neighbour);
                    neighbour.PreviousVertex = curVer;
                }

                visited.Add(curVer);
            }

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
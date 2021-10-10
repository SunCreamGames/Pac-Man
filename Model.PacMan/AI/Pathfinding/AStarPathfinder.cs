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
            while (end.All(v => visitedList.Contains(v)))
            {
                curVer = availableList.First(v => Math.Abs(v.Cost + grid.GetHeuristicCost(v, end) -
                                                           availableList.Min(vert =>
                                                               vert.Cost + grid.GetHeuristicCost(vert, end))) < 0.001f);

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
                while (way.Last().PreviousVertex != start)
                {
                    way.Add(way.Last().PreviousVertex);
                }

                way.Add(start);
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
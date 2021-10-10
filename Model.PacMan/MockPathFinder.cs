namespace Model.PacMan
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MockPathFinder : IPathFinder
    {
        public void SetPoints(Vertex start, Vertex[] end, Graph g)
        {
            throw new System.NotImplementedException();
        }

        public Task<(long, List<(int, List<Vertex>)>)> FindPath()
        {
            throw new System.NotImplementedException();
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }
    }
}
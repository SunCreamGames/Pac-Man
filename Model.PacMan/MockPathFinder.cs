namespace Model.PacMan
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MockPathFinder : IPathFinder
    {
        public void SetPoints(Graph grid, Vertex start, Vertex end)
        {
            throw new System.NotImplementedException();
        }

        Task<(int, List<Vertex>)> IPathFinder.FindPath()
        {
            throw new System.NotImplementedException();
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }
    }
}
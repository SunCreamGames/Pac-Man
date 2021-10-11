namespace Model.PacMan
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPathFinder
    {
        void SetPoints(Graph grid, Vertex start, Vertex end);
        Task<(int, List<Vertex>)> FindPath();
        string GetName();
    }
}
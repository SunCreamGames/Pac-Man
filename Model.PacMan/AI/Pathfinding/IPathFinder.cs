namespace Model.PacMan
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPathFinder
    {
        
        void SetPoints(Vertex start, Vertex[] end, Graph grid);
        Task<(long, List<(int, List<Vertex>)>)> FindPath();
        string GetName();
    }
}
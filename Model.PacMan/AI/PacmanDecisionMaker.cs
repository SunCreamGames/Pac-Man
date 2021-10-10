namespace Model.PacMan
{
    using System.Collections.Generic;

    public class PacmanDecisionMaker : IDecisionMaker
    {
        private Vertex[] _ghosts;
        private Vertex position;
        private Graph _grid;


        public PacmanDecisionMaker(Graph grid)
        {
            _grid = grid;
        }

        public void SetPositions()
        {
            _ghosts = _grid.GetGhostCells();
        }
    }
}
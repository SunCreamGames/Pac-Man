namespace Model.PacMan
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PacmanDecisionMaker : IPacmanDecisionMaker
    {
        private Vertex[] _ghosts;
        private Vertex _position;
        private readonly Graph _grid;
        private Pacman _pacman;
        private IPathFinder _pathfinder;
        private Vertex _goal;
        private List<Vertex> _path;
        private Direction _aiDecision;

        public PacmanDecisionMaker(Graph grid, Pacman pacman, IPathFinder pathfinder)
        {
            _grid = grid;
            _pacman = pacman;
            _pathfinder = pathfinder;
        }

        public void SetPositions(Vertex[] ghosts, Vertex position)
        {
            _ghosts = ghosts;
            _position = position;
        }

        public void MakeDecision()
        {
            if (_goal == null || !_goal.HasCoin)
            {
                _goal = _grid.GetClosestCoinVertex(_position);
                _path = FindPath(_goal).Result;
            }

            _aiDecision = GetDirectionToTheVertex(_path);

            if (_aiDecision != Direction.None)
            {
                _pacman.SetInputDirection(_aiDecision);

                if (_pacman.CurrentDirection != _aiDecision)
                {
                    _pacman.UpdatePosition();
                }
            }
        }

        private Direction GetDirectionToTheVertex(List<Vertex> targetPath)
        {
            if (!targetPath.Contains(_position))
            {
                throw new Exception("Path doesn't contain curVer");
            }

            var indexOfCurrentVertex = targetPath.IndexOf(_position);

            if (targetPath.First() == _position)
            {
                throw new Exception("Pacman on position");
            }


            if (targetPath[indexOfCurrentVertex - 1] == _position.LVertex)
            {
                return Direction.Left;
            }

            if (targetPath[indexOfCurrentVertex - 1] == _position.RVertex)
            {
                return Direction.Right;
            }

            if (targetPath[indexOfCurrentVertex - 1] == _position.UVertex)
            {
                return Direction.Up;
            }

            if (targetPath[indexOfCurrentVertex - 1] == _position.DVertex)
            {
                return Direction.Down;
            }


            throw new Exception("Default case exception");
        }


        private async Task<List<Vertex>> FindPath(Vertex target)
        {
            _pathfinder.SetPoints(_position, new[] {target}, _grid);
            var res = await _pathfinder.FindPath();
            return res.Item2[0].Item2;
        }
    }
}
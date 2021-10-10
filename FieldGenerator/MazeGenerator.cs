namespace Model.PacMan
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Dynamic;
    using System.Linq;
    using System.Text;

    public class MazeGenerator : IMazeCreator
    {
        int[,] IMazeCreator.GenerateMap(int width = 5, int height = 9)
        {
            int[,] map;

            do
            {
                map = new int[9, 5];
                map[3, 0] = map[3, 1] = map[4, 0] = map[4, 1] = 1;
                map = FillWithTetramino(map);
                map = MakeRoads(map);

                map = MakeFullMap(map);
                map = AddFinalWall(CreateTiledMap(map));
            } while (IsFullConnected(map));

            return map;
        }

        private bool IsFullConnected(int[,] map)
        {
            var graph = new Graph(map, new List<IPathFinder>() {new MockPathFinder()});
            var start = graph.GetRandomWalkableVertex();
            var all = new List<Vertex>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (graph.Vertices[i, j].IsWalkable == Walkablitity.Walkable)
                    {
                        all.Add(graph.Vertices[i, j]);
                    }
                }
            }

            var visited = BFSVisiting(graph, start);
            var firstNotSecond = visited.Except(all).ToList();
            var secondNotFirst = all.Except(visited).ToList();
            return firstNotSecond.Any() || secondNotFirst.Any();
        }

        private List<Vertex> BFSVisiting(Graph graph, Vertex start)
        {
            var visited = new List<Vertex>();
            var available = new Queue<Vertex>();
            available.Enqueue(start);

            while (available.Count > 0)
            {
                if (available.Peek().LVertex != null && available.Peek().LVertex.IsWalkable == Walkablitity.Walkable &&
                    !visited.Contains(available.Peek().LVertex))
                {
                    available.Enqueue(available.Peek().LVertex);
                }

                if (available.Peek().DVertex != null && available.Peek().DVertex.IsWalkable == Walkablitity.Walkable &&
                    !visited.Contains(available.Peek().DVertex))
                {
                    available.Enqueue(available.Peek().DVertex);
                }

                if (available.Peek().UVertex != null && available.Peek().UVertex.IsWalkable == Walkablitity.Walkable &&
                    !visited.Contains(available.Peek().UVertex))
                {
                    available.Enqueue(available.Peek().UVertex);
                }

                if (available.Peek().RVertex != null && available.Peek().RVertex.IsWalkable == Walkablitity.Walkable &&
                    !visited.Contains(available.Peek().RVertex))
                {
                    available.Enqueue(available.Peek().RVertex);
                }

                visited.Add(available.Dequeue());
            }

            return visited;
        }

        private int[,] AddFinalWall(int[,] createTiledMap)
        {
            int[,] map = new int[createTiledMap.GetLength(0) + 2, createTiledMap.GetLength(1) + 2];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i == 0 || i == map.GetLength(0) - 1 || j == 0 || j == map.GetLength(1) - 1)
                    {
                        map[i, j] = 2;
                    }
                    else
                    {
                        map[i, j] = createTiledMap[i - 1, j - 1];
                    }
                }
            }

            return map;
        }

        private int[,] MakeFullMap(int[,] halfMap)
        {
            halfMap = ReverseMatrix(halfMap);
            int[,] map = new int[halfMap.GetLength(0), halfMap.GetLength(1) * 2];
            for (int i = 0; i < halfMap.GetLength(0); i++)
            {
                for (int j = 0; j < halfMap.GetLength(1); j++)
                {
                    map[i, j] = halfMap[i, j];
                }
            }

            map = ReverseMatrix(map);
            for (int i = 0; i < halfMap.GetLength(0); i++)
            {
                for (int j = 0; j < halfMap.GetLength(1); j++)
                {
                    map[i, j] = halfMap[i, j];
                }
            }

            map = PolishMap(map);
            return map;
        }

        private int[,] CreateTiledMap(int[,] map)
        {
            var resultMap = new int[map.GetLength(0), map.GetLength(1) * 2 - 1];

            for (int i = 0; i < resultMap.GetLength(0); i++)
            {
                for (int j = 0; j < resultMap.GetLength(1); j++)
                {
                    if (j >= 7 && j <= 13 && (i == 7 || i == 9) || i == 8 && (j == 7 || j == 13))
                    {
                        if (i == 7 && j == 10)
                        {
                            resultMap[i, j] = 3;
                            continue;
                        }

                        resultMap[i, j] = 2;
                        continue;
                    }

                    if (j >= 7 && j <= 13 && i >= 7 && i <= 9)
                    {
                        resultMap[i, j] = 3;
                        continue;
                    }


                    if (i % 2 == 0)
                    {
                        if (j == 0)
                        {
                            resultMap[i, j] = map[i, j];
                        }
                        else if (j % 2 == 0)
                        {
                            if (map[i, j / 2 - 1] == 1 || j / 2 < map.GetLength(1) && map[i, j / 2] == 1)
                            {
                                resultMap[i, j] = 1;
                            }
                        }
                        else
                        {
                            resultMap[i, j] = map[i, j / 2];
                        }
                    }
                    else
                    {
                        if (j % 2 == 1)
                        {
                            resultMap[i, j] = 2;
                        }
                        else
                        {
                            if (j / 2 < map.GetLength(1))
                            {
                                resultMap[i, j] = map[i, j / 2];
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < resultMap.GetLength(0); i++)
            {
                for (int j = 0; j < resultMap.GetLength(1); j++)
                {
                    if (resultMap[i, j] == 0 || resultMap[i, j] == 2)
                    {
                        if (j == resultMap.GetLength(1) - 1)
                        {
                            resultMap[i, j] = 1;
                        }

                        else if (j == 0)
                        {
                            resultMap[i, j] = 1;
                        }

                        else if (i == 0)
                        {
                            resultMap[i, j] = 1;
                        }
                        else if (i == resultMap.GetLength(0) - 1)
                        {
                            resultMap[i, j] = 1;
                        }
                        else
                        {
                            if (resultMap[i, j] == 2)
                            {
                                continue;
                            }

                            if (resultMap[i, j - 1] == 1 && resultMap[i, j + 1] == 1 ||
                                resultMap[i - 1, j] == 1 && resultMap[i + 1, j] == 1)
                            {
                                resultMap[i, j] = 1;
                            }
                        }

                        if (resultMap[i, j] == 0)
                        {
                            resultMap[i, j] = 2;
                        }
                    }
                }
            }

            return resultMap;
        }

        private int[,] PolishMap(int[,] map)
        {
            int[,] result = new int[map.GetLength(0), map.GetLength(1) - 1];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        result[i, j] = map[i, j + 1];
                    }
                    else
                    {
                        if (j * 2 >= map.GetLength(1))
                        {
                            result[i, j] = map[i, j + 1];
                        }
                        else
                        {
                            result[i, j] = map[i, j];
                        }
                    }
                }
            }

            return result;
        }

        private int[,] ReverseMatrix(int[,] mat)
        {
            var newMat = new int[mat.GetLength(0), mat.GetLength(1)];
            for (int i = 0; i < newMat.GetLength(0); i++)
            {
                for (int j = 0; j < (newMat.GetLength(1)); j++)
                {
                    newMat[i, j] = mat[i, mat.GetLength(1) - 1 - j];
                }
            }

            return newMat;
        }

        private int[,] MakeRoads(int[,] halfMap)
        {
            var r = new Random();
            var mapWithRoads = new int[halfMap.GetLength(0) * 2 + 1, halfMap.GetLength(1) + 1];

            // 0 == didn't calculated

            // 2 == no path
            // 3 == not exist

            // 1 == path

            mapWithRoads[6, 0] = mapWithRoads[6, 1] =
                mapWithRoads[10, 0] = mapWithRoads[10, 1] = mapWithRoads[7, 2] = mapWithRoads[9, 2] = 1;
            for (int i = 0; i < mapWithRoads.GetLength(0); i++)
            {
                for (int j = 0; j < mapWithRoads.GetLength(1); j++)
                {
                    #region Pen

                    if (j < 2 && i > 6 && i < 10)
                    {
                        continue;
                    }

                    if ((i == 10 || i == 6) && j < 2 ||
                        j == 2 && (i == 7 || i == 9))
                    {
                        mapWithRoads[i, j] = 1;
                        continue;
                    }

                    #endregion

                    #region Borders

                    if (i == 0)
                    {
                        if (j == 5)
                        {
                            mapWithRoads[i, j] = 3;
                        }
                        else
                        {
                            mapWithRoads[i, j] = 1;
                        }

                        continue;
                    }


                    if (j == 0 || (j == 5 && i % 2 == 1))
                    {
                        if (i == 18)
                        {
                            mapWithRoads[i, j] = 1;
                        }

                        mapWithRoads[i, j] = r.Next(1, 3);
                        continue;
                    }

                    if (j == 5 && i % 2 == 0)
                    {
                        mapWithRoads[i, j] = 3;
                        continue;
                    }

                    if (i == mapWithRoads.GetLength(0) - 1)
                    {
                        mapWithRoads[i, j] = 1;
                        continue;
                    }

                    #endregion

                    if (i % 2 == 0)
                    {
                        if (j == halfMap.GetLength(1))
                        {
                            continue;
                        }

                        if (halfMap[i / 2, j] != halfMap[i / 2 - 1, j])
                        {
                            mapWithRoads[i, j] = 1;
                        }
                        else
                        {
                            mapWithRoads[i, j] = 2;
                        }
                    }
                    else
                    {
                        if (j == halfMap.GetLength(1))
                        {
                            continue;
                        }

                        if (halfMap[i / 2, j] != halfMap[i / 2, j - 1])
                        {
                            mapWithRoads[i, j] = 1;
                        }
                        else
                        {
                            mapWithRoads[i, j] = 2;
                        }
                    }
                }
            }

            return mapWithRoads;
        }

        private int[,] FillWithTetramino(int[,] helpPiece)
        {
            var nextPieceNumber = 2;
            while (HasZeros(helpPiece))
            {
                helpPiece = Cover(helpPiece, nextPieceNumber);
                nextPieceNumber++;
            }

            return helpPiece;
        }

        private static int[,] Cover(int[,] matrix, int num)
        {
            var r = new Random();
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if (matrix[i, j] == 0)
                    {
                        var power = r.Next(0, 4);
                        if (power != 3) power = 2;

                        matrix[i, j] = num;
                        List<(int, int)> coords = new List<(int, int)> {(i, j)};
                        var iterCount = 0;
                        while (power != 0 && iterCount < 30)
                        {
                            iterCount++;
                            var randomVertexCoords = coords[r.Next(0, coords.Count)];
                            var order = r.Next(0, 8);


                            if (order == 0)
                            {
                                if (randomVertexCoords.Item2 >= matrix.GetLength(1) - 1)
                                {
                                    continue;
                                }

                                else if (matrix[randomVertexCoords.Item1, randomVertexCoords.Item2 + 1] == 0)
                                {
                                    power--;
                                    coords.Add((randomVertexCoords.Item1, randomVertexCoords.Item2 + 1));

                                    matrix[randomVertexCoords.Item1, randomVertexCoords.Item2 + 1] = num;
                                }
                                else
                                {
                                    order = 1;
                                }
                            }

                            if (order % 2 == 1)
                            {
                                if (randomVertexCoords.Item1 >= matrix.GetLength(0) - 1)
                                {
                                    continue;
                                }

                                else if (matrix[randomVertexCoords.Item1 + 1, randomVertexCoords.Item2] == 0)
                                {
                                    power--;
                                    coords.Add((randomVertexCoords.Item1 + 1, randomVertexCoords.Item2));

                                    matrix[randomVertexCoords.Item1 + 1, randomVertexCoords.Item2] = num;
                                }
                                else
                                {
                                    order = 2;
                                }
                            }

                            if (order % 2 == 0 && order != 0)
                            {
                                if (randomVertexCoords.Item1 <= 0)
                                {
                                    continue;
                                }

                                if (matrix[randomVertexCoords.Item1 - 1, randomVertexCoords.Item2] == 0)
                                {
                                    power--;
                                    matrix[randomVertexCoords.Item1 - 1, randomVertexCoords.Item2] = num;
                                    coords.Add((randomVertexCoords.Item1 - 1, randomVertexCoords.Item2));
                                }
                            }
                        }

                        num++;
                    }
                }
            }

            return matrix;
        }

        private static bool HasZeros(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public static void Print(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // if (i % 2 != 0)
                // {
                //     Console.Write("  ");
                // }

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    switch (matrix[i, j])
                    {
                        case 0:
                            Console.Write(" ? ");
                            break;
                        case 1:
                            Console.Write(" 0 ");
                            break;
                        case 2:
                            Console.Write("   ");
                            break;
                        default:
                            Console.Write($" {matrix[i, j]} ");
                            break;
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
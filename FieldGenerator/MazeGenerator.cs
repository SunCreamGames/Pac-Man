namespace Model.PacMan
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public static class MazeGenerator
    {
        public static List<int[,]> GenerateMap(int width = 30, int height = 30)
        {
            int[,] matrix = new int[height, width];

            var result = new List<int[,]>();

            int[,] halfMap = new int[9, 5];
            halfMap[3, 0] = halfMap[3, 1] = halfMap[4, 0] = halfMap[4, 1] = 1;


            halfMap = FillWithTetramino(halfMap);
            halfMap = MakeRoads(halfMap);

            result.Add(halfMap);
            // Print(helpPiece);
            // return halfMap;
            var roadMap = MakeFullMap(halfMap);
            result.Add(roadMap);
            return result;
        }

        private static int[,] MakeFullMap(int[,] halfMap)
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

            return PolishMap(map);
        }

        private static int[,] PolishMap(int[,] map)
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

        private static int[,] ReverseMatrix(int[,] mat)
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

        private static int[,] MakeRoads(int[,] halfMap)
        {
            var r = new Random();
            var mapWithRoads = new int[19, 6];

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

                    if (i == 18)
                    {
                        mapWithRoads[i, j] = 1;
                        continue;
                    }

                    #endregion

                    if (i % 2 == 0)
                    {
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

        private static int[,] FillWithTetramino(int[,] helpPiece)
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
                            Console.Write(" _ ");
                            break;
                        case 1:
                            Console.Write(" 0 ");
                            break;
                        case 2:
                            Console.Write(" X ");
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
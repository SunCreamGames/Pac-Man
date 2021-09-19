namespace Model.PacMan
{
    using System;
    using System.IO;

    public class MockMazeCreator : IMazeCreator
    {
        public int[,] GenerateMap(int w = 30, int h = 30)
        {
            var map = new int[w, h];
            var r = new Random();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i == 0 || j == 0 || i == map.GetLength(0) - 1 || j == map.GetLength(1) - 1)
                    {
                        map[i, j] = 2;
                    }
                    else
                    {
                        if (map[i, j] != 0) continue;

                        if (r.Next(0, 5) == 4)
                        {
                            map[i, j] = 2;
                            if (r.Next(0, 4) == 0)
                            {
                                map[i, j + r.Next(-1, 2)] = 2;
                            }

                            if (r.Next(0, 4) == 0)
                            {
                                map[i + r.Next(-1, 2), j] = 2;
                            }
                        }
                        else
                        {
                            map[i, j] = 1;
                        }
                    }
                }
            }

            var writer = new StreamWriter("C:\\Users\\Богдан\\Desktop\\map.txt");
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 1)
                        writer.Write(" ");
                    else
                    {
                        writer.Write("X");
                    }
                }

                writer.WriteLine();
            }

            writer.Close();
            return map;
        }
    }
}
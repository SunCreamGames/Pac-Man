using System;
using Xunit;

namespace TestsM
{
    using System.Collections.Generic;
    using System.IO;
    using Model.PacMan;

    public class Tests
    {
        [Fact]
        public async  void Test1()
        {
            var dfsFPathfinder = new DfsPathFinder();
            var matrix = ReadMatrix();
            var a = new Graph(matrix, new List<IPathFinder>() {dfsFPathfinder});
            dfsFPathfinder.SetPoints(a.Vertices[1, 1],
                new Vertex[] {a.Vertices[1, 12], a.Vertices[7, 1], a.Vertices[7, 12], a.Vertices[7, 7]}, a);
            var x = new List<List<Vertex>>();
            var result = await dfsFPathfinder.FindPath();


            WriteMatrix(a, result);
            Assert.Equal(result.Item2[0].Item2,
                new List<Vertex>()
                {
                    a.Vertices[1, 12], a.Vertices[1, 11], a.Vertices[1, 10], a.Vertices[1, 9], a.Vertices[1, 8],
                    a.Vertices[1, 7], a.Vertices[1, 6], a.Vertices[1, 5], a.Vertices[1, 4], a.Vertices[1, 3],
                    a.Vertices[1, 2]
                });
        }

        private void WriteMatrix(Graph g, ( long, List<(int, List<Vertex>)>) ways)
        {
            StreamWriter writer = new StreamWriter("C:\\Users\\Богдан\\Desktop\\textMap1.txt");
            var matrix = new int[g.Vertices.GetLength(0), g.Vertices.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (ways.Item2[0].Item2.Contains(g.Vertices[i, j]))
                    {
                        writer.Write("1");
                    }
                    else if (ways.Item2[1].Item2.Contains(g.Vertices[i, j]))
                    {
                        writer.Write("2");
                    }
                    else if (ways.Item2[2].Item2.Contains(g.Vertices[i, j]))
                    {
                        writer.Write("3");
                    }
                    else if (ways.Item2[3].Item2.Contains(g.Vertices[i, j]))
                    {
                        writer.Write("4");
                    }
                    else
                    {
                        if (g.Vertices[i, j].IsWalkable == Walkablitity.Wall)
                        {
                            writer.Write("X");
                        }
                        else
                        {
                            writer.Write(" ");
                        }
                    }
                }

                writer.WriteLine();
            }

            writer.WriteLine(ways.Item1);
            writer.WriteLine(ways.Item2);
            writer.Close();
        }

        private int[,] ReadMatrix()
        {
            StreamReader streamReader = new StreamReader("C:\\Users\\Богдан\\Desktop\\textMap.txt");
            var size = streamReader.ReadLine().Split(' ');
            var matrix = new int[Int32.Parse(size[0]), Int32.Parse(size[1])];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = (streamReader.Read() == '1') ? 1 : 2;
                }

                streamReader.ReadLine();
            }

            return matrix;
        }
    }
}
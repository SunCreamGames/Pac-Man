namespace Tester
{
    using System;
    using Model.PacMan;

    internal class Program
    {
        public static void Main(string[] args)
        {
            var maps = MazeGenerator.GenerateMap();
            foreach (var map in maps)
            {
                MazeGenerator.Print(map);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
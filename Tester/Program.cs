namespace Tester
{
    using Model.PacMan;

    internal class Program
    {
        public static void Main(string[] args)
        {
           var map = MazeGenerator.GenerateMap();
           MazeGenerator.Print(map)
        }
    }
}
namespace Model.PacMan
{
    public interface IMazeCreator
    {
        int[,] GenerateMap(int w, int h);
    }
}
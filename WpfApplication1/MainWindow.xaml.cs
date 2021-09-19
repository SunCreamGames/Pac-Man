namespace WpfApplication1
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Model.PacMan;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Game game;
        private Pacman player;
        private Ghost ghost;
        private Direction direction;

        private ImageSource coinPic = new BitmapImage(new Uri("Sprites/Field/point.png"));


        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoad;
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            game = new Game();
            direction = Direction.Left;
            DrawWalls(game.map);
            game.DrawCall += DrawCall;
        }

        private void DrawWalls(Graph map)
        {
            for (int i = 0; i < map.Vertices.GetLength(0); i++)
            {
                for (int j = 0; j < map.Vertices.GetLength(1); j++)
                {
                    if (map.Vertices[i, j].IsWalkable && map.Vertices[i, j].HasCoin)
                    {
                        var coin = new ImageBrush();
                        coin.ImageSource = coinPic;
                    }
                }
            }
        }

        private void DrawCall(Graph arg1, Pacman arg2, Ghost[] arg3, int arg4)
        {
        }
    }
}
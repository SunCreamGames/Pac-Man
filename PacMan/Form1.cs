using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pac_Man
{
    using System.Timers;
    using Model.PacMan;

    public partial class Form1 : Form
    {
        private Game game;
        private Direction direction;

        private PictureBox pacMan;
        private bool paused;
        Image wall = Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Field\\wall.png");

        Image coin = Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Field\\point.png");

        private List<PictureBox> coins;

        private Image[] pacmanImages = new Image[]
        {
            Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\3_packman.png"),
            Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\L_1_packman.png"),
            Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\L_2_packman.png"),
            Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\U_1_packman.png"),
            Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\U_2_packman.png"),
            Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\R_1_packman.png"),
            Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\R_2_packman.png"),
            Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\D_1_packman.png"),
            Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\D_2_packman.png"),
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game();
            paused = false;
            direction = Direction.Left;
            game.DrawCall += DrawFrame;
            var v = game.map.GetRandomWalkableVertex();
            pacMan = new PictureBox()
            {
                Parent = this,
                Image = pacmanImages[0],
                Size = new Size(16, 16),
                Enabled = true,
                Visible = true
            };
            coins = new List<PictureBox>();
            DrawMap(game.map);
            DrawCoins(game.map);
        }

        private void DrawFrame(Graph graphMap, Pacman pacman, Ghost[] ghosts, int frameNumber)
        {
            DrawPacman(pacman, frameNumber);
        }

        private void DrawCoins(Graph graphMap)
        {
            for (int i = 0; i < graphMap.Vertices.GetLength(0); i++)
            {
                for (int j = 0; j < graphMap.Vertices.GetLength(1); j++)
                {
                    if (graphMap.Vertices[i, j].HasCoin)
                    {
                        var p = new PictureBox()
                        {
                            Parent = this,
                            Location = new Point(16 * (j + 1) - 2, 16 * (i + 1) - 2),
                            Image = coin,
                            Size = new Size(4, 4),
                            SizeMode = PictureBoxSizeMode.CenterImage,
                            Enabled = true,
                            Visible = true,
                        };
                        coins.Add(p);                                                                                                                                   
                    }
                }
            }
        }

        private void DrawPacman(Pacman pacman, int frameNumber)
        {
            Image curImg;

            switch (pacman.curentDirection)
            {
                case Direction.Left:
                    if (frameNumber % 2 == 0)
                        curImg = pacmanImages[1];

                    else
                        curImg = pacmanImages[2];

                    break;

                case Direction.Up:
                    if (frameNumber % 2 == 0)
                        curImg = pacmanImages[3];

                    else
                        curImg = pacmanImages[4];

                    break;

                case Direction.Right:
                    if (frameNumber % 2 == 0)
                        curImg = pacmanImages[5];

                    else
                        curImg = pacmanImages[6];

                    break;

                case Direction.Down:
                    if (frameNumber % 2 == 0)
                        curImg = pacmanImages[7];

                    else
                        curImg = pacmanImages[8];

                    break;

                default:
                    curImg = pacmanImages[0];

                    break;
            }

            pacMan.Image = curImg;
            pacMan.Location = new Point(pacman.Position.X, pacman.Position.Y);
        }

        private void DrawMap(Graph graphMap)
        {
            for (int i = 0; i < graphMap.Vertices.GetLength(0); i++)
            {
                for (int j = 0; j < graphMap.Vertices.GetLength(1); j++)
                {
                    if (graphMap.Vertices[i, j].IsWalkable == Walkablitity.Wall)
                    {
                        var p = new PictureBox()
                        {
                            Parent = this,
                            Location = new Point(8 + 16 * j, 8 + 16 * i),
                            Image = wall, Size = new Size(16, 16),
                            Enabled = true,
                            Visible = true,
                        };
                    }
                }
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!paused)
                game.UpdateFrame(direction);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    direction = Direction.Up;
                    break;
                case Keys.Space:
                    paused = !paused;
                    break;
                case Keys.Down:
                    direction = Direction.Down;
                    break;
                case Keys.Right:
                    direction = Direction.Right;
                    break;
                case Keys.Left:
                    direction = Direction.Left;
                    break;
            }
        }
    }
}
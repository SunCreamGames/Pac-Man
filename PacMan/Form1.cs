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
    using System.Net;
    using System.Threading;
    using System.Timers;
    using Model.PacMan;

    public partial class Form1 : Form
    {
        private Game game;
        private Direction direction;
        private PictureBox[] liveBoxes;
        private PictureBox pacMan;
        private PictureBox redBox, orangeBox, pinkBox, blueBox;
        private bool paused;
        Image wall, coin;

        private WinWindow winWindow;
        private LoseWindow loseWindow;

        private List<PictureBox> coins;

        private Image[] pacmanImages, redImages, pinkImages, orangeImages, blueImages;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadImages();
            Size = new Size(new Point(400, 420));
            game = new Game(new MazeGenerator());
            direction = Direction.Left;
            game.DrawCall += DrawFrame;
            game.OnCoinEaten += DestroyCoin;
            game.OnLevelCompleted += GameWinWindow;
            game.OnPacmanDie += OnPacmanDie;
            var v = game.map.GetRandomWalkableVertex();
            liveBoxes = new PictureBox[game.LivesCount];
            for (int i = 0; i < liveBoxes.Length; i++)
            {
                liveBoxes[i] = new PictureBox();
                liveBoxes[i].Visible = true;
                liveBoxes[i].Enabled = true;
                liveBoxes[i].Size = new Size(32, 32);
                liveBoxes[i].Parent = this;
                liveBoxes[i].Image = pacmanImages[6];
                liveBoxes[i].Location = new Point(10 + i * 30, game.map.Vertices.GetLength(0) * 17);
            }

            pacMan = new PictureBox()
            {
                Parent = this,
                Image = pacmanImages[0],
                Size = new Size(16, 16),
                Enabled = true,
                Visible = true
            };
            redBox = new PictureBox()
            {
                Parent = this,
                Image = redImages[0],
                Size = new Size(16, 16),
                Enabled = true,
                Visible = true
            };
            blueBox = new PictureBox()
            {
                Parent = this,
                Image = blueImages[0],
                Size = new Size(16, 16),
                Enabled = true,
                Visible = true
            };
            pinkBox = new PictureBox()
            {
                Parent = this,
                Image = pinkImages[0],
                Size = new Size(16, 16),
                Enabled = true,
                Visible = true
            };
            orangeBox = new PictureBox()
            {
                Parent = this,
                Image = orangeImages[0],
                Size = new Size(16, 16),
                Enabled = true,
                Visible = true
            };
            coins = new List<PictureBox>();
            DrawMap(game.map);
            DrawCoins(game.map);
            paused = true;
        }


        private void OnPacmanDie(int lives)
        {
            UpdateLivesIndicator(lives);
            if (lives <= 0)
            {
                GameLoseWidow();
            }
        }

        private void UpdateLivesIndicator(int lives)
        {
            for (int i = 0; i < liveBoxes.Length; i++)
            {
                if (i <= lives - 1)
                    liveBoxes[i].Visible = true;
                else
                    liveBoxes[i].Visible = false;
                game.SpawnActors();
                paused = false;
            }
        }

        private void GameLoseWidow()
        {
            game.DrawCall -= DrawFrame;
            game.OnCoinEaten -= DestroyCoin;
            game.OnLevelCompleted -= GameWinWindow;
            game.OnPacmanDie -= OnPacmanDie;

            Hide();
            loseWindow = new LoseWindow(this);
            loseWindow.Show();
            loseWindow.Closed += (s, args) => { Close(); };
        }

        private void GameWinWindow()
        {
            game.DrawCall -= DrawFrame;
            game.OnCoinEaten -= DestroyCoin;
            game.OnLevelCompleted -= GameWinWindow;
            game.OnPacmanDie -= OnPacmanDie;

            Hide();
            winWindow = new WinWindow(this);
            winWindow.Show();
            winWindow.Closed += (s, args) => { Close(); };
        }


        private void DestroyCoin(int x, int y)
        {
            var eatenCoins = coins.Where(pB => pB.Bounds.IntersectsWith(pacMan.Bounds));
            foreach (var eatenCoin in eatenCoins)
            {
                eatenCoin.Enabled = false;
                eatenCoin.Visible = false;
            }

            UpdateScoreLabel();
        }

        private void DrawFrame(Graph graphMap, Pacman pacman, Ghost red, Ghost orange, Ghost pink, Ghost blue,
            int frameNumber)
        {
            DrawPacman(pacman, frameNumber);
            DrawGhosts(red, orange, pink, blue, frameNumber);
        }

        private void DrawGhosts(Ghost red, Ghost orange, Ghost pink, Ghost blue, int frameNumber)
        {
            Image curImg;

            switch (red.CurrentDirection)
            {
                case Direction.Down:
                    if (frameNumber % 2 == 0)
                        curImg = redImages[0];

                    else
                        curImg = redImages[1];

                    break;

                case Direction.Left:
                    if (frameNumber % 2 == 0)
                        curImg = redImages[2];

                    else
                        curImg = redImages[3];

                    break;

                case Direction.Up:
                    if (frameNumber % 2 == 0)
                        curImg = redImages[4];

                    else
                        curImg = redImages[5];

                    break;

                case Direction.Right:
                    if (frameNumber % 2 == 0)
                        curImg = redImages[6];

                    else
                        curImg = redImages[7];

                    break;

                default:
                    curImg = redImages[0];

                    break;
            }

            redBox.Image = curImg;
            redBox.Location = new Point(red.Position.X, red.Position.Y);

            switch (pink.CurrentDirection)
            {
                case Direction.Down:
                    if (frameNumber % 2 == 0)
                        curImg = pinkImages[0];

                    else
                        curImg = pinkImages[1];

                    break;

                case Direction.Left:
                    if (frameNumber % 2 == 0)
                        curImg = pinkImages[2];

                    else
                        curImg = pinkImages[3];

                    break;

                case Direction.Up:
                    if (frameNumber % 2 == 0)
                        curImg = pinkImages[4];

                    else
                        curImg = pinkImages[5];

                    break;

                case Direction.Right:
                    if (frameNumber % 2 == 0)
                        curImg = pinkImages[6];

                    else
                        curImg = pinkImages[7];

                    break;

                default:
                    curImg = pinkImages[0];

                    break;
            }

            pinkBox.Image = curImg;
            pinkBox.Location = new Point(pink.Position.X, pink.Position.Y);

            switch (orange.CurrentDirection)
            {
                case Direction.Down:
                    if (frameNumber % 2 == 0)
                        curImg = orangeImages[0];

                    else
                        curImg = orangeImages[1];

                    break;

                case Direction.Left:
                    if (frameNumber % 2 == 0)
                        curImg = orangeImages[2];

                    else
                        curImg = orangeImages[3];

                    break;

                case Direction.Up:
                    if (frameNumber % 2 == 0)
                        curImg = orangeImages[4];

                    else
                        curImg = orangeImages[5];

                    break;

                case Direction.Right:
                    if (frameNumber % 2 == 0)
                        curImg = orangeImages[6];

                    else
                        curImg = orangeImages[7];

                    break;

                default:
                    curImg = orangeImages[0];

                    break;
            }

            orangeBox.Image = curImg;
            orangeBox.Location = new Point(orange.Position.X, orange.Position.Y);
            switch (blue.CurrentDirection)
            {
                case Direction.Down:
                    if (frameNumber % 2 == 0)
                        curImg = blueImages[0];

                    else
                        curImg = blueImages[1];

                    break;

                case Direction.Left:
                    if (frameNumber % 2 == 0)
                        curImg = blueImages[2];

                    else
                        curImg = blueImages[3];

                    break;

                case Direction.Up:
                    if (frameNumber % 2 == 0)
                        curImg = blueImages[4];

                    else
                        curImg = blueImages[5];

                    break;

                case Direction.Right:
                    if (frameNumber % 2 == 0)
                        curImg = blueImages[6];

                    else
                        curImg = blueImages[7];

                    break;

                default:
                    curImg = blueImages[0];

                    break;
            }

            blueBox.Image = curImg;
            blueBox.Location = new Point(blue.Position.X, blue.Position.Y);
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

            switch (pacman.CurrentDirection)
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

        private void LoadImages()
        {
            pacmanImages = new[]
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
            redImages = new Image[]
            {
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\blinky_d_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\blinky_d_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\blinky_l_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\blinky_l_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\blinky_u_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\blinky_u_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\blinky_r_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\blinky_r_2.png"),
            };
            pinkImages = new Image[]
            {
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\pinky_d_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\pinky_d_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\pinky_l_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\pinky_l_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\pinky_u_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\pinky_u_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\pinky_r_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\pinky_r_2.png"),
            };
            orangeImages = new Image[]
            {
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\clyde_d_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\clyde_d_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\clyde_l_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\clyde_l_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\clyde_u_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\clyde_u_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\clyde_r_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\clyde_r_2.png"),
            };
            blueImages = new Image[]
            {
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\twinky_d_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\twinky_d_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\twinky_l_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\twinky_l_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\twinky_u_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\twinky_u_2.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\twinky_r_1.png"),
                Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Entities\\twinky_r_2.png"),
            };
            wall = Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Field\\wall.png");
            coin = Image.FromFile("C:\\Users\\Богдан\\Desktop\\Sprites\\Field\\point.png");
        }

        public void RestartLevel()
        {
            game.DrawCall += DrawFrame;
            game.OnCoinEaten += DestroyCoin;
            game.OnLevelCompleted += GameWinWindow;
            game.OnPacmanDie += OnPacmanDie;

            game.RestartLevel();
            loseWindow.Enabled = false;
            loseWindow.Hide();
            DrawCoins(game.map);
            UpdateLivesIndicator(game.LivesCount);
            UpdateScoreLabel();
        }

        private void UpdateScoreLabel()
        {
            label1.Text = $"Score : {game.CurrentScore}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    public partial class Form1 : Form
    {
        private List<Circle> Snake = new List<Circle>();
        private Circle food = new Circle();
        public Form1()
        {
            InitializeComponent();

            //set sewttings to default.
            new Settings();

            //set game speed and start timer.
            gameTimer.Interval = 1000 / Settings.speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();

            //Start new game.
            StartGame();

        }
        private void StartGame()
        {

            lblGameOver.Visible = false;
            //set settings to default.
            new Settings();

            //create new player object
            Snake.Clear();
            Circle head = new Circle();
            head.x = 10;
            head.y = 5;
            Snake.Add(head);


            lblScore.Text = Settings.score.ToString();
            GenerateFood();
        }

        //place random food game.
        private void GenerateFood()
        {
            int maxXPos = pbcanvas.Size.Width / Settings.width;
            int maxYPos = pbcanvas.Size.Width / Settings.height;

            Random random = new Random();
            food = new Circle();
            food.x = random.Next(0, maxXPos);
            food.y = random.Next(0, maxYPos);
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            //check for game over
            if(Settings.gameOver == true)
            {
                //check if enter is passed
                if(Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
                
            }
            else
            {
                if(Input.KeyPressed(Keys.Right) && Settings.direction != Direction.left)
                {
                    Settings.direction = Direction.right;
                }
                else if(Input.KeyPressed(Keys.Left) && Settings.direction != Direction.right)
                    Settings.direction = Direction.left;
                else if (Input.KeyPressed(Keys.Up) && Settings.direction != Direction.down)
                    Settings.direction = Direction.up;
                else if (Input.KeyPressed(Keys.Down) && Settings.direction != Direction.up)
                    Settings.direction = Direction.down;

                MovePlayer();


            }

            pbcanvas.Invalidate();
        }
        private void lblScore_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pbcanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if(!Settings.gameOver != true)
            {
                //Set colur of snake.
                Brush snakeColour;

                //Draw snake
                for(int i = 0; i<Snake.Count; i++)
                {
                   
                    if (i==0)
                    {
                        snakeColour = Brushes.Black; //Draw Head

                    }
                    else
                    {
                        snakeColour = Brushes.Green;  //Rest of body

                        //draw snake
                        canvas.FillEllipse(snakeColour,
                            new Rectangle(Snake[i].x * Settings.width,
                                           Snake[i].y * Settings.height,
                                           Settings.width,Settings.height));

                        //Draw food
                        canvas.FillEllipse(Brushes.Red,
                            new Rectangle(food.x = Settings.width,
                            food.y * Settings.height, Settings.width, Settings.height));
                    }
                }
            }
            else
            {
                string gameOver = "Game Over \nYour final score is: " + Settings.score + "\n Press Enter to try again";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }
        private void MovePlayer()
        {
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                //Move had
                if (i==0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.right:
                            Snake[i].x++;
                            break;
                        case Direction.left:
                            Snake[i].x--;
                            break;
                        case Direction.up:
                            Snake[i].y--;
                            break;
                        case Direction.down:
                            Snake[i].y++;
                            break;

                    }
                    //Get maximum x and y Pos
                    int maxXPos = pbcanvas.Size.Width / Settings.width;
                    int maxYPos = pbcanvas.Size.Height / Settings.width;

                    //Detect collission width game borders.
                    if (Snake[i].x < 0 || Snake[i].y < 0
                        || Snake[i].x >= maxXPos || Snake[i].y >= maxXPos) 
                    {
                        Die();
                    }

                    //Detect collision with body
                    for (int j = 0; j < Snake.Count; j++)
                    {
                        if(Snake[i].x == Snake[j].x &&
                            Snake[i].y == Snake[j].y)
                        {
                            Die();
                        }
                    }
                    //Detect Collision with food piece
                    if (Snake[i].x ==food.x && Snake[0].y == food.y)
                    {
                        Eat();
                    }
                }
                else
                {
                    //move body
                    Snake[i].x = Snake[i - 1].x;
                    Snake[i].y = Snake[i - 1].y;

                }
            }
        }

        private void Eat()
        {
            //add circle to  body
            Circle food = new Circle();
            food.x = Snake[Snake.Count - 1].x;
            food.y = Snake[Snake.Count - 1].y;

            Snake.Add(food);

            //update score
            Settings.score += Settings.points;
            lblScore.Text = Settings.score.ToString();

            GenerateFood();

        }
        private void Die()
        {
            Settings.gameOver = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
                
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }
    }
}

using System;
using System.Windows.Forms;

namespace Programming_Coursework_2
{
    public partial class Form1 : Form
    {
        private Player gamePlayer;
        private int backgroundSpeed = 8;
        public bool isAirborne;

        Enemy enemy;

        public Form1()
        {
            InitializeComponent();
            gamePlayer = new Player(this.player);
            {
                InitializeComponent();
                enemy = new Enemy(this);
                player = new Player(this, playerHealthBar);
                enemy = new Enemy(this);
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            enemy.MoveLeft();
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            txtscore.Text = "Score: " + gamePlayer.Score;
            txtscore.Left = 536;

            gamePlayer.UpdateMovement(this.ClientSize.Width);

            if (gamePlayer.GoLeft && background.Left < 0)
            {
                background.Left += backgroundSpeed;
                MoveGameElements("forward");
            }

            if (gamePlayer.GoRight && background.Left > -1372)
            {
                background.Left -= backgroundSpeed;
                MoveGameElements("back");
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "Platform" && gamePlayer.PlayerPictureBox.Bounds.IntersectsWith(x.Bounds) && !gamePlayer.Jumping)
                    {
                        gamePlayer.LandOnPlatform(x);
                    }
                    x.BringToFront();

                    if ((string)x.Tag == "Coin" && gamePlayer.PlayerPictureBox.Bounds.IntersectsWith(x.Bounds) && x.Visible)
                    {
                        x.Visible = false;
                        gamePlayer.CollectCoin();
                    }
                }
            }

            if (gamePlayer.PlayerPictureBox.Bounds.IntersectsWith(Key.Bounds))
            {
                Key.Visible = false;
                gamePlayer.HasKey = true;
            }
            if (gamePlayer.PlayerPictureBox.Bounds.IntersectsWith(Door.Bounds) && gamePlayer.HasKey)
            {
                Door.Image = Properties.Resources.door_open_1_;
                GameTimer.Stop();
                MessageBox.Show("Level 1 Complete" + Environment.NewLine + "Click OK to continue");
                RestartGame();
            }

            if (gamePlayer.IsFallingOffScreen(this.ClientSize.Height))
            {
                GameTimer.Stop();
                MessageBox.Show("Level 1 Failed" + Environment.NewLine + "Click OK to Play again");
                RestartGame();
            }
        }

        private bool isAttacking = false;

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) gamePlayer.GoLeft = true;
            if (e.KeyCode == Keys.Right) gamePlayer.GoRight = true;
            if (e.KeyCode == Keys.Space && !gamePlayer.Jumping && isAirborne == false)
            {
                gamePlayer.Jumping = true;
                isAirborne = true;
            }
            if (e.KeyCode == Keys.E && !isAttacking)
            {
                isAttacking = true;
                PlayerAttack.PerformAttack(player, this.Controls);
                isAttacking = false;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) gamePlayer.GoLeft = false;
            if (e.KeyCode == Keys.Right) gamePlayer.GoRight = false;
            if (e.KeyCode == Keys.Space && gamePlayer.Jumping == true) ;

        }

        private void CloseGame(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void RestartGame()
        {
            Form1 newWindow = new Form1();
            newWindow.Show();
            this.Hide();
        }

        private void MoveGameElements(string direction)
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && ((string)x.Tag == "Platform" || (string)x.Tag == "Coin" || (string)x.Tag == "Key" || (string)x.Tag == "Door"))
                {
                    if (direction == "back")
                    {
                        x.Left -= backgroundSpeed;
                    }
                    if (direction == "forward")
                    {
                        x.Left += backgroundSpeed;
                    }
                }
            }
        }

   
    }
} 
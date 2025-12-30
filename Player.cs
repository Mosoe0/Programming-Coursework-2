using System.Drawing;
using System.Windows.Forms;

namespace Programming_Coursework_2
{
    public class Player
    {
        public PictureBox PlayerPictureBox { get; set; }
        public bool GoLeft { get; set; }
        public bool GoRight { get; set; }
        public bool Jumping { get; set; }
        public bool HasKey { get; set; }
        public int Score { get; set; }  

        private int jumpSpeed = 10;
        private int force = 8;
        private int playerSpeed = 10;

        public Player(PictureBox pictureBox)
        {
            PlayerPictureBox = pictureBox;
            Score = 0;
            Jumping = false;
            HasKey = false;
        }

        public void UpdateMovement(int clientWidth)
        {
           
            PlayerPictureBox.Top += jumpSpeed;

            if (GoLeft && PlayerPictureBox.Left > 60)
            {
                PlayerPictureBox.Left -= playerSpeed;
            }

            if (GoRight && PlayerPictureBox.Left + (PlayerPictureBox.Width + 60) < clientWidth)
            {
                PlayerPictureBox.Left += playerSpeed;
            }

            if (Jumping)
            {
                jumpSpeed = -12; 
                force -= 1;
            }
            else
            {
                jumpSpeed = 12; 
            }

            if (Jumping && force < 0)
            {
                Jumping = false; 
            }
        }

        public void LandOnPlatform(Control platform)
        {
            force = 8; 
            PlayerPictureBox.Top = platform.Top - PlayerPictureBox.Height;
            jumpSpeed = 0; 
            Jumping = false; 
        }

        public void CollectCoin()
        {
            Score += 1;
        }

        public bool IsFallingOffScreen(int clientHeight)
        {
            return PlayerPictureBox.Top + PlayerPictureBox.Height > clientHeight;
        }
    }
}
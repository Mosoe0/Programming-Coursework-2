using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;
using System.Media;

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
        public int Health { get; private set; } = 100;
        public int MaxHealth { get; private set; } = 100;

        private int jumpSpeed = 10;
        private int force = 8;
        private int playerSpeed = 10;
       
        private ProgressBar healthBar;
        private Label hpLabel;

        private SoundPlayer hitSound;
        private SoundPlayer healSound;


        public Player(Form form, ProgressBar hpBar, Label hpText)
        {
            healthBar = hpBar;
            hpLabel = hpText;

            hitSound = new SoundPlayer("hit.wav");
            healSound = new SoundPlayer("heal.wav");

            foreach (Control c in form.Controls)
            {
                if (c is PictureBox pb && pb.Tag?.ToString() == "player")
                {
                    PlayerPictureBox = pb;
                    break;
                }
            }    
                SetupUI();
            SetupHealthBar();


            if (PlayerPictureBox == null)
            {
                throw new System.Exception("Player PictureBox with tag 'player' not found.");
            }


        }
        private void SetupUI()
        {
            healthBar.Maximum = MaxHealth;
            healthBar.Value = Health;
            UpdateHPText();
        }

        private void SetupHealthBar()
        {
            healthBar.Maximum = MaxHealth;
            healthBar.Value = Health;
        }

        private void UpdateHPText()
        {
            hpLabel.Text = $"HP: {Health} / {MaxHealth}";
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;

            hitSound.Play();
            UpdateUI();

            if (Health <= 0)
            {
                Die();
            }
        }
        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth)
                Health = MaxHealth;

            healSound.Play();
            UpdateUI();
        }
        private void UpdateUI()
        {
            healthBar.Value = Health;
            UpdateHPText();
        }

        private void Die()
        {
            PlayerPictureBox.Visible = false;
            MessageBox.Show("Game Over");
        }
    

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
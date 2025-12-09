using System.Windows.Forms;
using System.Drawing;

namespace Programming_Coursework_2
{
    public static class PlayerAttack
    {
        private const int ATTACK_RANGE = 50;
        private const int ATTACK_HEIGHT = 60;

        /// <p name="playerBox"
        /// <p name="formControls"
        public static void PerformAttack(PictureBox playerBox, ControlCollection formControls)
        {
            Rectangle attackBounds = new Rectangle(
                playerBox.Right,            
                playerBox.Top,              
                ATTACK_RANGE,               
                ATTACK_HEIGHT               
            );

            foreach (Control control in formControls)
            {
                if (control is PictureBox target && (string)target.Tag == "Enemy")
                {
                    if (attackBounds.IntersectsWith(target.Bounds))
                    {
                        HandleEnemyHit(target);
                    }
                }
            }
        }

        private static void HandleEnemyHit(PictureBox enemy)
        {
            enemy.Visible = false; // kill
        }
    }
}
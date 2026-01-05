using Programming_Coursework_2;
using System.Drawing;
using System.Numerics;
using System.Security.Policy;
using System.Windows.Forms;

public class Enemy
{
    private Form gameForm;
    private Player player;

    public PictureBox Sprite { get; private set; }
    public int Speed { get; set; } = 3;
    public int Health { get; set; } = 100;

    public Enemy(Form form)
    {
        gameForm = form; 

        foreach (Control c in form.Controls)
        {
            if (c is PictureBox pb && pb.Tag?.ToString() == "enemy1")
            {
                Sprite = pb;
                break;
            }
        }

        if (Sprite == null)
        {
            throw new System.Exception("Enemy PictureBox tag 'enemy1' not found.");
        }
    }

    public Enemy(Form form, Player playerRef)
    {
        gameForm = form;
        player = playerRef;

        foreach (Control c in form.Controls)
        {
            if (c is PictureBox pb && pb.Tag?.ToString() == "enemy1")
            {
                Sprite = pb;
                break;
            }
        }
    }

    public void MoveLeft()
    {
        Sprite.Left -= Speed;
        CheckCollisions();
    }

    public void MoveRight()
    {
        Sprite.Left += Speed;
        CheckCollisions();
    }

    public void MoveUp()
    {
        Sprite.Top -= Speed;
        CheckCollisions();
    }

    public void MoveDown()
    {
        Sprite.Top += Speed;
        CheckCollisions();
    }

    public Rectangle GetBounds()
    {
        return Sprite.Bounds;
    }

    private void CheckCollisions()
    {
        foreach (Control c in gameForm.Controls) 
        {
            if (c is PictureBox pb && pb != Sprite)
            {
                if (Sprite.Bounds.IntersectsWith(pb.Bounds))
                {
                    if (pb.Tag?.ToString() == "player")
                    {
                        OnPlayerCollision(pb);
                    }
                    else if (pb.Tag?.ToString() == "wall")
                    {
                        UndoMove();
                    }
                }
            }
        }
    }
    

    private void UndoMove()
    {
        Sprite.Left -= Speed;
        Sprite.Top -= Speed;
    }

    private void OnPlayerCollision(PictureBox player)
    {
        Sprite.BackColor = Color.Red;
        player.TakeDamage(10);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Sprite.Visible = false;
    }
}

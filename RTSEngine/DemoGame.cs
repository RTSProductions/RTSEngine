using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTSEngine.RTSEngine;
using System.Drawing;
using System.Windows.Forms;

namespace RTSEngine
{
    class DemoGame : RTSEngine.RTSEngine
    {
        Sprite2D player = null;

        bool left;
        bool right;
        bool up;
        bool down;
        Vector2 lastPos = Vector2.Zero();

        string[,] Map =
        {
            {"g","g","g","g","g","g","g"},
            {"g",".",".",".",".",".","g"},
            {"g",".",".",".","g",".","g"},
            {"g",".","g","g","g",".","g"},
            {"g",".","g",".","g",".","g"},
            {"g",".","g",".",".",".","g"},
            {"g","g","g","g","g","g","g"},
        };
        public DemoGame() : base(new Vector2(615, 515), "RTS Engine Demo")
        {

        }
        public override void OnLoad()
        {
            BackroundColor = Color.Black;
            CameraPosition.x = 100;

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j  < Map.GetLength(0); j ++)
                {
                    if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), "Tiles/Blue tiles/tileBlue_03", "Ground");
                    }
                }
            }

            player = new Sprite2D(new Vector2(100, 100), new Vector2(30, 40), "Players/Player Green/playerGreen_walk1", "Player");
        }

        public override void OnDraw()
        {
            
        }

        int time = 0;
        public override void OnUpdate()
        {
            time++;
            if (up)
            {
                player.Position.y  -= 1f;
            }
            if (down)
            {
                player.Position.y += 1f;
            }
            if (left)
            {
                player.Position.x -= 1f;
            }
            if (right)
            {
                player.Position.x += 1f;
            }
            if (player.IsColliding("Ground"))
            {
               player.Position.x = lastPos.x;
               player.Position.y = lastPos.y;
            }
            else
            {
                lastPos.x = player.Position.x;
                lastPos.y = player.Position.y;
            }

        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { up = true; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { down = true; }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { right = true; }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { down = false; }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { right = false; }
        }
    }
}

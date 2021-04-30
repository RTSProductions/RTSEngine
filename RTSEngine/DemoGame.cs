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
        //the player
        Sprite2D player = null;

        bool left;
        bool right;
        bool up;
        bool down;
        Vector2 lastPos = Vector2.Zero();
        public float speed = 6;

        //a 2 dimentinal string array used to make the starting map.
        string[,] Map =
        {
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
            {"g", ".", ".", ".", ".", ".", "g", ".", ".", "c", ".", "j", "g", "j", "g"},
            {"g", ".", "g", ".", ".", ".", "g", ".", ".", "g", ".", "j", "g", "j", "g"},
            {"g", ".", "g", ".", ".", ".", "g", ".", ".", "g", ".", "j", "g", "j", "g"},
            {"g", "c", "g", ".", ".", ".", "g", ".", ".", "g", ".", ".", "g", "j", "g"},
            {"g", "c", "g", ".", ".", ".", "g", ".", "g", "g", "g", ".", "g", "j", "g"},
            {"g", "c", "g", "c", ".", ".", "g", ".", ".", ".", "g", ".", "g", "c", "g"},
            {"g", ".", "g", "g", "g", ".", "g", ".", ".", ".", "g", ".", "g", "c", "g"},
            {"g", ".", "g", "p", "g", ".", "g", "g", "g", ".", "g", ".", "g", "c", "g"},
            {"g", ".", "g", ".", "g", ".", "g", ".", ".", ".", "g", ".", "g", "c", "g"},
            {"g", ".", ".", ".", "g", ".", "j", ".", ".", ".", "g", ".", ".", "c", "g"},
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
        };

        string[,] MapOld =
       {
            {"g","g","g","g","g","g","g"},
            {"g",".",".",".",".",".","g"},
            {"g","j",".","j","g",".","g"},
            {"g",".","g","g","g",".","g"},
            {"g",".","g","j","g",".","g"},
            {"g",".","g","j",".",".","g"},
            {"g","g","g","g","g","g","g"},
        };
        public DemoGame() : base(new Vector2(615, 515), "RTS Engine Demo")
        {

        }
        //called when the game starts.
        public override void OnLoad()
        {
            Sprite2D groundRef = new Sprite2D("Tiles/Blue tiles/tileBlue_03");
            Sprite2D jewelRef = new Sprite2D("Items/yellowJewel");
            Sprite2D coinRef = new Sprite2D("Items/yellowCrystal");

            CameraZoom = new Vector2(.8f, .8f);

            BackroundColor = Color.Black;

            //Generate the map.
            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j  < Map.GetLength(0); j ++)
                {
                    if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), groundRef, "Ground");
                    }
                    if (Map[j, i] == "j")
                    {
                        new Sprite2D(new Vector2(i * 50 + 15, j * 50 + 15), new Vector2(25, 25), jewelRef, "Jewel");
                    }
                    if (Map[j, i] == "c")
                    {
                        new Sprite2D(new Vector2(i * 50 + 15, j * 50 + 15), new Vector2(25, 25), coinRef, "Coin");
                    }
                    if (Map[j, i] == "p")
                    {
                        player = new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(30, 40), "Players/Player Green/playerGreen_walk1", "Player");
                    }
                }
            }
            if (player == null)
            {
                player = new Sprite2D(new Vector2(100, 100), new Vector2(30, 40), "Players/Player Green/playerGreen_walk1", "Player");
            }
        }

        public override void OnDraw()
        {
            
        }

        int time = 0;

        //onUpdate is called once per frame.
        public override void OnUpdate()
        {
            time++;
            if (player != null)
            {
                if (up)
                {
                    player.Position.y -= speed;
                }
                if (down)
                {
                    player.Position.y += speed;
                }
                if (left)
                {
                    player.Position.x -= speed;
                }
                if (right)
                {
                    player.Position.x += speed;
                }
                if (player.IsColliding("Ground") != null)
                {
                    player.Position.x = lastPos.x;
                    player.Position.y = lastPos.y;
                }
                Sprite2D jewel = player.IsColliding("Jewel");
                if (jewel != null)
                {
                    jewel.DestroySelf();
                }
                Sprite2D coin = player.IsColliding("Coin");
                if (coin != null)
                {
                    coin.DestroySelf();
                }
                else
                {
                    lastPos.x = player.Position.x;
                    lastPos.y = player.Position.y;
                }
            }

        }

        //input
        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { up = true; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { down = true; }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { right = true; }
        }

        //input
        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { down = false; }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { right = false; }
        }
    }
}

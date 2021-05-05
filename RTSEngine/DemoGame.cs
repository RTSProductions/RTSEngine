using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTSEngine.RTSEngine;
using System.Drawing;
using System.Windows.Forms;
using Box2DX.Common;
using System.Collections;

namespace RTSEngine
{
    class DemoGame : RTSEngine.RTSEngine
    {
        //the player
        Sprite2D player = null;
        //Movement bools
        bool left;
        bool right;
        bool up;
        bool down;
        bool canFall = true;
        //the last position of the player
        Vector2 lastPos = Vector2.Zero();
        //the speed of the player.
        public float speed = 6;

        //a 2 dimentinal string array used to make the starting map.
        string[,] Map =
        {
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", "p", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", "g", "g", "g", "g", "g", "g", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
        };
        //The struct used to create the window.
        public DemoGame() : base(new Vector2(615, 515), "RTS Engine Demo")
        {
            gravity = new Vec2(0, 100);
        }
        //called when the game starts.
        public override void OnLoad()
        {
            //Refrences for the sprites.
            Sprite2D groundRef = new Sprite2D("Tiles/Blue tiles/tileBlue_02");
            Sprite2D jewelRef = new Sprite2D("Items/yellowJewel");
            Sprite2D coinRef = new Sprite2D("Items/yellowCrystal");

            CameraZoom = new Vector2(.8f, .8f);

            BackroundColor = Color.Aqua;

            //Generate the map.
            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j  < Map.GetLength(0); j ++)
                {
                    //Generate ground tiles
                    if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), groundRef, "Ground").CreateStatic();
                    }
                    //Generate jewels
                    if (Map[j, i] == "j")
                    {
                        new Sprite2D(new Vector2(i * 50 + 15, j * 50 + 15), new Vector2(25, 25), jewelRef, "Jewel");
                    }
                    //Generate coins
                    if (Map[j, i] == "c")
                    {
                        new Sprite2D(new Vector2(i * 50 + 15, j * 50 + 15), new Vector2(25, 25), coinRef, "Coin");
                    }
                    //Generate the player
                    if (Map[j, i] == "p")
                    {
                        player = new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(30, 40), "Players/Player Green/playerGreen_walk1", "Player");
                        player.CreateDynamic();
                    }
                }
            }
        }

        void FallWait()
        {
            DateTime t = DateTime.Now;
            DateTime tf = DateTime.Now.AddSeconds(1);

            while (t < tf)
            {
                t = DateTime.Now;
            }
            canFall = true;
        }

        //called when each frame is drawn
        public override void OnDraw()
        {
            
        }

        //onUpdate is called once per frame.
        public override void OnUpdate()
        {
            //checking if the player isn't null.
            if (player != null)
            {
                //moving the player bassed on input
                if (up)
                {
                    player.AddForce(new Vector2(0, -1000000), player.Position);
                }
                else
                {
                    player.AddForce(new Vector2(0, 100000), player.Position);
                }
                Vector2 velocity = player.GetVelocity();

                if (left)
                {
                    player.SetVelocity(new Vector2(-200, velocity.y));
                }
                if (right)
                {
                    player.SetVelocity(new Vector2(200, velocity.y));
                }
    
                if (!right && !left)
                {
                    player.SetVelocity(new Vector2(0, velocity.y));
                }

                //updating the players physics postion.
                player.UpdatePosition();

                //collecting jewels
                Sprite2D jewel = player.IsColliding("Jewel");
                if (jewel != null)
                {
                    jewel.DestroySelf();
                }
                //collecting coins
                Sprite2D coin = player.IsColliding("Coin");
                if (coin != null)
                {
                    coin.DestroySelf();
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

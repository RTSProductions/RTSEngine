using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTSEngine.RTSEngine;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;
using Color = System.Drawing.Color;

namespace RTSEngine
{
    class CrateFall : RTSEngine.RTSEngine
    {
        //Physics crate
        Sprite2D box = null;

        //New gravity
        Vector2 CurrentGravity = new Vector2(0.0f, 100.0f);

        //the map
        string[,] Map =
        {
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "."},
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
        };

        //The struct used to make the window
        public CrateFall() : base(new Vector2(515, 515), "Create Fall")
        {

        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            
        }

        public override void OnDraw()
        {
            
        }

        public override void OnLoad()
        {
            Sprite2D groundRef = new Sprite2D("Tiles/Blue tiles/tileBlue_03");

            gravity = new Vec2(CurrentGravity.x, CurrentGravity.y);

            BackroundColor = Color.Aqua;

            box = new Sprite2D(new Vector2(225, 0), new Vector2(50, 50), "Crate", "Box");

            box.CreateDynamic();

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
                    if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), groundRef, "Ground");
                    }

                }
            }
        }

        public override void OnUpdate()
        {
            if (box == null)
            {
                CreatBox();
            }

            if (box != null)
            {
                box.UpdatePosition();

                if (box.IsColliding("Ground") != null)
                {
                    box.DestroySelf();

                    box = null;
                }
            }
        }

        void CreatBox()
        {
            if (box == null)
            {
                box = new Sprite2D(new Vector2(225, 0), new Vector2(50, 50), "Crate", "Box");

                box.CreateDynamic();
            }
        }
    }
}

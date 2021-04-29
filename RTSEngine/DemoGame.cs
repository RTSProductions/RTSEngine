using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTSEngine.RTSEngine;
using System.Drawing;

namespace RTSEngine
{
    class DemoGame : RTSEngine.RTSEngine
    {
        public Shape2D player = null;
        public DemoGame() : base(new Vector2(615, 515), "RTS Engine Demo")
        {

        }
        public override void OnLoad()
        {
            BackroundColor = Color.Black;

            player = new Shape2D(new Vector2(10, 10), new Vector2(10, 10), "Test", Color.Red);
        }

        public override void OnDraw()
        {
            
        }

        int time = 0;
        public override void OnUpdate()
        {
            time++;
        }
    }
}

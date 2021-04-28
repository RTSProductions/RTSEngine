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
        public DemoGame() : base(new Vector2(615, 515), "RTS Engine Demo")
        {

        }
        public override void OnLoad()
        {
            BackroundColor = Color.Black;
        }

        public override void OnDraw()
        {
            
        }


        int frame = 0;
        public override void OnUpdate()
        {
            Console.WriteLine("Frame Count:" + frame);
            frame++;
        }
    }
}

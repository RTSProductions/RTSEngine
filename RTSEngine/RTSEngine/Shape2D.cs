using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSEngine.RTSEngine
{
    public class Shape2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;

        public Shape2D(Vector2 Position, Vector2 Scale)
        {
            this.Position = Position;
            this.Scale = Scale;
        }
    }
}

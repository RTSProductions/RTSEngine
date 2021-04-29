using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSEngine.RTSEngine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";

        public Sprite2D(Vector2 Position, Vector2 Scale, String Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Tag;

            Log.Info($"[SHAPE2D]({Tag}) - Has Been registered!");
            RTSEngine.RegisterSprite(this);
        }

        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({Directory}) - Has Been destroyed!");
            RTSEngine.UnRegisterSprite(this);
        }

    }
}

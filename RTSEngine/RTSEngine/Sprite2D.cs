using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RTSEngine.RTSEngine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";
        public string Tag = "";
        public Image sprite = null;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, String Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;
            this.Directory = Directory;

            Bitmap sprite = new Bitmap((int)this.Scale.x, (int)this.Scale.y, System.Drawing.Imaging.PixelFormat.Alpha);
            //sprite.
            Log.Info($"[SHAPE2D]({Tag}) - Has Been registered!");
            RTSEngine.RegisterSprite(this);
        }

        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({Tag}) - Has Been destroyed!");
            RTSEngine.UnRegisterSprite(this);
        }

    }
}

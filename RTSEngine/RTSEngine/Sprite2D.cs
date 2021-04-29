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
        public Bitmap Sprite = null;
        /// <summary>
        /// Sets the sacle position tag and immage of the sprite that you are using
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="Scale"></param>
        /// <param name="Directory"></param>
        /// <param name="Tag"></param>
        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, String Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;
            this.Directory = Directory;

            Image temp =  Image.FromFile($"Assets/Sprites/PNG/{Directory}.png");

            Bitmap sprite = new Bitmap(temp, (int)this.Scale.x, (int)this.Scale.y);

            this.Sprite = sprite;

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

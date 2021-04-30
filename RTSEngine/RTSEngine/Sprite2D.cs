using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RTSEngine.RTSEngine
{
    /// <summary>
    /// A sprite.
    /// </summary>
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Directory = "";
        public string Tag = "";
        public Bitmap Sprite = null;
        public bool isReference = false;

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

            Log.Info($"[SPRITE2D]({Tag}) - Has Been registered!");
            RTSEngine.RegisterSprite(this);
        }

        public Sprite2D(string Directory)
        {
            this.isReference = true;
            this.Directory = Directory;

            Image temp = Image.FromFile($"Assets/Sprites/PNG/{Directory}.png");

            Bitmap sprite = new Bitmap(temp, 50, 50);

            this.Sprite = sprite;

            Log.Info($"[SPRITE2D]({Tag}) - Has Been registered!");
            RTSEngine.RegisterSprite(this);
        }

        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D refrence, String Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            this.Sprite = refrence.Sprite;

            Log.Info($"[SPRITE2D]({Tag}) - Has Been registered!");
            RTSEngine.RegisterSprite(this);
        }

        public bool IsColliding(Sprite2D a, Sprite2D b)
        {
           if (a.Position.x < b.Position.x + b.Scale.x &&
                a.Position.x + a.Scale.x > b.Position.x &&
                a.Position.y < b.Position.y + b.Scale.y &&
                a.Position.y + a.Scale.y > b.Position.y)
            {
                return true;
            }

            return false;
        }

        public Sprite2D IsColliding(string Tag)
        {
            foreach(Sprite2D b in RTSEngine.AllSprites)
            {
                if (b.Tag == Tag)
                {
                    if (Position.x < b.Position.x + b.Scale.x &&
                         Position.x + Scale.x > b.Position.x &&
                         Position.y < b.Position.y + b.Scale.y &&
                         Position.y + Scale.y > b.Position.y)
                    {
                        return b;
                    }
                }
            }


            return null;
        }

        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({Tag}) - Has Been destroyed!");
            RTSEngine.UnRegisterSprite(this);
        }

    }
}

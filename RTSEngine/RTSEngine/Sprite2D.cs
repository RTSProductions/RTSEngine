using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;


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
        Body body;
        BodyDef bodyDef = new BodyDef();

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

        public Vector2 GetVelocity()
        {
            Vec2 vel = body.GetLinearVelocity();
            Vector2 get = new Vector2(vel.X, vel.Y);
            return get;
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
        /// <summary>
        /// Creates a static physics objec.
        /// </summary>
        public void CreateStatic()
        {
            bodyDef = new BodyDef();
            bodyDef.Position = new Vec2(this.Position.x, this.Position.y);

            body = RTSEngine.world.CreateBody(bodyDef);

            // Define the ground box shape.
            PolygonDef shapeDef = new PolygonDef();

            // The extents are the half-widths of the box.
            shapeDef.SetAsBox(Scale.x/2, Scale.y / 2);

            // Add the ground shape to the ground body.
            body.CreateShape(shapeDef);
        }

        public void AddForce(Vector2 force, Vector2 point)
        {
            body.ApplyImpulse(new Vec2(force.x, force.y), new Vec2(point.x, point.y));
        }
        public void SetVelocity(Vector2 vel)
        {
            body.SetLinearVelocity(new Vec2(vel.x, vel.y));
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

        /// <summary>
        /// Creates a dynamic physics object.
        /// </summary>
        public void CreateDynamic()
        {
            bodyDef = new BodyDef();
            bodyDef.Position.X = Position.x;
            bodyDef.Position.Y = Position.y;
            // Define the dynamic body. We set its position and call the body factory.
            bodyDef.Position = new Vec2(this.Position.x, this.Position.y);
            Body body = RTSEngine.world.CreateBody(bodyDef);

            this.body = body;

            // Define another box shape for our dynamic body.
            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox(Scale.x / 2, Scale.y / 2);

            // Set the box density to be non-zero, so it will be dynamic.
            shapeDef.Density = 1.0f;

            // Override the default friction.
            shapeDef.Friction = 0.3f;

            // Add the shape to the body.
            body.CreateShape(shapeDef);

            // Now tell the dynamic body to compute it's mass properties base
            // on its shape.
            body.SetMassFromShapes();

        }
        /// <summary>
        /// Updates the position of a dynamic physics object.
        /// </summary>
        public void UpdatePosition()
        {
            if (body == null)
            {
                body = RTSEngine.world.CreateBody(bodyDef);
                Log.Error("Body was null");
            }
            if (body != null)
            {
                this.Position.y = body.GetPosition().Y;
                this.Position.x = body.GetPosition().X;
               // Log.Info("Body is not null");
            }
            else
            {
                Log.Error("Body Is null");
            }
        }

        /// <summary>
        /// Check if sprite a is colliding with sprite b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Check if this sprite is colliding with any sprite with the given tag.
        /// </summary>
        /// <param name="Tag"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Destory the sprite.
        /// </summary>
        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({Tag}) - Has Been destroyed!");
            RTSEngine.UnRegisterSprite(this);
        }

    }
}

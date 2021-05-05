using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Numerics;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;

namespace RTSEngine.RTSEngine
{

    class Canvas : Form
    {
        //used to draw the window
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }
    /// <summary>
    /// The game engine that all of this uses.
    /// </summary>
    public abstract class RTSEngine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "New Game";
        private Canvas Window = null;
        private Thread GameLoopThread = null;


        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();

        public System.Drawing.Color BackroundColor = System.Drawing.Color.Aqua;

        public int timeScale = 2;

        public Vector2 CameraPosition = Vector2.Zero();
        public Vector2 CameraZoom = new Vector2(1, 1);
        public float CameraAngle = 0f;

        // Define the size of the world. Simulation will still work
        // if bodies reach the end of the world, but it will be slower.
        AABB worldAABB = new AABB
        {
            UpperBound = new Vec2(2000, 2000),
            LowerBound = new Vec2(-2000, -2000)
        };

        bool doSleep = false;

        // Define the gravity vector.
        public  Vec2 gravity = new Vec2(0.0f, 10.0f);


        public static World world = null;

        /// <summary>
        /// Initalizing The window
        /// </summary>
        /// <param name="screenSize"></param>
        /// <param name="title"></param>
        public RTSEngine(Vector2 screenSize, string title)
        {
            Log.Info("Game is starting");
            Title = title;
            ScreenSize = screenSize;

            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.x, (int)ScreenSize.y);
            Window.Text = Title;
            Window.Paint += Renderer;
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;
            Window.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Window.FormClosing += Window_FormClosing;
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();

            world = new World(worldAABB, gravity, doSleep);

            Application.Run(Window);
        }

        //Ending The Game
        private void Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            GameLoopThread.Abort();
        }

        //Input
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        //Input
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }

        //Gameloop
        void GameLoop()
        {
            OnLoad();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(timeScale);
                }
                catch
                {
                    Log.Error("Game has not been found");
                }
            }
        }

        //called when the game starts
        public abstract void OnLoad();

        //called once per frame
        public abstract void OnUpdate();

        public abstract void OnDraw();

        public static void RegisterShape(Shape2D shape)
        {
            if (!AllShapes.Contains(shape))
            {
                AllShapes.Add(shape);
            }
        }

        public static void RegisterSprite(Sprite2D sprite)
        {
            if (!AllSprites.Contains(sprite))
            {
                AllSprites.Add(sprite);
            }
        }
        public static void UnRegisterShape(Shape2D shape)
        {
            if (AllShapes.Contains(shape))
            {
                AllShapes.Remove(shape);
            }
        }

        public static void UnRegisterSprite(Sprite2D sprite)
        {
            if (AllSprites.Contains(sprite))
            {
                AllSprites.Remove(sprite);
            }
        }
            float timeStep = 1.0f / 60.0f;
            int velocityIterations = 8;
            int positionIterations = 1;


        //used to render everything.
        private void Renderer (object sender, PaintEventArgs e)
        {
            world.Step(timeStep, velocityIterations, positionIterations);

            Graphics g = e.Graphics;
            g.Clear(BackroundColor);

            g.TranslateTransform(CameraPosition.x, CameraPosition.y);
            g.RotateTransform(CameraAngle);
            g.ScaleTransform(CameraZoom.x, CameraZoom.y);
            try
            {
                foreach (Shape2D shape in AllShapes)
                {
                    g.FillRectangle(new SolidBrush(shape.color), shape.Position.x, shape.Position.y, shape.Scale.x, shape.Scale.y);
                }
                foreach (Sprite2D sprite in AllSprites)
                {
                    if (!sprite.isReference)
                    {
                        g.DrawImage(sprite.Sprite, sprite.Position.x, sprite.Position.y, sprite.Scale.x, sprite.Scale.y);
                    }
                }
            }
            catch
            {

            }

        }
        /// <summary>
        /// Used for input
        /// </summary>
        /// <param name="e"></param>
        public abstract void GetKeyDown(KeyEventArgs e);

        /// <summary>
        /// Used for input
        /// </summary>
        /// <param name="e"></param>
        public abstract void GetKeyUp(KeyEventArgs e);

    }
}

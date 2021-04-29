using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace RTSEngine.RTSEngine
{

    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;
        }
    }
    public abstract class RTSEngine
    {
        private Vector2 ScreenSize = new Vector2(512, 512);
        private string Title = "New Game";
        private Canvas Window = null;
        private Thread GameLoopThread = null;


        public static List<Shape2D> AllShapes = new List<Shape2D>();
        public static List<Sprite2D> AllSprites = new List<Sprite2D>();

        public Color BackroundColor = Color.Aqua;

        public Vector2 CameraPosition = Vector2.Zero();
        public float CameraAngle = 0f;
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
            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();
            

            Application.Run(Window);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            GetKeyDown(e);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            GetKeyUp(e);
        }



        void GameLoop()
        {
            OnLoad();
            while (GameLoopThread.IsAlive)
            {
                try
                {
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(1);
                }
                catch
                {
                    Log.Error("Game has not been found");
                }
            }
        }

        public abstract void OnLoad();

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

        private void Renderer (object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackroundColor);

            g.TranslateTransform(CameraPosition.x, CameraPosition.y);
            g.RotateTransform(CameraAngle);
            foreach (Shape2D shape in AllShapes)
            {
                g.FillRectangle(new SolidBrush(shape.color), shape.Position.x, shape.Position.y, shape.Scale.x, shape.Scale.y);
            }
            foreach (Sprite2D sprite in AllSprites)
            {
                g.DrawImage(sprite.Sprite, sprite.Position.x, sprite.Position.y, sprite.Scale.x, sprite.Scale.y);
            }

        }
        public abstract void GetKeyDown(KeyEventArgs e);
        public abstract void GetKeyUp(KeyEventArgs e);

    }
}

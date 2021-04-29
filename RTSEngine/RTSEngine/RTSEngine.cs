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


        private static List<Shape2D> AllShapes = new List<Shape2D>();

        public Color BackroundColor = Color.Aqua;
        public RTSEngine(Vector2 screenSize, string title)
        {
            Log.Info("Game is starting");
            Title = title;
            ScreenSize = screenSize;

            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.x, (int)ScreenSize.y);
            Window.Text = Title;
            Window.Paint += Renderer;

            GameLoopThread = new Thread(GameLoop);
            GameLoopThread.Start();
            

            Application.Run(Window);
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
        public static void UnRegisterShape(Shape2D shape)
        {
            if (AllShapes.Contains(shape))
            {
                AllShapes.Remove(shape);
            }
        }

        private void Renderer (object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackroundColor);

            foreach (Shape2D shape in AllShapes)
            {
                g.FillRectangle(new SolidBrush(Color.Red), shape.Position.x, shape.Position.y, shape.Scale.x, shape.Scale.y);
            }

        }
    }
}

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
        private Canvas Window;
        private Thread GameLoopThread;

        public Color BackroundColor = Color.Aqua;
        public RTSEngine(Vector2 screenSize, string title)
        {
            Title = title;
            ScreenSize = screenSize;

            Window = new Canvas();
            Window.Size = new Size((int)ScreenSize.x, (int)ScreenSize.y);
            Window.Text = Title;
            Window.Paint += Renderer;
            OnLoad();

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
                    Console.WriteLine("Game is loading...");
                }
            }
        }

        public abstract void OnLoad();

        public abstract void OnUpdate();

        public abstract void OnDraw();

        private void Renderer (object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(BackroundColor);

        }
    }
}

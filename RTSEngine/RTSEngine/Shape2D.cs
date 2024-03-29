﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RTSEngine.RTSEngine
{
    /// <summary>
    /// A squar.
    /// </summary>
    public class Shape2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;
        public string Tag = "";
        public Color color;

        /// <summary>
        /// Used to generate the shape
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="Scale"></param>
        /// <param name="Tag"></param>
        /// <param name="color"></param>
        public Shape2D(Vector2 Position, Vector2 Scale, String Tag, Color color)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;
            this.color = color;

            Log.Info($"[SHAPE2D]({Tag}) - Has Been registered!");
            RTSEngine.RegisterShape(this);
        }

        public void DestroySelf()
        {
            Log.Info($"[SHAPE2D]({Tag}) - Has Been destroyed!");
            RTSEngine.UnRegisterShape(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSEngine.RTSEngine
{
    /// <summary>
    /// A 2D float array that can be used for the scale or position of an object
    /// </summary>
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }

        public Vector2()
        {
            x = Vector2.Zero().x;
            y = Vector2.Zero().y;
        }

        public Vector2(float X, float Y)
        {
            this.x = X;
            this.y = Y;
        }

        /// <summary>
        /// Returns x and y as 0.
        /// </summary>
        /// <returns></returns>
        public static Vector2 Zero()
        {
            return new Vector2(0,0);
        }

        /// <summary>
        /// Linearly intorpolates from the first vector2 to the secound vector2 using the speed peramitor.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static Vector2 Lerp(Vector2 a, Vector2 b, float speed)
        {
            return new Vector2(Mathf.Lerp(a.x, b.x, speed), Mathf.Lerp(a.y, b.y, speed));
        }

    }
}
    
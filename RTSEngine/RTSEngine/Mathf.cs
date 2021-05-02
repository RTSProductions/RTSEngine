using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTSEngine.RTSEngine
{
    public class Mathf
    {
        /// <summary>
        /// Linearly intorpolates from the first float to the secound float using the speed peramitor called by.
        /// </summary>
        /// <param name="firstFloat"></param>
        /// <param name="secondFloat"></param>
        /// <param name="by"></param>
        /// <returns></returns>
        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }

        /// <summary>
        /// Clamps the value of the given float.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Clamp(float value, float min, float max)
        {
            float newValue = value;

            if (newValue > max)
            {
                newValue = max;
            }
            if (newValue < min)
            {
                value = min;
            }
            return newValue;
        }
    }
}

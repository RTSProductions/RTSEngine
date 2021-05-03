using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RTSEngine.RTSEngine
{
   
    public class Time
    {
        public static float time;

        public Time()
        {
           
        }

        public static void wait(float x)
        {
            DateTime t = DateTime.Now;
            DateTime tf = DateTime.Now.AddSeconds(x);

            while (t < tf)
            {
                t = DateTime.Now;
            }
        }

        void waitTime(float x)
        {
            DateTime t = DateTime.Now;
            DateTime tf = DateTime.Now.AddSeconds(x);

            while (t < tf)
            {
                t = DateTime.Now;
                time++;
                Log.Info("The time is " + time);
            }
        }
    }
}

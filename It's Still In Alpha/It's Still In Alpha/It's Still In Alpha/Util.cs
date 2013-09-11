using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace It_s_Still_In_Alpha
{
    public class Util
    {
        public static double degreesToRadians(double degrees)
        {
            return ((double)(degrees / 180.0f)) * Math.PI;
        }

        public static double radiansToDegrees(double radians)
        {
            return ((double)(radians / Math.PI)) * 180.0f;
        }

        public static double cos( double radians)
        {
            return Math.Cos(radians);
        }

        public static double sin(double radians)
        {
            return Math.Sin(radians);
        }
    }
}
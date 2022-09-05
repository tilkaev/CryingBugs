using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CryingBugs.Core
{
    internal struct Dimensions
    {
        public double x;
        public double y;
        public double w;
        public double h;
        public double r;

        public static bool CollisionDetection(Dimensions a, Dimensions b)
        {
            /*
            if (a.min.X > b.min.X & 
                a.max.X < b.max.X &
                a.min.Y < b.min.Y & 
                a.max.Y > b.max.Y)
                return true;

            return false;*/

            if (a.x < b.x + b.w &
                a.x + a.w > b.x &
                a.y < b.y + b.h &
                a.h + a.y > b.y)
                return true;
            else
                return false;
        }

    }
}

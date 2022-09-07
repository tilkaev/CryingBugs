using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CryingBugs.Models
{
    internal struct Shape
    {
        public double x;
        public double y;
        public double w;
        public double h;
        public double radius;

        public (double X, double Y) min;
        public (double X, double Y) max;

        public static bool collideCircle(Shape a, Shape b)
        {
            /*
            if (a.min.X > b.min.X & 
                a.max.X < b.max.X &
                a.min.Y < b.min.Y & 
                a.max.Y > b.max.Y)
                return true;

            return false;*/

            var dx = a.x - b.x;
            var dy = a.y - b.y;
            var distance = Math.Sqrt(dx * dx + dy * dy);
            var sumOfRadii = a.radius + b.radius;
            if (distance < sumOfRadii)
                return true;

            return false;
        }

        public static bool collideSquare(Shape a, Shape b)
        {
            if (a.min.X > b.min.X &
                a.max.X < b.max.X &
                a.min.Y < b.min.Y &
                a.max.Y > b.max.Y)
                return true;
            return false;

        }

    }
}

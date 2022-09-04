using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CryingBugs.Core
{
    internal struct AABB
    {
        public Vector min;
        public Vector max;

        public static bool vsAABB(AABB a, AABB b)
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

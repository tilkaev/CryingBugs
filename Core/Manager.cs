using CryingBugs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace CryingBugs.Core
{
    internal struct Manager
    {
        public static Canvas MainLayout { get; set; }

        public static List<Target> targets { get; set; }

        public static List<Bug> bugs { get; set; }

        public static TextBox DebugTB { get; set; }

        public static double Speed { get; set; }
    }
}

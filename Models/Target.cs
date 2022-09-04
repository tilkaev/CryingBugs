using CryingBugs.Core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CryingBugs.Models
{
    internal class Target
    {
        UIElement element;
        public AABB aabb;
        private Vector _position;
        public int Radius = 4;
        int pixel = 5;


        Random rnd = new Random();

        public Target()
        {
            element = new Ellipse
            {
                Width = pixel* Radius,
                Height = pixel * Radius,
                Fill = new SolidColorBrush(Colors.DarkRed),
            };
            element.MouseDown += Ellipse_MouseDown;
            Manager.MainLayout.Children.Add(element);

            var width = Convert.ToInt32(Properties.Resources.Width);
            var height = Convert.ToInt32(Properties.Resources.Height);
            _position = new Vector
            {
                X = rnd.Next(0, width-pixel* Radius),
                Y = rnd.Next(0, height - pixel * Radius)
            };
            Canvas.SetLeft(element, _position.X);
            Canvas.SetTop(element, _position.Y);

            aabb = new AABB
            {
                min = new Vector(_position.X, _position.Y + pixel*Radius),
                max = new Vector(_position.X + pixel * Radius, _position.Y)
            };
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(_position.ToString());
        }
    }
}

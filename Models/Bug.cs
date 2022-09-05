using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Text;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using CryingBugs.Core;
using System.Threading.Tasks;

namespace CryingBugs.Models
{
    internal class Bug : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        UIElement element;
        Dimensions dimensions;

        public Vector _way;
        private Vector _position;
        double Speed;
        int pixel = 5;
        float hearingDistance = 50;

        Target DesiredTarget = null; // Искомая цель
        List<double> distanceToTarget; // Расстояние до определенной цели
        int indexForScream = 0; // Индекс для крика определенной цели
        int maxIndexForScream = Manager.targets.Count; // Индекс для крика определенной цели


        Random rnd = new Random();

        public void TargetFound(Target target)
        {
            if (DesiredTarget == target || DesiredTarget == null)
            {

            }
        }

        public void ToListen(Target target, double distance)
        {

        }

        public void Cry()
        {
            foreach (var item in Manager.bugs)
            {
                if (item != this)
                {
                    /*
                    var newAABB = new Dimensions
                    {
                        min = new Vector(item.aabb.min.X - hearingDistance, item.aabb.min.Y + hearingDistance),
                        max = new Vector(item.aabb.min.X + hearingDistance, item.aabb.min.Y - hearingDistance)
                    };
                    var element1 = new Ellipse
                    {
                        Width = 2,
                        Height = 2,
                        Fill = new SolidColorBrush(Colors.Yellow),
                    };
                    Manager.MainLayout.Children.Add(element1);
                    Canvas.SetLeft(element1, newAABB.min.X);
                    Canvas.SetTop(element1, newAABB.min.Y);
                    var element2 = new Ellipse
                    {
                        Width = 2,
                        Height = 2,
                        Fill = new SolidColorBrush(Colors.Yellow),
                    };
                    Manager.MainLayout.Children.Add(element2);
                    Canvas.SetLeft(element2, newAABB.max.X);
                    Canvas.SetTop(element2, newAABB.max.Y);

                    if (Dimensions.CollisionDetection(item.aabb, newAABB))
                    {

                    }*/
                   
                }
            }
        }

        public Bug()
        {
            element = new Ellipse
            {
                Width = pixel,
                Height = pixel,
                Fill = new SolidColorBrush(Colors.White),
            };
            Manager.MainLayout.Children.Add(element);

            var width = Convert.ToInt32(Properties.Resources.Width);
            var height = Convert.ToInt32(Properties.Resources.Height);
            Position = new Vector
            {
                X = rnd.Next(0, width),
                Y = rnd.Next(0, height)
            };
            _way = new Vector
            {
                X = rnd.NextDouble() * 2 - 1,
                Y = rnd.NextDouble() * 2 - 1
            };
            Normalize();
        }

        void Normalize()
        {
            Speed = Math.Sqrt(_way.X * _way.X + _way.Y * _way.Y);//вычислили длину вектора
            _way.X *= 1 / Speed;//нормализуем вектор
            _way.Y *= 1 / Speed;
        }

        public void Step()
        {
            var newPos = new Vector(_position.X + _way.X, _position.Y + _way.Y);
            //var newPos = new Vector(_position.X + _way.X * Speed, _position.Y + _way.Y * Speed);
            var newDimensions = new Dimensions
            {
                x = newPos.X,
                y = newPos.Y,
                w = pixel,
                h = pixel
            };


            if (newPos.Y < 0 ||
                newPos.Y > Convert.ToInt32(Properties.Resources.Height))
            {
                _way.Y *= -1;
                return;
            }
            if (newPos.X < 0 ||
                newPos.X > Convert.ToInt32(Properties.Resources.Width))
            {
                _way.X *= -1;
                return;
            }

            
            foreach (var item in Manager.targets)
            {
                if (Dimensions.CollisionDetection(newDimensions, item.aabb))
                {
                    TargetFound(item);
                    _way.X *= -1;
                    _way.Y *= -1;
                    return;
                }
            }
            Position = newPos;
            Cry();
        }


        public Vector Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            Canvas.SetLeft(element, _position.X);
            Canvas.SetTop(element, _position.Y);

            dimensions = new Dimensions
            {
                x = _position.X,
                y = _position.Y
            };
        }
    }





}

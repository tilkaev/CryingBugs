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
using System.Linq;


namespace CryingBugs.Models
{
    internal class Bug : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public int id;
        UIElement element;
        Shape shape;

        public Vector _way;
        private Vector _position;
        public double Speed;
        int pixel = 6;
        float hearingDistance = 50;

        Target MainTarget; // Искомая цель
        Dictionary<Target, double> dictDistanceToTarget; // Расстояние до определенной цели
        int indexScream = 0; // Индекс для крика определенной цели
        int maxIndexScream = Manager.targets.Count - 1; // Максимальный индекс для крика определенной цели


        public int _indexScream
        {
            get { return indexScream; }
            set { indexScream = indexScream < maxIndexScream ? value : 0; }
        }

        Random rnd = new Random();


        public void ToListen(Target target, double distance, Bug bug_s)
        {
            if (distance < dictDistanceToTarget[target])
            {
                dictDistanceToTarget[target] = distance;
                if (MainTarget == target)
                {
                    _way = new Vector
                    {
                        X = bug_s.Position.X - Position.X,
                        Y = bug_s.Position.Y - Position.Y
                    };
                    _way.Normalize();
                }
            }
            
        }



        public void Cry()
        {
            foreach (var bug in Manager.bugs)
            {
                if (bug != this)
                {

                    var newShape = new Shape
                    {
                        x = shape.x,
                        y = shape.y,
                        radius = pixel / 2 + hearingDistance / 2
                    };


                    //Manager.DebugTB.Text = $"{id} | {dictDistanceToTarget.ElementAt(indexScream).Value + hearingDistance} - {dictDistanceToTarget.ElementAt(indexScream).Key.id} | main-{MainTarget.id}\n"+ Manager.DebugTB.Text;
                    if (Shape.collideCircle(bug.shape, newShape))
                    {
                        bug.ToListen(dictDistanceToTarget.ElementAt(indexScream).Key,
                            dictDistanceToTarget.ElementAt(indexScream).Value + hearingDistance,
                            this);
                    }


                    _indexScream++;
                }
            }
        }

        public Bug()
        {
            dictDistanceToTarget = new Dictionary<Target, double>();
            MainTarget = Manager.targets[rnd.Next(0, Manager.targets.Count)];
            foreach (var item in Manager.targets)
            {
                dictDistanceToTarget.Add(item, rnd.Next(0, Convert.ToInt32(Properties.Resources.Width)));
            }

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
            Speed = Math.Sqrt(_way.X * _way.X + _way.Y * _way.Y);//вычисляем длину вектора
            _way.X *= 1 / Speed; //нормализуем вектор
            _way.Y *= 1 / Speed;
        }

        public void Step()
        {
            var newPos = new Vector(_position.X + _way.X, _position.Y + _way.Y);
            //var newPos = new Vector(_position.X + _way.X * Speed, _position.Y + _way.Y * Speed);

            // Шаг
            var shape = new Shape
            {
                x = newPos.X + pixel / 2,
                y = newPos.Y + pixel / 2,
                radius = pixel / 2
            };


            // Проверка на столконовение границ
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

            // Проверка какая цель перед Bug, если main -> развернуться, иначе продолжить путь
            foreach (var target in Manager.targets)
            {
                if (Shape.collideCircle(shape, target.shape))
                {
                    dictDistanceToTarget[target] = 0;
                    if (MainTarget == target)
                    {
                        _indexScream += 1;
                        MainTarget = dictDistanceToTarget.ElementAt(_indexScream).Key;
                        _way.X *= -1;
                        _way.Y *= -1;

                        return;
                    }

                }
            }
            for (int i = 0; i < dictDistanceToTarget.Count; i++)
            {
                dictDistanceToTarget[dictDistanceToTarget.ElementAt(i).Key] += 1;
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

            shape = new Shape
            {
                x = _position.X,
                y = _position.Y
            };
        }
    }





}

using CryingBugs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryingBugs
{
    public partial class MainWindow : Window
    {
        int width, height;
        int pixel = 5;


        const float fps = 100;
        const float dt = 1 / fps;
        float accumulator = 0;

        /*
        // Единицы измерения - секунды
        float frameStart = DateTime.Today();

// основной цикл
while(true){
  const float currentTime = GetCurrentTime();

        // Сохраняется время, прошедшее с начала последнего кадра
        accumulator += currentTime - frameStart();

        // Записывается начало этого кадра
        frameStart = currentTime;

  while(accumulator > dt){
    UpdatePhysics(dt );
        accumulator -= dt;}

  RenderGame()
            }*/


        Ellipse el;
        public MainWindow()
        {
            InitializeComponent();

            Manager.MainLayout = mainLayout;

            width = (int)mainLayout.Width;
            height = (int)mainLayout.Height;

            el = new Ellipse
            {
                Width = pixel,
                Height = pixel,
                Fill = new SolidColorBrush(Colors.White),
            };

            mainLayout.Children.Add(el);

            Start();
        }

        public async void Start()
        {
            int x = 1;
            while (true)
            {

                Canvas.SetTop(el, x++);
                await Task.Delay(TimeSpan.FromSeconds(dt));
            }
        }


    }
}

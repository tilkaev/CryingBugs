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
using System.Windows.Shapes;

namespace CryingBugs
{
    /// <summary>
    /// Логика взаимодействия для RemoteControl.xaml
    /// </summary>
    public partial class RemoteControl : Window
    {
        public RemoteControl()
        {
            InitializeComponent();
        }

        private void sliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            foreach (var bug in Manager.bugs)
            {
                Manager.Speed = sliderSpeed.Value;
            }
            currentSpeed.Text = sliderSpeed.Value.ToString();
        }
    }
}

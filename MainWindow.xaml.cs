using CryingBugs.Core;
using CryingBugs.Models;
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
        const float fps = 100;
        const float dt = 1 / fps;

        int qtyTargets = 2;
        int qtyBugs = 200;


        public MainWindow()
        {
            InitializeComponent();
            Manager.MainLayout = mainLayout;
            Manager.targets = new List<Target>(qtyTargets);
            Manager.bugs = new List<Bug>(qtyBugs);

            for (int i = 0; i < qtyTargets; i++)
            {
                var el = new Target();
                el.id = i;
                Manager.targets.Add(el);
            }

            for (int i = 0; i < qtyBugs; i++)
            {
                var el = new Bug();
                el.id = i;
                Manager.bugs.Add(el);
            }

            var win = new Debug();
            win.Show();
            Manager.DebugTB = win.debugtextbox;
            var win2 = new RemoteControl();
            win2.Show();

            Start();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //pass
            this.DragMove();
        }

        public async void Start()
        {
            while (true)
            {
                foreach (var item in Manager.bugs)
                {
                    item.Step();
                }
                await Task.Delay(TimeSpan.FromSeconds(dt / Manager.Speed));
                Manager.DebugTB.Text = "";
            }
        }


    }
}

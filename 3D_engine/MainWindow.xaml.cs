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

namespace _3D_engine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Engine engine;
        bool WkeyDown = false;
        bool SkeyDown = false;
        bool AkeyDown = false;
        bool DkeyDown = false;

        int speed = 5;
        double rotatespeed = 0.05;
        public MainWindow()
        {
            InitializeComponent();
            engine = new();
        }
        private void PrintBlocks()
        {
            foreach (Block x in engine.GetListOfObjects())
            {
                var list = x.ReturnLines(engine.Angle);
                foreach (Line line in list)
                    GameGrid.Children.Add(line);
            }
        }
        private void GameLogic()
        {
            if (WkeyDown)
                engine.Move(-speed);
            if (SkeyDown)
                engine.Move(speed);
            if (AkeyDown)
                engine.Rotate(-rotatespeed);
            if (DkeyDown)
                engine.Rotate(rotatespeed);

        }

        private void MoveRotate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
                WkeyDown = true;
            if (e.Key == Key.S)
                SkeyDown = true;
            if (e.Key == Key.A)
                AkeyDown = true;
            if (e.Key == Key.D)
                DkeyDown = true;
        }

        private void StopMoveRotate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
                WkeyDown = false;
            if (e.Key == Key.S)
                SkeyDown = false;
            if (e.Key == Key.A)
                AkeyDown = false;
            if (e.Key == Key.D)
                DkeyDown = false;
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                await Task.Delay(1000 / 144);
                GameLogic();
                GameGrid.Children.Clear();
                PrintBlocks();
            }
        }
    }
}

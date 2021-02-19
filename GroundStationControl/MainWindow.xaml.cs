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

namespace GroundStationControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool downKeyPressed = false;
        private bool upKeyPressed = false;
        private bool rightKeyPressed = false;
        private bool leftKeyPressed = false;
        private int keysPressed = 0;
        public static MainWindow window;

        public MainWindow()
        {
            InitializeComponent();
            this.UpArrow.Background = Brushes.Gray;
            this.DownArrow.Background = Brushes.Gray;
            this.LeftArrow.Background = Brushes.Gray;
            this.RightArrow.Background = Brushes.Gray;
            Communication.OpenCommunication();
            window = this;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;
            System.Threading.Thread.Sleep(20);
            switch (key)
            {
                case Key.Up:
                    if(keysPressed < 2 && !upKeyPressed)
                    {
                        this.UpArrow.Background = Brushes.Green;
                        upKeyPressed = true;
                        keysPressed++;
                    }
                    break;
                case Key.Down:
                    if (keysPressed < 2 && !downKeyPressed)
                    {
                        this.DownArrow.Background = Brushes.Green;
                        downKeyPressed = true;
                        keysPressed++;
                    }
                    break;
                case Key.Left:
                    if (keysPressed < 2 && !leftKeyPressed)
                    {
                        this.LeftArrow.Background = Brushes.Green;
                        leftKeyPressed = true;
                        keysPressed++;
                    }
                    break;
                case Key.Right:
                    if (keysPressed < 2 && !rightKeyPressed)
                    {
                        this.RightArrow.Background = Brushes.Green;
                        rightKeyPressed = true;
                        keysPressed++;
                    }
                    break;
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            var key = e.Key;
            switch (key)
            {
                case Key.Up:
                    if (upKeyPressed)
                    {
                        this.UpArrow.Background = Brushes.Gray;
                        upKeyPressed = false;
                        keysPressed--;
                    }
                    break;
                case Key.Down:
                    if (downKeyPressed)
                    {
                        this.DownArrow.Background = Brushes.Gray;
                        downKeyPressed = false;
                        keysPressed--;
                    }
                    break;
                case Key.Left:
                    if (leftKeyPressed)
                    {
                        this.LeftArrow.Background = Brushes.Gray;
                        leftKeyPressed = false;
                        keysPressed--;
                    }
                    break;
                case Key.Right:
                    if (rightKeyPressed)
                    {
                        this.RightArrow.Background = Brushes.Gray;
                        rightKeyPressed = false;
                        keysPressed--;
                    }
                    break;
            }
        }


    }
}

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
    public partial class MainWindow : Window
    {
        const int MAX_KEYS = 1;
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;
            
            System.Threading.Thread.Sleep(20);
            switch (key)
            {
                case Key.Up:
                    if (keysPressed < MAX_KEYS && !upKeyPressed)
                    {
                        this.UpArrow.Background = Brushes.Green;
                        upKeyPressed = true;
                        keysPressed++;
                    }
                    break;
                case Key.Down:
                    if (keysPressed < MAX_KEYS && !downKeyPressed)
                    {
                        this.DownArrow.Background = Brushes.Green;
                        downKeyPressed = true;
                        keysPressed++;
                    }
                    break;
                case Key.Left:
                    if (keysPressed < MAX_KEYS && !leftKeyPressed)
                    {
                        this.LeftArrow.Background = Brushes.Green;
                        leftKeyPressed = true;
                        keysPressed++;
                    }
                    break;
                case Key.Right:
                    if (keysPressed < MAX_KEYS && !rightKeyPressed)
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

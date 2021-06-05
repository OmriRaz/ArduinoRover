using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GroundStationControl
{
    public partial class MainWindow : Window
    {
        const int MAX_KEYS = 1;
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;

            System.Threading.Thread.Sleep(20); // short delay between sending move commands and processing what keys are pressed
            switch (key)
            {   // switch case makes sure the user doesn't press more than the amount that is allowed to press at a time
                // if he can press, it will also check if that key isn't already pressed and if not, set the key to pressed (change color and update var)
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
            {   // switch case for releasing keys (update color and vars)
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
        private void OnClickOverride(object sender, RoutedEventArgs e)
        {
            overrideEnabled = !overrideEnabled;
            if (overrideEnabled)
                this.OverrideButton.Background = Brushes.Green;
            else
                this.OverrideButton.Background = Brushes.Gray;
            
        }
    }
}

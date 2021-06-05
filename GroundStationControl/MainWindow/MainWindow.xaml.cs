using System;
using System.Windows;
using System.Windows.Media;

namespace GroundStationControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool downKeyPressed = false;
        public static bool upKeyPressed = false;
        public static bool rightKeyPressed = false;
        public static bool leftKeyPressed = false;
        public static bool overrideEnabled = false;
        private int keysPressed = 0;
        public static MainWindow window; // instance to the main window, so that other classes can access and edit it.

        public MainWindow()
        {
            InitializeComponent();

            this.UpArrow.Background = Brushes.Gray;
            this.DownArrow.Background = Brushes.Gray;
            this.LeftArrow.Background = Brushes.Gray;
            this.RightArrow.Background = Brushes.Gray;

            this.OverrideButton.Background = Brushes.Gray;

            Communication.HandleCommunication(); // open serial communication with Ground Station Arduino
            window = this; // set the main window instance
        }
    }
}

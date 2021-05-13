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

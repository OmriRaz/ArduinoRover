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
    }
}

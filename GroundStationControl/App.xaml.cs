using System.Windows;

namespace GroundStationControl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Communication.port.Close(); // close serial port as it stays open even when the application terminates
        }
    }
}

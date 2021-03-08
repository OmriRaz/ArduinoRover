using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;

namespace GroundStationControl
{
    public static class Communication
    {
        public static SerialPort port;
        public const int BYTES_READ = 100;
        public static async void OpenCommunication()
        {
            port = new SerialPort("COM4", 9600); // SerialPort.GetPortNames();
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                char[] buffer = new char[BYTES_READ];
                while (true)
                {
                    try
                    {
                        if(!port.IsOpen)
                        {
                            port.Open();
                            MainWindow.window.Dispatcher.Invoke(() =>
                            {
                                MainWindow.window.MainText.Text = "Connected!";
                            });
                        }

                        //TO DO: port.Write() - write data to the arduino, according to arrows pressed (move car)

                        port.Read(buffer, 0, BYTES_READ); // read sensors data
                        MainWindow.window.Dispatcher.Invoke(() =>
                        {
                            MainWindow.window.MainText.Text = new string(buffer);
                        });

                        // TO DO: analyze buffer and display data on UI

                    }
                    catch (Exception ex)
                    {
                        MainWindow.window.Dispatcher.Invoke(() =>
                        {
                            MainWindow.window.MainText.Text = "ERROR: " + ex.Message;
                        });
                    }
                }
            });
        }
    }
}

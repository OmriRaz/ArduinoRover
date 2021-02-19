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
        public static async void OpenCommunication()
        {
            port = new SerialPort("COM4", 9600);
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
                while (true)
                {
                    try
                    {
                        port.Open();
                        String s = Console.ReadLine();
                        if (s.Equals("exit"))
                        {
                            break;
                        }
                        port.Write(s + '\n');
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

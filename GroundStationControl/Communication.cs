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
        public const int BYTES_READ = 32;
        // DATA STRUCTURE: ~TEMP|HUMIDITY%|PRESSURE|SEA_PRESSURE|GAS_VALUE|IR_VALUE~
        // ↓
        private const int SEPARATORS_COUNT = 5;
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

                        //port.Write(GetMovementString().ToCharArray(), 0, 4);

                        System.Threading.Thread.Sleep(500);
                        port.Read(buffer, 0, BYTES_READ); // read sensors data
                        string bufferString = new string(buffer);
                        port.Read(buffer, 0, BYTES_READ); // read sensors data
                        bufferString += new string(buffer);

                        // find start and end of data
                        int startSymbolIndex = bufferString.IndexOf('~');
                        int endSymbolIndex = bufferString.IndexOf('~', startSymbolIndex + 1);

                        if (startSymbolIndex != -1 && startSymbolIndex+1 < bufferString.Length && endSymbolIndex != -1)
                        {
                            string data = bufferString.Substring(startSymbolIndex + 1, endSymbolIndex - 2);
                            MainWindow.window.Dispatcher.Invoke(() =>
                            {
                                MainWindow.window.MainText.Text = data;
                            });
                        }
                        port.DiscardInBuffer();
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
        private static string GetMovementString()
        {
            // string consists of: L+ L- R+ R-
            string moveString = "0000";
            if(MainWindow.upKeyPressed)
            {
                moveString = "1010";
            }
            else if(MainWindow.downKeyPressed)
            {
                moveString = "0101";
            }
            else if(MainWindow.leftKeyPressed)
            {
                moveString = "1001";
            }
            else if(MainWindow.rightKeyPressed)
            {
                moveString = "0110";
            }
            
            return moveString;
        }
    }
}

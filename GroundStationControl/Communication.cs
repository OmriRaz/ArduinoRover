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
using System.Text.RegularExpressions;

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
                char[] moveBuffer = new char[1];
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

                        moveBuffer[0] = GetMovementString(); // get command for key pressed
                        port.Write(moveBuffer, 0, 1); // write command to arduino 

                        moveBuffer[0] = Constants.SWITCH_CHAR; 
                        port.Write(moveBuffer, 0, 1);

                        System.Threading.Thread.Sleep(20);
                        
                        port.Read(buffer, 0, BYTES_READ); // read sensors data
                        string bufferString = new string(buffer);
                        port.Read(buffer, 0, BYTES_READ); // read sensors data
                        bufferString += new string(buffer);

                        // find start and end of data
                        int startSymbolIndex = bufferString.IndexOf('~');
                        int endSymbolIndex = bufferString.IndexOf('~', startSymbolIndex + 1);
                        string data = "";

                        if (startSymbolIndex != -1 && startSymbolIndex+1 < bufferString.Length && endSymbolIndex != -1)
                        {
                            data = bufferString.Substring(startSymbolIndex + 1, endSymbolIndex - 1);
                            // REGEX for checking if string in format
                            // [-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|]([-+]?[0-9]*)[|][-+]?[0-9]*[|][-+]?[0-9]*
                            Regex regex = new Regex(@"[-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|]([-+]?[0-9]*)[|][-+]?[0-9]*[|][-+]?[0-9]*");
                            Match match = regex.Match(data);
                            if (match.Success)
                            {
                                MainWindow.window.Dispatcher.Invoke(() =>
                                {
                                    MainWindow.window.MainText.Text = data;
                                });

                                ParseAndShowData(data);
                            }
                        }

                        

                        port.DiscardInBuffer();
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage(ex.Message);
                    }
                }
            });
        }
        private static char GetMovementString()
        {
            // string consists of: L+ L- R+ R-
            char moveChar = '0'; // 0000
            if(MainWindow.upKeyPressed)
            {
                moveChar = '1'; // 1010
            }
            else if(MainWindow.downKeyPressed)
            {
                moveChar = '2'; // 0101
            }
            else if(MainWindow.leftKeyPressed)
            {
                moveChar = '3'; // 1001
            }
            else if(MainWindow.rightKeyPressed)
            {
                moveChar = '4'; // 0110
            }
            
            return moveChar;
        }

        public static void ParseAndShowData(string data)
        {
            try
            {
                MainWindow.window.Dispatcher.Invoke(() =>
                {
                    MainWindow.window.TemperatureText.Text =  data.Substring(0, data.IndexOf('|')) + "° C";
                    data = data.Remove(0, data.IndexOf('|') + 1);

                    MainWindow.window.HumidityText.Text = data.Substring(0, data.IndexOf('|')) + "% Humidity";
                    data = data.Remove(0, data.IndexOf('|') + 1);

                    MainWindow.window.PressureText.Text = data.Substring(0, data.IndexOf('|')) + " hPa";
                    data = data.Remove(0, data.IndexOf('|') + 1);

                    MainWindow.window.SeaPressureText.Text = data.Substring(0, data.IndexOf('|')) + " hPa (Sea)";
                    data = data.Remove(0, data.IndexOf('|') + 1);

                    int gas = int.Parse(data.Substring(0, data.IndexOf('|')));
                    if(gas >= Constants.SMOKE_DETECTED_VALUE)
                    {
                        MainWindow.window.GasText.Text = "Gas Value: " + gas + "\nSMOKE DETECTED";
                    }
                    else
                    {
                        MainWindow.window.GasText.Text = "Gas Value: " + gas;
                    }

                    data = data.Remove(0, data.IndexOf('|') + 1);

                    MainWindow.window.IRText.Text = "IR Value: " + data.Substring(0, data.IndexOf('|'));
                    data = data.Remove(0, data.IndexOf('|') + 1);

                    if(int.Parse(data) != -1) // -1 value means PM is in process of collecting data, so this wont show the -1 till we get an actual value
                        MainWindow.window.DustText.Text = "PM 2.5: " + data + " µm"; // last piece of data is just PM value
                });
                
            }
            catch(Exception e)
            {
                ErrorMessage(e.Message);
            }
        }
        public static void ErrorMessage(string error)
        {
            MainWindow.window.Dispatcher.Invoke(() =>
            {
                MainWindow.window.MainText.Text = "ERROR: " + error;
            });
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Threading; // Threading for sleep function and async tasks (communication)
using System.IO.Ports; // Serial
using System.Text.RegularExpressions; // regex

namespace GroundStationControl
{
    public static class Communication
    {
        public static SerialPort port; // variable holding the instance for the serial communication
        public static bool stepDetected = false; // variable for keeping track of if the rover is near a step

        // DATA STRUCTURE: ~TEMP|HUMIDITY%|PRESSURE|SEA_PRESSURE|GAS_VALUE|IR_VALUE|PARTICLES~
        // this is the structure for strings received from the Ground Station Arduino so we can parse it into separate data
        public static async void HandleCommunication()
        {
            port = new SerialPort(Constants.COM_STRING, Constants.BAUD_RATE); // opens the serial communication with the Ground Station Arduino at the baud rate given
            await Task.Run(() => // async task for communication, so GUI can work/update while the application communicates
            {
                Thread.Sleep(2000); // 2 seconds delay to let the Arduino start things up
                char[] moveBuffer = new char[1]; // moveBuffer - will hold the move command to send to the 
                // Ground Station Arduino according to keys pressed
                while (true)
                {
                    try
                    {
                        if(!port.IsOpen) // if port isn't open, open it
                        { // if port.Open fails, it'll throw an exception causing this if to be checked again. This is to avoid
                            // reading/writing on a NULL instance
                            port.Open();
                            MainWindow.window.Dispatcher.Invoke(() =>
                            {
                                MainWindow.window.MainText.Text = "Connected!";
                            });
                        }

                        moveBuffer[0] = GetMovementChar(); // get move command for key pressed
                        port.Write(moveBuffer, 0, 1); // write move command to arduino 

                        string bufferString = port.ReadLine(); // read data from arduino (sensors data)

                        MainWindow.window.Dispatcher.Invoke(() =>
                        {
                            MainWindow.window.MainText.Text = bufferString; // for debugging, to see the messages received from the arduino
                        });

                        // find start and end of data
                        int startSymbolIndex = bufferString.IndexOf('~');
                        int endSymbolIndex = bufferString.IndexOf('~', startSymbolIndex + 1);
                        string data = "";

                        // following if checks if it found a start & end symbol, and also that the string has proper length
                        if (startSymbolIndex != -1 && startSymbolIndex + 1 < bufferString.Length && endSymbolIndex != -1)
                        {
                            data = bufferString.Substring(startSymbolIndex + 1, endSymbolIndex - 1); // cut out the start & end symbol

                            // REGEX for checking if string in format
                            // [-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|]([-+]?[0-9]*)[|][-+]?[0-9]*[|][-+]?[0-9]*
                            Regex regex = new Regex(@"[-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|][-+]?[0-9]*(\.[0-9]+)[|]([-+]?[0-9]*)[|][-+]?[0-9]*[|][-+]?[0-9]*");
                            Match match = regex.Match(data); // checks if string is in the above Regex format
                            if (match.Success)
                            { // confirmed data is in correct format, so now update main text (for debugging) and parse the data
                              // so that it can be displayed properly using each data's units
                                MainWindow.window.Dispatcher.Invoke(() =>
                                {
                                    MainWindow.window.MainText.Text = data;
                                });

                                ParseAndShowData(data);
                            }
                        }                    
                        port.DiscardInBuffer(); // clear buffer after reading so that we can get fresh data
                    }
                    catch (Exception ex) // caught an exception (can be triggered by regex, symbol finding, parsing data)
                    {
                        ErrorMessage(ex.Message); // display the error message
                    }
                }
            });
        }
        private static char GetMovementChar()
        {
            char moveChar = '0';
            if(MainWindow.upKeyPressed)
            {
                if(MainWindow.overrideEnabled || !stepDetected)
                    moveChar = '1'; // forward
            }
            else if(MainWindow.downKeyPressed)
            {
                moveChar = '2'; // backward
            }
            else if(MainWindow.leftKeyPressed)
            {
                if (MainWindow.overrideEnabled || !stepDetected)
                    moveChar = '3'; // left
            }
            else if(MainWindow.rightKeyPressed)
            {
                if (MainWindow.overrideEnabled || !stepDetected)
                    moveChar = '4'; // right
            }
            
            return moveChar;
        }

        /* Receives the data string received from the Arduino, parses and displays it
         * data is parsed by taking the string from the start of data till the separator ('|')
         * and then cutting that data till the separator (including the separator)
         */ 
        private static void ParseAndShowData(string data)
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

                    int ir = int.Parse(data.Substring(0, data.IndexOf('|')));
                    if (ir >= Constants.STEP_DETECTED_VALUE)
                    {
                        MainWindow.window.IRText.Text = "IR Value: " + ir + "\nSTEP DETECTED";
                        stepDetected = true;
                    }
                    else
                    {
                        stepDetected = false;
                        MainWindow.window.IRText.Text = "IR Value: " + ir;
                    }

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
        public static void ErrorMessage(string error) // updates main text with error message
        {
            MainWindow.window.Dispatcher.Invoke(() =>
            {
                MainWindow.window.MainText.Text = "ERROR: " + error;
            });
        }
    }
}

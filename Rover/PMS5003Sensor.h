#include "Adafruit_PM25AQI.h"

#include <SoftwareSerial.h>
SoftwareSerial pmSerial(7, 8); // TX RX
 
Adafruit_PM25AQI aqi = Adafruit_PM25AQI();
 
void initializePMSSensor()
{
  pmSerial.begin(9600);

  if (! aqi.begin_UART(&pmSerial)) { // connect to the sensor over software serial 
    Serial.println("Could not find PM 2.5 sensor!");
  }
}
 
int getPMSValues()
{
  PM25_AQI_Data data;
  
  if (! aqi.read(&data)) {
    return -1;
  }
  Serial.print(F("PM 2.5: ")); Serial.println(data.pm25_standard);
  return data.pm25_standard;
}

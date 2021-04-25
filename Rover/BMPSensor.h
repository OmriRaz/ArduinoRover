#include <SFE_BMP180.h>

#define ALTITUDE 179

SFE_BMP180 bmp;

void initializeBMPSensor()
{
    if (bmp.begin())
    {}
    else
    {
      // connection problem
      Serial.println("BMP180 initialization fail (disconnected)\n\n");
    }
}

void getBMPValues(double* pressure, double* seaPressure)
{
  double temperature;
  char status = bmp.startTemperature();
  status = bmp.getPressure(*pressure, temperature);
        if (status != 0) {
          /*Serial.print("Pressure measurement: ");
          Serial.print(*pressure);
          Serial.println(" hPa");*/

          *seaPressure = bmp.sealevel(*pressure, ALTITUDE);
          /*Serial.print("Sea-level pressure: ");
          Serial.print(*seaPressure);
          Serial.println("hPa");*/
        }

}

#include "DHT.h"

#define DHTPIN 2     

#define DHTTYPE DHT11   

#include <SFE_BMP180.h>
#include <Wire.h>

#define ALTITUDE 179

DHT dht(DHTPIN, DHTTYPE);
const int gasPin = A0; //GAS sensor output pin to Arduino analog A0 pin
SFE_BMP180 pressure;


void setup() 
{
  Serial.begin(9600);
  dht.begin();

  if (pressure.begin())
  {}
  else
  {
    // Oops, something went wrong, this is usually a connection problem,
    // see the comments at the top of this sketch for the proper connections.

    Serial.println("BMP180 init fail (disconnected?)\n\n");
    while(1); // Pause forever.
  }
}

void loop() 
{
  
  float h = dht.readHumidity();
  float t = dht.readTemperature();
  Serial.print("Temperature: ");
  Serial.print(t);
  Serial.print("° Celsius\tHumidity: ");
  Serial.print(h);
  Serial.println("%");
  
  if (isnan(h) || isnan(t)) 
  {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }

  Serial.print("Gas value: ");
  Serial.println(analogRead(gasPin));
  Serial.println();

  double T, P, p0; //Creating variables for temp, pressure and relative pressure
  char status = pressure.startTemperature();
  status = pressure.getPressure(P, T);
        if (status != 0) {
          Serial.print("Pressure measurement: ");
          Serial.print(P);
          Serial.println(" hPa");

          p0 = pressure.sealevel(P, ALTITUDE);
          Serial.print("Sea-level pressure: ");
          Serial.print(p0);
          Serial.println("hPa\n");
        }
  delay(5000);
}

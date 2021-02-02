#include "DHT.h"

#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>

#define DHTPIN 2     

#define DHTTYPE DHT11   

#include <SFE_BMP180.h>
#include <Wire.h>

#define ALTITUDE 179

DHT dht(DHTPIN, DHTTYPE);
const int gasPin = A0; //GAS sensor output pin to Arduino analog A0 pin
SFE_BMP180 pressure;
RF24 radio(9, 10); // CE, CSN  

const byte address[6] = "00001";     //Byte of array representing the address. This is the address where we will send the data. This should be same on the receiving side.


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


  radio.begin();                  //Starting the Wireless communication
radio.openWritingPipe(address); //Setting the address where we will send the data
radio.setPALevel(RF24_PA_MIN);  //You can set it as minimum or maximum depending on the distance between the transmitter and receiver.
radio.stopListening();          //This sets the module as transmitter
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

   const char text[] = "Hello!";
    radio.write(&text, sizeof(text));                  //Sending the message to receiver
  delay(5000);
}

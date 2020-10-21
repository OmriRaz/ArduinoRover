//Nir Naknik

//========Include Libraries========//
#include <TinyGPS++.h>
#include <SoftwareSerial.h>
//================================//


//4800-baud serial GPS device hooked up on pins 4(rx) and 3(tx).

static const int RXPin = 4, TXPin = 3; //Software Serial connections
static const uint32_t GPSBaud = 4800;  //SoftwareSerial Baud rate for GPS communication
TinyGPSPlus gps;                       //Define GPS object
SoftwareSerial ss(RXPin, TXPin);       //Define Software Serial 

void setup()
{
  Serial.begin(115200); //Begin serial communication with PC 
  ss.begin(GPSBaud);    //Begin Serial communication with GPS    
}

void loop()
{
//===================GPS PRINT===========================//
  while (ss.available() > 0){                   // If serial communication with GPS is active, enter loop
    if (gps.encode(ss.read())){                 // Check if gps encoding is correct
      displayInfo();                            // Display time, date and location                          
      Serial.println(gps.satellites.value());   // Print number of satellites available
    }
  }
//====================GPS DETECTION======================//
  if (millis() > 5000 && gps.charsProcessed() < 10)  //If no data was received from the GPS module for more than 5 seconds, halt the program
  {
    Serial.println(F("No GPS detected: check wiring."));
    while(true);
  }
}
//====================Display Info=======================//
void displayInfo()
{
  Serial.println("Nir Naknik");
  Serial.print(F("Location: ")); 
  if (gps.location.isValid())
  {
    Serial.print(gps.location.lat(), 6);
    Serial.print(F(","));
    Serial.print(gps.location.lng(), 6);
  }
  else
  {
    Serial.print(F("INVALID"));
  }

  Serial.print(F("  Date/Time: "));
  if (gps.date.isValid())
  {
    Serial.print(gps.date.month());
    Serial.print(F("/"));
    Serial.print(gps.date.day());
    Serial.print(F("/"));
    Serial.print(gps.date.year());
  }
  else
  {
    Serial.print(F("INVALID"));
  }

  Serial.print(F(" "));
  if (gps.time.isValid())
  {
    if (gps.time.hour() < 10) Serial.print(F("0"));
    Serial.print(gps.time.hour());
    Serial.print(F(":"));
    if (gps.time.minute() < 10) Serial.print(F("0"));
    Serial.print(gps.time.minute());
    Serial.print(F(":"));
    if (gps.time.second() < 10) Serial.print(F("0"));
    Serial.print(gps.time.second());
    Serial.print(F("."));
    if (gps.time.centisecond() < 10) Serial.print(F("0"));
    Serial.print(gps.time.centisecond());
  }
  else
  {
    Serial.print(F("INVALID"));
  }

  Serial.println();
}





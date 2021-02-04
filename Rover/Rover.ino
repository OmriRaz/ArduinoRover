#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>     
#include "DHT11Sensor.h"
#include "BMPSensor.h"
#include "MQ2GasSensor.h"


/*
 * PINS FOR SENSORS
 */
#define DHT11_PIN 2 - defined in DHT11Sensor.h
#define GAS_PIN A0 //GAS sensor output pin to Arduino analog A0 pin DEFINED IN MQ2GasSensor.h

RF24 radio(9, 10); // CE, CSN  

const byte address[6] = "00001";     //Byte of array representing the address. This is the address where we will send the data. This should be same on the receiving side.


void setup() 
{
  Serial.begin(9600);
  initializeDHTSensor();
  initializeBMPSensor();

  radio.begin();                  //Starting the Wireless communication
radio.openWritingPipe(address); //Setting the address where we will send the data
radio.setPALevel(RF24_PA_MIN);  //You can set it as minimum or maximum depending on the distance between the transmitter and receiver.
radio.stopListening();          //This sets the module as transmitter
}

void loop() 
{
  float temp, humidity;
  getDHTValues(&temp, &humidity);

  double pressure, seaPressure;
  getBMPValues(&pressure, &seaPressure);

  getMQ2Values();
  
   const char text[] = "Hello!";
    radio.write(&text, sizeof(text));                  //Sending the message to receiver
  delay(5000);
}

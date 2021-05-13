 #include "Defines.h"

#include "DHT11Sensor.h"
#include "BMPSensor.h"
#include "MQ2GasSensor.h"
#include "NRFCommunication.h"
#include "PMS5003Sensor.h"
#include "IRSensor.h"
#include "Motor.h"

/*
 * PINS FOR SENSORS DEFINED IN Defines.h
 */


void setup() 
{
  Serial.begin(9600);
  initializeDHTSensor();
  initializeBMPSensor();
  initializeNRFCommunication();
  initializePMSSensor();

  initializeMotors();
    
  // Gas and IR don't need initialization, they return the values to analog pins directly
}

void loop() 
{
    getDHTValues(&message.temp, &message.humidity);
    
    getBMPValues(&message.pressure, &message.seaPressure);
  
    message.gas = getMQ2Values();
  
    message.ir = getIRValue();
  
    message.particles = getPMSValues(); // 2.5 dust particles
    
    bool report = radio.write(&message, sizeof(message));    // transmit & save success report

    if (report) // message was sent
    {
      Serial.print(F("Transmission successful! "));
      
      uint8_t pipe;
      if (radio.available(&pipe)) // is there an ACK response?
      {
        MessageStruct received;
        radio.read(&received, sizeof(received));  // get incoming ACK message
        Serial.print(F(" Recieved "));
        Serial.print(radio.getDynamicPayloadSize()); // print incoming message size
        Serial.print(F(" bytes on pipe "));
        Serial.print(pipe); // print which pipe received
        Serial.print(" Received move command: "); // print move command received
        Serial.println(received.moveCommand);
        handleMoveData(received.moveCommand);
      } 
      else 
      {
        Serial.println(F(" Recieved: an empty ACK packet"));
      }
    } 
    else 
    {
      Serial.println(F("Transmission failed or timed out"));
    }   
    delay(20);
}

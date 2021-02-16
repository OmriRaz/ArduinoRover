 #include "Defines.h"

#include "DHT11Sensor.h"
#include "BMPSensor.h"
#include "MQ2GasSensor.h"
#include "NRFCommunication.h"
#include "PMS5003Sensor.h"


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
}

void loop() 
{
  float temp, humidity;
  getDHTValues(&temp, &humidity);

  double pressure, seaPressure;
  getBMPValues(&pressure, &seaPressure);

  getMQ2Values();

  getPMSValues();

  char message[] = "Hello!";
  radio.write(&message, sizeof(message));
  Serial.print("Message sent: ");
  Serial.println(message);
  //sendNRFMessage(message);
  delay(5000);
}

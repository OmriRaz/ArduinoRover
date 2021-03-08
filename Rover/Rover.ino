 #include "Defines.h"

#include "DHT11Sensor.h"
#include "BMPSensor.h"
#include "MQ2GasSensor.h"
#include "NRFCommunication.h"
#include "PMS5003Sensor.h"
#include "IRSensor.h"


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
  // Gas and IR don't need initialization, they return the values to analog pins directly
}

void loop() 
{
  float temp = 0, humidity = 0;
  getDHTValues(&temp, &humidity);

  double pressure = 0, seaPressure = 0;
  getBMPValues(&pressure, &seaPressure);

  int gas = getMQ2Values();

  int ir = getIRValue();

  getPMSValues();

  String data = String(temp) + "|" + String(humidity) + "|" + String(pressure) + "|" + String(seaPressure) + "|" + String(gas) + "|" + String(ir);
  
  char dataArray[DATA_ARR_LEN] = { 0 };
  data.toCharArray(dataArray, DATA_ARR_LEN);

  radio.startListening(); // start listening to receive move data

  String moveData = "";
  if (radio.available())
  {
    moveData = Serial.readString(); // read string from ground station
  }

  radio.stopListening(); // stop listening in order to send sensors' data

  // TO DO: ANALYZE MOVEMENT DATA
  
  radio.write(&dataArray, sizeof(dataArray));
  Serial.print("Message sent: ");
  Serial.println(dataArray);
  //sendNRFMessage(message);
  delay(5000);
}

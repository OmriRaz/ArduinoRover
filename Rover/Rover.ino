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

  int particles = getPMSValues(); // 2.5 dust particles

  String data = "~" + String(temp) + "|" + String(humidity) + "|" + String(pressure) + "|";
  String dataSecondPart = String(seaPressure) + "|" + String(gas) + "|" + String(ir) +  "|" + String(particles) + "~";
  
  char dataArray[DATA_ARR_LEN] = { 0 };
  data.toCharArray(dataArray, DATA_ARR_LEN);
  
  char dataSecondPartArray[DATA_ARR_LEN] = { 0 };
  dataSecondPart.toCharArray(dataSecondPartArray, DATA_ARR_LEN);

  radio.startListening(); // start listening to receive move data

  if (radio.available()) // get move data and move car accordingly
  {
    char moveData[MOVE_DATA_LEN] = "";
    radio.read(&moveData, sizeof(moveData)); // read from ground station
    
    digitalWrite(MOTOR_MLP, moveData[0] - '0');
    digitalWrite(MOTOR_MLN, moveData[1] - '0');
    digitalWrite(MOTOR_MRP, moveData[2] - '0');
    digitalWrite(MOTOR_MRN, moveData[3] - '0');
  }

  radio.stopListening(); // stop listening in order to send sensors' data
  
  radio.write(&dataArray, sizeof(dataArray));
  radio.write(&dataSecondPartArray, sizeof(dataSecondPartArray));
  Serial.print("Message sent: ");
  Serial.print(dataArray);
  Serial.println(dataSecondPartArray);
  //sendNRFMessage(message);
}

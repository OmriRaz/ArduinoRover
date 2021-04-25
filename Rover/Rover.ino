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

  for(int pins=MLP; pins <= MRN; pins++)  //loop through 4 pins to define if the pins are inputs or outputs
    pinMode(pins, OUTPUT);
    
  // Gas and IR don't need initialization, they return the values to analog pins directly
}

void loop() 
{
  radio.startListening(); // start listening to receive move data
  if (radio.available()) // get move data and move car accordingly
  {
    char moveData[MOVE_DATA_LEN] = "";
    radio.read(&moveData, sizeof(moveData)); // read from ground station

    Serial.print(moveData[0]);

    if(moveData[0] != 's') // move command is not switch
      handleMoveData(moveData[0]); // move car according to command
    else // switch mode so the rover sends sensors data
    {
      radio.stopListening(); // stop listening in order to send sensors data

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
      radio.write(&dataArray, sizeof(dataArray));
      radio.write(&dataSecondPartArray, sizeof(dataSecondPartArray));
      /*Serial.print("Message sent: ");
      Serial.print(dataArray);
      Serial.println(dataSecondPartArray);*/

    }
  }
}

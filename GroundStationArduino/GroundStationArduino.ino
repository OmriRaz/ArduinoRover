#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>
RF24 radio(9, 10); // CE, CSN
const byte READING_ADDRESS[6] = "00030";
const byte WRITING_ADDRESS[6] = "00010";

struct MessageStruct {
  float temp;
  float humidity;
  double pressure;
  double seaPressure;
  int gas;
  int ir;
  int particles;
  char moveCommand;
};
MessageStruct message;

void setup() 
{
    Serial.begin(9600);
    radio.begin();

    radio.setPALevel(RF24_PA_MIN);      
    
    radio.enableDynamicPayloads();
    radio.enableAckPayload();

    radio.openWritingPipe(WRITING_ADDRESS); // set the address where we will send the data
    radio.openReadingPipe(1, READING_ADDRESS);   //Setting the address at which we will receive the data

    message.temp = 0.0;
    message.humidity = 0.0;
    message.pressure = 0.0;
    message.seaPressure = 0.0;
    message.gas = 0;
    message.ir = 0;
    message.particles = 0;
    message.moveCommand = '0';
    radio.writeAckPayload(1, &message, sizeof(MessageStruct));

    radio.startListening();  // receiver mode                                 
}
void loop()
{
    if (Serial.available() > 0) // if ground station sent data (move car data)
      message.moveCommand = Serial.read();        

    uint8_t pipe;
    if (radio.available(&pipe)) // is there an ACK payload?
    {
      MessageStruct received;
      radio.read(&received, sizeof(received));       // get incoming payload
      
      radio.writeAckPayload(1, &message, sizeof(message)); // send ACK payload with movement command

      String data = "~" + String(received.temp) + "|" + String(received.humidity) + "|" + String(received.pressure) + "|"
      + String(received.seaPressure) + "|" + String(received.gas) + "|" + String(received.ir) +  "|" + String(received.particles) + "~";
      Serial.println(data); // write to ground station's port the data we got from rover 
    }  
    delay(20);

}

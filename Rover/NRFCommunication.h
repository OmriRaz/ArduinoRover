#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>    

RF24 radio(NRF_CE_PIN, NRF_CSN_PIN); // CE, CSN  

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

void initializeNRFCommunication()
{
    radio.begin();

    radio.setPALevel(RF24_PA_MIN);
    
    radio.enableDynamicPayloads();
    radio.enableAckPayload();

    radio.openWritingPipe(WRITING_ADDRESS); // set the address where we will send the data
    radio.openReadingPipe(1, READING_ADDRESS);   // set the address at which we will receive the data

    // initialize data
    message.temp = 0.0;
    message.humidity = 0.0;
    message.pressure = 0.0;
    message.seaPressure = 0.0;
    message.gas = 0;
    message.ir = 0;
    message.particles = 0;
    message.moveCommand = 'X';
    radio.writeAckPayload(1, &message, sizeof(MessageStruct));

    radio.stopListening();
}

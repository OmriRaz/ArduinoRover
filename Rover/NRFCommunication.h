#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>    


RF24 radio(NRF_CE_PIN, NRF_CSN_PIN); // CE, CSN  

void initializeNRFCommunication()
{
    radio.begin();                      // start wireless communication
    radio.openWritingPipe(NRF_ADDRESS); // set the address where we will send the data
    radio.setPALevel(RF24_PA_MAX);      // you can set it as minimum or maximum depending on the distance between the transmitter and receiver.
    radio.stopListening();              // set as transmitter
}


void sendNRFMessage(char* message)
{
  radio.write(&message, sizeof(message));
  Serial.println(message);
}

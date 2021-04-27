#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>
RF24 radio(9, 10); // CE, CSN
const byte READING_ADDRESS[6] = "00030";
const byte WRITING_ADDRESS[6] = "00010";

#define DATA_ARR_LEN 250
#define SWITCH_CHAR 's'

void setup() 
{

    Serial.begin(9600);
    radio.begin();
    radio.openReadingPipe(0, READING_ADDRESS);   //Setting the address at which we will receive the data
    radio.openWritingPipe(WRITING_ADDRESS); // set the address where we will send the data
    radio.setPALevel(RF24_PA_MIN);       //You can set this as minimum or maximum depending on the distance between the transmitter and receiver.
    radio.stopListening();              //This sets the module as transmitter

    pinMode(LED_BUILTIN, OUTPUT);
    digitalWrite(LED_BUILTIN, LOW);
}
void loop()
{
  radio.stopListening();
  if (Serial.available() > 0) // if ground station sent data (move car data)
    {
        char moveData[1] = "";
        
        moveData[0] = Serial.read();

        radio.write(&moveData, sizeof(moveData));
        if(moveData[0] == SWITCH_CHAR) // switch command to switch between receiver and transmitter
        {
          radio.startListening(); // switch to receiver
        }
        
        delay(20);
        
        //Serial.print("Message sent: ");
        //Serial.println(message);
          
        
    }

    if (radio.available())
    {
        char data[DATA_ARR_LEN] = "";
        char dataSecondPart[DATA_ARR_LEN] = "";
        
        radio.read(&data, sizeof(data)); // read from rover
        delay(10);
        radio.read(&dataSecondPart, sizeof(dataSecondPart)); // read from rover

        if(radio.available())
        {
          digitalWrite(LED_BUILTIN, HIGH);
        }
        
        Serial.print(data); // write to ground station's port the data we got from rover 
        Serial.print(dataSecondPart); // write to ground station's port the data we got from rover 
    }

    delay(20);
}

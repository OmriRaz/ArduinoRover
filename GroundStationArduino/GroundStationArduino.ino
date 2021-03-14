#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>
RF24 radio(9, 10); // CE, CSN
const byte READING_ADDRESS[6] = "00001";
const byte WRITING_ADDRESS[6] = "00011";

#define DATA_ARR_LEN 250

void setup() 
{

    Serial.begin(9600);
    radio.begin();
    radio.openReadingPipe(0, READING_ADDRESS);   //Setting the address at which we will receive the data
    radio.openWritingPipe(WRITING_ADDRESS); // set the address where we will send the data
    radio.setPALevel(RF24_PA_MIN);       //You can set this as minimum or maximum depending on the distance between the transmitter and receiver.
    radio.startListening();              //This sets the module as receiver
}
void loop()
{
  if (Serial.available() > 0) // if ground station sent data (move car data)
    {
        String serialText = "";
        serialText = Serial.readString(); // read string from ground station

        if(serialText.length() >= 4)
        {
          radio.stopListening(); // stop listening so we can send move data
          char message[4];
          serialText.toCharArray(message, 4);
          radio.write(&message, sizeof(message));
          //Serial.print("Message sent: ");
          //Serial.println(message);
          
          radio.startListening();
          
        }
        
    }
    // delay?
    if (radio.available())
    {
        char data[DATA_ARR_LEN] = "";
        char dataSecondPart[DATA_ARR_LEN] = "";
        
        radio.read(&data, sizeof(data)); // read from rover
        radio.read(&dataSecondPart, sizeof(dataSecondPart)); // read from rover
        
        Serial.write(data, sizeof(data)); // write to ground station's port the data we got from rover 
        Serial.write(dataSecondPart, sizeof(dataSecondPart)); // write to ground station's port the data we got from rover 
    }

    delay(20);
}

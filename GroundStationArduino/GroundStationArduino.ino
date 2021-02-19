#include <SPI.h>
#include <nRF24L01.h>
#include <RF24.h>
RF24 radio(9, 10); // CE, CSN
const byte address[6] = "00001";

void setup() 
{

    Serial.begin(9600);
    radio.begin();
    radio.openReadingPipe(0, address);   //Setting the address at which we will receive the data
    radio.setPALevel(RF24_PA_MIN);       //You can set this as minimum or maximum depending on the distance between the transmitter and receiver.
    radio.startListening();              //This sets the module as receiver
}
void loop()
{
    if (radio.available())
    {
        char text[100] = "";
        radio.read(&text, sizeof(text)); // read from rover
        Serial.println(text);
        
        Serial.write(text); // write to ground station's port the data we got from rover
    }

    if (Serial.available() > 0) // if ground station sent data (move car data)
    {
        String serialText = "";
        serialText = Serial.readString(); // read string from ground station

        
        // TO DO: analyze ground station's string and send data to rover with movement

        radio.stopListening();
        
        char message[] = "Hello!";
        radio.write(&message, sizeof(message));
        Serial.print("Message sent: ");
        Serial.println(message);
        
        radio.startListening();
    }

    delay(5);
}

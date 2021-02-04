#include "DHT.h"

#define DHTTYPE DHT11
#define DHT11_PIN 2

DHT dht(DHTPIN, DHTTYPE);

void initializeDHTSensor()
{
    dht.begin();
}

void getDHTValues(float* temp, float* humidity)
{
    *temp = 0;
    *humidity = 0;
    float h = dht.readHumidity();
    float t = dht.readTemperature();
    Serial.print("Temperature: ");
    Serial.print(t);
    Serial.print("° Celsius\tHumidity: ");
    Serial.print(h);
    Serial.println("%");
    
    if (isnan(h) || isnan(t)) 
    {
        Serial.println(F("Failed to read from DHT sensor!"));
        return;
    }
    *temp = t;
    *humidity = h;
}

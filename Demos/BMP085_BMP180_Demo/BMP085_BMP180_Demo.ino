#include <Wire.h>
#include <BMP180.h>

/*
BMP180(resolution)

resolution:
BMP180_ULTRALOWPOWER - pressure oversampled 1 time  & power consumption 3μA
BMP180_STANDARD      - pressure oversampled 2 times & power consumption 5μA
BMP180_HIGHRES       - pressure oversampled 4 times & power consumption 7μA
BMP180_ULTRAHIGHRES  - pressure oversampled 8 times & power consumption 12μA, library default
*/
BMP180 myBMP(BMP180_ULTRAHIGHRES);
  
void setup()
{
  Serial.begin(115200);

  while (myBMP.begin() != true)
  {
    Serial.println(F("Bosch BMP180/BMP085 is not connected or fail to read calibration coefficients"));
    delay(5000);
  }
  
  Serial.println(F("Bosch BMP180/BMP085 sensor is OK")); //(F()) saves string to flash & keeps dynamic memory free
}
  
void loop()
{
  Serial.print(F("Pressure: ")); Serial.print(myBMP.getPressure());       Serial.println(F(" +-100Pa"));

  Serial.println(F("Starts over again in 10 sec."));
  delay(10000);  
}

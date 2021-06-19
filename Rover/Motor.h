#include <Servo.h>
Servo LeftMotor;
Servo RightMotor;

void stopMotors()
{
  LeftMotor.write(94.5);
  RightMotor.write(94.5);
}

void initializeMotors()
{
    LeftMotor.attach(MOTOR_LEFT);
    RightMotor.attach(MOTOR_RIGHT);
    stopMotors();
}

void forward()
{
    LeftMotor.write(0);
    RightMotor.write(180);
}

void backward() {
    LeftMotor.write(180);
    RightMotor.write(0);
}

void left() {
    LeftMotor.write(0);
    RightMotor.write(0);
}

void right() {
    LeftMotor.write(180);
    RightMotor.write(180);
}

void handleMoveData(char data)
{
  switch(data)
    {
      case '0':
        stopMotors();
        break;
      case '1':
        forward();  
        break;
      case '2':
        backward();
        break;
      case '3':
        left();
        break;
      case '4':
        right();
        break;
      default:
        Serial.print("Error in move data: " + data);
        stopMotors();
        break;
    }
}

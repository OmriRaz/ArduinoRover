#include <Servo.h>
Servo LeftMotor;
Servo RightMotor;
void setup() {
  LeftMotor.attach(4);
  RightMotor.attach(5);
}

void loop() {
  forward();
  delay(1500);
  backward();
  delay(1500);
  left();
  delay(1500);
  right();
  delay(1500);
  
}

void forward(){
    LeftMotor.write(0);
    RightMotor.write(180);
}

void backward() {
    LeftMotor.write(180);
    RightMotor.write(0);
}

void left() {
    LeftMotor.write(180);
    RightMotor.write(180);
}

void right() {
    LeftMotor.write(0);
    RightMotor.write(0);
}
void stop() {
  LeftMotor.write(94.5);
  RightMotor.write(94.5);
}

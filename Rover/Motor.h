#define MLP 3 //Positive LEFT
#define MLN 4 //Negative LEFT

#define MRP 5 //Positive RIGHT
#define MRN 6 //Negative RIGHT

void stopMotors()
{
  digitalWrite(MLP, LOW);
    digitalWrite(MLN, LOW);
    digitalWrite(MRP, LOW);
    digitalWrite(MRN, LOW);
}

void forward(){
    digitalWrite(MLP, HIGH);
    digitalWrite(MLN, LOW);
    digitalWrite(MRP, HIGH);
    digitalWrite(MRN, LOW);
}

void backward() {
    digitalWrite(MLP, LOW);
    digitalWrite(MLN, HIGH);
    digitalWrite(MRP, LOW);
    digitalWrite(MRN, HIGH);
}

void left() {
    digitalWrite(MLP, HIGH);
    digitalWrite(MLN, LOW);
    digitalWrite(MRP, LOW);
    digitalWrite(MRN, HIGH);
}

void right() {
    digitalWrite(MLP, LOW);
    digitalWrite(MLN, HIGH);
    digitalWrite(MRP, HIGH);
    digitalWrite(MRN, LOW);
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
        break;
    }
}

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

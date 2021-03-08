#define MLP 4 //Positive LEFT
#define MLN 5 //Negative LEFT

#define MRP 6 //Positive RIGHT
#define MRN 7 //Negative RIGHT


void setup() {
  for(int pins=MLP; pins <= MRN; pins++)  //loop through 4 pins to define if the pins are inputs or outputs
    pinMode(pins, OUTPUT);
}

void loop() {
  
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

void left() {
    digitalWrite(MLP, LOW);
    digitalWrite(MLN, HIGH);
    digitalWrite(MRP, HIGH);
    digitalWrite(MRN, LOW);
}

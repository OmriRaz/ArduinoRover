int getIRValue()
{
  int irValue = analogRead(IR_PIN);
  /*Serial.print("IR Value: ");
  Serial.println(irValue);*/
  return irValue;
}

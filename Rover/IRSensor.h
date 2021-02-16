int getIRValue()
{
  int irValue = analogRead(A1);
  Serial.print("IR Value: ");
  Serial.println(irValue);
  return irValue;
}

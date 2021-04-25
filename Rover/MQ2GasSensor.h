int getMQ2Values()
{
  int gas = analogRead(GAS_PIN);
  /*Serial.print("Gas value: ");
  Serial.println(gas);*/
  return gas;
}

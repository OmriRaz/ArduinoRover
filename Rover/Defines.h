/*
 * DIGITAL PINS
 */
#define DHT11_PIN       2


// NRF PINS
#define NRF_CE_PIN      9
#define NRF_CSN_PIN     10

// NRF SEND/RECEIVE ADDRESS
const byte NRF_ADDRESS[6] = "00001";     //Byte of array representing the address. This is the address where we will send the data. This should be same on the receiving side.

/*
 * ANALOG PINS
 */
#define GAS_PIN A0 

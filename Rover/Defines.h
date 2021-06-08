#define DATA_ARR_LEN 32
#define MOVE_DATA_LEN 1

/*
 * DIGITAL PINS
 */
#define DHT11_PIN       2

// Motor
#define MOTOR_LEFT    5
#define MOTOR_RIGHT   6
// ---------


// NRF PINS
#define NRF_CE_PIN      9
#define NRF_CSN_PIN     10

// NRF SEND/RECEIVE ADDRESS
const byte WRITING_ADDRESS[6] = "00030";     //Byte of array representing the address. This is the address where we will send the data. This should be same on the receiving side.
const byte READING_ADDRESS[6] = "00010";

/*
 * ANALOG PINS
 */
#define GAS_PIN A0 
#define IR_PIN  A1

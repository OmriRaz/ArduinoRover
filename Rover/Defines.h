#define DATA_ARR_LEN 32
#define MOVE_DATA_LEN 1

/*
 * DIGITAL PINS
 */
#define DHT11_PIN       2

// Motor
#define MOTOR_MLP 4 //Positive LEFT
#define MOTOR_MLN 5 //Negative LEFT

#define MOTOR_MRP 6 //Positive RIGHT
#define MOTOR_MRN 7 //Negative RIGHT
// ---------


// NRF PINS
#define NRF_CE_PIN      9
#define NRF_CSN_PIN     10

// NRF SEND/RECEIVE ADDRESS
const byte WRITING_ADDRESS[6] = "00001";     //Byte of array representing the address. This is the address where we will send the data. This should be same on the receiving side.
const byte READING_ADDRESS[6] = "00002";

/*
 * ANALOG PINS
 */
#define GAS_PIN A0 
#define IR_PIN  A1

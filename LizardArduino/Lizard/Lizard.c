/*
 * Lizard.c
 *
 * Created: 5/27/2015 12:07:02 AM
 *  Author: Paul
 *
 * Description:
 * This file implements the Lizard project described in the report.
 * It is able to read a temperature sensor through I2C communication and implements a PID loop for error checking on a heating coil
 * vs. an ideal temperature ramp.  It also sends data from 5 lizard temperature sensors and an ambient temperature sensor to a
 * computer through the USART.  The Arduino will wait for a signal from the computer to start the experiment.
 *
 */ 
#define F_CPU 16000000UL


#include <avr/io.h>
#include <util/delay.h>
#include <stdio.h>
#include "usb.h"
#include "easy_timer.h"
#include "twi_i2c.h"
#include "easy_usart.h"

//Signal constants
#define HOLD 0x0001
#define START 0x0002
#define STOP 0x0003

//PID Loop Constants
#define SAMPLE_TIME 1			//Sample time represents time in seconds
#define IDEAL_DERIV 0.1			//the ideal derivate for a 1 degree C per minute ramp
#define PROP_CORRECT 0.5f		//constant to multiply by the proportional difference
#define INTEG_CORRECT 0			//constant to multiply by the integral difference
#define DERIV_CORRECT 0.25f		//constant to multiply by the differential difference

//Port assignment constants
#define SDA (1<<PC4)
#define SCL (1<<PC5)
#define HEATER (1<<PD6)

//Timing constants
#define SAMPLES_PER_MIN 60				//Samples is in terms of seconds
#define SAMPLE_RATE 1f/SAMPLES_PER_MIN	//the rate at which samples are read from the ambient temp sensor

//Temp Sensor addresses
#define AMB_TEMP 0
#define HEATER_TEMP 1
#define LIZ_TEMP_0 2
#define LIZ_TEMP_1 3
#define LIZ_TEMP_2 4
#define LIZ_TEMP_3 5
#define LIZ_TEMP_4 6

//other constants
#define ON 1
#define OFF 0
#define MAX_RATE 0xFF
#define NUM_LIZARDS 5
#define BYTE_MASK 0xFF

static int idealTemp = 0;

/*
 *Function Name: is_data_on_usart()
 *
 *Description:	This is a conditional that checks to see if any data is on the USART
 *			    waiting to be read
 *
 *Parameters: None
 *
 *Returns: 1; if there is data waiting to be read
 *		   0; if there is not data waiting to be read	
 */
int is_data_on_usart() {
	return (UCSR0A & (1<<RXC0));
}

/*
 *Function Name: initPWM()
 *
 *Description:	This initializes the Arduino in fast PWM mode with an 8-bit timer
 *				It start the PWM at a 20% duty cycle on PD5
 *
 *Parameters: None
 *
 *Returns: void
 *
 */
void initPWM()// initializes motors
{
	DDRD = 0xFF; 			//set port D as outputs
	TCCR0A = 0b10100011; 		//timer set to fast pwm
	TCCR0B = 5; 			//timer clk = system clk / 1024;
	//outputs 16E6/1024/65535 = .238Hz PWM
	OCR0A = 50; 			//compare value => 20% duty cycle to PD6
	OCR0B = 255; 			//compare value => 75% duty cycle to PD5
}

/*
 *Function Name: heat_rate()
 *
 *Description: Changes the duty cycle on the PWM to either increase or
 *				decrease the the amount of heat released.
 *
 *Parameters: int; the new OCR0A value to compare for PWM.
 *
 *Returns: void
 *
 */
void heat_rate(int rate) {
	if(rate > MAX_RATE) {
		OCR0A = MAX_RATE;
	}
	else if(rate < OFF) {
		OCR0A = OFF;
	}
	else {
		OCR0A = rate;
	}
	OCR0B = MAX_RATE - OCR0A;
}

/*
 *Function Name: read_sig(int numbytes)
 *
 *Description: Reads a signal from the USART.
 *
 *Parameters: int; the number of bytes to read.
 *
 *Returns: int; the value read from the USART
 *
 */
int read_sig(int numbytes) {
	int signal = 0, i;
	
	for(i = 0; i < numbytes; i++) {
		signal |= read() << i * 8;
	}
	
	return signal;
}

/*
 *Function Name: read_temp(int addr)
 *
 *Description: Reads a signal from the ambient temperature sensor using I2C.
 *
 *Parameters: int; the address of the I2C device.
 *
 *Returns: int; the value read from the I2C device
 *
 */
int read_temp(int addr) {
	return read_reg(addr);
}

/*
 *Function Name: power_heater(int power)
 *
 *Description: Turns on the heater by initializing the PWM wave or turns
 *				the heater off by setting OCR0A to 0.
 *
 *Parameters: int; 1 or 0,determines if you are turning heater ON or OFF.
 *
 *Returns: void
 *
 */
void power_heater(int power) {
	if(power == ON) {
		initPWM();
	}
	else if(power == OFF) {
		OCR0A = 0;
	}
}

/*
 *Function Name: heat_until(int temp)
 *
 *Description: Puts heater to maximum rate possible (ideally 100% duty cycle) until
 *				the ambient temperature reaches a certain temperature then the heater is
 *				turned off.
 *
 *Parameters: int; the temperature to heat to.
 *
 *Returns: void
 *
 */
void heat_until(int temp) {
	heat_rate(MAX_RATE);
	while(read_temp(AMB_TEMP) < temp) {}
	power_heater(OFF);				
}

/*
 *NOTE: Function is only used for testing purposes only to get lizard temperatures
 *Function Name: rand_temp()
 *
 *Description: Returns a random temperature between 27 and 55.
 *
 *Parameters: None
 *
 *Returns: int; the random value between 27 and 55
 *
 */
int rand_temp() {
	int max = 55, min = 27;
	return ((rand() % (max+1-min)) + min) << 4;
}

/*
 *NOTE: function currently does not work correctly.  Currently, for testing purposes,
 *		this function just stores random values for lizard temperatures since we did not
 *		have actual lizard temperature sensors to use.
 *Function Name: read_liz_temps(int *liz_temps)
 *
 *Description: Reads 5 lizard temperature and stores them in the array that was passed in
 *
 *Parameters: int *; a pointer to an array to store temperatures in.
 *
 *Returns: void
 *
 */

void read_liz_temps(int *liz_temps) {
	
	liz_temps[0] = rand_temp();//read_temp(LIZ_TEMP_0);
	liz_temps[1] = rand_temp();//read_temp(LIZ_TEMP_1);
	liz_temps[2] = rand_temp();//read_temp(LIZ_TEMP_2);
	liz_temps[3] = rand_temp();//read_temp(LIZ_TEMP_3);
	liz_temps[4] = rand_temp();//read_temp(LIZ_TEMP_4);	
}

/*
 *Function Name: send_data(int data, int num_bytes)
 *
 *Description: Sends data through the USART to the computer.
 *				Sends in little endian.
 *
 *Parameters: int; the data to send;
 *			  int; the number of bytes of data to be sent.
 *
 *Returns: void
 *
 */
void send_data(int data, int num_bytes) {
	uint8_t str[4];
	int i;
	for(i = 0; i < num_bytes; i++) {
		str[i] = (char)(data & BYTE_MASK);
		data >>= 8;							//right shift data to get the next byte
	}
	send(str, num_bytes);
}

/*
 *Function Name: send_deadbeef()
 *
 *Description: Sends 0xDEADBEEF through the USART.  This value serves
 *				as the start and end signal for a data packet.
 *
 *Parameters: None
 *
 *Returns: void
 *
 */
void send_deadbeef() {
	send_data(0xDEAD, 2);
	send_data(0xBEEF, 2);
}

/*
 *Function Name: send_temp(int temp)
 *
 *Description: Sends temperature data through the USART. Temp data must not exceed 2 Bytes.
 *
 *Parameters: int; temperature to be sent through USART
 *
 *Returns: void
 *
 */
void send_temp(int temp) {
	send_data(temp, 2);
}

/*
 *Function Name: send_packet(int amb_temp, int *liz_temps)
 *
 *Description: Sends a full packet of data to be read by the computer through the USART.
 *				The packet begins with a 0xDEADBEEF then includes all the temperature data
 *				and ends with another 0xDEADBEEF.
 *
 *Parameters: int; the ambient temperature to be sent.
 *			  int *; the lizard temperature array to be sent.
 *
 *Returns: void
 *
 */
void send_packet(int amb_temp, int heater_actual_temp, int *liz_temps) {
	int i;
	send_deadbeef();
	
	send_temp(heater_actual_temp);
	send_temp(amb_temp);
	for(i = 0; i < NUM_LIZARDS; i++) {
		send_temp(liz_temps[i]);
	}
	
	send_deadbeef();
}

/*
 *Function Name: integrate(int curTemp)
 *
 *Description: Integrates the ambient temperature ramp.
 *				This is used for PID loop purposes.
 *
 *Parameters: int; the current ambient temperature.
 *
 *Returns: int; the runnning integral for the ramp.
 *
 */
int integrate(int curTemp) {
	static int runningInteg = 0;
	
	runningInteg += curTemp * SAMPLE_TIME;
	
	return runningInteg;
}

/*
 *Function Name: derivate(int curTemp)
 *
 *Description: Takes the derivative the ambient temperature ramp.
 *				This is used for PID loop purposes.
 *
 *Parameters: int; the current ambient temperature.
 *
 *Returns: int; the derivate for the ramp since the last temperature read.
 *
 */
int derivate(int curTemp) {
	static int prevTemp = 0;
	int tempRate;
	
	tempRate = (curTemp - prevTemp) / SAMPLE_TIME;
	prevTemp = curTemp;
	
	return tempRate;
}

/*
 *Function Name: correct_temp(int prop, int integ, int deriv)
 *
 *Description: Corrects the OCR0A value to create a new duty cycle for the PWM
 *				to try to make the ambient temperature ramp look more like a 
 *				1 degree C per minute ramp.
 *				This is used for PID loop purposes.
 *
 *Parameters: int; the proportional difference of the ideal ramp - actual ramp.
 *			  int; the integral difference of the ideal ramp - actual ramp.
 *			  int; the derivate difference of the ideal ramp - actual ramp.
 *
 *Returns: void
 *
 */
void correct_temp(int prop, int integ, int deriv) {
	heat_rate(OCR0A  + prop * PROP_CORRECT + deriv * DERIV_CORRECT + integ * INTEG_CORRECT);
}

/*
 *Function Name: pid_loop(int amb_temp, int start_temp)
 *
 *Description: Corrects the actual temperature ramp to conform to a 1 degree C per
 *				minute ramp by find the proportional difference, the integral difference
 *				and the differential difference between the ideal temperature ramp
 *				and the actual temperature ramp 
 *
 *Parameters: int; the ambient temperature.
 *			  int; the starting temperature for the experiment.
 *
 *Returns: int; the ideal temperature
 *
 */
int pid_loop(int amb_temp, int start_temp) {
	static int idealInteg = 0;
	int prop_diff = 0, integ_diff = 0, deriv_diff = 0; 
	int actualTemp = 0;

	actualTemp = amb_temp;
	
	idealTemp += 1;										// increase ideal temperature by a desired ramp int this case 1/16 per 1/10 second
	idealInteg += idealTemp * SAMPLE_TIME;
	
	prop_diff = idealTemp - actualTemp;
	integ_diff = idealInteg - integrate(actualTemp);
	deriv_diff = IDEAL_DERIV - derivate(actualTemp);
	
	correct_temp(prop_diff, integ_diff, deriv_diff);	// send proportional, integral, and differential differences to correct PWM
	
	return idealTemp;
}

int main(void)
{
	DDRC = SCL | SDA;								//Set the SCL and SDA pins to outputs
	uint16_t sig = 0;
	int liz_temps[NUM_LIZARDS] = {1, 1, 1, 1, 1};
	int environ_temp = 0, ideal_temp = 0, heater_temp = 0;
	int ramp = 0, start_temp = 0;
	int end_temp = 0;

	power_heater(ON);							//power heater on to initialize and set PWM mode
	heat_rate(OFF);								//heat at a rate of 0 so the heater is not heating until it receives a command to do so
	usb_init();
	
	//Initialized I2C to a specified frequency based on these register values
	TWBR = 0x12;
	TWSR = 0x01;
	
	while(1)
	{	
		sig = read_sig(2);
		if(sig == HOLD) {
			start_temp = read_sig(2);			//get start temp from USART
			heater_temp = read_sig(2);			//get the heating chamber temp from USART
			idealTemp = start_temp;				//set ideal temp to the start temp
			heat_until(heater_temp);			//heat the environment until it reaches the start temp
		}
		else if(sig == START) {
			ramp = read_sig(2);					// read in the ramping multiplier, NOTE: currently not being implemented
			end_temp = read_sig(2);				// read in a target end temperature
			while(!is_data_on_usart() && ideal_temp < end_temp) {		//continue until the ideal temperature reaches the end temperature
				environ_temp = read_temp(AMB_TEMP);
				int heater_actual_temp = read_temp(HEATER_TEMP);
				ideal_temp = pid_loop(environ_temp, start_temp);			//correct heating to reach a 1 degree C per minute ramp
				read_liz_temps(liz_temps);
				send_packet(environ_temp, heater_actual_temp, liz_temps);				//send packet through USART containing all the data fromt the temp sensors
				_delay_ms(100);									//delay for timing purposes
			}
		}
		else if(sig == STOP) {
			power_heater(OFF);
		}
	}
}
/* file easy_usart.c copyright © 2015 CPE 329 Team Paul/David
/*******************************************************************************
/*******************************************************************************
Project 2 usart library, not used in final implementation (only for debugging)
the DAC
IDE: Atmel Studio 6.2
Target: ATmega328P 8b microcontroller
================================================================================
DEVELOPMENT TEAM
David Maxson <dmaxson@calpoly.edu>,
Paul Hurd <phurd@calpoly.edu>
================================================================================
REVISION HISTORY
1.00 Draft used for project 2
================================================================================
*/

#include "easy_usart.h"
#include <stdio.h>
#include <avr/io.h>

#define USART_BAUDRATE 9600
#define UBRR_VALUE (((F_CPU / (USART_BAUDRATE * 16UL))) - 1)

inline void USART0Init(void)
{
	// Set baud rate
	UBRR0H = (uint8_t)(UBRR_VALUE>>8);
	UBRR0L = (uint8_t)UBRR_VALUE;
	// Set frame format to 8 data bits, no parity, 1 stop bit
	UCSR0C |= (1<<UCSZ01)|(1<<UCSZ00);
	//enable transmission and reception
	UCSR0B |= (1<<RXEN0)|(1<<TXEN0);
}
inline int USART0SendByte(char u8Data, FILE *stream)
{
	if(u8Data == '\n')
	{
		USART0SendByte('\r', 0);
	}
	//wait while previous byte is completed
while(!(UCSR0A&(1<<UDRE0))){};
// Transmit data
UDR0 = u8Data;
return 0;
}
inline int USART0ReceiveByte(FILE *stream)
{
uint8_t u8Data;
// Wait for byte to be received
while(!(UCSR0A&(1<<RXC0))){};
u8Data=UDR0;
//echo input data
USART0SendByte(u8Data,stream); 
// Return received data
return u8Data;
}

void usart_init() {
	USART0Init();

	FILE usart0_str = FDEV_SETUP_STREAM(USART0SendByte, USART0ReceiveByte, _FDEV_SETUP_RW);

	//assign our stream to standard I/O streams
	stdin=stdout=&usart0_str;
}

void print(char* str) {
	while(*str) {
		USART0SendByte(*str, NULL);
		str++;
	}
}

inline void printch(char ch) {
	USART0SendByte(ch, NULL);
}

/* Note that buffer should have room for max_length + 1 (for the trailing 0) */
void read(char* buffer, int max_length) {
	int i;
	for(i = 0; i < max_length; i++) {
		buffer[i] = USART0ReceiveByte(NULL);
		if(buffer[i] == '\n' || buffer[i] == '\r')
			break;
	}
	buffer[i] = 0; // Overwrites the EOL, or appends to end of string if max_length was reached
}

int str_eq(char* a, char* b) {
	while(*a && *b && *a == *b) { a++; b++; }
	return *a == *b;
}
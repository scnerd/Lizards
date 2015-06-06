#include "usb.h"
#include <stdio.h>
#include <avr/io.h>

#define F_CPU 16000000UL

#define USART_BAUDRATE 9600
#define UBRR_VALUE (((F_CPU / (USART_BAUDRATE * 16UL))) - 1)

void USART0Init()
{
	// Set baud rate
	UBRR0 = UBRR_VALUE;
	// Set frame format to 8 data bits, even parity, 1 stop bit
	UCSR0C |= (1<<UPM01)|(1<<UCSZ01)|(1<<UCSZ00);
	//enable transmission and reception
	UCSR0B |= (1<<RXEN0)|(1<<TXEN0);
}

int USART0SendByte(uint8_t u8Data)
{
	//wait while previous byte is completed
	while(!(UCSR0A&(1<<UDRE0))){};
	// Transmit data
	UDR0 = u8Data;
	return 0;
}

int USART0ReceiveByte()
{
	uint8_t u8Data;
	// Wait for byte to be received
	while(!(UCSR0A&(1<<RXC0))){};
	u8Data=UDR0;
	// Return received data
	return u8Data;
}

void usb_init() {
	USART0Init();
}

void send(uint8_t str[], int count) {
	for(int i = 0; i < count; i++) {
		USART0SendByte(str[i]);
	}
}

/* Note that buffer should have room for max_length + 1 (for the trailing 0) */
uint8_t read() {
	return USART0ReceiveByte();
}
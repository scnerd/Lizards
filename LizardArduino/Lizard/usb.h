/*
 * easy_usart.h
 *
 * Created: 4/24/2015 2:56:26 PM
 *  Author: dmaxson
 */ 


#ifndef EASY_USART_H_
#define EASY_USART_H_

#include <avr/io.h>

void usb_init();
void send(uint8_t str[], int count);
uint8_t read();

#endif /* EASY_USART_H_ */
/*
 * easy_usart.h
 *
 * Created: 4/24/2015 2:56:26 PM
 *  Author: dmaxson
 */ 

#define F_CPU 16000000UL
#ifndef EASY_USART_H_
#define EASY_USART_H_


void usart_init();
void print(char* str);
void printch(char ch);
void read(char* buffer, int max_length);
int str_eq(char* a, char* b);

#endif /* EASY_USART_H_ */
#define F_CPU 16000000UL
#include <avr/io.h>
#include <util/delay.h>
#include <avr/interrupt.h>
#include <util/twi.h>
#include <stdlib.h>					// Standard C library


int read_reg(int regAdd);
int write_reg(int regAdd, int data);

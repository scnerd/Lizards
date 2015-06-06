/* file easy_timer.c copyright © 2015 CPE 329 Team Paul/David
/*******************************************************************************
/*******************************************************************************
Project 2 timing wrapper, provides some easy timing functions based on
the 16-bit timer
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

#include <avr/io.h>
#include "easy_timer.h"

void inline timer_init(uint8_t prescale) {
	TCCR1B = 0b00000111 & prescale;
	//TIFR1 |= (1<<TOV1); // Clears the overflow
}

void inline timer_set_target(uint16_t target) {
	TCCR1B |= (1<<WGM12);
	OCR1A = target;
}

void inline timer_clear_target() {
	TCCR1B &= ~(1<<WGM12);
}

void inline timer_start() {
	TCNT1 = 0;
}

void inline timer_until(uint16_t duration) {
	while(TCNT1 <= duration) {}
}

void inline timer_wait(uint16_t duration) {
	timer_start();
	timer_until(duration);
}

uint8_t inline timer_grab_ovf() {
	uint8_t to_return = TIFR1 & (1<<TOV1);
	TIFR1 |= (1<<TOV1); // Clears the overflow
	return to_return;
}
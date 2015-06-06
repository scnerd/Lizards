/*
 * easy_timer.h
 *
 * Created: 4/24/2015 2:49:00 PM
 *  Author: dmaxson
 */ 


#ifndef EASY_TIMER_H_
#define EASY_TIMER_H_

//#include "ADCTestbed.h"

#define PRESCALE_0 (uint8_t)0x00
#define PRESCALE_1 (uint8_t)0x01
#define PRESCALE_8 (uint8_t)0x02
#define PRESCALE_64 (uint8_t)0x03
#define PRESCALE_256 (uint8_t)0x04
#define PRESCALE_1024 (uint8_t)0x05

void timer_init(uint8_t prescale);
void timer_set_target(uint16_t target);
void timer_clear_target();
void timer_wait(uint16_t duration);
void timer_start();
void timer_until(uint16_t duration);
uint8_t timer_grab_ovf();


#endif /* EASY_TIMER_H_ */
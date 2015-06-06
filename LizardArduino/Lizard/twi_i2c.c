#include "twi_i2c.h"
#include "I2C_master.h"

#define TA_POINTER 0x05
#define ADDR_BYTE 0x30

//reads data value from register with address regAdd
//return data value read on success, -1 on failure
int read_reg(int regAdd){

  int data;

  I2C_start(ADDR_BYTE | TW_WRITE);

  // send ambient temp address
  I2C_write(TA_POINTER);

  // send start condition
  I2C_start(ADDR_BYTE | TW_READ);

  //read data
  data = I2C_read_ack() & 0x0F;
  data <<= 8;
  
  data |= I2C_read_nack();
	
  // transmit STOP
  I2C_stop();

  return data;
}


//writes data value to register with address regAdd
//return data value written on success, -1 on failure
int write_reg(int regAdd, int data){

  // send start condition
  TWCR = (1<<TWINT) | (1<<TWSTA) | (1<<TWEN);

  // wait to see of start condition has been transmitted
  while( !(TWCR & (1<<TWINT)) ){

  }

  // check if status is no "start"
  if( (TWSR & 0xF8) != TW_START ){
     return -1; // error has occured
  }

  // slave address to write to
  TWDR = (0x74<<1) | TW_WRITE;

  TWCR = (1<<TWINT) | (1<<TWEN);

  while( !(TWCR & (1<<TWINT)) ){

  }

  if( (TWSR & 0xF8) != TW_MT_SLA_ACK ){
     return -1;
  }

  // register to write to
  TWDR = regAdd;

  TWCR = (1<<TWINT) | (1<<TWEN);

  while( !(TWCR & (1<<TWINT)) ){

  }

  if( (TWSR & 0xF8) != TW_MT_DATA_ACK ){
     return -1;
  }


  // write data to register
  TWDR = data;

  TWCR = (1<<TWINT) | (1<<TWEN);

  while( !(TWCR & (1<<TWINT)) ){

  }


  if( (TWSR & 0xF8) != TW_MT_DATA_ACK ){
     return -1;
  }

  // transmit STOP
  TWCR = (1<<TWINT) | (1<<TWEN) | (1<<TWSTO); 

  return data;


}

#ifndef MY_LONG_INT
#define MY_LONG_INT
#include <stdlib.h>

struct num
{
	int length;
	int* digits;
};
typedef struct num bit_integer;

struct bit_integer* form_big_int(int size, int value);

struct bit_integer* multiply(bit_integer* number, bit_integer* b);

struct bit_integer* power(bit_integer* number, int pow);

void free_int(bit_integer* number);

void normalize(bit_integer* number); 

void print_long_num(bit_integer* number);

#endif
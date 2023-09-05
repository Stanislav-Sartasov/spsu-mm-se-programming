#ifndef MY_LONG_INT
#define MY_LONG_INT
#include <stdlib.h>
#include <stdint.h>
#include <stdio.h>

#define BASE 268435456

typedef struct bit_integer
{
	int length;
	int* digits;
}bit_integer;

struct bit_integer* form_bit_int(int value, int size);

struct bit_integer* multiply(bit_integer* a, bit_integer* b, int cleaner);

struct bit_integer* power(int value , int pow);

void free_int(bit_integer* number);

void print_long_num(bit_integer* number);

#endif
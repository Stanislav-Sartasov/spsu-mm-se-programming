#pragma once
#include <stdint.h>
#include <stdlib.h>
#include <stdio.h>

#define HEX_BASE 268435456 // 16^7

typedef struct big_integer
{
	int32_t length;
	int32_t* digits;
} big_integer;

big_integer* create_big_int(int32_t value, int32_t size);
void delete_big_int(big_integer* number);
/*
*the cleaner parameter removes a large number that is unnecessary to use,
*if you enter 1, the function will delete the first number, if you enter 2, the function will delete the second number,
*if you enter 0, it will not delete anything, in other cases it will delete both numbers.
*/
big_integer* multiply(big_integer* a, big_integer* b, int32_t cleaner);
big_integer* power(int32_t value, int32_t degree);
void printf_big_int_hex(big_integer* number);
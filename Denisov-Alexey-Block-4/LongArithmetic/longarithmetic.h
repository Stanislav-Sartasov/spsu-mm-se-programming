#include <stdio.h>
#include <stdlib.h>
#include <math.h>

#ifndef LONG_ARITHMETIC
#define LONG_ARITHMETIC

#define BASE 4294967296 

struct longNumber
{
	unsigned int* digits;
	unsigned int length;
};
typedef struct longNumber longNumber;

longNumber longPow(int base, int degree);

void longPrint(longNumber* number);

void longFree(longNumber* number);

#endif // LONG_ARITHMETIC
#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include <math.h>

typedef struct numb256
{
	unsigned char* number;
	int startNumber;
	int sizeNumber;
} base256;

void printHexNumber(base256 result);

base256 my256NumPow(int numb, int exp);
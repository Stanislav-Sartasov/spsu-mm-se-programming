#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>

typedef struct hexNumber
{
	char* number;
	int startNumber;
	int sizeNumber;
} hex;

void printNumber(hex result);

int myLog16(int n);

hex myHexPow(int numb, int exp);
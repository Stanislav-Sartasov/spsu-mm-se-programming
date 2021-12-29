#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>

#define BASE (16 * 16 * 16 * 16 * 16 * 16 * 16)

typedef struct bigInt
{
	size_t size;
	uint64_t* digits;
}bigInt;

bigInt* newBigInt(size_t size, uint64_t value);
void freeBigInt(bigInt* number);
void printfBigIntHex(bigInt* number);
bigInt* multiply(bigInt* left, bigInt* right);
bigInt* power(bigInt* number, uint64_t pow);


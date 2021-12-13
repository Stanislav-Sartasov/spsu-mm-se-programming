#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "bigint.h"

void* error(char* message)
{
	printf(message);
	return NULL;
}

bigint_t* new_bigint(unsigned int initial)
{
	bigint_t* bigint = (bigint_t*)malloc(sizeof(bigint_t));
	if (bigint == NULL)
		return error("Unable to allocate memory for bigint.\n");

	bigint->digits = (unsigned int*)malloc(sizeof(unsigned int) * SIZE);
	if (bigint->digits == NULL)
		return error("Unable to allocate memory for bigint digits.\n");

	memset(bigint->digits, 0, sizeof(unsigned int) * SIZE);

	bigint->digits[SIZE - 1] = initial;

	return bigint;
}

void free_bigint(bigint_t* bigint)
{
	free(bigint->digits);
	free(bigint);
}

bigint_t* multiply_bigint(bigint_t* bigint, unsigned int multiplier)
{
	unsigned int extra = 0;
	unsigned int current = 0;
	for (int i = SIZE - 1; i >= 0; i--)
	{
		current = ((unsigned long long)bigint->digits[i] * multiplier + extra) % BASE;
		extra = ((unsigned long long)bigint->digits[i] * multiplier + extra) / BASE;
		bigint->digits[i] = current;
	}

	return bigint;
}

void print_hex_bigint(bigint_t* bigint)
{
	for (int i = 0; i < SIZE; i++)
		if (bigint->digits[i] != 0)
			printf(i == 0 ? "%x" : "%08x", bigint->digits[i]);
	printf("\n");
}
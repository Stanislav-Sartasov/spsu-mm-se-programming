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

	bigint->length = 1;
	bigint->digits = (unsigned int*)malloc(sizeof(unsigned int));
	if (bigint->digits == NULL)
		return error("Unable to allocate memory for bigint digits.\n");

	bigint->digits[0] = initial;

	return bigint;
}

void free_bigint(bigint_t* bigint)
{
	free(bigint->digits);
	free(bigint);
}

bigint_t* expand_bigint(bigint_t* bigint)
{
	unsigned int* new_digits = (unsigned int*)realloc(bigint->digits, (bigint->length + 1) * sizeof(unsigned int));
	if (new_digits == NULL)
		return error("Unable to reallocate memory for bigint digits.\n");

	bigint->digits = new_digits;
	memcpy(bigint->digits + 1, bigint->digits, bigint->length * sizeof(unsigned int));
	bigint->digits[0] = 0;
	bigint->length++;

	return bigint;
}

bigint_t* multiply_bigint(bigint_t* bigint, unsigned int multiplier)
{
	unsigned int extra = 0;
	unsigned int current = 0;

	for (int i = bigint->length - 1; i >= 0; i--)
	{
		current = ((unsigned long long)bigint->digits[i] * multiplier + extra) % BASE;
		extra = ((unsigned long long)bigint->digits[i] * multiplier + extra) / BASE;
		bigint->digits[i] = current;
	}

	if (extra != 0)
	{
		if (expand_bigint(bigint) == NULL)
			return NULL;
		bigint->digits[0] = extra;
	}

	return bigint;
}

void print_hex_bigint(bigint_t* bigint)
{
	for (int i = 0; i < bigint->length; i++)
		if (bigint->digits[i] != 0)
			printf(i == 0 ? "%x" : "%08x", bigint->digits[i]);
	printf("\n");
}
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>

#define BASE 268435456 // 16^7

typedef struct big_int
{
	size_t size;
	uint64_t* chunks;
}big_int;

big_int* new_big_int(size_t size, uint64_t value);
void free_big_int(big_int* number);
void print_big_int_as_hex(big_int* number);
big_int* multiply(big_int* higher, big_int* lower);
big_int* power(big_int* number, int32_t pow);


int main()
{
	big_int* number = new_big_int(2, 3);
	number = power(number, 5000);
	print_big_int_as_hex(number);
	free_big_int(number);
	return 0;
}


big_int* new_big_int(size_t size, uint64_t value)
{
	big_int* number = (big_int*)malloc(sizeof(big_int));
	number->chunks = (uint64_t*)calloc(size, sizeof(uint64_t));
	number->size = size;
	number->chunks[0] = value;
	return number;
}

void print_big_int_as_hex(big_int* number)
{
	int32_t i = number->size;
	while (number->chunks[--i] == 0 && i > 0);
	printf("0x%x", number->chunks[i--]);
	for (; i >= 0; i--)
		printf("%.7x", number->chunks[i]);
}

void free_big_int(big_int* number)
{
	free(number->chunks);
	free(number);
}

big_int* multiply(big_int* higher, big_int* lower)
{
	big_int* result = new_big_int(higher->size + lower->size, 0);
	for (size_t i = 0; i < higher->size; i++)
		for (size_t j = 0; j < lower->size; j++)
			result->chunks[i + j] += higher->chunks[i] * lower->chunks[j];


	for (size_t i = 0; i < result->size - 1; i++)
	{
		result->chunks[i + 1] += result->chunks[i] / BASE;
		result->chunks[i] %= BASE;
	}

	return result;
}

big_int* power(big_int* number, int32_t pow)
{
	if (pow == 1)
		return number;
	
	if (pow % 2)
		return multiply(multiply(power(number, pow / 2), power(number, pow / 2)), number);

	return multiply(power(number, pow / 2), power(number, pow / 2));
}
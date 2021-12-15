#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>

#define BASE 268435456 // 16^7

typedef struct big_int
{
	size_t size;
	uint32_t* chunks;
}big_int;

big_int* new_big_int(size_t size, uint32_t value);
void free_big_int(big_int* number);
void print_big_int_as_hex(big_int* number);
big_int* multiply(big_int* higher, big_int* lower);
big_int* power(big_int* number, int32_t pow);


int main()
{
	big_int* number = new_big_int(1, 3);
	number = power(number, 5000);
	print_big_int_as_hex(number);
	free_big_int(number);
	return 0;
}


big_int* new_big_int(size_t size, uint32_t value)
{
	big_int* number = (big_int*)malloc(sizeof(big_int));
	number->chunks = (uint32_t*)calloc(size, sizeof(uint32_t));
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
	uint64_t* temp = (uint64_t*)calloc(higher->size + lower->size, sizeof(uint64_t));
	for (size_t i = 0; i < higher->size; i++)
		for (size_t j = 0; j < lower->size; j++)
			temp[i + j] += (uint64_t)higher->chunks[i] * (uint64_t)lower->chunks[j];
	
	size_t new_size = 1;
	for (size_t i = 0; i < higher->size + lower->size - 1; i++)
	{
		temp[i + 1] += temp[i] / BASE;
		temp[i] %= BASE;
		if (temp[i + 1])
			new_size++;
	}

	big_int* result = new_big_int(new_size, 0);
	for (size_t i = 0; i < new_size; i++)
		result->chunks[i] = (uint32_t)temp[i];
	
	free(temp);
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
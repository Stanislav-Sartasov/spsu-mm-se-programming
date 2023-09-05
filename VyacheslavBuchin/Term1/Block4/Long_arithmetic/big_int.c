//
// Created by Вячеслав Бучин on 26.11.2021.
//

#include "big_int.h"

#include <stdlib.h>
#include <memory.h>

uint32_t* zero_array(uint32_t size)
{
	uint32_t* result = (uint32_t*) malloc(sizeof(uint32_t) * size);
	memset(result, 0, sizeof(uint32_t) * size);
	return result;
}

big_int_t* big_int_reserved_size(uint32_t size)
{
	big_int_t* result = (big_int_t*) malloc(sizeof(big_int_t));
	result->digits = zero_array(size);

	return result;
}

uint32_t log_base(uint64_t value, uint64_t base)
{
	uint32_t result = 0;
	while (value > 0)
	{
		result++;
		value /= base;
	}
	return result;
}

big_int_t* big_int_by_value(uint64_t value)
{
	uint32_t size = log_base(value, BIG_INTEGER_BASE);
	size += size == 0 ? 1 : 0;
	big_int_t* result = big_int_reserved_size(size);
	result->size = size;

	for (int i = 0; i < size; i++)
	{
		result->digits[i] = value % BIG_INTEGER_BASE;
		value /= BIG_INTEGER_BASE;
	}

	return result;
}

char* fill_char(uint32_t value, char* destination, uint32_t symbol_ctr)
{
	static char symbol[] = "0123456789ABCDEF";
	for (; symbol_ctr > 0; symbol_ctr--)
	{
		*(destination--) = symbol[value % 16];
		value /= 16;
	}
	return destination;
}

char* big_int_to_string(big_int_t* number)
{
	uint32_t head = number->digits[number->size - 1];
	uint32_t log16 = log_base(head, 16);
	size_t str_sz = (number->size - 1) * 7 + log16;

	char* result = (char*) malloc((str_sz + 1) * sizeof(char));
	char* current = result + str_sz;
	*(current--) = '\0';

	for (int i = 0; i < number->size - 1; i++)
		current = fill_char(number->digits[i], current, 7);
	fill_char(head, current, log16);

	return result;
}

void big_int_free(big_int_t* number)
{
	free(number->digits);
	free(number);
}

big_int_t* big_int_add(big_int_t* left, big_int_t* right)
{
	uint32_t size = (left->size > right->size ? left->size : right->size) + 1;
	uint32_t* digits = zero_array(size);

	uint32_t remainder = 0;
	for (int i = 0; i < size; i++)
	{
		digits[i] = remainder;
		if (i < left->size)
			digits[i] += left->digits[i];
		if (i < right->size)
			digits[i] += right->digits[i];
		remainder = digits[i] / BIG_INTEGER_BASE;
		digits[i] %= BIG_INTEGER_BASE;
	}

	free(left->digits);
	left->digits = digits;
	left->size = size;

	if (left->digits[left->size - 1] == 0)
		left->size--;

	return left;
}

#include "big_int.h"
#include <string.h>

void memory_error()
{
	printf("Not enough memory!");
	exit(-1);
}

void slice(const char *str, char *result, size_t start, size_t end)
{
	strncpy(result, str + start, end - start);
}

uint64_t count_digits(uint64_t value, uint64_t base)
{
	uint64_t count = 0;
	while (value > 0)
	{
		count++;
		value /= base;
	}
	return count;
}

void big_int_delete_unused_zeros(big_int_t *number)
{
	while (number->digits[number->size - 1] == 0)
		number->size--;
	realloc(number->digits, number->size * sizeof(uint64_t));
}


big_int_t *big_int_by_value(uint64_t value)
{
	big_int_t *result = malloc(sizeof(big_int_t));
	if (result == NULL)
		memory_error();

	size_t size = count_digits(value, LONG_ARITHMETIC_BASE) + 1;
	uint64_t *digits = calloc(size, sizeof(uint64_t));

	if (digits == NULL)
		memory_error();

	int digit = 0;
	while (value > 0)
	{
		digits[digit++] = value % LONG_ARITHMETIC_BASE;
		value /= LONG_ARITHMETIC_BASE;
	}
	result->digits = digits;
	result->size = size;
	big_int_delete_unused_zeros(result);
	return result;
}

big_int_t *big_int_mul(big_int_t *left, big_int_t *right)
{
	big_int_t *result = malloc(sizeof(big_int_t));

	if (result == NULL)
		memory_error();

	result->size = (left->size + right->size);
	result->digits = calloc(result->size, sizeof(uint64_t));

	if (result->digits == NULL)
		memory_error();

	for (int i = 0; i < left->size; i++)
		for (int j = 0; j < right->size; j++)
		{
			uint64_t product = left->digits[i] * right->digits[j];
			uint64_t remainder = product % LONG_ARITHMETIC_BASE;
			uint64_t temp = (result->digits[i + j] + remainder);
			result->digits[i + j] = temp % LONG_ARITHMETIC_BASE;
			result->digits[i + j + 1] += product / LONG_ARITHMETIC_BASE + temp / LONG_ARITHMETIC_BASE;
		}

	big_int_delete_unused_zeros(result);
	return result;
}

void big_int_free(big_int_t *number)
{
	free(number->digits);
	free(number);
}

char compare(uint64_t i)
{
	switch (i)
	{
		case 0: return '0';
		case 1: return '1';
		case 2: return '2';
		case 3: return '3';
		case 4: return '4';
		case 5: return '5';
		case 6: return '6';
		case 7: return '7';
		case 8: return '8';
		case 9: return '9';
		case 10: return 'A';
		case 11: return 'B';
		case 12: return 'C';
		case 13: return 'D';
		case 14: return 'E';
		case 15: return 'F';
		default: return '0';
	}
}

void big_int_print_hex(big_int_t number)
{
	size_t hex_size = number.size * 6;
	char *string = malloc(sizeof(char) * hex_size);
	if (string == NULL)
		memory_error();
	string[number.size * 6] = '\0';

	for (int i = 0; i < number.size; ++i)
	{
		for (int j = 0; j < 6; j++)
		{
			string[hex_size - 1 - 6 * i - j] = compare(number.digits[i] % 16);
			number.digits[i] /= 16;
		}
	}

	int count = 0;
	while (string[count] == '0')
		count++;


	char * string_to_print = malloc(sizeof(char) * (hex_size - count));
	slice(string, string_to_print, count, hex_size);

	printf("%s\n", string_to_print);
	free(string);
	free(string_to_print);
}
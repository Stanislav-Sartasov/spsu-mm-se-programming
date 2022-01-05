#include "long_arithmetic.h"

struct bit_integer* form_bit_int(int value, int size)
{
	bit_integer* number = (bit_integer*)malloc(sizeof(bit_integer));

	int copy_value = value;
	number->length = size;
	number->digits = (int*)malloc(number->length * sizeof(int));

	for (int i = 0; i < number->length; i++)
	{
		number->digits[i] = value % 268435456;
		value /= 268435456;
	}

	if (value)
	{
		free_int(number);
		form_bit_int(number, copy_value, size + 10);
	}

	return number;
}

void free_int(bit_integer* number)
{
	free(number->digits);
	free(number);
}

void print_long_num(bit_integer* number)
{
	int i = number->length;
	while (number->digits[--i] == 0);
	printf("0x%X", number->digits[i--]);
	for (; i >= 0; i--)
	{
		for (int j = pow_hex() - length_hex(number->digits[i]); j > 0; j--)
		{
			printf("0");
		}
		printf("%X", number->digits[i]);
	}
}

struct bit_integer* power(int value, int pow)
{
	bit_integer* result = form_bit_int(1, 1);;

	bit_integer* number = form_bit_int(value, result->length);

	if (pow)
	{
		while (pow)
		{
			if (pow % 2)
			{
				result = multiply(result, number, 1);
			}
			number = multiply(number, number, 1);
			pow /= 2;
		}
		free_int(number);
		return result;
	}
}

struct bit_integer* multiply(bit_integer* a, bit_integer* b, int cleaner)
{
	bit_integer* result = form_bit_int(0, a->length + b->length);

	for (int i = 0; i < a->length; i++)
	{
		int64_t perform = 0;
		for (int j = 0; j < b->length || perform != 0; j++)
		{
			int64_t present = result->digits[i + j] + a->digits[i] * 1LL * (j < b->length ? b->digits[j] : 0) + perform;
			result->digits[i + j] = present % 268435456;
			perform = present / 268435456;
		}
	}

	if (cleaner == 1)
	{
		free_int(a);
	}
	else if (cleaner == 2)
	{
		free_int(b);
	}
	else if (cleaner)
	{
		free_int(a);
		free_int(b);
	}

	return result;
}

int length_hex(int number)
{
	int result = number != 0 ? 0 : 1;
	for (; number != 0; result++, number /= 16);
	return result;
}

int pow_hex() 
{
	int result = 0;
	for (int i = 268435456; i != 1; result++, i /= 16);
	return result;
}

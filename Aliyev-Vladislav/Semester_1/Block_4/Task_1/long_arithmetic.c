#include "long_arithmetic.h"

struct bit_integer* form_big_int(int length, int value)
{
	bit_integer* number = (bit_integer*)malloc(sizeof(bit_integer));
	number->length = length;
	number->digits = (int*)calloc(number->length, sizeof(int));

	int k = 0;
	while (value)
	{
		number->digits[k] = value % 4096;
		value = value / 4096;
		k++;
	}

	return number;
}
void free_int(bit_integer* number)
{
	free(number->digits);
	free(number);
}

void normalize(bit_integer* number)
{
	if (number->digits[number->length - 1] >= 4096)
	{
		number->length += 4;
		number->digits = (int*)realloc(number->digits, (number->length * sizeof(int)));
		for (int i = 1; i < 5; i++)
		{
			number->digits[number->length - i] = 0;
		}
	}

	for (int i = 0; i < number->length - 1; i++)
	{
		if (number->digits[i] >= 4096)
		{
			number->digits[i + 1] += number->digits[i] / 4096;
			number->digits[i] = number->digits[i] % 4096;
		}
	}
}

void print_long_num(bit_integer* number)
{
	int k = number->length - 1;
	while (!(number->digits[k]))
	{
		k--;
	}

	printf("0x%X", number->digits[k]);
	for (int i = k - 1; i > -1; i--)
	{
		printf("%03X", number->digits[i]);
	}
}

struct bit_integer* power(bit_integer* number, int pow)
{
	if (pow == 1)
	{
		return number;
	}

	bit_integer* b = power(number, pow / 2);

	if (pow % 2)
	{
		return multiply(multiply(b, b), number);
	}

	return multiply(b, b);
}

struct bit_integer* multiply(bit_integer* number, bit_integer* b)
{
	bit_integer* rezult = form_big_int(number->length + b->length, 0);

	for (int i = 0; i < number->length; i++)
	{
		for (int j = 0; j < b->length; j++)
		{
			rezult->digits[i + j] += number->digits[i] * b->digits[j];
		}
	}

	normalize(rezult);

	return rezult;
}

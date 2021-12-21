#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "long_arithmetic.h"

long_number* create_number(int size, int value, int base)
{
	// if the base os the number is greater than MAX_BASE, we may have some mistakes because of overflow during product

	if (base > MAX_BASE)
	{
		return NULL;
	}

	long_number* number = (long_number*)malloc(sizeof(long_number));
	number->digits = (int*)calloc(size, sizeof(int));
	number->size = size;
	number->base = base;
	number->sign = value >= 0 ? 1 : -1;
	value = value >= 0 ? value : -value;

	for (int i = 0; value > 0; i++)
	{
		number->digits[i] = value % base;
		value /= base;
	}

	return number;
}

long_number* copy_long_number(long_number* number)
{
	long_number* result = (long_number*)malloc(sizeof(long_number));
	result->digits = (int*)calloc(number->size, sizeof(int));
	result->base = number->base;
	result->sign = number->sign;
	result->size = number->size;

	for (int i = 0; i < result->size; i++)
	{
		result->digits[i] = number->digits[i];
	}

	return result;
}

void cut_empety_digits(long_number* number)
{
	int size = number->size - 1;
	while (number->digits[size] == 0)
		size--;

	number->size = size + 1;
	number->digits = (int*)realloc(number->digits, (number->size * sizeof(int)));
	
}

long_number* normalize(long_number* number)
{
	if (number->digits[number->size - 1] >= number->base) // if the largest digit is greater than the base
	{
		number->size += 1;
		number->digits = (int*)realloc(number->digits, (number->size * sizeof(int)));
		number->digits[number->size - 1] = 0;
	}

	for (int i = 0; i < number->size; i++) // normalize other digits if they are greater than the base
	{
		if (number->digits[i] >= number->base)
		{
			number->digits[i + 1] += number->digits[i] / number->base;
			number->digits[i] = number->digits[i] % number->base;
		}
	}

	return number;
}

void free_number(long_number* num)
{
	free(num->digits);
	free(num);
	return;
}

int long_comparator(long_number* first, long_number* second)
{
	if (first->sign != second->sign)
		return first->sign >= second->sign ? 1 : 0;
	else
	{
		for (int i = 0; i < first->size; i++)
		{
			if (first->digits[i] != second->digits[i])
				return first->digits[i] >= second->digits[i] ? 1 : 0;
		}
	}
}

long_number* long_max(long_number* first, long_number* second)
{
	return long_comparator(first, second) == 1 ? first : second;
}

long_number* long_sum(long_number* first, long_number* second) // calculates a + b; a - b; -a - b; -a + b
{
	int end = first->size >= second->size ? first->size : second->size;
	long_number* result = create_number(end + 1, 0, first->base);

	if (first->sign != second->sign)
	{
		int flag = !(long_comparator(first, second));
		long_number* temp = first;

		if (flag)
		{
			first = second;
			second = temp;
		}

		int difference_flag = 0;

		for (int i = 0; i < end; i++)
		{
			if (difference_flag)
			{
				if (first->digits[i] > 0)
				{
					first->digits[i]--;
					difference_flag = 0;
				}
				else
				{
					first->digits[i] += result->base;
				}
			}
			if (i < second->size)
			{
				if (result->digits[i] < 0)
				{
					result->digits[i] += result->base;
					int difference_flag = 1;
				}
				result->digits[i] = first->digits[i] - second->digits[i];
			}
			else
			{
				result->digits[i] = first->digits[i];
			}
		}

		if (flag)
		{
			second = first;
			first = temp;
		}
	}
	else
	{
		result->sign = first->sign;

		for (int i = 0; i < end; i++)
		{
			if (i < first->size && i < second->size)
			{
				result->digits[i] = first->digits[i] + second->digits[i];
				result->digits[i + 1] += result->digits[i] / result->base;
				result->digits[i] %= result->base;
			}
			else if (i < first->size)
				result->digits[i] += first->digits[i];
			else
				result->digits[i] += second->digits[i];
		}
	}
	return result;
}

long_number* column_product(long_number* first, long_number* second, int flag_memory_free)
{
	long_number* result = create_number(first->size + second->size, 0, first->base);
	result->sign = first->sign * second->sign;

	for (int i = 0; i < first->size; i++)
		for (int j = 0; j < second->size; j++)
			result->digits[i + j] += first->digits[i] * second->digits[j];

	if (flag_memory_free == 2)
		free_number(first), free_number(second);
	else if (flag_memory_free == 1)
		free_number(first);

	normalize(result);
	return result;
}

long_number* long_power(long_number* number, int power) 
{
	long_number* result = create_number(1, 1, number->base);

	if (power == 0)
		return result;
		
	if (power == 1)
		return number;
	
	long_number* temp = copy_long_number(number);

	while (power > 0)
	{
		if (power & 1)
		{
			result = column_product(result, temp, 1);
		}
		power >>= 1;
		temp = column_product(temp, temp, 1);
	}

	free_number(temp);
	return result;
}

void print_long_number(long_number* number)
{
	cut_empety_digits(number);
	printf("%X", number->digits[number->size - 1]);
	for (int i = number->size - 2; i > -1; i--)
	{
		printf("%03X", number->digits[i]);
	}
	printf("\n");
}
#include "long_arithmetics_uint.h"
#include "malloc.h"
#include "string.h"
#include <stdio.h>

struct long_ar_uint *initialize_long_ar_uint(int base)
{
	if (base > 16 || base < 2) 
	{
		return 0;
	}
	struct long_ar_uint *my_int = (struct long_ar_uint *) malloc(sizeof(struct long_ar_uint));
	my_int->current_length = 0;
	my_int->max_length = START_LONG_ARITHMETICS_UINT_LENGTH;
	my_int->digits_array = (unsigned short int *) malloc(sizeof(unsigned short int) * my_int->max_length);
	my_int->base = base;

	return my_int;
}

void set_number_for_long_ar_uint(struct long_ar_uint *my_int, unsigned int source_number)
{

	unsigned int temp_number = source_number;
	int current_digit_count = 0;

	if (temp_number == 0) 
	{
		my_int->current_length = 1;
		my_int->digits_array[0] = 0;
		return;
	}

	while (temp_number > 0) 
	{
		my_int->digits_array[current_digit_count] = (temp_number % my_int->base);
		temp_number /= my_int->base;
		++current_digit_count;
	}
	my_int->current_length = current_digit_count;
}

void increase_long_ar_int_size(struct long_ar_uint *my_int, int count)
{
	my_int->digits_array = realloc(my_int->digits_array, (my_int->max_length + count)*sizeof(unsigned short int));
	my_int->max_length += count;
}

void set_long_ar_int_size(struct long_ar_uint *my_int, int count)
{
	my_int->digits_array = realloc(my_int->digits_array, count*sizeof(unsigned short int));
	my_int->max_length = count;
}

struct long_ar_uint *addition_for_long_ar_uint(struct long_ar_uint *int1, struct long_ar_uint *int2)
{

	if (int1->base != int2->base) 
	{
		return 0;
	}
	struct long_ar_uint *result = initialize_long_ar_uint(int1->base);
	int max_expected_length =
			int1->current_length >= int2->current_length ? int1->current_length : int2->current_length;
	max_expected_length += 1;

	struct long_ar_uint *bigger_int, *smaller_int;

	if (int1->current_length >= int2->current_length) 
	{
		bigger_int = int1;
		smaller_int = int2;
	}
	else 
	{
		bigger_int = int2;
		smaller_int = int1;
	}

	if (max_expected_length >= result->max_length) 
	{
		set_long_ar_int_size(result, max_expected_length);
	}

	for (int i = 0; i < bigger_int->current_length + 1; ++i)
	{
		result->digits_array[i] = 0;
	}

	for (int i = 0; i < bigger_int->current_length; ++i) 
	{
		unsigned int temp_result = bigger_int->digits_array[i] + result->digits_array[i];
		if (i < smaller_int->current_length) 
		{
			temp_result += smaller_int->digits_array[i];
		}
		if (temp_result >= result->base) 
		{
			temp_result = temp_result % result->base;
			result->digits_array[i + 1] += 1;
		}
		result->digits_array[i] = temp_result;
	}

	result->current_length = bigger_int->current_length;
	result->current_length += result->digits_array[bigger_int->current_length] > 0 ? 1 : 0;


	return result;
}

struct long_ar_uint *multiply_by_lower_than_base_for_long_ar_uint(struct long_ar_uint *int1, unsigned int int2)
{
	if (int1->base < int2) 
	{
		return 0;
	}
	if (int2 == 0) 
	{
		struct long_ar_uint *result = initialize_long_ar_uint(int1->base);
		result->current_length = 1;
		result->digits_array[0] = 0;
		return result;
	}

	struct long_ar_uint *result = initialize_long_ar_uint(int1->base);
	int max_expected_length = int1->current_length + 1;

	if (max_expected_length >= result->max_length) 
	{
		set_long_ar_int_size(result, max_expected_length);
	}

	for (int i = 0; i < int1->current_length + 1; ++i) 
	{
		result->digits_array[i] = 0;
	}

	for (int i = 0; i < int1->current_length; ++i) 
	{
		unsigned int temp_result = int1->digits_array[i] * int2 + result->digits_array[i];
		if (temp_result >= result->base) 
		{
			result->digits_array[i + 1] += temp_result / result->base;
			temp_result = temp_result % result->base;
		}
		result->digits_array[i] = temp_result;
	}

	result->current_length = int1->current_length;
	result->current_length += result->digits_array[int1->current_length] > 0 ? 1 : 0;

	return result;
}

struct long_ar_uint *multiplication_for_long_ar_uint(struct long_ar_uint *int1, struct long_ar_uint *int2)
{
	if (int1->base != int2->base) 
	{
		return 0;
	}

	struct long_ar_uint *bigger_int, *smaller_int;

	if (int1->current_length >= int2->current_length) 
	{
		bigger_int = int1;
		smaller_int = int2;
	}
	else 
	{
		bigger_int = int2;
		smaller_int = int1;
	}

	struct long_ar_uint *sum_result = 0;

	for (int i = 0; i < smaller_int->current_length; ++i) 
	{
		if (i == 0) 
		{
			sum_result = multiply_by_lower_than_base_for_long_ar_uint(bigger_int, smaller_int->digits_array[i]);
		}
		else 
		{
			struct long_ar_uint *temp_int = multiply_by_lower_than_base_for_long_ar_uint(bigger_int,
																						 smaller_int->digits_array[i]);
			struct long_ar_uint *multiplication_result = 0;
			for (int j = 0; j < i; ++j) 
			{
				multiplication_result = multiply_by_lower_than_base_for_long_ar_uint(temp_int, temp_int->base);
				free_long_ar_uint(temp_int);
				temp_int = multiplication_result;
			}
			struct long_ar_uint *temp_sum = addition_for_long_ar_uint(sum_result, multiplication_result);
			free_long_ar_uint(sum_result);
			free_long_ar_uint(temp_int);
			sum_result = temp_sum;
		}
	}

	return sum_result;
}

struct long_ar_uint *pow_for_long_ar_uint(struct long_ar_uint *int1, unsigned int int2)
{
	if (int2 <= 1) 
	{
		return 0;
	}

	struct long_ar_uint *result = 0;

	for (int i = 1; i < int2; ++i)
	{
		struct long_ar_uint *temp_int;
		if (i == 1) 
		{
			temp_int = multiplication_for_long_ar_uint(int1, int1);
		}
		else
		{
			temp_int = multiplication_for_long_ar_uint(result, int1);
			free_long_ar_uint(result);
		}
		result = temp_int;
	}

	return result;
}

void free_long_ar_uint(struct long_ar_uint *my_int)
{
	free(my_int->digits_array);
	free(my_int);
}

void print_long_ar_uint(struct long_ar_uint *my_int)
{
	if (my_int->current_length == 0) 
	{
		printf("На данный момент число не хранится.\n");
	}
	else 
	{
		char digits_string[] = DIGIT_SYMBOLS;
		for (int i = my_int->current_length - 1; i >= 0; --i)
		{
			printf("%c", digits_string[my_int->digits_array[i]]);
		}
		printf("\n");
	}
}
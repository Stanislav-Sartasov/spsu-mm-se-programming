#include "long_int.h"
#include <stdlib.h>
#include <stdio.h>

// Create an instance
struct my_long_int* init_long_number(int dec_number, int max_length)
{
	// Only creating a number with 1 digit is possible
	if (dec_number >= 4096)
		return NULL;
	struct my_long_int* result = (struct my_long_int*)calloc(1, sizeof(struct my_long_int));

	// Allocate memory
	result->digits = (int*)calloc(max_length, sizeof(int));
	result->length = 1;
	result->max_length = max_length;
	result->digits[0] = dec_number;

	return result;
}

// Prints in base 16
void print_long_num(struct my_long_int* output)
{
	// The first zeros are meaningless
	printf("%x", output->digits[output->length - 1]);
	// Printing rest with zeros
	for (int i = 0; i < output->length; i++)
		printf("%03x", output->digits[output->length - i - 1]);
}


/* 
* Special addition function, to use as normal addition begin index should be 0
* It works like this: left + right * base ^ begin_index
*/ 
struct my_long_int* add_int(struct my_long_int* left, struct my_long_int* right, int begin_index)
{
	// Variables for algorithm
	int remainder = 0;

	// Create new number
	int new_max_len = max(left->length, right->length) + 1;
	struct my_long_int* result = init_long_number(0, new_max_len * sizeof(int));
	result->max_length = new_max_len;

	// Transfer first digits from the previous count
	for (int i = 0; i < begin_index; i++)
	{
		result->digits[i] = left->digits[i];
	}
	// Add digit by digit
	for (int i = begin_index; i < new_max_len + 1; i++)
	{
		result->digits[i] = (left->digits[i] + right->digits[i - begin_index] + remainder) % 4096;
		remainder = (left->digits[i] + right->digits[i - begin_index] + remainder) / 4096;
		if (left->digits[i] + right->digits[i - begin_index] + remainder != 0)
			result->length = i + 1;
	}
	return result;
}

// Creates new long int storing multiplication result
struct my_long_int* mult_int(struct my_long_int* left, struct my_long_int* right)
{
	// Variables for algorithm
	int remainder = 0;
	struct my_long_int* tmp = NULL;

	// Create new number (result)
	int new_max_len = max(left->max_length, right->max_length) + 1;
	struct my_long_int* result = init_long_number(0, (left->length + 1) * (right->length + 1) * sizeof(int));
	result->max_length = new_max_len;

	// Create number for buffer (holding copies of right number)
	struct my_long_int* buffer = init_long_number(0, (right->length + 1) * sizeof(int));
	buffer->max_length = new_max_len;
	buffer->length = right->length;

	// Multiplying digits with each others
	for (int i = 0; i < left->length; i++)
	{
		// Count next part
		memset(buffer->digits, 0, (right->length + 1) * sizeof(int));
		remainder = 0;
		for (int j = 0; j < right->length; j++)
		{
			buffer->digits[j] += (right->digits[j] * left->digits[i] + remainder) % 4096;
			remainder = (right->digits[j] * left->digits[i] + remainder) / 4096;
		}
		buffer->digits[right->length] = remainder;
		// Add result from buffer to result variable
		tmp = add_int(result, buffer, i);
		free_int(result);
		result = tmp;
	}
	return result;
}

// An algorithm I came upon while thinking
struct my_long_int* power_int(int number, int power)
{
	int tmp_power = power;
	int count = 0;
	// Count how much numbers will be needed (1 in binary of given int)
	for (int i = 0; i < 32; i++)
		if (tmp_power >> (32 - i) & 0x1) count++;

	// Allocate 'count' long number pointers
	struct my_long_int** power_two_numbers = (struct my_long_int**)malloc(sizeof(struct my_long_int*) * count);

	// Allocate other useful variables
	struct my_long_int* tmp;
	struct my_long_int* result;

	// Count every part of power
	int index = 0;
	for (int i = 0; i < 32; i++)
		if (tmp_power >> (32 - i) & 0x1) 
		{
			// Init next number
			power_two_numbers[index] = init_long_number(number, 666);
			// Increase power by 2
			for (int j = 0; j < (32 - i); j++) 
			{
				tmp = mult_int(power_two_numbers[index], power_two_numbers[index]);
				free_int(power_two_numbers[index]);
				power_two_numbers[index] = tmp;
			}
			index++;
		}
	// Multiply every number in array
	result = init_long_number(1, 666);
	for (int i = 0; i < count; i++) 
		result = mult_int(result, power_two_numbers[i]);

	// Free section
	for (int i = 0; i < count; i++)
		free(power_two_numbers[i]);

	return result;
}

// Free the long integer
void free_int(struct my_long_int* target) 
{
	free(target->digits);
	free(target);
}

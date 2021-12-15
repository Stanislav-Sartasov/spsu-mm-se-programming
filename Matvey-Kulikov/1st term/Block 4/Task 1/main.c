#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <stdint.h>
#define DIGIT_BASE ((uint64_t)1 << 32)

struct number
{
	int length;
	uint32_t* digits;
};

typedef struct number number;

void remove_useless_digits(number* num)
{
	uint32_t* digit_pointer = num->digits;
	while (!*digit_pointer)
	{
		digit_pointer++;
		num->length--;
	}

	uint32_t* new_digits = (uint32_t*)malloc(sizeof(uint32_t) * num->length);
	memcpy(new_digits, digit_pointer, sizeof(uint32_t) * num->length);
	free(num->digits);
	num->digits = new_digits;
}

void free_num(number* num)
{
	free(num->digits);
	free(num);
}

number* sum(number* num_one, number* num_two)
{
	number* output_num = (number*)malloc(sizeof(number));
	int len_one = num_one->length;
	int len_two = num_two->length;
	int max_length = (len_one > len_two) ? len_one : len_two;
	max_length++;
	output_num->length = max_length;
	output_num->digits = (uint32_t*)calloc(max_length, sizeof(uint32_t));

	while (len_one && len_two)
	{
		uint64_t sum = num_one->digits[len_one - 1] + num_two->digits[len_two - 1] + output_num->digits[max_length - 1];
		output_num->digits[max_length - 1] = (uint32_t)(sum % DIGIT_BASE);
		output_num->digits[max_length - 2] = (uint32_t)(sum / DIGIT_BASE);
		max_length--;
		len_one--;
		len_two--;
	}

	number* left_num;
	int left_len = 0;
	if (len_one)
	{
		left_num = num_one;
		left_len = len_one;
	}
	else if (len_two)
	{
		left_num = num_two;
		left_len = len_two;
	}

	while (left_len)
	{
		uint64_t sum = left_num->digits[left_len - 1] + output_num->digits[max_length - 1];
		output_num->digits[max_length - 1] = (uint32_t)(sum % DIGIT_BASE);
		output_num->digits[max_length - 2] = (uint32_t)(sum / DIGIT_BASE);
		max_length--;
		left_len--;
	}

	remove_useless_digits(output_num);
	return output_num;
}

number* multiply(number* num_one, number* num_two)
{
	number* output_num = (number*)malloc(sizeof(number));
	output_num->length = 1;
	output_num->digits = (uint32_t*)calloc(1, sizeof(uint32_t));

	for (int index_one = num_one->length; index_one > 0; index_one--)
	{
		number* temp_num = (number*)malloc(sizeof(number));
		int temp_num_offset = num_one->length - index_one;
		temp_num->length = num_two->length + 1 + temp_num_offset;
		temp_num->digits = (uint32_t*)calloc(temp_num->length, sizeof(uint32_t));

		uint64_t digit_one = num_one->digits[index_one - 1];

		for (int index_two = num_two->length; index_two > 0; index_two--)
		{
			uint64_t product = num_two->digits[index_two - 1] * digit_one + temp_num->digits[index_two];
			temp_num->digits[index_two] = (uint32_t)(product % DIGIT_BASE);
			temp_num->digits[index_two - 1] = (uint32_t)(product / DIGIT_BASE);
		}

		remove_useless_digits(temp_num);
		number* old_num = output_num;
		output_num = sum(old_num, temp_num);
		free_num(old_num);
		free_num(temp_num);
	}
	return output_num;
}

int main()
{
	printf("This program counts value of 3^5000 using bignum arithmetic and outputs it.\n");
	number* three = (number*)malloc(sizeof(number));
	three->length = 1;
	three->digits = (uint32_t*)malloc(sizeof(uint32_t));
	three->digits[0] = 3;

	number result_number = *three;
	number* result = &result_number;

	for (int i = 0; i < 4999; i++)
	{
		number* old_result = result;
		result = multiply(old_result, three);
	}

	printf("0x%X", result->digits[0]);
	for (int i = 1; i < result->length; i++)
	{
		printf("%08X", result->digits[i]);
	}
	return 0;
}

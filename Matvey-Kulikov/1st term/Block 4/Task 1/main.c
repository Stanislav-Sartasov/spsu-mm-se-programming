#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <stdint.h>
#define DIGIT_BASE ((uint64_t)1 << 32)

struct number
{
	int length;
	uint64_t* digits;
};

typedef struct number number;

void remove_useless_digits(number* num)
{
	uint64_t* digit_pointer = num->digits;
	while (!*digit_pointer)
	{
		digit_pointer++;
		num->length--;
	}

	uint64_t* new_digits = (uint64_t*)malloc(sizeof(uint64_t) * num->length);
	memcpy(new_digits, digit_pointer, sizeof(uint64_t) * num->length);
	free(num->digits);
	num->digits = new_digits;
}

void free_num(number* num)
{
	free(num->digits);
	free(num);
}

number* sum(number* num_1, number* num_2)
{
	number* output_num = (number*)malloc(sizeof(number));
	int len_1 = num_1->length;
	int len_2 = num_2->length;
	int max_length = (len_1 > len_2) ? len_1 : len_2;
	max_length++;
	output_num->length = max_length;
	output_num->digits = (uint64_t*)calloc(max_length, sizeof(uint64_t));

	while (len_1 && len_2)
	{
		uint64_t sum = num_1->digits[len_1 - 1] + num_2->digits[len_2 - 1] + output_num->digits[max_length - 1];
		output_num->digits[max_length - 1] = sum % DIGIT_BASE;
		output_num->digits[max_length - 2] = sum / DIGIT_BASE;
		max_length--;
		len_1--;
		len_2--;
	}

	number* left_num;
	int left_len = 0;
	if (len_1)
	{
		left_num = num_1;
		left_len = len_1;
	}
	else if (len_2)
	{
		left_num = num_2;
		left_len = len_2;
	}

	while (left_len)
	{
		uint64_t sum = left_num->digits[left_len - 1] + output_num->digits[max_length - 1];
		output_num->digits[max_length - 1] = sum % DIGIT_BASE;
		output_num->digits[max_length - 2] = sum / DIGIT_BASE;
		max_length--;
		left_len--;
	}

	remove_useless_digits(output_num);
	return output_num;
}

number* multiply(number* num_1, number* num_2)
{
	number* output_num = (number*)malloc(sizeof(number));
	output_num->length = 1;
	output_num->digits = (uint64_t*)calloc(1, sizeof(uint64_t));

	for (int index_1 = num_1->length; index_1 > 0; index_1--)
	{
		number* temp_num = (number*)malloc(sizeof(number));
		int temp_num_offset = num_1->length - index_1;
		temp_num->length = num_2->length + 1 + temp_num_offset;
		temp_num->digits = (uint64_t*)calloc(temp_num->length, sizeof(uint64_t));

		uint64_t digit_1 = num_1->digits[index_1 - 1];

		for (int index_2 = num_2->length; index_2 > 0; index_2--)
		{
			uint64_t product = num_2->digits[index_2 - 1] * digit_1 + temp_num->digits[index_2];
			temp_num->digits[index_2] = product % DIGIT_BASE;
			temp_num->digits[index_2 - 1] = product /DIGIT_BASE;
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
	printf("This program counts value of 3^5000 using bignum arithmetic and outputs it.\n0x");
	number* three = (number*)malloc(sizeof(number));
	three->length = 1;
	three->digits = (uint64_t*)malloc(sizeof(uint64_t));
	three->digits[0] = 3;

	number result_number = *three;
	number* result = &result_number;

	for (int i = 0; i < 4999; i++)
	{
		number* old_result = result;
		result = multiply(old_result, three);
	}

	for (int i = 0; i < result->length; i++)
	{
		printf("%08lX", result->digits[i]);
	}
	return 0;
}

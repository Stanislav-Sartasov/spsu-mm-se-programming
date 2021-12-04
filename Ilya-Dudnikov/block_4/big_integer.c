#include "big_integer.h"
#include <string.h>
#include <minmax.h>
#include <stdlib.h>

big_int *set_value(int val, int size)
{
	big_int *number = (big_int*) malloc(sizeof(big_int));
	number->digits = (int*) calloc(size, sizeof(int));
	number->size = size;
	number->digits_cnt = 0;

	for (int i = 0; val; i++)
	{
		number->digits_cnt++;
		number->digits[i] = val % BASE;
		val /= BASE;
	}
	return number;
}

void delete_big_int(big_int *number)
{
	free(number->digits);
	number->digits = NULL;
	free(number);
	number = NULL;
}

char *int_to_hexadecimal(int number)
{
	char alphabet[] = "0123456789ABCDEF";
	char *result = (char*) malloc(3 * sizeof(char));

	result[0] = alphabet[number % 16];
	result[1] = '0';
	result[2] = '\0';

	if (number >= 16) {
		result[1] = alphabet[number / 16];
	}

	return result;
}

char *big_int_to_hexadecimal(big_int *num)
{
	int result_size = (1 + 2 * num->size);

	if (num->digits[num->digits_cnt - 1] < 16)
		result_size--;

	char *result = (char*) malloc(result_size * sizeof(char));

	for (int i = 0; i < num->size; i++)
	{
		char *current_digit = int_to_hexadecimal(num->digits[i]);
		if (result_size >= 2 * i - 2)
			result[result_size - 2 * i - 2] = current_digit[0];
		result[result_size - 2 * i - 3] = current_digit[1];

		free(current_digit);
	}
	result[result_size - 1] = '\0';
	return result;
}

big_int *big_int_add(big_int *left, big_int *right)
{
	int remainder = 0;
	big_int *result = set_value(0, max(left->digits_cnt, right->digits_cnt));
	for (int i = 0; i < max(left->digits_cnt, right->digits_cnt) || remainder; i++)
	{
		if (i >= result->size)
		{
			result->size += 10;
			result->digits = (int*)realloc(result->digits, result->size * sizeof(int));

			for (int k = 0; k < 10; k++)
				result->digits[result->size - k - 1] = 0;
		}

		result->digits_cnt++;
		int left_digit = (i < left->size ? left->digits[i] : 0);
		int right_digit = (i < right->size ? right->digits[i] : 0);

		result->digits[i] = remainder + left_digit + right_digit;
		remainder = result->digits[i] / BASE;
		result->digits[i] %= BASE;
	}
	return result;
}

big_int *big_int_multiply(big_int *left, big_int *right)
{
	big_int *result = set_value(0, left->digits_cnt + right->digits_cnt);

	int remainder = 0;
	for (int i = 0; i < left->size; i++)
	{
		big_int *tmp = set_value(0, i + right->digits_cnt);
		tmp->digits_cnt = i;
		for (int j = 0; j < right->digits_cnt || remainder; j++)
		{
			if (i + j >= tmp->size)
			{
				tmp->size += 10;
				tmp->digits = (int*)realloc(tmp->digits, tmp->size * sizeof(int));

				for (int k = 0; k < 10; k++)
					tmp->digits[tmp->size - k - 1] = 0;
			}
			tmp->digits_cnt++;

			int left_digit = (i < left->size ? left->digits[i] : 0);
			int right_digit = (j < right->size ? right->digits[j] : 0);

			tmp->digits[i + j] = left_digit * right_digit + remainder;
			remainder = tmp->digits[i + j] / BASE;
			tmp->digits[i + j] %= BASE;
		}
		big_int *addition_result = big_int_add(result, tmp);
		delete_big_int(result);
		result = addition_result;
		delete_big_int(tmp);
	}
	return result;
}

big_int *big_int_power(big_int *num, int power)
{
	big_int *tmp, *result;
	if (power == 0)
	{
		result = set_value(1, 1);
		return result;
	}

	if (power % 2)
	{
		tmp = big_int_power(num, power - 1);
		result = big_int_multiply(num, tmp);
		delete_big_int(tmp);
		return result;
	}
	tmp = big_int_power(num, power / 2);
	result = big_int_multiply(tmp, tmp);
	delete_big_int(tmp);
	return result;
}
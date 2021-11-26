#include "big_integer.h"
#include <limits.h>
#include <string.h>
#include <minmax.h>

void set_to_zero(big_int *value)
{
	memset(value->digits, 0, sizeof(value->digits));
	value->size = 0;
}

long long decimal(big_int *num)
{
	long long res = 0;
	long long current_power = 1;
	for (int i = 0; i < min(7, num->size); i++)
	{
		if (res > LLONG_MAX - current_power * num->digits[i])
		{
			return TOO_BIG_FOR_DECIMAL;
		}
		res += current_power * num->digits[i];
		current_power *= BASE;
	}
	return res;
}

void big_int_add(big_int *left, big_int *right)
{
	int remainder = 0;
	for (int i = 0; i < max(left->size, right->size) || remainder; i++)
	{
		left->digits[i] += remainder + right->digits[i];
		remainder = left->digits[i] / BASE;
		left->digits[i] %= BASE;

		if (i >= left->size)
			left->size++;
	}
}

big_int big_int_multiply(big_int *left, big_int *right)
{
	big_int result;
	set_to_zero(&result);

	int remainder = 0;
	for (int i = 0; i < left->size; i++)
	{
		big_int tmp;
		set_to_zero(&tmp);
		tmp.size = i + 1;
		for (int j = 0; j < right->size || remainder; j++)
		{
			tmp.size++;
			tmp.digits[i + j] = left->digits[i] * right->digits[j] + remainder;
			remainder = tmp.digits[i + j] / BASE;
			tmp.digits[i + j] %= BASE;
		}
		big_int_add(&result, &tmp);
	}
	return result;
}
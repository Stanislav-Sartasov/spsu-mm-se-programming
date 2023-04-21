#include "big_library.h"

big_integer* create_big_int(int32_t value, int32_t size)
{
	big_integer* number = (big_integer*)malloc(sizeof(big_integer));
	if (number == NULL)
	{
		printf("failed to allocate memory");
		return -1;
	}
	int32_t copy_value = value;
	number->length = size;
	number->digits = (int32_t*)malloc(number->length * sizeof(int32_t));
	if (number->digits == NULL)
	{
		printf("failed to allocate memory");
		return -1;
	}
	for (size_t i = 0; i < number->length; i++)
	{
		number->digits[i] = value % HEX_BASE;
		value /= HEX_BASE;
	}

	if (value)
	{
		delete_big_int(number);
		create_big_int(number, copy_value, size + 10);
	}
	return number;
}

void delete_big_int(big_integer* number)
{
	free(number->digits);
	free(number);
}

big_integer* multiply(big_integer* a, big_integer* b, int32_t cleaner)
{
	big_integer* result = create_big_int(0, a->length + b->length);
	for (size_t i = 0; i < a->length; i++)
	{
		uint64_t carry = 0;
		for (size_t j = 0; j < b->length || carry != 0; j++)
		{
			uint64_t current = result->digits[i + j] + a->digits[i] * 1LL * (j < b->length ? b->digits[j] : 0) + carry;
			result->digits[i + j] = current % HEX_BASE;
			carry = current / HEX_BASE;
		}
	}

	if (cleaner == 1)
	{
		delete_big_int(a);
	}
	else if (cleaner == 2)
	{
		delete_big_int(b);
	}
	else if (cleaner)
	{
		delete_big_int(a);
		delete_big_int(b);
	}

	return result;
}

big_integer* power(int32_t value, int32_t degree)
{
	big_integer* result = create_big_int(1, 1);;
	big_integer* number = create_big_int(value, result->length);
	while (degree)
	{
		if (degree % 2)
		{
			result = multiply(result, number, 1);
		}
		number = multiply(number, number, 1);
		degree /= 2;
	}
	delete_big_int(number);
	return result;
}

void printf_big_int_hex(big_integer* number)
{
	int32_t i = number->length;
	while (number->digits[--i] == 0);
	printf("0x%x", number->digits[i--]);
	for (; i >= 0; i--)
	{
		for (size_t j = degree_base_hex() - length_hex(number->digits[i]); j > 0; j--)
		{
			printf("0");
		}
		printf("%x", number->digits[i]);
	}
}

int32_t length_hex(int32_t number) // calculates the number of digits in the hexadecimal number system
{
	int32_t result = number != 0 ? 0 : 1;
	for (; number != 0; result++, number /= 16);
	return result;
}

int32_t degree_base_hex() // calculates the degree indicator of a number with a base of sixteen
{
	int32_t result = 0;
	for (size_t i = HEX_BASE; i != 1; result++, i /= 16);
	return result;
}
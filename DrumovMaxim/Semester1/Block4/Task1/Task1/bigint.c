#include "bigint.h"


bigInt* newBigInt(size_t size, uint32_t value)
{
	bigInt* number = (bigInt*)malloc(sizeof(bigInt));

	if (number == NULL)
	{
		printf("Unable to allocate memory for bigint.\n");
		return -1;
	}

	number->size = size;
	number->digits = (uint32_t*)calloc(size, sizeof(uint32_t));

	if (number == NULL)
	{
		printf("Unable to allocate memory for bigint digits.\n");
		return -1;
	}

	number->digits[0] = value;

	return number;
}

void printfBigIntHex(bigInt* number)
{
	int32_t i = number->size;
	while (number->digits[--i] == 0);
	printf("0x%x", number->digits[i--]);
	for (; i >= 0; i--)
		printf("%.7x", number->digits[i]);
}

void freeBigInt(bigInt* number)
{
	free(number->digits);
	free(number);
}

bigInt* multiply(bigInt* left, bigInt* right)
{
	size_t firstSize = (left->size + right->size);
	uint64_t* temp = (uint64_t*)calloc(firstSize, sizeof(uint64_t));

	for (size_t i = 0; i < left->size; i++)
	{
		for (size_t j = 0; j < right->size; j++)
		{
			temp[i + j] += (uint64_t)left->digits[i] * (uint64_t)right->digits[j];
		}
	}
	for (size_t i = 0; i < firstSize - 1; i++)
	{
		temp[i + 1] += temp[i] / BASE;
		temp[i] %= BASE;
	}

	size_t newSize = firstSize;
	do
	{
		newSize--;
	} while (!temp[newSize] && newSize > 0);


	bigInt* result = newBigInt(newSize + 1, 0);
	for (size_t i = 0; i < newSize + 1; i++)
		result->digits[i] = (uint32_t)temp[i];

	free(temp);
	return result;
}

bigInt* power(bigInt* number, int32_t pow)
{
	if (pow == 1)
	{
		return number;
	}
	else if (pow % 2)
	{
		pow /= 2;
		return multiply(multiply(power(number, pow), power(number, pow)), number);
	}
	else
	{
		pow /= 2;
		return multiply(power(number, pow), power(number, pow));
	}
}
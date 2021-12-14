#include "longarithmetic.h"

longNumber longPow(int base, int degree)
{
	longNumber number;
	number.length = (unsigned int)floor(degree * (log(base) / log(BASE))) + 1;
	number.digits = (unsigned int*)calloc(number.length, sizeof(unsigned int));
	number.digits[number.length - 1] = 1;

	unsigned int current, t;
	for (int i = 0; i < degree; i++)
	{
		t = 0;
		for (int j = number.length - 1; j >= 0; j--)
		{
			current = ((unsigned long long) base * number.digits[j] + t) % BASE;
			t = ((unsigned long long) base * number.digits[j] + t) / BASE;
			number.digits[j] = current;
		}
	}

	return number;
}

void longPrint(longNumber* number)
{
	for (int i = 0; i < number->length; i++)
	{
		if (number->digits[i])
		{
			if (i == 0)
				printf("%X", number->digits[i]);
			else
				printf("%08X", number->digits[i]);
		}
	}
	printf("\n");
}

void longFree(longNumber* number)
{
	free(number->digits);
}
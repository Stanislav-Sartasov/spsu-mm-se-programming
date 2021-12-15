#define POWER 5000
#define CALC_DEGREE 4294967296   //16^8 - основание
#define EXP 3

#include <stdio.h>
#include <stdlib.h>
#include <string.h>



struct bigInt
{
	int amount;
	unsigned int* result;
};

int main()
{
	printf("The programm calculates the value of the number 3^5000\nusing long arithmetic algorithms and represents it in hexadecimal notation.\n\n>>>");
	unsigned short countPower = 1;
	struct bigInt number;

	if ((number.result = (unsigned int*)malloc(sizeof(unsigned int))) == NULL)
	{
		printf("ERROR: failed to allocate memory.\n");
		printf("Completing the program.\n");
		exit(-1);
	}
	number.amount = 1;
	number.result[0] = EXP;

	int coef1;
	unsigned int multiple;
	unsigned long long digit;
	while (countPower < POWER)
	{
		multiple = EXP;
		digit = 0;
		countPower++;
		while (multiple < CALC_DEGREE / EXP + 1 && countPower < POWER)
		{
			multiple *= EXP;
			countPower++;
		}

		for (coef1 = 0; coef1 < number.amount; coef1++)
		{
			digit = (unsigned long long)number.result[coef1] * (unsigned long long)multiple + digit;
			number.result[coef1] = digit % CALC_DEGREE;
			digit = digit / CALC_DEGREE;
		}

		if (digit != 0)
		{
			number.amount++;
			if ((number.result = (unsigned int*)realloc(number.result, number.amount * sizeof(unsigned int))) == NULL)
			{
				printf("ERROR: failed to reallocate memory.\n");
				printf("Completing the program.\n");
				exit(-1);
			}
			number.result[number.amount - 1] = digit;
		}
	}

	printf("%x", number.result[number.amount - 1]);
	for (coef1 = number.amount - 2; coef1 >= 0; coef1--)
	{
		printf("%08x", number.result[coef1]);
	}
	printf("\n");

	free(number.result);
	return 0;
}
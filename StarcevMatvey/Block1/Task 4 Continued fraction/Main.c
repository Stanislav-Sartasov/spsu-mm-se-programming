#include <stdio.h>
#include <math.h>
#include "inputAndTools.h"

int main()
{
	printf("Calculates the order of a continued fraction and period for sqrt(number)\n");

	long int number;
	while (1)
	{
		printf("Input your integer number\n");
		number = get_int();
		if (number > 0 && sqrt(number) != trunc(sqrt(number)))
		{
			break;
		}
		printf("Your input is incorrect\n\n");
	}

	int rounded_number = trunc(sqrt(number));

	// period
	int i = 1;

	// coefficients for quadratic surd
	int q = 1;
	int p = 0;

	// first element
	int a = rounded_number;

	printf("Order is: %d ", a);

	do
	{
		// calculation of new coefficients for new interaction
		p = a * q - p;
		q = (number - p * p) / q;

		a = (rounded_number + p) / q;

		i++;
		printf("%d ", a);

	} while (q != 1); // wiil end when coefficents repeated

	printf("\nPeriod is: %d", i);

	return 0;
}

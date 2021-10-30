//Homework 1.1
//Mersenne Prime Numbers

#include <stdio.h>
#include <math.h>
#include <stdbool.h>

#define RANGE 31

int prime_number(int number)
{
	int i;

	if (number == 1)
	{
		return false;
	}
	else
	{
		for (i = 2; i <= (int)sqrt(number); i++) //i <= number or i < sqrt(number)
		{
			if (number % i == 0)
			{
				return false;
				break;
			}
		}
	}
	return true;
}

int main()
{
	int i, mersenne_numbers;

	printf("\nThis program prints Mersenne prime numbers in the range [1; 2^31 - 1]:\n\n");

	for (i = 1; i <= RANGE; i++)
	{
		mersenne_numbers = pow(2, (i)) - 1;

		if (prime_number(mersenne_numbers))
		{
			printf("%d\t", mersenne_numbers);
		}
	}

	return 0;
}
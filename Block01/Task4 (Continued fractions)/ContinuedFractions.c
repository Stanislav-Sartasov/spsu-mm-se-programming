#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>

void clearStdin()
{
	char temp;
	do
	{
		temp = getchar();
	} while (temp != '\n' && temp != EOF);
}

int checkNumber(int message)
{
	double number;
	int count;
	printf(message);
	do
	{
		printf("Input a positive number. It should not be a perfect square and less than 1: \n");
		count = scanf("%lf", &number);
		if (count != 0) 
		{
			if ((int)number != (double)number)
			{
				number = checkNumber("The given number is not integer. Try again.\n");
			}
			else if (number <= 1)
			{
				number = checkNumber("The number should be more than 1. Try again.\n");
			}
			else if (floor(sqrt(number)) * floor(sqrt(number)) == number)
			{
				number = checkNumber("The given number is perfect square. Try again.\n");
			}
		}
		else
		{
			printf("The input is not a number. Try again.\n");
			clearStdin();
		}
		while (getchar() != '\n');
	} while (!(count == 1 && number > 1));
	return number;
}

int main()
{
	printf("The program calculates the period and sequence of continued fraction.\n");
	long long number;
	number = checkNumber("");
	long long divider = 1, firstElement, substract, element = 1;
	firstElement = (long long)sqrt(number);
	substract = firstElement;
	printf("[%lld", firstElement);
	int period = 0;
	do
	{
		divider = (number - substract * substract) / divider;
		element = (firstElement + substract) / divider;
		substract = element * divider - substract;
		period += 1;
		printf(", %lld", element);
	} while (divider != 1);
	printf("] The period equals to %d", period);
	return 0;
}
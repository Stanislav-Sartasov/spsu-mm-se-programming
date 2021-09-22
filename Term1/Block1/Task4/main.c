#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <math.h>

long ContinuedFraction(long num, long den, int period) // the main algorithm for finding a continued fraction
{
	if (den == 1)
	{
		printf("%ld]\n", num);
		printf("The period of this continued fraction is %d\n", period);
	}
	else if (den == 0)
	{
		printf("\b\b]\n");
		printf("The period of this continued fraction is %d\n", period - 1);
	}
	else
	{
		printf("%ld; ", num / den);
		long saved = den;
		den = num - (num / den) * den;
		num = saved;
		period++;
		return ContinuedFraction(num, den, period);
	}
}

long CheckingNegative(long numerator, long denominator, int period) // reducing the denominator and numerator to the standard form
{
	if ((numerator < 0) ^ (denominator < 0))
	{
		printf("%d; ", numerator / denominator - 1);
		long saved = denominator;
		denominator = abs((numerator / saved - 1) * abs(saved)) - abs(numerator);
		numerator = abs(saved);
		period++;
		return ContinuedFraction(numerator, denominator, period);
	}
	else
		return ContinuedFraction(abs(numerator), abs(denominator), period);
}

int main()
{
	long numerator, denominator;
	int flag, period = 0;
	printf("Enter \"1\" if you want to enter a non-fractional number and \"2\" if you want to enter a fractional number: ");
	scanf("%d", &flag);

	if (flag == 1)
	{
		float number;
		denominator = 1;
		printf("Enter a number: ");
		scanf("%f", &number);

		for (int i = 10; (number - (long)number) != 0; i *= 10, number *= 10)
			denominator = i;
		numerator = (long)number;

		printf("[");
		CheckingNegative(numerator, denominator, period);
	}
	else if (flag == 2)
	{
		printf("Enter the numerator of the fraction: ");
		scanf("%ld", &numerator);
		printf("Enter the denominator of the fraction: ");
		scanf("%ld", &denominator);

		if (denominator == 0)
		{
			printf("Zero can't be the denominator, please repeat the input.\n");
			return 0;
		}

		printf("[");
		CheckingNegative(numerator, denominator, period);
	}
	else
		printf("Wrong input, please try again\n");

	return 0;
}
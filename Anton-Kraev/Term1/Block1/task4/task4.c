#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>

int is_rational(long long number)
{
	return ((long long)sqrtl(number) == sqrtl(number));
}

void continued_fraction(long long number)
{
	int period = 0;
	long long first_element = (long long)sqrtl(number);
	long long divider = 1, subtrahend = first_element, element;
	printf("\n[%lld; ", first_element);
	
	do
	{
		divider = (number - subtrahend * subtrahend) / divider;
		element = (long long)((first_element + subtrahend) / divider);
		subtrahend = element * divider - subtrahend;
		if (period == 0)
			printf("%lld", element);
		else
			printf(", %lld", element);
		period++;
	} 
	while (divider != 1);
	
	printf("]\nPeriod = %d\n", period);
}

int main()
{
	printf("This program outputs the continued fraction "
		"of the square root of the entered number and its period.\n");
	char end;
	long long number;
	printf("Enter a number that is not the square of an integer:\n");
	while (scanf("%lld%c", &number, &end) != 2 || end != '\n' || is_rational(number) || number <= 0)
	{
		while (end != '\n')
			scanf("%c", &end);
		end = '\0';
		printf("Enter an integer that is not a square of another integer!\n");
	}
	continued_fraction(number);
	return 0;
}
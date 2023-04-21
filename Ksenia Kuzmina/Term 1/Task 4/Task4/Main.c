#include <stdio.h>
#include <math.h>

double correct_input()
{
	double input;
	char clean = 0;
	while ((clean != '\n') && (clean != EOF))
		clean = getchar();
	scanf_s("%lf", &input);
	return input;
}

int is_square(int n)
{
	int i;
	int flag = 0;
	for (i = 1; i * i <= n; i++)
	{
		if (i * i == n)
			flag = 1;
	}
	if (flag == 1)
		return 1;
	else
		return 0;
}

int main()
{
	int den, count, rem, res, a0;  //denominator, counter, remainder, result
	double input;
	printf("This program decomposes the root of a number into a chain fraction and outputs its period\n");
	printf("If your number is not an integer, the program will take the integer part from it\n");
	printf("Enter an integer that is not a square of another number:\n");
	scanf_s("%lf", &input);
	int number = (int)floor(input);
	while ((input < 0) || (is_square(number)))
	{
		if (input < 0)
		{
			printf("You cannot input negative numbers or letters. Please try again.\n");
			input = correct_input();
			number = (int)floor(input);
		}
		else
		{
			printf("You cannot input the numbers that are a square of another numbers.\n");
			input = correct_input();
			number = (int)floor(input);
		}
	}
	printf("The chain fraction of this root: \n[%d", (int)floor(sqrt(number)));
	rem = 0;
	den = 1;
	count = 0;
	a0 = (int)floor(sqrt(number));
	res = a0;
	do
	{
		rem = res * den - rem;
		den = (number - rem * rem) / den;
		res = (a0 + rem) / den;
		printf("; %d", res);
		count += 1;
	} 
	while (den != 1);
	printf("]\nThe length of the period is %d", count);
	return 0;
}
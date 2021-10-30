//Homework 1.4
//Continued fractions

#include <stdio.h>
#include <math.h>
#include <stdbool.h>

void get_number(long long* number_ptr);
int check_the_entered_number(long long* number_ptr, int correct_input);
int computing_sequence(long long* number_ptr);
unsigned long computing(long long* number_ptr);

int main()
{
	long long number;

	printf("\nEnter a natural number, then This program will show the period i \n");
	printf("of the Continued fraction and its sequence [a0; a1 ... ai].\n");
	printf("The number entered must not be another square of the integer.\n");

	get_number(&number);

	printf("period of number %lld is: %d\n\n", number, computing_sequence(&number));

	return 0;
}

void get_number(long long* number_ptr)
{
	int correct_input;

	do
	{
		printf("\nPlease enter a natural number that is not a square of an integer:\n");

		correct_input = scanf_s("%lld", number_ptr);

		while (getchar() != '\n');

	} while (check_the_entered_number(number_ptr, correct_input));
}

int check_the_entered_number(long long* number_ptr, int correct_input)
{
	if (!(correct_input == 1 && *number_ptr > 0 && (pow(floorl(sqrtl(*number_ptr)), 2) != *number_ptr)))
	{
		return true;
	}
	else
	{
		return false;
	}
}

int computing_sequence(long long* number_ptr)
{
	unsigned long f = computing(number_ptr);
	unsigned long g = computing(number_ptr);
	unsigned long m = 0, n = 1;
	int i = 0;

	do
	{
		m = f * n - m;
		n = (*number_ptr - pow(m, 2)) / n;
		f = (g + m) / n;


		if (i == 0)
		{
			printf("\nThe sequence is: [%lu;", m);
		}

		printf(" %lu", f);

		if (n != 1)
		{
			printf(",");
		}

		i++;
	} while (n != 1);

	printf("]\n");

	return i;
}

unsigned long computing(long long* number_ptr)
{
	return floorl(sqrtl(*number_ptr));
}
#include <stdio.h>
#include <math.h>
#include <stdbool.h>

void input(int* n)
{
	printf("Enter a natural number that is not the square of another integer whose root you "
		"want to represent as a continued fraction:\n");
	char char_after_number;
	while (true)
	{
		int input_check = scanf_s("%lld%c", n, &char_after_number);
		if (input_check == 2 && (sqrt(*n) != (int)sqrt(*n)) && (char_after_number == '\n' || char_after_number == ' '))
		{
			break;
		}
		else
		{
			printf("Your input is not a natural number or is the square of another natural number. Please re-enter:\n");
			fseek(stdin, 0, 0);
		}
	}
}

void print_answer(long long n)
{
	printf("Continuous sequence of fractions for the root of the entered number: [");
	long long a = 1, b = (long long)sqrt(n); /* A continued fraction of the square root of a natural number
								 is constructed by constantly separating
								 an integer from a fraction of the form a / (sqrt(n) - b),
								 and the next fraction can be calculated from the values of a and b
								 of the previous fraction*/
	printf("%lld", b);
	long long i = 0, period_a = a, period_b = b; /* keep the original values of a and b to stop the loop
									when the period is found*/
	while (true)
	{
		if (i != 0 && a == period_a && b == period_b) break;
		long long a_i = (long long)((sqrt(n) + b) / ((n - b * b) / a));
		printf(", %lld", a_i);
		long long next_a = (n - b * b) / a, next_b = ((n - b * b) / a) * (long long)((sqrt(n) + b) / ((n - b * b) / a)) - b; /*
		formulas for calculating the following values of a and b
		based on the results of the previous one (preliminary calculation)	*/
		a = next_a, b = next_b;
		i++;
	}
	printf("]\n");
	printf("The period of this continued fraction is %lld\n", i);
}

int main()
{
	long long n;
	printf("This program for a natural number you entered that is not the square of an integer, "
		"finds the sequence and period of the continued fraction of the square root of that number.\n");
	input(&n);
	print_answer(n);
	return 0;
}

#include <stdio.h>
#include <stdbool.h>
#include <limits.h>
#include <math.h>

long long long_int_input(char message[])
{
	bool check_correction = true;
	long long number = 0;
	int sign = 1, end = 0, check_overflow = 0;
	do
	{
		bool first_mark_input = true;
		char symbol_input = '\0';
		sign = 1, number = 0, end = 0, check_overflow = 0;
		check_correction = true;
		printf_s("%s", message);
		while (check_correction && check_overflow < 19)
		{
			end = scanf_s("%c", &symbol_input);
			if (first_mark_input)
			{
				first_mark_input = false;
				if (symbol_input == '-')
				{
					sign = -1;
					first_mark_input = true;
					continue;
				}
				else if (symbol_input >= '0' && symbol_input <= '9')
				{
					number = symbol_input - '0';
					check_overflow++;
					continue;
				}
				check_correction = false;
			}
			else
			{
				if (symbol_input >= '0' && symbol_input <= '9')
				{
					if (check_overflow == 17 && LLONG_MAX / 100 <= number)
					{
						check_overflow += 100;	// raising a variable due to overflow
						break;
					}
					number = number * 10 + symbol_input - '0';
					check_overflow++;
					continue;
				}
				else if (end == EOF || symbol_input == '\n')
				{
					break;
				}
				check_correction = false;
			}
		}
		if (!check_correction)
		{
			printf("incorrect input, there are string or float type, try again.\n");
			if (end == EOF || symbol_input == '\n')		// if the user immediately pressed Enter
			{
				continue;
			}
			symbol_input = '\0';
			while (symbol_input != '\n')	// clearing the buffer of unnecessary user input characters
			{
				scanf_s("%c", &symbol_input);
			}
		}
		else if (check_overflow > 18)
		{
			if (sign == 1)
			{
				printf("overflow, there is more than llong_max, try again.\n");
			}
			else
			{
				printf("overflow, there is less than llong_min, try again.\n");
			}
			symbol_input = '\0';
			while (symbol_input != '\n')	// clearing the buffer of unnecessary user input characters
			{
				scanf_s("%c", &symbol_input);
			}
		}
	} while (!check_correction || check_overflow > 18);
	return number * sign;
}

long long input_not_square()
{
	long long a = 0;
	do
	{
		a = long_int_input("Enter a natural number that is not the square of another number: ");
		if ((trunc(sqrt(a)) * trunc(sqrt(a))) == a)
		{
			printf("That is the square of %f .\n", sqrt(a));
		}
		if (a <= 0)
		{
			printf("That is not a natural number. \n");
		}
	} while (((trunc(sqrt(a)) * trunc(sqrt(a))) == a) || a <= 0);
	return a;
}

void chain_fraction_square_root(long long number)
{
	int period = 1;
	long long first_term = floorl(sqrt(number));
	printf("The sequence of a square root: [%lld", first_term);
	for (long long denominator = 1, terms = first_term, k = 0; denominator != 1 || period == 1; period++)
	{
		k = terms * denominator - k;
		denominator = (number - k * k) / denominator;
		terms = (first_term + k) / denominator;
		printf("; %lld", terms);
	}
	printf("] \n");
	printf("The period: %d", period);
}

int main()
{
	printf("This program displays the period and the sequence of a square root for the entered number that is not the square of an integer. \n");
	long long a = 0;
	a = input_not_square();
	chain_fraction_square_root(a);
	return 0;
}
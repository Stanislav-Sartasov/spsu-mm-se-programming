#include <stdio.h>
#include <stdbool.h>
#include <limits.h>
#include <math.h>

int int_input(char message[])
{
	bool check_correction = true;
	int sign = 1, number = 0, end = 0, check_overflow = 0;
	do
	{
		bool first_mark_input = true;
		char symbol_input = '\0';
		sign = 1, number = 0, end = 0, check_overflow = 0;
		check_correction = true;
		printf_s("%s", message);
		while (check_correction && check_overflow < 11)
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
					if (check_overflow == 9 && INT_MAX / 10 <= number)
					{
						check_overflow += 10;	// raising a variable due to overflow
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
		else if (check_overflow > 10)
		{
			if (sign == 1)
			{
				printf("overflow, there is more than int_max, try again.\n");
			}
			else
			{
				printf("overflow, there is less than int_min, try again.\n");
			}
			symbol_input = '\0';
			while (symbol_input != '\n')	// clearing the buffer of unnecessary user input characters
			{
				scanf_s("%c", &symbol_input);
			}
		}
	} while (!check_correction || check_overflow > 10);
	return number * sign;
}

int input_not_square()
{
	int a;
	do
	{
		a = int_input("Enter a natural number that is not the square of another number: ");
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

void chainFractionSquareRoot(int a)
{
	int period = 1;
	int a0 = trunc(sqrt(a));
	printf("The sequence of a square root: [%d", a0);
	double x = sqrt(a) - a0; // auxiliary variable for calculating a chain of continued fractions
	int ai = trunc(1 / x); // each subsequent member of the sequence
	printf("; %d", ai);
	while (ai != (a0 * 2)) // by Evariste Galois' theorem, the last term of the periodic sequence of the square root is the doubled first term of the chain of fractions of the square root.
	{
		period++; // And also by Galois theorem the first term of the periodic sequence of the square root is the second term of the fractional chain of the square root.
		x = (1 / x) - ai;
		ai = trunc(1 / x);
		printf("; %d", ai);
	}
	printf("] \n");
	printf("The period: %d", period);
}

int main()
{
	printf("This program displays the period and the sequence of a square root for the entered number that is not the square of an integer. \n");
	int a = input_not_square();
	chainFractionSquareRoot(a);
	return 0;
}
#include <stdio.h>
#include <stdbool.h>
#include <limits.h>

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

bool isPythagoreanTriples(int a, int b, int c)
{
	return (a * a + b * b == c * c) || (a * a + c * c == b * b) || (b * b + c * c == a * a);
}

bool isPrimeCouple(int first, int second)
{
	while (first != 0 && second != 0)
	{
		if (first > second)
		{
			first %= second;
		}
		else
		{
			second %= first;
		}
	}
	return (first + second) == 1;
}

void natural_number_input(int* number, char message[])
{
	*number = 0;
	do
	{
		*number = int_input(message);
	} while (*number <= 0);
}

int main()
{
	int x, y, z;
	printf("This program determines if three numbers are Pythagorean triples and, if so, checks their mutual simplicity.\n");
	natural_number_input(&x, "Please enter the first natural number: ");
	natural_number_input(&y, "Please enter the second natural number: ");
	natural_number_input(&z, "Please enter a third natural number: ");
	printf("%d, %d, %d ", x, y, z);
	if (isPythagoreanTriples(x, y, z))
	{
		if (isPrimeCouple(x, y) && isPrimeCouple(x, z) && isPrimeCouple(z, y))
		{
			printf("are prime Pythagorean triples.");
		}
		else
		{
			printf("are Pythagorean triples");
		}
	}
	else
	{
		printf("are not Pythagorean triples ");
	}
	return 0;
}
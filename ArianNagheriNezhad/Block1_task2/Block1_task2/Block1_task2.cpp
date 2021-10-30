//Homework 1.2
//Pythagorean triplets

#include <stdio.h>
#include <stdbool.h>

void get_numbers(int* number_one_ptr, int* number_two_ptr, int* number_three_ptr, char* after_number_ptr);
void print_pythagorean_triple(int* number_one_ptr, int* number_two_ptr, int* number_three_ptr, int* check_number_ptr);
int computing_pythagorean_triple(int* number_one_ptr, int* number_two_ptr, int* number_three_ptr);
void print_prime_pythagorean(int number_one, int number_two, int number_three, int check_number);
int is_prime_pythagorean_one(int number_one, int number_two, int number_three);
int is_prime_pythagorean_two(int x, int y);

int main()
{
	int number_one, number_two, number_three, check_number;
	char after_number;

	printf("\nPlease enter three natural numbers, then this program will ");
	printf("print whether they are Pythagorean triple or not, and\n");
	printf("if so, whether they are also prime Pythagorean triple or not.");
	printf("The order in which numbers are entered is arbitrary.\n\n");

	get_numbers(&number_one, &number_two, &number_three, &after_number);
	print_pythagorean_triple(&number_one, &number_two, &number_three, &check_number);
	print_prime_pythagorean(number_one, number_two, number_three, check_number);
	is_prime_pythagorean_one(number_one, number_two, number_three);

	return 0;
}

void get_numbers(int* number_one_ptr, int* number_two_ptr, int* number_three_ptr, char* after_number_ptr)
{
	int correct_input;

	printf("Please enter three natural numbers:\n");

	while (true)
	{
		correct_input = scanf_s("%d%d%d%c", number_one_ptr, number_two_ptr, number_three_ptr, after_number_ptr);

		if (correct_input == 4 && *number_one_ptr > 0 && *number_two_ptr > 0
			&& *number_three_ptr > 0 && *after_number_ptr == '\n')
		{
			break;
		}
		else
		{
			while (*after_number_ptr != '\n')
			{
				scanf_s("%c", after_number_ptr);
			}

			*after_number_ptr = '\0';

			printf("Error! The number(s) entered are incorrect. Please enter ");
			printf("three natural numbers.\n");
		}
	}
}

void print_pythagorean_triple(int* number_one_ptr, int* number_two_ptr, int* number_three_ptr, int* check_number_ptr)
{
	if (computing_pythagorean_triple(number_one_ptr, number_two_ptr, number_three_ptr))
	{
		printf("This is a pythagorean triple.\n");
		*check_number_ptr = true;
	}
	else
	{
		printf("This is Not a pythagorean triple.\n");
		*check_number_ptr = false;
	}
}

int computing_pythagorean_triple(int* number_one_ptr, int* number_two_ptr, int* number_three_ptr)
{
	if (*number_one_ptr >= *number_two_ptr && *number_one_ptr >= *number_three_ptr && (*number_two_ptr *
		*number_two_ptr + *number_three_ptr * *number_three_ptr == *number_one_ptr * *number_one_ptr))
	{
		return true;
	}
	else if (*number_two_ptr >= *number_one_ptr && *number_two_ptr >= *number_three_ptr && (*number_one_ptr *
		*number_one_ptr + *number_three_ptr * *number_three_ptr == *number_two_ptr * *number_two_ptr))
	{
		return true;
	}
	else if (*number_three_ptr >= *number_two_ptr && *number_three_ptr >= *number_one_ptr && (*number_two_ptr *
		*number_two_ptr + *number_one_ptr * *number_one_ptr == *number_three_ptr * *number_three_ptr))
	{
		return true;
	}
	else
	{
		return false;
	}
}

void print_prime_pythagorean(int number_one, int number_two, int number_three, int check_number)
{
	if (is_prime_pythagorean_one(number_one, number_two, number_three) && check_number == true)
	{
		printf("This is a prime (primitive) pythagorean triple.\n");
	}
	else
	{
		printf("This is Not a prime (primitive) pythagorean triple.\n");
	}
}

int is_prime_pythagorean_one(int number_one, int number_two, int number_three)
{
	if (is_prime_pythagorean_two(number_one, number_two) == 1 &&
		is_prime_pythagorean_two(number_one, number_three) == 1 &&
		is_prime_pythagorean_two(number_two, number_three) == 1)
	{
		return true;
	}
	else
	{
		return false;
	}
}

int is_prime_pythagorean_two(int x, int y)
{
	int z = 0, t = 0;

	if (x > y || y > x)
	{
		if (y > x)
		{
			t = y;
			y = x;
			x = t;
		}
		while (y != 0)
		{
			z = x % y;
			x = y;
			y = z;
		}
	}
	return x;
}
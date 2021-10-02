#include <stdio.h>
#include <stdbool.h>
#include <math.h>

void user_input(int* a, int* b, int* c)
{
	printf("Please enter three natural numbers separated by spaces\n");
	while (true)
	{
		char char_after_numbers;
		int status_of_read = scanf_s("%d %d %d%c", a, b, c, &char_after_numbers);
		if (status_of_read == 4 && *a > 0 && *b > 0 && *c > 0 && (char_after_numbers == ' ' || char_after_numbers == '\n'))
		{
			break;
		}
		else
		{
			printf("At least one of the parameters you entered is not a natural number. Please re-enter\n");
			fseek(stdin, 0, 0);
		}
	}
}

int gcd(int a, int b)
{
	while (b)
	{
		int tmp = a % b;
		a = b;
		b = tmp;
	}
	return a;
}

void sort_triple(int* a, int* b, int* c)
{
	int tmp;
	if (*c > *b)
	{
		tmp = *b;
		*b = *c;
		*c = tmp;
	}
	if (*b > *a)
	{
		tmp = *a;
		*a = *b;
		*b = tmp;
	}
	if (*c > *b)
	{
		tmp = *b;
		*b = *c;
		*c = tmp;
	}
}

void print_answer(int a, int b, int c)
{
	sort_triple(&a, &b, &c);
	if (a * a != b * b + c * c)
	{
		printf("The entered three numbers are not a Pythagorean triple.\n");
	}
	else if (gcd(a, b) == 1 || gcd(b, c) == 1 || gcd(a, c) == 1)
	{
		printf("The three numbers entered are the primitive Pythagorean triple.\n");
	}
	else
	{
		printf("The three numbers entered are the Pythagorean triplet.\n");
	}
}

int main()
{
	int a, b, c;
	printf("This program by the entered three natural numbers determines "
		"whether the given triple of numbers is Pythagorean or a primitive Pythagorean\n");
	user_input(&a, &b, &c);
	print_answer(a, b, c);
	return 0;
}
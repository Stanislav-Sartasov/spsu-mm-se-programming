#include <stdio.h>
#include <math.h>

long long gcd(long long a, long long b)  // Euclid's algorithm
{
	while (a != 0 && b != 0)
	{
		if (a >= b)
		{
			a = a % b;
		}
		else
		{
			b = b % a;
		}
	}
	if (a)
	{
		return a;
	}
	else
	{
		return b;
	}
}

int is_pythagorean_triple(long long a, long long b, long long c)
{
	if (a >= b && a >= c && (b * b + c * c == a * a))  // a is the hypotenuse
	{
		return 1;
	}
	if (b >= a && b >= c && (a * a + c * c == b * b))  // b is the hypotenuse
	{
		return 1;
	}
	if (c >= b && c >= a && (b * b + a * a == c * c))  // c is the hypotenuse
	{
		return 1;
	}
	else
	{
		return 0;
	}
}

int is_prime_triple(long long a, long long b, long long c)
{
	if (gcd(a, b) == 1 && gcd(b, c) == 1 && gcd(a, c) == 1)
	{
		return 1;
	}
	else {
		return 0;
	}
}

void clean_input()  // Cleaning the input area before using scanf_s function
{
	char s = 0;
	while (s != '\n' && s != EOF)
	{
		s = getchar();
	}
}

int is_correct_input(long long* number)  // Returns 1 only if the input is correct
{
	return (scanf_s("%lld", number) && 1 <= *number && *number <= 1000000000);
}

long long get_input_number()
{
	long long number = 0;
	while (!(is_correct_input(&number) && getchar() == '\n'))
	{
		clean_input();
		printf("Your input is incorrect! Please, try again:\n");
	}
	return number;
}

int main()
{
	printf("This program takes three natural numbers and determines whether they are a pythagorean triple,\n");
	printf("and if so, whether they are also a prime pythagorean triple.\n");
	printf("Restrictions on the entered numbers: 1 <= n <= 1000000000.\n");
	printf("\n");

	printf("Please, input the first natural number:\n");
	long long a = get_input_number();
	printf("Please, input the second natural number:\n");
	long long b = get_input_number();
	printf("Please, input the third natural number:\n");
	long long c = get_input_number();

	if (is_pythagorean_triple(a, b, c))
	{
		printf("This is the pythagorean triple! ");
		if (is_prime_triple(a, b, c))
		{
			printf("And it is prime.\n");
		}
		else
		{
			printf("But it is not prime.\n");
		}
	}
	else
	{
		printf("This is not the pythagorean triple.\n");
	}
}
#include <stdio.h>

int get_natural_number()
{
	printf(">>> ");
	int num;
	char end;
	int read_result = scanf("%d%c", &num, &end);
	if ((read_result == 2) && (end == '\n'))
	{
		if (num > 0)
		{
			return num;
		}

		printf("Number must be natural (positive integer)\n");
	}
	else
	{
		printf("Please enter correct natural number\n");
	}

	while (end != '\n')
	{
		scanf("%c", &end);
	}

	return get_natural_number();
}


int gcd(int x1, int x2)
{
	while (x1 != x2)
	{
		if (x1 > x2)
		{
			x1 = x1 - x2;
		}
		else
		{
			x2 = x2 - x1;
		}
	}
	return x1;
}


int main()
{
	printf("This programm checks if 3 natural numbers are a Pythagorean triple and if they are coprime\n");
	printf("Enter 3 natural numbers (each one on next line)\n");
	int a = get_natural_number();
	int b = get_natural_number();
	int c = get_natural_number();
	if ((a * a + b * b == c * c) || (b * b + c * c == a * a) || (a * a + c * c == b * b))
	{
		if ((gcd(a, b) == 1) || (gcd(b, c) == 1) || (gcd(a, c) == 1))
		{
			printf("This is a primitive Pythagorean triple");
		}
		else
		{
			printf("This is a Pythagorean triple");

		}
	}
	else
	{
		printf("This is not a Pythagorean triple");
	}
	return 0;

}

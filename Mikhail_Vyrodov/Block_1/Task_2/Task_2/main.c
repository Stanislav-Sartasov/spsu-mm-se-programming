#include <stdio.h>
#include <stdlib.h>

void clear_input()
{
	char step;
	step = 0;
	while (step != '\n' && step != EOF)
	{
		step = getchar();
	}
}

void get_numbers(long long* x, long long* y, long long* z)
{
	printf("Enter 3 natural numbers - x, y, z.\n");
	while (1)
	{
		if (scanf_s("%lld %lld %lld", x, y, z) == 3)
		{
			if (*x <= 0 || *y <= 0 || *z <= 0)
			{
				printf("Some numbers are less than one. ");
			}
			else if (getchar() == '\n')
			{
				break;
			}
		}
		printf("Input was incorrect, please try again:\n");
		clear_input();
	}
}

int evklid_alghoritm(long long a, long long b)
{
	long long c, d, memorized_number;
	c = max(a, b);
	d = min(a, b);
	while (c != 0 || d != 0)
	{
		if (c % d == 0)
		{
			if (d == 1)
			{
				return 1;
			}
			else
			{
				return 0;
			}
		}
		else
		{
			if (c - d >= d)
			{
				c = c - d;
			}
			else
			{
				memorized_number = d;
				d = c - d;
				c = memorized_number;
			}
		}
	}
}

int main()
{
	printf("This program determines whether three numbers are a Pythagorean triple and checks if this Pythagorean triple is prime.\n");
	long long x, y, z, memorized_number;
	get_numbers(&x, &y, &z);
	if (x >= y && x >= z)
	{
		memorized_number = z;
		z = x;
		x = memorized_number;
	}
	else if (y >= x && y >= z)
	{
		memorized_number = z;
		z = y;
		y = memorized_number;
	}
	if (x * x + y * y == z * z)
	{
		printf("This triple is pythagorean\n");
		if (evklid_alghoritm(x, y))
		{
			printf("This pythagorean triple is prime\n");
		}
	}
	else
	{
		printf("This triple is not pythagorean\n");
	}
	return 0;
}
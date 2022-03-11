#include <stdio.h>
#include "inputAndTools.h"


int gcd(int a, int b)
{
	while (a != b)
	{
		if (a > b)
		{
			a = a - b;
		}
		else
		{
			b = b - a;
		}
	}
	return a;
}

int all_numbers_is_prime(int* arr)
{
	for (int i = 0; i < 2; i++)
	{
		for (int j = i + 1; j < 3; j++)
		{
			if (gcd(arr[i], arr[j]) != 1)
			{
				return 0;
			}
		}
	}
	return 1;
}

int main()
{
	printf("Defines Pythagorean triple\n");

	int arr[3];

	int i = 0;
	do
	{
		printf("Input positive number number %d\n", i + 1);
		arr[i] = get_int();
		if (arr[i] > 0)
		{
			i++;
		}
		else
		{
			printf("Input error. Try again\n\n");
		}
	} while (i < 3);

	sort_of_array(arr, 3);

	if (arr[0] * arr[0] + arr[1] * arr[1] == arr[2] * arr[2])
	{
		if (all_numbers_is_prime(arr))
		{
			printf("This is a mutually prime Pythagorean triple\n");
		}
		else
		{
			printf("This isn't a mutually prime Pythagorean triple\n");
		}
	}
	else
	{
		printf("This isn't a Pythagorean triple\n");
	}

	return 0;
}
#include <stdio.h>
#include <math.h>


int is_prime(int num)
{
	for (int d = 2; d <= (int)sqrt(num) + 1; d++)
	{
		if (num % d == 0 || num == 1)
		{
			return 0;
		}
	}
	return 1;
}

int main()
{
	printf("This program print prime Mersenne numbers in range[1, 2 ^ 31 - 1]:\n");
	int n = 1;
	for (int i = 1; i <= 31; i++)
	{
		n *= 2;
		if (is_prime(n - 1))
		{
			printf("%d\n", n - 1);
		}
	}
	return 0;
}
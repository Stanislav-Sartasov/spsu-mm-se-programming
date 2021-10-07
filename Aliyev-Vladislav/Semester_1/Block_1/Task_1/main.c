#include <stdio.h>

int is_prime(unsigned int n)
{
	for (int i = 2; i * i < n; ++i)
	{
		if (n % i == 0)
		{
			return 0;
		}
	}
	return 1;
}

int main()
{
	printf("This program outputs Mersenne primes in range [1; 2 ^ 31 - 1]\n");
	int number = 2;
	for (int i = 0; i < 31; ++i)
	{
		number = number * 2;
		if (is_prime(number - 1))
		{
			printf("%d\n", number - 1);
		}
	}
}
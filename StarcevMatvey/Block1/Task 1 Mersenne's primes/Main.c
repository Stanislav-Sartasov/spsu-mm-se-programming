#include <stdio.h>
#include <math.h>

int is_prime(int number)
{
	for (int i = 3; i < floor(sqrt(number)) + 1; i = i + 2)
	{
		if (number % i == 0)
		{
			return 0;
		}
	}
	return 1;
}

int main()
{
	printf("Calculates Mersenne's primes\n");
	for (int i = 1; i < 31; i++)
	{
		int number = (2 << i) - 1;
		if (is_prime(number))
		{
			printf("%d ", number);
		}
	}
	return 0;
}
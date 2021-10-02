#include <stdio.h>
#include <math.h>

int is_prime(int n)
{
	if (n == 0 || n == 1)
	{
		return 0;
	}
	int square_root = sqrt(n);
	for (int d = 2; d <= square_root; d++)
	{
		if (n % d == 0)
		{
			return 0;
		}
	}
	return 1;
}

// Numbers of the form 2^k - 1, 1 <= k <= 31
int main()
{
	printf("This program prints Mersenne primes in the range [1; 2^31 - 1]:\n");

	int number;
	for (int k = 1; k <= 31; k++)
	{
		number = pow(2, k) - 1;
		if (is_prime(number))
		{
			printf("%d\n", number);
		}
	}
}
#include <stdio.h>
#include <math.h>
#include <stdbool.h>

bool is_prime(int n) // prime check algorithm
{
	bool result = true;
	for (int divisor = 2; divisor <= sqrt(n); divisor++)
	{
		if (n % divisor == 0)
		{
			result = false;
			break;
		}
	}
	return result;
}

int main()
{
	int mersenne_number = 2 * 2 - 1; /* first mersenne number
	is 2 - 1 = 1 not prime */
	printf("The program calculates mersenne primes that do not exceed 2 ^ 31 - 1\n");
	for (int i = 1; i <= 30; i++)
	{
		if (is_prime(mersenne_number))
		{
			printf("%d ", mersenne_number);
		}
		mersenne_number = 2 * mersenne_number + 1; /* getting
		the next mersenne number */
	}
	return 0;
}
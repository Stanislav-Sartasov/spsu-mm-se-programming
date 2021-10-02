#include <stdio.h>
#include <stdbool.h>
#include <math.h>

bool isPrime(int* number);

int main()
{
	printf("This program prints Mersenne prime numbers (2^n-1) in the range [1; 2^31 - 1]\n\n");
	printf("Mersenne primes in the range [1; 2^31 - 1]:\n\n");

	for (int i = 1; i < 32; i++)
	{
		int mersenneNumber = (pow(2, i) - 1);

		if (isPrime(&mersenneNumber))
		{
			printf("%d\n", mersenneNumber);
		}
	}
	return 0;
}

bool isPrime(int* number)
{
	double squareRoot = pow(*number, 0.5);
	bool result = true;

	if (*number == 1)
	{
		return false;
	}

	for (int i = 2; i <= squareRoot; i++)
	{
		if (*number % i == 0)
		{
			result = false;
			break;
		}
	}

	return result;
}
#include <stdio.h>
#include <stdbool.h>
#include <math.h>

bool isPrime(int* number)
{
	if (*number == 1)
	{
		return false;
	}
	int squareRoot = pow(*number, 0.5) + 1;
	for (int i = 2; i <= squareRoot; i++)
	{
		if (*number % i == 0)
		{
			return false;
		}
	}
	return true;
}

int main()
{
	printf("This program prints Mersenne primes numbers on the segment [1; 2^31 - 1]\n");
	for (int i = 1; i <= 31; i++)
	{
		int mersenne_number = pow(2, i) - 1;
		if (isPrime(&mersenne_number))
		{
			printf("Found a new Mersenne prime number: %d \n", mersenne_number);
		}
	}
	return 0;
}
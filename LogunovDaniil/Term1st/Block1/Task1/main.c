#include <stdio.h>
#include <math.h>

int isPrime(int n)
{
	if (n <= 1)
	{
		return 0;
	}
	int root = (int)sqrt(n);
	for (int i = 2; i <= root; i++)
	{
		if ((n % i) == 0)
		{
			return 0;
		}
	}
	return 1;
}

int main()
{
	int powerOf2 = 1;
	for (int i = 0; i < 32; i++)
	{
		if (isPrime(powerOf2 - 1))
		{
			printf("%d\n", powerOf2 - 1);
		}
		powerOf2 *= 2;
	}

	return 0;
}
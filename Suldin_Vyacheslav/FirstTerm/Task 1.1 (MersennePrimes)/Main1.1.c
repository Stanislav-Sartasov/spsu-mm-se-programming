#include <stdio.h>
#include <math.h>

int primeTest(int number);

int main()
{
	int N, t;

	printf("Mersenne primes on [1,..2^31-1] :\n");
	t = 2;
	for (N = 1; N <= 31; N = N + 2)
	{
		if (primeTest(N) && primeTest(t - 1))
		{
			printf("N=%d, 2^N-1 = %d\n", N, t - 1);
		}
		if (t == 2 && primeTest(t * t - 1))
		{
			printf("N=%d, 2^N-1 = %d\n", N, t * t - 1);
		}
		t = t * 4;
	}
	return 0;
}

int primeTest(int number)
{
	int j;
	for (j = 3; j <= pow(number, 0.5); j = j + 2)
	{
		if (number % j == 0)
		{
			return 0;
		}
	}
	if (number == 1)
	{
		return 0;
	}
	return 1;
}
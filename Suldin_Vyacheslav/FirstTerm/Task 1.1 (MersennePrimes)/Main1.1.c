#include <stdio.h>
#include <math.h>

int primeTest(int number);

int main()
{
	int n, t;

	printf("Mersenne primes on [1,..2^31-1] :\n");
	t = 2;
	for (n = 1; n <= 31; n += 2)
	{
		if (primeTest(n) && primeTest(t - 1))
		{
			printf("N=%d, 2^N-1 = %d\n", n, t - 1);
		}
		if (t == 2 && primeTest(t * t - 1))
		{
			printf("N=%d, 2^N-1 = %d\n", n, t * t - 1);
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
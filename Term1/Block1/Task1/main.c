#include <stdio.h>
#include <math.h>

_Bool isPrime(unsigned long long N)
{
	if (N <= 1)
		return 0;

	for (int j = 2; j <= sqrt(N); j++)
		if (N % j == 0) return 0;

	return 1;
}

int main()
{
	printf("Output all prime Mersenne numbers from 1 to 2^31 - 1:\n");

	for (unsigned long long i = 2; i <= pow(2, 31); i *= 2)
		if (isPrime(i - 1))
			printf("%llu\n", i - 1);

	return 0;
}

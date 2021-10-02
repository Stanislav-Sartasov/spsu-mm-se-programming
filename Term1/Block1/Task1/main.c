#include <stdio.h>
#include <math.h>

_Bool is_prime(unsigned long long n)
{
	if (n <= 1)
		return 0;

	for (int j = 2; j <= sqrt(n); j++)
		if (n % j == 0) return 0;

	return 1;
}

int main()
{
	printf("The program outputs all prime Mersenne numbers from 1 to 2^31 - 1:\n");

	for (unsigned long long i = 2; i <= pow(2, 31); i *= 2)
		if (is_prime(i - 1))
			printf("%llu\n", i - 1);

	return 0;
}
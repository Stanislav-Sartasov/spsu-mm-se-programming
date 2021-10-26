#include <stdio.h>
#include <math.h>
#include <stdlib.h>

long long digital_root(long long n)
{
	return (n - 1) % 9 + 1;
}

long long max(long long a, long long b)
{
	return a > b ? a : b;
}

long long mdrs(long long n, long long *memoization)
{
	if (memoization[n] != 0)
		return memoization[n];

	long long result = digital_root(n);
	for (long long d = 2; d < (long long) sqrt(n) + 1; ++d)
		if (n % d == 0)
			result = max(result, mdrs(d, memoization) + mdrs(n / d, memoization));
	memoization[n] = result;
	return result;
}

int main()
{
	printf("mdrs(n) is the maximum sum of digital roots "
		   "among all factorizations of the number n.\n");
	printf("This program outputs sum of mdrs(n) for n in range[2; 999999]:\n");
	int size = 1000000;
	long long* memoization = calloc(size, sizeof(long long));
	long long s = 0;
	for (long long i = 2; i < size; ++i)
		s += mdrs(i, memoization);
	printf("%lld", s);
	free(memoization);
	return 0;
}
#include <stdio.h>
#include <math.h>


long long memoization[1000000] = {0};

long long digital_root(long long n)
{
	return (n - 1) % 9 + 1;
}

long long max(long long a, long long b)
{
	return a > b ? a : b;
}

long long mdrs(long long n)
{
	if (memoization[n] != 0)
		return memoization[n];
	long long result = digital_root(n);
	for (long long d = 2; d < (long long) sqrt(n) + 1; ++d)
		if (n % d == 0)
			result = max(result, mdrs(d) + mdrs(n / d));
	memoization[n] = result;
	return result;
}

int main()
{
	printf("mdrs(n) is the maximum sum of digital roots "
		   "among all factorizations of the number n.\n");
	printf("This program outputs sum of mdrs(n) for n in range[2; 999999]:\n");
	long long s = 0;
	for (long long i = 2; i < 1000000; ++i)
		s += mdrs(i);
	printf("%lld", s);
	return 0;
}
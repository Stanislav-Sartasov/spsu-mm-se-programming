#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

#define INT64 __int64_t

void swap(INT64* a, INT64* b)
{
	INT64 t = *a;
	*a = *b;
	*b = t;
}

int bin_pow(INT64 a, INT64 n, INT64 m)
{
	INT64 res = 1;
	while (n)
	{
		if (n & 1)
			res *= a;
		a *= a;
		a %= m;
		res %= m;
		n >>= 1;
	}
	return res;
}

INT64 gcd(INT64 a, INT64 b)
{
	while (b)
	{
		a %= b;
		swap(&a, &b);
	}
	return a;
}

bool is_prime(INT64 n)
{
	// Discard simple cases
	if (n == 2 || n == 3)
		return true;
	if (n < 2 || n % 2 == 0)
		return false;

	// Fermat primality test

	for (int i = 0; i < 15; i++)
	{
		INT64 a = rand() % (n - 1) + 1;
		if (bin_pow(a, n - 1, n) != 1)
			return false;
	}

	// Miller-Rabin primality test

	INT64 s = 0;
	while ((n - 1) % (1 << (s + 1)) == 0)
		s++;
	INT64 t = (n - 1) / (1 << s);
	for (int i = 0; i < 15; i++)
	{
		INT64 a = (rand() % (n - 3)) + 2;
		INT64 x = bin_pow(a, t, n);
		if (x == 1 || x == n - 1)
			continue;
		bool flag = true;
		for (int j = 0; j < s - 1; j++)
		{
			x = (x * x) % n;
			if (x == 1)
				return false;
			if (x == n - 1)
			{
				flag = false;
				break;
			}
		}
		if (flag)
			return false;
	}
	return true;
}

int main()
{
	printf("Mersenne prime\n");
	for (int i = 1; i <= 31; i++)
	{
		if (is_prime(((INT64) 1 << i) - 1))
			printf("%d\n", ((INT64) 1 << i) - 1);
	}
	return 0;
}

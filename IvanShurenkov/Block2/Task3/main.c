#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

#define INT64 __int64_t
#define MIN(a, b) ((a) < (b) ? (a) : (b))
#define MAX(a, b) ((a) > (b) ? (a) : (b))
#define SQ(x) (x) * (x)

void swap(INT64 *a, INT64 *b)
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

int digital_root(int n)
{
	return (n % 9 ? n % 9 : 9);
}

int main()
{
	printf("Digital root.\nThis programm calculates sum of all MDRS for number between 2 and 999999.\n");
	const int max_length = 1000000;
	int mdrs[max_length];
	for (int i = 2; i < max_length; i++)
	{
		mdrs[i] = digital_root(i);
	}
	int ans = 0;
	for (int i = 2; i < max_length; i++)
	{
		for (int j = 2; j <= i && i * j < max_length; j++)
		{
			mdrs[i * j] = MAX(mdrs[i * j], mdrs[i] + mdrs[j]);
		}
		ans += mdrs[i];
	}
	printf("Sum of all MDRS(n) for n = [2, 999999]: %d", ans);
	return 0;
}
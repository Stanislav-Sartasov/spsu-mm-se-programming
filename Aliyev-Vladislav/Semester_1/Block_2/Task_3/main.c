#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

int factor[100];
int sum_roots = 0;
int max = 0;

int digital_root(int num)
{
	int sum_digits = 0;
	while (num > 0)
	{
		sum_digits += num % 10;
		num /= 10;
	}
	if (sum_digits >= 10)
	{
		digital_root(sum_digits);
	}
	else
		return sum_digits;
}

void max_root(int i, int j, int a)
{
	if (i < j)
	{
		sum_roots += digital_root(factor[i]);
		max_root(i + 1, j, a * factor[i]);
	}
	else
	{
		sum_roots += digital_root(factor[i]);
		if (max < sum_roots)
		{
			max = sum_roots;
		}
		sum_roots = 0;
	}
}

void checking_divisors(int k, int n, int j)
{
	if (k * k <= n)
	{
		if (n % k == 0)
		{
			factor[j] = k;
			mdrs(n / k, k, j + 1);
		}
		checking_divisors(k + 1, n, j);
	}
}

int mdrs(int n, int i, int j)
{
	factor[j] = n;
	max_root(0, j, 1);
	checking_divisors(i, n, j);
	return max;
}

int main()
{
	printf("The digital root is a decimal number obtained from the digits of the original number by adding them and repeating\nthis process over the resulting sum until a number less than 10 is obtained.\n");
	printf("Let us denote the maximum sum of digital roots among all factorizations of the number n as MDRS (n).\n");
	printf("This program calculates the sum of all MDRS (n) with n = [2; 999999]\n");
	int answer = 0;
	for (int n = 2; n < 1000000; n++)
	{
		answer += mdrs(n, 2, 0);
		max = 0;
	}
	printf("Answer: %d", answer);
	
	return 0;
}
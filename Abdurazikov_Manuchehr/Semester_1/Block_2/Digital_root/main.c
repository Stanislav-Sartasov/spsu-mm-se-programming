#include <stdio.h>
#include <stdlib.h>
#include <memory.h>

const int MAX_N = (int)(1e6);

long long amount(int* left, const int* right);

int max(int a, int b);

int digitalRoot(int digit);

void computeDP(int* dp);

int main()
{
	printf("This program prints the amount of all MDRS(n) for n in [2; 999999].\n\n");

	int* dp = (int*) malloc(sizeof(int) * MAX_N);

	memset(dp, 0, sizeof(int) * MAX_N);
	computeDP(dp);

	printf("The amount of all MDRS(n) for n in [2; 999999] is %lld.", amount(dp, dp + MAX_N));

	free(dp);
	return 0;
}

long long amount(int* left, const int* right)
{
	long long result = 0;
	while (left < right)
		result += *(left++);
	return result;
}

int max(int a, int b)
{
	return a > b ? a : b;
}

int digitalRoot(int digit)
{
	if (digit == 0)
		return 0;
	return 1 + (digit - 1) % 9;
}

void computeDP(int* dp)
{
	for (int digit = 2; digit < MAX_N; digit++)
	{
		dp[digit] = digitalRoot(digit);
		for (int divisor = 2; (long long) divisor * divisor <= digit; divisor++)
		{
			if (digit % divisor == 0)
				dp[digit] = max(dp[digit], dp[digit / divisor] + dp[divisor]);
		}
	}
}
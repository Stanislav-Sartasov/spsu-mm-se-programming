#include <stdio.h>
#include <stdlib.h>
#include <memory.h>

const int MAX_N = (int)(1e6);

long long sum(int* left, const int* right)
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

int digitalRoot(int number)
{
	if (number == 0)
		return 0;
	return 1 + (number - 1) % 9;
}

void computeDP(int* dp)
{
	for (int number = 2; number < MAX_N; number++)
	{
		dp[number] = digitalRoot(number);
		for (int divisor = 2; (long long) divisor * divisor <= number; divisor++)
		{
			if (number % divisor == 0)
				dp[number] = max(dp[number], dp[number / divisor] + dp[divisor]);
		}
	}
}

int main()
{
	printf("This program prints the sum of all MDRS(n) for n in [2; 999999].\n\n");

	int* dp = (int*) malloc(sizeof(int) * MAX_N);

	memset(dp, 0, sizeof(int) * MAX_N);
	computeDP(dp);

	printf("The sum of all MDRS(n) for n in [2; 999999] is %lld.", sum(dp, dp + MAX_N));

	free(dp);
	return 0;
}

#include <stdio.h>
#include <stdlib.h>

int main()
{
	printf("This program calculates the sum of the maximum sums "
		"of the digital roots of natural numbers from the range [2; 999999]\n");
	long long sum = 0, n = 1000001, * dp = (long long*)malloc(n * sizeof(long long));
	for (int i = 0; i < n; i++)
	{
		dp[i] = (i % 9) + 9 * (long long)(i % 9 == 0);
	}
	for (int i = 2; i < n; i++)
	{
		for (int j = 2; j <= n / i; j++)
		{
			dp[i * j] = max(dp[i * j], dp[i] + dp[j]);
		}
	}
	for (int i = 2; i < n - 1; i++)
	{
		sum += dp[i];
	}
	free(dp);
	printf("%lld", sum);
	return 0;
}
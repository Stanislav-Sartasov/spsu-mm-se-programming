#include <stdio.h>
#include <minmax.h>
#include <stdlib.h>

const int MAXN = (int)1e6;

int sum_of_digits(int n)
{
	return (n % 9 == 0 ? 9 : n % 9);
}

long long calculate_mdrs(int max_n)
{
	long long result = 0;
	int *dp = (int *) malloc(max_n * sizeof(int));
	dp[0] = dp[1] = 0;
	for (int n = 2; n < max_n; n++)
	{
		dp[n] = 0;
		int current_result = sum_of_digits(n);
		for (int i = 2; i * i <= n; i++)
		{
			if (n % i == 0)
			{
				current_result = max(current_result, sum_of_digits(i) + dp[n / i]);
				current_result = max(current_result, sum_of_digits(n / i) + dp[i]);
			}
		}
		result += (dp[n] = current_result);
	}

	free(dp);
	return result;
}

int main()
{
	printf("This program prints maximum digit root sum for n in [2; 999999]\n");
	printf("%lld", calculate_mdrs(MAXN));
	return 0;
}
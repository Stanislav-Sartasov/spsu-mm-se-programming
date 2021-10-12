#include <stdio.h>
#include <stdbool.h>

#define INT64 __int64_t
#define UINT64 __uint64_t

int main()
{
	printf("English Coins\nEnter positive integer less 130000:\n");
	int correctly_scan = 0;
	int n = -1;
	while (1 != correctly_scan || 0 >= n || n > 130000)
	{
		correctly_scan = scanf("%d", &n);
		if (1 != correctly_scan || 0 >= n)
		{
			while (fgetc(stdin) != '\n')
				;
			printf("It's not positive number\nTry again:\n");
		}
		if (n > 130000)
		{
			while (fgetc(stdin) != '\n')
				;
			printf("Number is too big\nTry again:\n");
		}
	}
	UINT64 value_coins[8] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	UINT64 dp[8 * (n + 1)];// [0, 1, 2], [3, 4, 5] [0][1] = 1, 0 * 3 + i;
	for (int i = 0; i < 8 * (n + 1); i++)
	{
		dp[i] = 0;
	}
	dp[0] = 1; // dp[i][j] = dp[i * (n + 1) + j]
	// dp[i][j] - cnt coins which we can get used value_coins <= value_coins[i]
	// j - sum of coins
	// i - index last coins
	bool overflow = false;
	for (int i = 0; i < n; i++)
	{
		for (int j = 0; j < 8; j++)
		{
			for (int q = j; q < 8; q++)
			{
				if (i + value_coins[q] <= n)
				{
					dp[q * (n + 1) + i + value_coins[q]] += dp[j * (n + 1) + i];
					if (dp[q * (n + 1) + i + value_coins[q]] >= (1 << 63) - 1)
					{
						overflow = true;
					}
				}
			}
		}
	}
	UINT64 cnt_options = 0;
	for (int i = 0; i < 8; i++)
	{
		cnt_options += dp[i * (n + 1) + n];
		if (cnt_options >= (1 << 63) - 1)
		{
			overflow = true;
		}
	}
	if (overflow)
		printf("Perhaps it was overflow");
	printf("%llu", cnt_options);
	return 0;
}

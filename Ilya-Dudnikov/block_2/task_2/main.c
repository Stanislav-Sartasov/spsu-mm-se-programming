#include <stdio.h>
#include <stdlib.h>

void calculate_answer(int sum, long long **dp)
{
	int coins[] = {1, 2, 5, 10, 20, 50, 100, 200};

	dp[0][0] = 1;
	for (int i = 0; i < 8; i++)
	{
		dp[i + 1][0] = 1;
		for (int cur_sum = 1; cur_sum <= sum; cur_sum++)
		{
			dp[i + 1][cur_sum] += dp[i][cur_sum];
			if (cur_sum >= coins[i])
				dp[i + 1][cur_sum] += dp[i + 1][cur_sum - coins[i]];
		}
	}
}

int main()
{
	printf("This program prints the amount of ways you can get a certain sum of money using coins worth 1, 2, 5, 10, 20, 50, 100 and 200 pence\n");
	int sum;
	char after = '\0';

	printf("Enter a natural number:\n");
	while (scanf("%d%c", &sum, &after) != 2 || after != '\n')
	{
		printf("Invalid input: you should enter a natural number\n");
		while (after != '\n') scanf("%c", after);
		after = '\0';
		printf("Enter a natural number: ");
	}

	long long **dp = (long long **) malloc(9 * sizeof(long long *));
	for (int i = 0; i < 9; i++)
	{
		dp[i] = (long long *) malloc((sum + 1) * sizeof(long long));
		for (int j = 0; j <= sum; j++)
			dp[i][j] = 0;
	}

	calculate_answer(sum, dp);
	printf("%lld\n", dp[8][sum]);

	for (int i = 0; i < 9; i++)
		free(dp[i]);
	free(dp);

	return 0;
}

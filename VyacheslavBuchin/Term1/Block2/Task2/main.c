#include <stdio.h>
#include <stdlib.h>

const int PENS_COUNT = 8;
const int pens[] = { 1, 2, 5, 10, 20, 50, 100, 200 };

void input(int* sum)
{
	printf("Enter the amount (the positive integer or zero):\n");
	while (1)
	{
		int correctlyScanned = scanf("%d", sum);
		if (correctlyScanned == 1 && *sum >= 0)
			break;
		else
		{
			while (fgetc(stdin) != '\n')
				;
			printf("Value you entered is not a positive integer of a zero. Please, try again:\n");
		}
	}
}

void init(long long** dp, size_t width, size_t height, long long init_value)
{
	for (int i = 0; i < height; i++)
	{
		dp[i] = (long long *) malloc(sizeof(long long) * width);
		for (int j = 0; j < width; j++)
			dp[i][j] = init_value;
	}
}

void finalize(long long** dp, size_t height)
{
	for (int i = 0; i < height; i++)
		free(dp[i]);
}

void computeDP(long long** dp, int sum)
{
	dp[0][0] = 1;

	for (int currentPrefix = 1; currentPrefix <= PENS_COUNT; currentPrefix++)
	{
		dp[currentPrefix][0] = 1;
		for (int currentSum = 1; currentSum <= sum; currentSum++)
		{
			dp[currentPrefix][currentSum] += dp[currentPrefix - 1][currentSum];
			if (currentSum - pens[currentPrefix - 1] >= 0)
				dp[currentPrefix][currentSum] += dp[currentPrefix][currentSum - pens[currentPrefix - 1]];
		}
	}
}

int main()
{
	printf("This program computes how many English coins representations of given amount exists.\n\n");

	int sum;
	input(&sum);

	long long* dp[PENS_COUNT + 1];
	init(dp, sum + 1, PENS_COUNT + 1, 0);

	computeDP(dp, sum);

	printf("The given amount can be represented in %lld ways.\n", dp[PENS_COUNT][sum]);

	finalize(dp, PENS_COUNT + 1);
	return 0;
}

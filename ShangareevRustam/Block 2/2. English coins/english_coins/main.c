#include <stdio.h>
#include <stdlib.h>

unsigned long long int** create_array(int sum)
{
	unsigned long long int** dp;
	dp = (unsigned long long int**) malloc(9 * sizeof(unsigned long long int*));
	for (int i = 0; i < 9; i++)
	{
		dp[i] = (unsigned long long int*) malloc((sum + 1) * sizeof(unsigned long long int));
	}
	for (int i = 0; i < 9; i++)
	{
		for (int j = 0; j <= sum; j++)
		{
			dp[i][j] = 0;
		}
	}
	dp[0][0] = 1;
	return dp;
}

void fill_array(unsigned long long int** dp, int sum, int* denominations)
{
	for (int i = 0; i <= sum; i++)
	{
		for (int max_denomination = 0; max_denomination < 9; max_denomination++)
		{
			for (int next_max_denomination = max_denomination; next_max_denomination < 9; next_max_denomination++)
			{
				if (i + denominations[next_max_denomination] <= sum && next_max_denomination != 0)
				{
					dp[next_max_denomination][i + denominations[next_max_denomination]] += dp[max_denomination][i];
				}
			}
		}
	}
}

void print_answer(unsigned long long int** dp, int sum)
{
	unsigned long long int ans = 0;
	for (int i = 0; i < 9; i++)
	{
		ans += dp[i][sum];
	}
	printf("The number of ways to get %d pence using any number of any English coins: %llu\n", sum, ans);
}

void delete_array(unsigned long long int** dp)
{
	for (int i = 0; i < 9; i++)
	{
		free(dp[i]);
	}
	free(dp);
}

void input(int* sum)
{
	printf("Enter a natural number denoting a certain amount of money in pence, "
		"which you need to collect using any number of any English coins:\n");
	char char_after_number;
	while (1)
	{
		int status_of_input = scanf_s("%d%c", sum, &char_after_number);
		if (status_of_input == 2 && *sum > 0 && (char_after_number == ' ' || char_after_number == '\n'))
		{
			break;
		}
		printf("Your input is not a natural number or contains invalid characters. Please re-enter:\n");
		fseek(stdin, 0, 0);
	}
}

int main()
{
	printf("This program, based on the amount entered by the user, "
		"displays the number of ways in which this amount can be collected using any number of any English coins.\n");
	int sum;
	input(&sum);
	unsigned long long int** dp = create_array(sum);
	int denominations[9] = { 0, 1, 2, 5, 10, 20, 50, 100, 200 };
	fill_array(dp, sum, denominations);
	print_answer(dp, sum);
	delete_array(dp);
	return 0;
}
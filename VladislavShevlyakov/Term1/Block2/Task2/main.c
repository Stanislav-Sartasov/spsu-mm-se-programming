#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>

long long int number_of_ways(int* coins, int money)
{
	long long int* total_counts = (long long int*)malloc(sizeof(long long int) * (money + 1));
	total_counts[0] = 1;
	for (int i = 1; i <= money; i++)
	{
		total_counts[i] = 0;
	}
	for (int i = 0; i < 8; i++)
	{
		for (int j = 0; j <= money; j++)
		{
			if (coins[i] <= j)
			{
				total_counts[j] += total_counts[j - coins[i]];
			}
		}
	}
	long long int result = total_counts[money];
	free(total_counts);
	return result;
}

int check_input(char* str)
{
	for (int i = 0; i < strlen(str); i++)
	{
		if (!(str[i] >= '0' && str[i] <= '9'))
		{
			return 0;
		}
	}
	return 1;
}

int main()
{
	printf("The program outputs the number of ways to decompose coins by 1, 2, 5, 10, 20, 50, 100 and 200 pence.\n\n");
	char money_input[256];
	int coins[] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	int money;
	printf("Enter the number of money: ");
	scanf("%s", money_input);
	while (!check_input(money_input) || atoi(money_input) <= 0)
	{
		printf("\nThe number of coins must be a natural number, repeat the input: \n");
		scanf("%s", money_input);
	}
	money = atoi(money_input);
	printf("The total number of ways: %lld", number_of_ways(coins, money));
	return 0;
}
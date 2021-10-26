#include <stdio.h>
#include <stdlib.h>

int get_natural_number()
{
	printf(">>> ");
	int num;
	char end;
	int read_result = scanf("%d%c", &num, &end);
	if ((read_result == 2) && (end == '\n'))
	{
		if (num > 0)
		{
			return num;
		}

		printf("Number must be natural (positive integer)\n");
	}
	else
	{
		printf("Please enter correct natural number\n");
	}

	while (end != '\n')
	{
		scanf("%c", &end);
	}

	return get_natural_number();
}

int main()
{
	const int coins[] = {1, 2, 5, 10, 20, 50, 100, 200};
	const int coins_amount = 8;

	printf("This program outputs amount of ways to reach some sum in pence using 1p, 2p, 5p, 10p, 20p, 50p, 100p and 200p coins\n");
	printf("Enter sum you want to change\n");

	int sum = get_natural_number();
	unsigned long long* ways = (unsigned long long*)malloc(sizeof(unsigned long long) * sum);
	for (int a = 0; a < sum; a++)
	{
		ways[a] = 0;
	}

	for (int i = 0; i < coins_amount; i++)
	{
		int coin = coins[i];

		if (coin > sum)
		{
			break;
		}

		ways[coin - 1] += 1; // when sum equals one of coins, it definitely has one way to reach it
		for (int x = coin; x < sum; x++)
		{
			/* we can reach one sum from another if and only if
			 * difference between them is equal to the coin value we use */
			ways[x] += ways[x - coin];
		}
	}
	printf("Amount of ways is %llu", ways[sum-1]);
	free(ways);
	return 0;
}

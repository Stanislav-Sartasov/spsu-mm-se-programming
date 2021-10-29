#include <stdio.h>
#include<stdlib.h>
#define NUMBER_OF_COINS 8

void numberOfWays(long long** counter, int amount, int* denominations)
{
	for (int i = 0; i < NUMBER_OF_COINS; i++)
	{
		for (int j = 0; j <= amount; j++) 
		{
			counter[i][0] = 1;
			counter[0][j] = 1;
		}
	}
	for (int i = 1; i < NUMBER_OF_COINS; i++)
	{
		for (int j = 1; j <= amount; j++) 
		{
			if (j < denominations[i])
			{
				counter[i][j] = counter[i - 1][j];
			}
			else
			{
				counter[i][j] = counter[i - 1][j] + counter[i][j - denominations[i]];
			}
		}
	}
}

int main()
{
	printf("This program counts the number of ways to dial the amount of einglish coins.\n");
	printf("Please input a desired amount: ");
	int amount;
	int denominations[8] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	scanf("%d", &amount);
	while ((amount <= 0) || (amount > 36000)) 
	{
		printf("Invalid input. Enter another number:");
		char clean = 0;
		while (clean != '\n' && clean != EOF)
			clean = getchar();
		scanf("%d", &amount);
	}


	long long** counter = (long long**)malloc(NUMBER_OF_COINS * sizeof(long long*));
	for (int i = 0; i < NUMBER_OF_COINS; i++)
	{
		counter[i] = (long long*)malloc((amount + 1) * sizeof(long long));
	}

	numberOfWays(counter, amount, denominations);
	printf("\nThe number of ways is %lld.\n", counter[7][amount]);

	for (int i = 0; i < NUMBER_OF_COINS; i++)
	{
		free(counter[i]);
	}
	free(counter);

	return 0;
}







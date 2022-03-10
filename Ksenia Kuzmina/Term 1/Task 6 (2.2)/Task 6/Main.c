#include <stdio.h>
#include <stdlib.h>

int main()
{
	int coins[8] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	int amount;
	unsigned long long** options;
	options = (unsigned  long long**)malloc(8 * sizeof(unsigned long long*));

	printf("This program counts the number of ways to change the amount of coins.\n");
	printf("Coins available for exchange: 1, 2, 5, 10, 20, 50, 100, 200\n");
	printf("Please enter the number of coins you want to change: ");
	scanf_s("%d", &amount);

	while ((amount <= 0) || (amount > 36696))
	{
		if (amount <= 0)
			printf("Please input natural number");
		else if (amount > 36696)
			printf("Sorry, this program only counts numbers less than or equal to 36696. Try again.");
		char clean = 0;
		while (clean != '\n' && clean != EOF)
			clean = getchar();
		scanf_s("%d", &amount);
	}

	amount += 1;

	for (int i = 0; i < 8; i++)
		options[i] = (unsigned long long*)malloc(amount * sizeof(unsigned long long));

	for (int i = 0; i < 8; i++)
		for (int j = 0; j < amount; j++)
		{
			options[i][0] = 1;
			options[0][j] = 1;
		}

	for (int i = 1; i < 8; i++)
		for (int j = 1; j < amount; j++)
		{
			if (j < coins[i])
				options[i][j] = options[i - 1][j];
			else
				options[i][j] = options[i - 1][j] + options[i][j - coins[i]];
		}

	if (options[7][amount - 1] == 1)
		printf("Result: %lld option", options[7][amount - 1]);
	else
		printf("Result: %lld options", options[7][amount - 1]);

	for (int i = 0; i < 8; i++)
	{
		free(options[i]);
	}
	free(options);

	return 0;
}
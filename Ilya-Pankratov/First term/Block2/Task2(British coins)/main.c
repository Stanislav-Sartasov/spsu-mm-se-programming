#include <stdio.h>

unsigned long long int countPossibleWays(int size, int sum, int* coins);

int main()
{
	int coins[] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	int size = (sizeof(coins) / sizeof(coins[0]));
	int sum = 0;
	float checkSum = 0;
	char check;
	printf("This program calculates the number of ways to represent the amount of money using coins "
		"of 1 pence, 2 pence, 5 pence, 10 pence, 20 pence, 50 pence, 1 pound (100 pence), and 2 pence (200 pence).\n\n");
	printf("Input the amount of money(It must be natural and less than 39,000): ");

	while ((scanf_s("%f%c", &checkSum, &check) != 2) || (check != '\n') || (checkSum <= 0) || (checkSum != (int)checkSum) || (checkSum > 39000))
	{
		printf("\nYou input incorrect data\nPlease, the again\n");

		while (check != '\n')
			scanf_s("%c", &check);
		check = '\0';

		printf("\nInput the amount of money(It must be natural and less than 39,000): ");
	}

	sum = (int)checkSum;
	printf("\nThe number of ways is %llu!\n", countPossibleWays(size, sum, &coins));
	return 0;
}

unsigned long long int countPossibleWays(int size, int sum, int* coins)
{
	unsigned long long int** ways = (unsigned long long int**)malloc(sizeof(unsigned long long int*) * (size + 1));

	for (int i = 0; i < size + 1; i++)
		ways[i] = (unsigned long long int*)malloc(sizeof(unsigned long long int) * (sum + 1));

	for (int i = 1; i < sum + 1; i++)
		ways[0][i] = 0;

	for (int i = 0; i < size + 1; i++)
		ways[i][0] = 1;

	for (int i = 1; i < size + 1; i++)
		for (int j = 1; j < sum + 1; j++)
		{
			if (coins[i - 1] > j)
				ways[i][j] = ways[i - 1][j];
			else
				ways[i][j] = ways[i - 1][j] + ways[i][j - coins[i - 1]];
		}
	unsigned long long int result = ways[size][sum];

	for (int i = 0; i < size + 1; i++)
		free(ways[i]);
	free(ways);

	return result;
}

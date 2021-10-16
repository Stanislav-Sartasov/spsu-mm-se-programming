#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

int input()
{
	int sum;
	char end;
	while (scanf("%d%c", &sum, &end) != 2 || end != '\n' || sum > 36696 || sum <= 0)
	{
		while (end != '\n')
			scanf("%c", &end);
		end = '\0';
		printf("You must enter a natural number no greater than 36696!\n");
	}
	return sum;
}

long long result(long long** matrix, int* coins, int sum)
{
	for (int i = 1; i <= 8; i++)
	{
		matrix[i][0] = 1;
		for (int j = 1; j <= sum; j++)
		{
			matrix[i][j] += matrix[i - 1][j];
			if (j - coins[i - 1] >= 0)
				matrix[i][j] += matrix[i][j - coins[i - 1]];
		}
	}
	return matrix[8][sum];
}

int main()
{
	printf("This program prints the number of ways to score a certain amount using any number "
		"of English coins.\n*(Program works for amount of no more than 36696)*\n\n");
	printf("Enter the amount:\n");
	int coins[] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	int sum = input();
	
	long long** matrix = (long long**)malloc(sizeof(long long*) * 9);
	for (int i = 0; i <= 8; i++)
	{
		matrix[i] = (long long*)malloc(sizeof(long long) * (sum + 1));
		for (int j = 0; j <= sum; j++)
			matrix[i][j] = 0;
	}
	
	printf("The number of ways = %lld\n", result(matrix, coins, sum));

	for (int i = 0; i <= 8; i++)
		free(matrix[i]);
	free(matrix);
	return 0;
}
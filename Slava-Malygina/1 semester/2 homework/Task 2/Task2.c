#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void count(int sum)
{
	int coins[] = {1, 2, 5, 10, 20, 50, 100, 200};
	int m = 200;
	long long* buffer;
	buffer = malloc(sizeof(long long) * 9 * 201);
	memset(buffer, 0, sizeof(long long) * 9 * 201);
	long long** table = malloc(sizeof(long long*) * 9);
	if (buffer == NULL || table == NULL)
		printf("Failed to allocate memory\n");
	for (int i = 0; i < 9; i++)
	{
		table[i] = &buffer[i * 201];
	}
	for (int i = 1; i <= 8; i++)
	{
		table[i][0] = 1;
	}
	for (int l = 1; l <= sum; l++)
	{
		int i = l % m;
		for (int j = 1; j <= 8; j++)
		{
			table[j][i] = table[j][(i + m - coins[j - 1]) % m] + table[j - 1][i];
		}
	}
	printf("The number of ways in which the entered amount can be converted: %lld\n", table[8][sum % m]);
	free(table[0]);
	free(table);
}

int main()
{
	printf("This program counts the number of ways in which the entered amount can be made using any number of different denominations English coins.\n");
	printf("Please, enter a natural number that denotes a certain amount of money in pence:\n");
	int sum;
	char d;
	scanf("%d", &sum);
	d = getchar();
	while ((sum <= 0) || (isalpha(d)) || (sum >= 25000))
	{
		printf("Invalid value. The entered number is too large or input cannot be converted into coins. Please, re-enter: ");
		scanf("%d", &sum);
		d = getchar();
	}
	count(sum);
	return 0;
 }
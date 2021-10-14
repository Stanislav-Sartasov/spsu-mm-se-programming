#include <stdio.h>
#include <stdlib.h>
#define ALL_COINS_VALUES { 0, 1, 2, 5, 10, 20, 50, 100, 200 }


long long number_of_decompositions(int n, int k, long long** matrix)
{
	if (n == 0)
		return 1;
	if (k == 0)
		return 0;
	int all_coins_values[9] = ALL_COINS_VALUES;
	if (n - all_coins_values[k] < 0)
		return matrix[n][k - 1];
	return matrix[n - all_coins_values[k]][k] + matrix[n][k - 1];
}

int main()
{
	int coins, right_value;
	printf("this program shows how many ways a given number(from 0 to 36000) of coins can be decomposed\n"
		"using coins of the form: { 0, 1, 2, 5, 10, 20, 50, 100, 200 }\n\n");
	do
	{
		printf("Enter one natural number: ");
		right_value = scanf("%d", &coins);
		while (getchar() != '\n');
	} while (!(right_value == 1 && coins > 0));

	long long** table_values = (long long**)malloc((coins + 1) * sizeof(long long*));

	for (int i = 0; i < coins + 1; i++)
	{
		table_values[i] = (long long*)malloc(9 * sizeof(long long));
	}

	for (int row = 0; row < coins + 1; row++)
	{
		for (int col = 0; col < 9; col++)
		{
			table_values[row][col] = number_of_decompositions(row, col, table_values);
		}
	}

	printf("\nThe number of ways is: %lld", table_values[coins][8]);

	for (int i = 0; i < coins + 1; i++)
	{
		free(table_values[i]);
	}
	free(table_values);
}
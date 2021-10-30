#include <stdio.h>
#include <stdlib.h>

long long number_of_decompositions(int row, int col, long long** matrix)
{
	if (row == 0)
		return 1;
	if (col == 0)
		return 0;
	int coins_values[9] = {0, 1, 2, 5, 10, 20, 50, 100, 200 };
	if (row - coins_values[col] < 0)
		return matrix[row][col - 1];
	return matrix[row - coins_values[col]][col] + matrix[row][col - 1];
}

int main()
{
	int coins, right_value;
	printf("This program shows how many ways a given number of coins can be decomposed\n"
		   "using 0, 1, 2, 5, 10, 20, 50, 100 and 200 pennies\n\n");
	do
	{
		printf("Enter one natural number: ");
		right_value = scanf("%d", &coins);
		while (getchar() != '\n');
	} while (right_value != 1 || coins <= 0);
	coins++;
	long long** values = (long long**)malloc((coins) * sizeof(long long*));
	for (int i = 0; i < coins; i++)
	{
		values[i] = (long long*)malloc(9 * sizeof(long long));
	}
	for (int row = 0; row < coins; row++)
	{
		for (int col = 0; col < 9; col++)
		{
			values[row][col] = number_of_decompositions(row, col, values);
		}
	}
	printf("\nThe number of ways is: %lld", values[coins - 1][8]);
	for (int i = 0; i < coins; i++)
	{
		free(values[i]);
	}
	free(values);
}
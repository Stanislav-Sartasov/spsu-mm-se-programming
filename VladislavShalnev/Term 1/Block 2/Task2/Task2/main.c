
#include <stdio.h>

#define COINS_LENGTH 8


void init_array(long long** array, int sum)
{
	for (int i = 0; i < COINS_LENGTH; i++)
	{
		array[i] = (long long*)malloc(sum * sizeof(long long));
		array[i][0] = 1;
		for (int j = 1; j < sum; j++)
			array[i][j] = i == 0 ? 1 : 0;
	}
}

void calculate(long long** array, int sum, int coins[])
{
	for (int i = 1; i < COINS_LENGTH; i++)
	{
		for (int j = 1; j < sum; j++)
		{
			array[i][j] = array[i - 1][j];
			if (j >= coins[i])
				array[i][j] += array[i][j - coins[i]];
		}
	}
}

void free_array(long long** array)
{
	for (int i = 0; i < COINS_LENGTH; i++)
		free(array[i]);
}

void input(int* adress, char* message)
{
	while (1)
	{
		char input[256];

		printf(message);
		fgets(input, sizeof(input), stdin);

		if (!sscanf_s(input, "%d", adress))
		{
			printf("Inputed value is not a positive integer.\n");
			continue;
		}
		if (*adress <= 0)
		{
			printf("Inputed number can't be less than zero or equal to zero.\n");
			continue;
		}
		return;
	}
}

int main()
{
	printf("This program counts representations of entered sum in English coins (1, 2, 5, 10, 20, 50, 100, 200 pennies).\n\n");

	int sum;
	input(&sum, "Enter a positive integer number: ");
	sum++;

	int coins[COINS_LENGTH] = { 1, 2, 5, 10, 20, 50, 100, 200 };

	long long* array[COINS_LENGTH];

	init_array(array, sum);

	calculate(array, sum, coins);

	printf("\nThis sum can be presented in %lld ways.\n", array[COINS_LENGTH - 1][sum - 1]);

	free_array(array);

	return 0;
}

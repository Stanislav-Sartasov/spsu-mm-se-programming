#include <stdio.h>
#define NUMBER_OF_COINS 8
#define COINS { 1, 2, 5, 10, 20, 50, 100, 200 }


void recurrent_function(int n_coins, int sum, long long** matrix)
{
	if (sum == 0 || n_coins == 1)
	{
		matrix[sum][n_coins] = 1;
	}
	else
	{
		int coins[] = COINS;
		long long part_1 = matrix[sum][n_coins - 1];
		long long part_2 = 0;
		if (sum >= coins[n_coins - 1])
		{
			part_2 = matrix[sum - coins[n_coins - 1]][n_coins];
		}
		matrix[sum][n_coins] = part_1 + part_2;
	}
}


void clean_input()  // Cleaning the input area before using scanf_s function
{
	char s = 0;
	while (s != '\n' && s != EOF)
	{
		s = getchar();
	}
}


int is_correct_input(int* number)  // Returns 1 only if the input is correct
{
	return (scanf_s("%d", number) && 1 <= *number && *number <= 36000);
}


int get_input_number()
{
	int number = 0;
	while (!(is_correct_input(&number) && getchar() == '\n'))
	{
		clean_input();
		printf("Your input is incorrect! Please, try again:\n");
	}
	return number;
}


int main()
{
	printf("This program displays the number of ways in which it is possible to dial\n");
	printf("the amount entered by the user using any number of any English coins.\n");
	printf("Restrictions on the entered numbers: 1 <= n <= 36000.\n");
	printf("\n");

	printf("Please, input a desired amount of coins:\n");
	int n = get_input_number();

	long long** matrix = (long long**)malloc((n + 1) * sizeof(long long*));
	for (int i = 0; i <= n; i++)
	{
		matrix[i] = (long long*)malloc((NUMBER_OF_COINS + 1) * sizeof(long long)); //!
	}

	for (int sum = 0; sum <= n; sum++)
	{
		for (int n_coins = 1; n_coins <= NUMBER_OF_COINS; n_coins++)
		{
			recurrent_function(n_coins, sum, matrix);
		}
	}

	printf("The number of ways is %lld.\n", matrix[n][NUMBER_OF_COINS]);

	for (int i = 0; i <= n; i++)
	{
		free(matrix[i]);
	}
	free(matrix);
}
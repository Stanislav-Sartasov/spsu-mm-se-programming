#include <stdio.h>
#include <stdbool.h>
#include <limits.h>
#include <stdlib.h>

int int_input(char message[])
{
	bool check_correction = true;
	int sign = 1, number = 0, end = 0, check_overflow = 0;
	do
	{
		bool first_mark_input = true;
		char symbol_input = '\0';
		sign = 1, number = 0, end = 0, check_overflow = 0;
		check_correction = true;
		printf_s("%s", message);
		while (check_correction && check_overflow < 11)
		{
			end = scanf_s("%c", &symbol_input);
			if (first_mark_input)
			{
				first_mark_input = false;
				if (symbol_input == '-')
				{
					sign = -1;
					first_mark_input = true;
					continue;
				}
				else if (symbol_input >= '0' && symbol_input <= '9')
				{
					number = symbol_input - '0';
					check_overflow++;
					continue;
				}
				check_correction = false;
			}
			else
			{
				if (symbol_input >= '0' && symbol_input <= '9')
				{
					if (check_overflow == 9 && INT_MAX / 10 <= number)
					{
						check_overflow += 10;	// raising a variable due to overflow
						break;
					}
					number = number * 10 + symbol_input - '0';
					check_overflow++;
					continue;
				}
				else if (end == EOF || symbol_input == '\n')
				{
					break;
				}
				check_correction = false;
			}
		}
		if (!check_correction)
		{
			printf("incorrect input, there are string or float type, try again.\n");
			if (end == EOF || symbol_input == '\n')		// if the user immediately pressed Enter
			{
				continue;
			}
			symbol_input = '\0';
			while (symbol_input != '\n')	// clearing the buffer of unnecessary user input characters
			{
				scanf_s("%c", &symbol_input);
			}
		}
		else if (check_overflow > 10)
		{
			if (sign == 1)
			{
				printf("overflow, there is more than int_max, try again.\n");
			}
			else
			{
				printf("overflow, there is less than int_min, try again.\n");
			}
			symbol_input = '\0';
			while (symbol_input != '\n')	// clearing the buffer of unnecessary user input characters
			{
				scanf_s("%c", &symbol_input);
			}
		}
	} while (!check_correction || check_overflow > 10);
	return number * sign;
}

void print_combination(long long** matrix, int sum, int coins[], int length_coins)
{
	for (int i = 0; i < length_coins; i++)
	{
		for (int j = 0; j < sum + 1; j++)
		{
			if (i == 0 || j == 0)
			{
				matrix[i][j] = 1;
				continue;
			}
			if (j < coins[i])
			{
				matrix[i][j] = matrix[i - 1][j];
			}
			else
			{
				matrix[i][j] = matrix[i - 1][j] + matrix[i][j - coins[i]];
			}
		}
	}
	printf("Number of ways: %lld", matrix[length_coins - 1][sum]);
}

int main()
{
	int coins[] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	int length_coins = sizeof(coins) / sizeof(coins[0]);
	int sum;
	printf("The user enters a natural number representing a certain amount of money in pence.\n");
	printf("The program displays the number of ways in which this amount can be collected using any number of any English coins.\n");
	printf("Coins of the following denominations are used: 1 pence, 2 pence, 5 pence, 10 pence, 20 pence, 50 pence,\n1 pound (100 pence) and 2 pence (200 pence).\n");
	do
	{
		sum = int_input("Please enter a natural number between 1 and 36695: ");
		if (sum > 36695 || sum < 1)
		{
			printf("The number is out of range. Try again.\n");
		}
	} while (sum > 36695 || sum < 1);
	long long** matrix = (long long**)malloc(sizeof(long long*) * length_coins);
	for (int i = 0; i < length_coins; i++)
	{
		matrix[i] = (long long*)malloc(sizeof(long long) * (sum + 1));
	}
	print_combination(matrix, sum, coins, length_coins);
	for (int i = 0; i < length_coins; i++)
	{
		free(matrix[i]);
	}
	free(matrix);
	return 0;
}
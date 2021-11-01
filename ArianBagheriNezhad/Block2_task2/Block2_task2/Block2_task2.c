//Homework 2.2
//English coins

#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

#define NOC 8 //Number Of Coins = NOC.

void get_number(int* number, char* message);
void number_of_ways(int* number, int* coins, long long** array);

int main()
{
	int number, i, j;
	int coins[NOC] = { 1, 2, 5, 10, 20, 50, 100, 200 };

	printf("In England, coins of the following denominations are in ");
	printf("circulation: 1 pence, 2 pence, 5 pence, 10 pence, 20 pence\n");
	printf("50 pence, 1 pound (100 pence) and 2 pence (200 pence).The ");
	printf("user enters a natural number representing a certain amount\n");
	printf("of money in pence. Display the number of ways in which this ");
	printf("amount can be collected using any number of any English coins.\n\n");

	get_number(&number, "Enter a natural number denoting a certain amount of money in pence(Less than 35000):\n");

	long long** array = (long long**)malloc(NOC + 1 * sizeof(long long*));
	for (i = 0; i < NOC + 1; i++)
	{
		array[i] = (long long*)malloc((number + 1) * sizeof(long long));
		for (j = 0; j <= number; j++)
		{
			array[i][j] = 0;
		}
	}

	array[0][0] = 1;
	number_of_ways(&number, coins, array);

	for (i = 0; i < NOC + 1; i++)
	{
		free(array[i]);
	}
	free(array);

	return 0;
}

void get_number(int* number, char* message)
{
	printf(message);

	while (true)
	{
		scanf_s("%d", number);
		if (*number > 0 && *number < 35000 && getchar() == '\n')
		{
			break;
		}
		else
		{
			printf("\nNot right! try again...\n");
			continue;
		}
	}
}

void number_of_ways(int* number, int* coins, long long** array)
{
	int i, j;

	for (i = 0; i < NOC; i++)
	{
		array[i + 1][0] = 1;
		for (j = 1; j <= *number; j++)
		{
			array[i + 1][j] = array[i + 1][j] + array[i][j];
			if (j >= *(coins + i))
			{
				array[i + 1][j] = array[i + 1][j] + array[i + 1][j - *(coins + i)];
			}
		}
	}

	printf("\nThe number of ways in which this amount can be collected using any number of any English coins: %lld\n\n", array[NOC][*number]);
}
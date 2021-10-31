#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

int main()
{
	int coins[] = { 1, 2, 5, 10, 20, 50, 100, 200 };
	int number, correctly_read;

	printf("The program displays the number of ways in which you can get the amount presented as a user-entered natural number\n using any number of any English coins.\n");
	printf("Coins of the following denominations are used: 1 pence, 2 pence, 5 pence, 10 pence, 20 pence,\n 50 pence, 1 pound (100 pence) and 2 pence (200 pence).\n\n");
	printf("Please enter a natural number in the range [1; 36695]: ");

	while (true)
	{
		char ch;
		correctly_read = scanf("%d%c", &number, &ch);
		if (correctly_read == 2 && number > 0 && number < 36696 && (ch == ' ' || ch == '\n'))
		{
			break;
		}
		else
		{
			printf("Incorrect input. Please, try again: ");
			fseek(stdin, 0, 0);
		}
	}

	unsigned long long maxsize = number + 1;
	unsigned long long **ways = (unsigned long long **) malloc(8 * sizeof(unsigned long long*));
	for (int i = 0; i < 8; ++i)
		ways[i] = (unsigned long long*) malloc(maxsize * sizeof(unsigned long long));

	for (int i = 0; i < 8; ++i)
	{
		for (int j = 0; j < maxsize; ++j)
		{
			ways[i][j] = 0;
		}
	}
	ways[0][0] = 1;

	for (int i = 0; i < maxsize; ++i)
	{
		for (int j = 0; j < 8; ++j)
		{
			for (int k = j; k < 8; ++k)
			{
				if (i + coins[k] < maxsize)
				{
					ways[k][i + coins[k]] += ways[j][i];
				}
			}
		}
	}

	long long int answer = 0;
	for (int i = 0; i < 8; ++i)
	{
		answer += ways[i][number];
	}

	printf("There are %lld ways.", answer);

	for (int i = 0; i < 8; ++i)
	{
		free(ways[i]);
	}
	free(ways);

	return 0;
}
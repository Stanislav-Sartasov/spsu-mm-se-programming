#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

void clearStdin()
{
	char temp;
	do
	{
		temp = getchar();
	} while (temp != '\n' && temp != EOF);
}

int checkNumber(int message)
{
	int number;
	int count;
	printf(message);
	do
	{
		printf("Enter the amount of money which you want to be exchanged: \n");
		count = scanf("%d", &number);
		if (count != 0)
		{
			if ((int)number != (double)number)
			{
				number = checkNumber("The given number is not integer. Try again.\n");
			}
			else if (number <= 1)
			{
				number = checkNumber("The sum should be more than 1. Try again.\n");
			}
			else if (number > 36696)
			{
				number = checkNumber("The sum cannot be more than 36696. Try again.\n");
			}
		}
		else
		{
			printf("The input is not a number. Try again.\n");
			clearStdin();
		}
		while (getchar() != '\n');
	} while (!(count == 1 && number > 1));
	return number + 1;
}

void releaseMem(char* arr[])
{
	for (int i = 0; i < 8; i++)
	{
		free(arr[i]);
		arr[i] = NULL;
	}

}

int main()
{
	printf("The program calculates the number of ways how the inputed amount can be exchanged.\n");
	unsigned int coins[8] = {1, 2, 5, 10, 20, 50, 100, 200};
	unsigned int bank;
	unsigned long long** way = (unsigned long long**)malloc(8 * sizeof(unsigned long long*));

	bank = checkNumber("");
	for (int i = 0; i < 8; i++)
		way[i] = (unsigned long long*)malloc(bank * sizeof(unsigned long long));

	for (int i = 0; i < 8; i++)
		for (int j = 0; j < bank; j++)
		{
			way[i][0] = 1;
			way[0][j] = 1;
		}

	for (int i = 1; i < 8; i++)
		for (int j = 1; j < bank; j++)
		{
			if (j < coins[i])
				way[i][j] = way[i - 1][j];
			else
				way[i][j] = way[i - 1][j] + way[i][j - coins[i]];
		}

	printf("There are %lld ways of exchange for inputed amount", way[7][bank - 1]);

	releaseMem(way);
	return 0;
}
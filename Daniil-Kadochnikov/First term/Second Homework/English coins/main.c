#include <stdio.h>
#include <stdlib.h>



int checkError(long long int* pnumber, int* operation)
{
	if (*operation == 1 && fgetc(stdin) == '\n')
	{
		if (*pnumber > 0)
		{
			return 0;
		}
		else
		{
			printf("\nYou entered a non-positive number, try again, please.\n\n");
			return 1;
		}
	}
	else
	{
		printf("\nYou didn't enter an integer, try again, please.\n\n");
		return 1;
	}
}

void scanning(long long int* amountOfMoney)
{
	int operation;
	do
	{
		fseek(stdin, 0, 0);
		printf("Enter a positive natural number which represents amount of money in peso.\nAmount of money = ");
		operation = scanf_s("%lld", amountOfMoney);
	} while (checkError(amountOfMoney, &operation));
}

int main()
{
	printf("The programm counts number of ways in which the entered amount of money in peso could be exchanged using British coins.\n\n");
	long long int amountOfMoney;
	int coins[7] = { 2, 5, 10, 20, 50, 100, 200 };   //without "1" because on the next step we consider one important thing about "1", so it is useless "coins".
	scanning(&amountOfMoney);
	long long int* amountOfWays;
	amountOfWays = (long long int*)malloc((amountOfMoney + 1) * sizeof(long long int));
	for (int firstRow = 0; firstRow < amountOfMoney + 1; firstRow++)
	{
		amountOfWays[firstRow] = 1;   //first row fills fully with "1", because every number has only one way to be exchanged with coins with value 1.
	}
	int typeOfCoins, growingAmount;
	for (typeOfCoins = 0; typeOfCoins < 7; typeOfCoins++)
	{
		for (growingAmount = 1; growingAmount < amountOfMoney + 1; growingAmount++)
		{
			if (coins[typeOfCoins] > growingAmount)
			{
				continue;
			}
			else
			{
				amountOfWays[growingAmount] += amountOfWays[growingAmount - coins[typeOfCoins]];
			}
		}
	}
	
	printf("\n%lld peso could be exchanged in %lld ways.\n", amountOfMoney, amountOfWays[amountOfMoney]);
	free(amountOfWays);
	return 0;
}
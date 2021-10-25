#include <stdio.h>
#include <stdlib.h>



int checkError(float* pnumber, int* operation)
{
	if (*operation == 1 && fgetc(stdin) == '\n')
	{
		if (*pnumber > 0)
		{
			if ((int)*pnumber == *pnumber)
			{
				return 0;
			}
			else
			{
				printf("\nYou entered a number that is not an integer, try again, please.\n\n");
				return 1;
			}
		}
		else
		{
			printf("\nYou entered a non-positive number, try again, please.\n\n");
			return 1;
		}
	}
	else
	{
		printf("\nYou didn't enter a number, try again, please.\n\n");
		return 1;
	}
}

void scanning(float* amountOfMoney)
{
	int operation;
	do
	{
		fseek(stdin, 0, 0);
		printf("Enter a positive natural number which represents amount of money in peso.\nAmount of money = ");
		operation = scanf_s("%f", amountOfMoney);
	} while (checkError(amountOfMoney, &operation));
}

void counting(int sum, int maxCoin, int* count, int coinsAmount[2][8])
{
	if (sum == 0)
	{
		*count+=1;
		return 0;
	}
	if (coinsAmount[0][maxCoin] <= sum)
	{
		coinsAmount[1][maxCoin]++;
		counting(sum - coinsAmount[0][maxCoin], maxCoin, count, coinsAmount);
		coinsAmount[1][maxCoin]--;
	}
	if (maxCoin < 7)
	{
		counting(sum, maxCoin + 1, count, coinsAmount);
	}
}

int main()
{
	printf("The programm counts number of ways in which the entered amount of money in peso could be exchanged using British coins.\n\n");
	int count = 0;
	int coinsAmount[2][8] = { 200, 100, 50, 20, 10, 5, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0 };
	float amountOfMoney;
	scanning(&amountOfMoney);
	int maxCoin = 0;
	counting((int)amountOfMoney, maxCoin, &count, coinsAmount);
	printf("\n%d peso could be exchanged in %d ways.\n", (int)amountOfMoney, count);
	return 0;
}
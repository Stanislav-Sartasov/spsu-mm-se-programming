#include <stdio.h>

void cleanInput()
{
	char s;
	do
	{
		s = getchar();
	} while (s != '\n' && s != EOF);
}

int correctInput(long long* number)
{
	return (scanf("%lld", number) && *number >= 1 && *number <= 35000);
}

int getInputNum()
{
	long long number = 0;
	while (!(correctInput(&number) && getchar() == '\n'))
	{
		cleanInput();
		printf("Your input is not correct! Try again:\n");
	}
	return number;
}

void fillMatrix(long long** arrMatrix, int valueOfMoney, int* coins)
{
	for (int i = 0; i < 8; i++)
	{
		for (int j = 0; j <= valueOfMoney; j++)
		{
			arrMatrix[i][0] = 1;
			arrMatrix[0][j] = 1;
		}
	}
	for (int i = 1; i < 8; i++)
	{
		for (int j = 1; j <= valueOfMoney; j++)
		{
			if (j < coins[i])
			{
				arrMatrix[i][j] = arrMatrix[i - 1][j];
			}
			else
			{
				arrMatrix[i][j] = arrMatrix[i - 1][j] + arrMatrix[i][j - coins[i]];
			}
		}
	}
}


int main()
{
	printf("This program outputs the number of the ways a given quantity of money can be presented using coins with these values:\n");
	printf("1 penny, 2 pence, 5 pence, 10 pence, 20 pence, 50 pence, 1 pound (100 pence) and 2 pounds (200 pence)\n\n");
	printf("Input a number between 1 and 35000, because there are too many ways to represent it for large numbers.\n\n");

	int valueOfMoney = getInputNum();
	int coins[] = {1, 2, 5, 10, 20, 50, 100, 200};

	long long** arrMatrix = (long long**)malloc(sizeof(long long*) * 8);
	for (int i = 0; i < 8; i++)
	{
		arrMatrix[i] = (long long*)malloc(sizeof(long long) * (valueOfMoney + 1));
	}

	fillMatrix(arrMatrix, valueOfMoney, coins);
	printf("\nThis number can be presented in %lld ways.\n", arrMatrix[7][valueOfMoney]);
	
	for (int i = 0; i < 8; i++)
	{
		free(arrMatrix[i]);
	}
	free(arrMatrix);

	return 0;
}
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>

#define IO_BUFFER_LIMIT 128
#define MAX_INPUT_VALUE_POW10 4

#define COINS_AVAILABLE {1, 2, 5, 10, 20, 50, 100, 200}

int numberOfCoins()
{
	int coins[] = COINS_AVAILABLE;
	return sizeof(coins) / sizeof(int);
}

int maxCoinValue()
{
	int coins[] = COINS_AVAILABLE;
	int numCoins = numberOfCoins();
	int maxValue = -1;
	for (int i = 0; i < numCoins; i++)
	{
		if (maxValue < coins[i])
		{
			maxValue = coins[i];
		}
	}
	return maxValue;
}

int maxInputValue()
{
	int maxValue = 1;
	for (int i = 0; i < MAX_INPUT_VALUE_POW10; i++)
	{
		maxValue *= 10;
	}
	return maxValue;
}

_Bool isEmptyLine(char input[])
{
	int curPos = 0;
	while (input[curPos] == ' ')
	{
		curPos++;
	}
	return input[curPos] == '\n';
}

void greetingsMessage()
{
	printf("This program is designed to calculate the number of all possible\n");
	printf(" ways to get the desired sum with British coins.\n");
}

void incorrectInputMessage(char error[])
{
	printf("Incorrect input: %s\n\n", error);
}

void inputAwaitingMessage()
{
	printf("Please, write the sum you wish to get in range [0, 1e%d]\n", MAX_INPUT_VALUE_POW10);
	printf(" or enter an empty line to exit: ");
}

_Bool checkRange(int number)
{
	return number <= maxInputValue() && number >= 0;
}

long long** createTwoDimArrayL(int n, int m)
{
	long long *mem = malloc(sizeof(long long) * n * m);
	if (mem == NULL)
	{
		return NULL;
	}
	memset(mem, 0, sizeof(long long) * n * m);
	long long **arr = malloc(sizeof(long long*) * n);
	if (arr == NULL)
	{
		return NULL;
	}
	for (int i = 0; i < n; i++)
	{
		arr[i] = &mem[i * m];
	}
	return arr;
}

void freeTwoDimArrayL(long long **arr)
{
	free(arr[0]); // for the memory allocated for the whole array
	free(arr);
}

void countAndPrintSplits(int sum)
{
	int coins[] = COINS_AVAILABLE;
	int numCoins = numberOfCoins();
	int maxCoinP = maxCoinValue() + 1;
	long long **splitsNum = createTwoDimArrayL(numCoins + 1, maxCoinP);

	if (splitsNum == NULL)
	{
		printf("could not allocate memory\n\n");
		return;
	}

	for (int i = 1; i <= numCoins; i++)
	{
		splitsNum[i][0] = 1;
	}
	for (int s = 1; s <= sum; s++)
	{
		int curSum = s % maxCoinP;
		for (int curCoin = 1; curCoin <= numCoins; curCoin++)
		{
			splitsNum[curCoin][curSum] = splitsNum[curCoin - 1][curSum]
				+ splitsNum[curCoin][(curSum + maxCoinP - coins[curCoin - 1]) % maxCoinP];
		}
	}

	printf("You can get the sum of [%d] with British coins in ", sum);
	printf("[%lld] different ways.\n\n", splitsNum[numCoins][sum % maxCoinP]);

	freeTwoDimArrayL(splitsNum);
}

void mainInterfaceLoop()
{
	_Bool userExitSignal = 0;
	char ioBuffer[IO_BUFFER_LIMIT];
	char leftover;
	int sum;
	while (!userExitSignal)
	{
		inputAwaitingMessage();
		fgets(ioBuffer, IO_BUFFER_LIMIT, stdin);

		if (isEmptyLine(ioBuffer))
		{
			userExitSignal = 1;
			continue;
		}

		if (sscanf_s(ioBuffer, "%d %c", &sum, &leftover) != 1)
		{
			incorrectInputMessage("input has too many numbers and/or it could not be converted");
		}
		else if (!checkRange(sum))
		{
			incorrectInputMessage("the number is out of range");
		}
		else
		{
			countAndPrintSplits(sum);
		}
	}
}
int main()
{
	greetingsMessage();
	mainInterfaceLoop();

	return 0;
}
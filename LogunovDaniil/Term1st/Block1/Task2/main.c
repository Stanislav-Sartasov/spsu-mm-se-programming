#include <stdio.h>
#include <math.h>
#include <stdlib.h>

#define IO_BUFFER_LIMIT 128
#define MAX_INPUT_VALUE_POW10 4
#define POW2(A) ((A) * (A))

int maxInputValue()
{
	int maxValue = 1;
	for (int i = 0; i < MAX_INPUT_VALUE_POW10; i++)
	{
		maxValue *= 10;
	}
	return maxValue;
}

void swap(int* a, int* b)
{
	*a ^= *b;
	*b ^= *a;
	*a ^= *b;
}

int gcd(int a, int b)
{
	if (a < b)
	{
		swap(&a, &b);
	}
	if (b == 0)
	{
		return a;
	}
	while (a >= b)
	{
		a -= b;
	}
	return gcd(b, a);
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
	printf("This program is designed to identify Pythagorean triples.\n");
}

void incorrectInputMessage(char error[])
{
	printf("Incorrect input: %s\n\n", error);
}

void inputAwaitingMessage()
{
	printf("Please, write three natural numbers in range [1, 1e%d] you wish to check\n", MAX_INPUT_VALUE_POW10);
    printf(" or enter an empty line to exit : ");
}

void checkPythagorean(int triple[])
{
	if (triple[1] < triple[2])
	{
		swap(&triple[1], &triple[2]);
	}
	if (triple[0] < triple[2])
	{
		swap(&triple[0], &triple[1]);
	}

	printf("[%d, %d, %d] is ", triple[0], triple[1], triple[2]);
	if (POW2(triple[0]) == POW2(triple[1]) + POW2(triple[2]))
	{
		if (gcd(triple[0], triple[1]) == 1
			&& gcd(triple[1], triple[2]) == 1
			&& gcd(triple[0], triple[2]) == 1)
		{
			printf("a Primitive Pythagorian triple\n\n");
		}
		else
		{
			printf("a Pythagorian triple\n\n");
		}
	}
	else
	{
		printf("not a Pythagorian triple\n\n");
	}
}

_Bool checkRange(int number)
{
	return (number >= 1) && (number <= maxInputValue());
}

_Bool checkRangeArray(int arr[], int size)
{
	int res = 1;
	for (int i = 0; i < size; i++)
	{
		res *= checkRange(arr[i]);
	}
	return res;
}

void mainInterfaceLoop()
{
	_Bool userExitSignal = 0;
	char ioBuffer[IO_BUFFER_LIMIT];
	char leftover;
	int triple[3];
	while (!userExitSignal)
	{
		inputAwaitingMessage();
		fgets(ioBuffer, IO_BUFFER_LIMIT, stdin);
		
		if (isEmptyLine(ioBuffer))
		{
			userExitSignal = 1;
			continue;
		}
		
		    // Visual Studio goes with a warning here regarding missing fifth variative argument...
		    // ...but the format string has only four? Nevertheless, it works just fine
		if (sscanf_s(ioBuffer, "%d %d %d %c", &triple[0], &triple[1], &triple[2], &leftover) != 3)
		{
			incorrectInputMessage("input has too many numbers and/or one of them could not be converted");
		}
		else if (!checkRangeArray(triple, 3))
		{
			incorrectInputMessage("one (or more) numbers is out of range");
		}
		else
		{
			checkPythagorean(triple);
		}
	}
}

int main()
{
	greetingsMessage();
	mainInterfaceLoop();

	return 0;
}
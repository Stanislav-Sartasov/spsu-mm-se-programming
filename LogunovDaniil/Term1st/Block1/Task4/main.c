#include <stdio.h>
#include <stdlib.h>
#include <math.h>

#define IO_BUFFER_LIMIT 128
#define MAX_INPUT_VALUE_POW10 4
#define POW2(A) ((A) * (A))

int gcd(int a, int b)
{
	if (a < b)
	{
		// swapping a and b
		a ^= b;
		b ^= a;
		a ^= b;
	}
	if (b == 0)
	{
		return a;
	}
	return gcd(b, a % b);
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
	printf("This program is designed to calculate continued fraction of a square root.\n");
}

void incorrectInputMessage(char error[])
{
	printf("Incorrect input: %s\n\n", error);
}

void inputAwaitingMessage()
{
	printf("Please, write a natural number in range [0, 1e%d]\n", MAX_INPUT_VALUE_POW10);
	printf(" or enter an empty line to exit: ");
}

_Bool checkRange(int number)
{
	return number <= maxInputValue() && number > 0;
}

void calculateAndPrintFraction(int n)
{
	printf("Arguments of the continued fraction are: ");
	int rootN = (int)sqrt(n);
	if (POW2(rootN + 1) == n)
	{
		rootN++;
	}
	if (POW2(rootN) == n)
	{
		printf("[%d]", rootN);
	}
	else
	{
		// using the formula for the next sequence member
		// (mul * sqrt(n) + add) / div = prevAlpha + 1 / alpha

		// initial values
		int prevAdd = 0;
		int prevMul = 1;
		int prevDiv = 1;
		int prevAlpha = rootN;

		// calculating the first member
		int add = -prevDiv * (prevAdd - prevAlpha * prevDiv);
		int mul = prevDiv * prevMul;
		int div = POW2(prevMul) * n - POW2(prevAdd - prevAlpha * prevDiv);
		int alpha = (int)((mul * rootN + add) / div);

		// simplifying fraction
		int cmn = gcd(gcd(mul, add), div);
		add /= cmn;
		mul /= cmn;
		div /= cmn;

		// updating values
		prevAdd = add;
		prevDiv = div;
		prevMul = mul;
		prevAlpha = alpha;

		// period-registering variables
		int cmpAdd = add;
		int cmpDiv = div;
		int cmpMul = mul;
		int cmpAlpha = alpha;

		printf("[%d", rootN);

		// calculating further until the period goes back to the first member
		int isFirstCycle = 1;
		while (isFirstCycle || alpha != cmpAlpha || add != cmpAdd || div != cmpDiv || mul != cmpMul)
		{
			if (isFirstCycle)
			{
				isFirstCycle = 0;
				printf("; ");
			}
			else
			{
				printf(", ");
			}
			printf("%d", alpha);

			add = -prevDiv * (prevAdd - prevAlpha * prevDiv);
			mul = prevDiv * prevMul;
			div = POW2(prevMul) * n - POW2(prevAdd - prevAlpha * prevDiv);

			cmn = gcd(gcd(mul, add), div);
			add /= cmn;
			mul /= cmn;
			div /= cmn;

			alpha = (int)(mul * rootN + add) / div;

			prevAdd = add;
			prevDiv = div;
			prevMul = mul;
			prevAlpha = alpha;
		}
		printf("]");
	}
	printf("\n\n");
}

void mainInterfaceLoop()
{
	_Bool userExitSignal = 0;
	char ioBuffer[IO_BUFFER_LIMIT];
	char leftover;
	int n;
	while (!userExitSignal)
	{
		inputAwaitingMessage();
		fgets(ioBuffer, IO_BUFFER_LIMIT, stdin);

		if (isEmptyLine(ioBuffer))
		{
			userExitSignal = 1;
			continue;
		}

		if (sscanf_s(ioBuffer, "%d %c", &n, &leftover) != 1)
		{
			incorrectInputMessage("input could not be converted to a single number");
		}
		else if (!checkRange(n))
		{
			incorrectInputMessage("number is out of range");
		}
		else
		{
			calculateAndPrintFraction(n);
		}
	}
}
int main()
{
	greetingsMessage();
	mainInterfaceLoop();

	return 0;
}
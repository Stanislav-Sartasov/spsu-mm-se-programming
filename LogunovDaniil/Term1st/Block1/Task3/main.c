#include <stdio.h>
#include <stdlib.h>
#include <math.h>

#define IO_BUFFER_LIMIT 128
#define MAX_INPUT_VALUE_POW10 8
#define PI (double)3.1415926535
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
	printf("This program is designed to calculate angles of a triangle by lengths of its sides.\n");
}

void incorrectInputMessage(char error[])
{
	printf("Incorrect input: %s\n\n", error);
}

void inputAwaitingMessage()
{
	printf("Please, write triangle sides in range (0, 1e%d]\nor enter an empty line to exit: ", MAX_INPUT_VALUE_POW10);
}

_Bool checkRange(double number)
{
	return number <= (double)maxInputValue() && number > 0;
}

_Bool checkRangeArray(double arr[], int size)
{
	int res = 1;
	for (int i = 0; i < size; i++)
	{
		res *= checkRange(arr[i]);
	}
	return res;
}

_Bool isTriangle(double a, double b, double c)
{
	return (a + b) > c && (a + c) > b && (b + c) > a;
}

// calculate the angle of a triangle
double getAngle(double side1, double side2, double opposite)
{
	return acos((POW2(side1) + POW2(side2) - POW2(opposite)) / (2 * side1 * side2));
}

void printAngleInDeg(double rad)
{
	double deg = (rad * 180) / PI;
	int fullDegrees = (int)deg;
	int fullMinutes = (int)((deg - fullDegrees) * 60);
	int fullSeconds = (int)(((deg - fullDegrees) * 60 - fullMinutes) * 60);
	printf("%d %d'%d\"", fullDegrees, fullMinutes, fullSeconds);
}

void calculateAndPrintAngles(double a, double b, double c)
{
	double angle[3];
	angle[0] = getAngle(a, b, c);
	angle[1] = getAngle(a, c, b);
	angle[2] = PI - angle[0] - angle[1];
	printf("Approximate angles of a given triangle are: ");
	for (int i = 0; i < 3; i++)
	{
		printAngleInDeg(angle[i]);
		if (i < 2)
		{
			printf(", ");
		}
	}
	printf("\n\n");
}

void mainInterfaceLoop()
{
	_Bool userExitSignal = 0;
	char ioBuffer[IO_BUFFER_LIMIT];
	char leftover;
	double triple[3];
	while (!userExitSignal)
	{
		inputAwaitingMessage();
		fgets(ioBuffer, IO_BUFFER_LIMIT, stdin);

		if (isEmptyLine(ioBuffer))
		{
			userExitSignal = 1;
			continue;
		}

		if (sscanf_s(ioBuffer, "%lf %lf %lf %c", &triple[0], &triple[1], &triple[2], &leftover) != 3)
		{
			incorrectInputMessage("input has too many numbers and/or one of them could not be converted");
		}
		else if (!checkRangeArray(triple, 3))
		{
			incorrectInputMessage("one (or more) numbers are not in the range");
		}
		else if (!isTriangle(triple[0], triple[1], triple[2]))
		{
			incorrectInputMessage("triangle with such sides does not exist");
		}
		else
		{
			calculateAndPrintAngles(triple[0], triple[1], triple[2]);
		}
	}
}
int main()
{
	greetingsMessage();
	mainInterfaceLoop();
	
	return 0;
}
#include <stdio.h>
#include <stdbool.h>

bool isNumbersPythogoreanTriple(int FirstNum, int SecondNum, int ThirdNum);
bool greatestCommonDivisor(int firstNumber, int secondNumber);
bool isNumbersPrimitive(int* arr);
void answer(int* arr);

int main()
{
	printf("This program detects if numbers are a Pythogorean triple and checks if they are prime Pythogorean triple\n\n");
	int arrayOfNumbers[3];
	char check;
	float checkNatural;

	for (int i = 0; i < 3; i++)
	{
		printf("Input the %d number of Pythogorean triple(It must be natural): ", i + 1);

		if ((scanf_s("%f%c", &checkNatural, &check) != 2) || (check != '\n'))
		{
			printf("\nError: you enter incorrect data(letters or symbols)\nPlease, try again\n");
			return 0;
		}

		if (checkNatural != (int)checkNatural)
		{
			printf("\nError: you entered a fractional number\nPlease try again\n");
			return 0;
		}

		if (checkNatural <= 0)
		{
			printf("\nError: you entered an unnatural number\nPlease, try again\n");
			return 0;
		}

		arrayOfNumbers[i] = (int)checkNatural;	
	}

	printf("\n");

	if (isNumbersPythogoreanTriple(arrayOfNumbers[0], arrayOfNumbers[1], arrayOfNumbers[2]))
	{
		answer(&arrayOfNumbers);
	}
	else if ((isNumbersPythogoreanTriple(arrayOfNumbers[0], arrayOfNumbers[2], arrayOfNumbers[1])))
	{
		answer(&arrayOfNumbers);
	}
	else if ((isNumbersPythogoreanTriple(arrayOfNumbers[2], arrayOfNumbers[1], arrayOfNumbers[0])))
	{
		answer(&arrayOfNumbers);
	}
	else
	{
		printf("Your numbers are not Pythogorean triple\n");
	}
	return 0;
}

bool isNumbersPythogoreanTriple(int firstNum, int secondNum, int thirdNum)
{
	if (firstNum*firstNum + secondNum*secondNum == thirdNum*thirdNum)
	{
		return true;
	}
	else
	{
		return false;
	}
}
bool greatestCommonDivisor(int firstNumber, int secondNumber)
{
	while (firstNumber != secondNumber)
	{
		if (firstNumber > secondNumber)
		{
			firstNumber = firstNumber - secondNumber;
		}
		else
		{
			secondNumber = secondNumber - firstNumber;
		}
	}
	if (firstNumber == 1)
	{
		return true;
	}
	else
	{
		return false;
	}
}
bool isNumbersPrimitive(int* arr)
{
	if (greatestCommonDivisor(arr[0], arr[1]) && greatestCommonDivisor(arr[0], arr[2]) && greatestCommonDivisor(arr[2], arr[1]))
	{
		return true;
	}
	else
	{
		return false;
	}
}
void answer(int* arr)
{
	if (isNumbersPrimitive(arr))
	{
		printf("Your numbers form a primitive Pythagorean triple\n");
	}
	else
	{
		printf("Your numbers form a non-primitive Pythagorean triple\n");
	}
}
#include <stdio.h>
#include <stdbool.h>

bool isNumbersPythogoreanTriple(int firstNumber, int secondNumber, int thirdNumber);

int gcd(int firstNumber, int secondNumber);

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

		while ((scanf_s("%f%c", &checkNatural, &check) != 2) || (check != '\n') || checkNatural <= 0 || checkNatural != (int)checkNatural)
		{
			printf("\nError: you enter incorrect data. You have to input natural number. \nPlease, try again\n");
			
			if (i > 0 && checkNatural == arrayOfNumbers[i-1] && check == '\n')
				check = '\0';

			while (check != '\n')
				scanf_s("%c", &check);

			check = '\0';
			printf("\nInput the %d number of Pythogorean triple(It must be natural): ", i + 1);
		}
		arrayOfNumbers[i] = (int)checkNatural;	
	}

	printf("\n");

	if (isNumbersPythogoreanTriple(arrayOfNumbers[0], arrayOfNumbers[1], arrayOfNumbers[2]))
		answer(&arrayOfNumbers);
	else
		printf("Your numbers are not Pythogorean triple\n");

	return 0;
}

bool isNumbersPythogoreanTriple(int firstNumber, int secondNumber, int thirdNumber)
{
	if (firstNumber * firstNumber + secondNumber * secondNumber == thirdNumber * thirdNumber)
		return true;
	else if (firstNumber * firstNumber + thirdNumber * thirdNumber == secondNumber * secondNumber)
		return true;
	else if (secondNumber * secondNumber + thirdNumber * thirdNumber == firstNumber * firstNumber)
		return true;
	else
		return false;
}

int gcd(int firstNumber, int secondNumber)
{
	while (firstNumber != secondNumber)
	{
		if (firstNumber > secondNumber)
			firstNumber = firstNumber - secondNumber;
		else
			secondNumber = secondNumber - firstNumber;
	}
		return firstNumber;
}

bool isNumbersPrimitive(int* arr)
{
	if (gcd(arr[0], arr[1]) == 1 && gcd(arr[0], arr[2]) == 1 && gcd(arr[2], arr[1]) == 1)
		return true;
	else
		return false;
}

void answer(int* arr)
{
	if (isNumbersPrimitive(arr))
		printf("Your numbers form a primitive Pythagorean triple\n");
	else
		printf("Your numbers form a non-primitive Pythagorean triple\n");
}
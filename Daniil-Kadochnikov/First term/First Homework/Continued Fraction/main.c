#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>
#include <stdlib.h>



int checkError(double* pnumber, int *operation)
{
	if (*operation == 1 && fgetc(stdin) == '\n')
	{
		if (*pnumber > 0)
		{
			if ((int)*pnumber == *pnumber)
			{
				if (sqrt(*pnumber) != (int)sqrt(*pnumber))
				{
					return 0;
				}
				else
				{
					printf("\nYou entered a number that is a square of another number, try again, please.\n\n");
					return 1;
				}
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

double userInput(double*pnumber)
{
	int operation;
	do
	{
		fseek(stdin, 0, 0);
		printf("Enter the positive integer which isn't a square of another integer (numbers 1, 4, 9, 16 ... doesn't fit).\nPositive integer = ");
		operation = scanf("%lf", pnumber);
	} while (checkError(pnumber, &operation));
	return *pnumber;
}

int chain(double* pnumber)
{
	int originalNumber = (int)*pnumber;
	int a0 = (int)sqrt(originalNumber);
	int remains = 0; 
	int aNumber = a0, denominator = 1, count = 0; 
	printf("\nThe chain is [%d;", a0);
	do
	{
		remains = aNumber * denominator - remains;
		denominator = (originalNumber - remains * remains) / denominator;
		aNumber = (a0 + remains) / denominator;
		printf(" %d,", aNumber);
		count++;
	} while (denominator != 1);
	printf("]\nPeriod is equal to %d.\n", count);
	return 0;
}

int main()
{
	printf("The programm evaluates the chain [a0; a1 ... ai] and counts the period for positive every integer\nthat is not a square of another number.\n\n");
	double number;
	userInput(&number);
	chain(&number);
	return 0;
}
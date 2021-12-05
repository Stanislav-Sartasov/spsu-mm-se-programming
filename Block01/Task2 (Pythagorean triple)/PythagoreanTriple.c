#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <math.h>
#include <stdbool.h>


bool isPythagoreanTriple(int a, int b, int c)
{
	if (pow(a, 2) + pow(b, 2) == pow(c, 2) || pow(a, 2) + pow(c, 2) == pow(b, 2) || pow(c, 2) + pow(b, 2) == pow(a, 2))
	{
		return true;
	}
	return false;
}

int isCoprimeTwo(int a, int b)
{
	while (a != b && a != 0 && b != 0)
	{
		if (a > b)
		{
			a %= b;
		}
		else
		{
			b %= a;
		}
	}
	return a + b;
}

bool isCoprimeThree(int a, int b, int c)
{
	if (isCoprimeTwo(isCoprimeTwo(a, b), c) == 1)
	{
		return true;
	}
	return false;
}

bool isPPT(int a, int b, int c) //PPT - Primitive Pythagorean Triple
{
	if (isPythagoreanTriple(a, b, c) && (isCoprimeThree(a, b, c)))
	{
		return true;
	}
	return false;
}

int main()
{
	double legA, legB, legC;
	int count;
	printf("The programm defines given numbers for Pyathagorean Triple.\n");
	do
	{
		printf("Input three positive numbers devided by space. Use dot for float numbers: ");
		count = scanf("%lf%lf%lf", &legA, &legB, &legC);
		while (getchar() != '\n');
	} while (!(count == 3 && legA > 0 && legB > 0 && legC > 0));

	if (isPythagoreanTriple(legA, legB, legC) && isPPT(legA, legB, legC))
	{
		printf("The given numbers are Primitive Pythagorean Triple.");
	}
	else if (isPythagoreanTriple(legA, legB, legC) && !isPPT(legA, legB, legC))
	{
		printf("The given numbers are not coprime, but Pythagorean Triple.");
	}
	else if (!isPythagoreanTriple(legA, legB, legC) && isPPT(legA, legB, legC))
	{
		printf("The given numbers are coprime, but not Pythagorean Triple.");
	}
	else
	{
		printf("The given numbers are both not coprime and Pythagorean Triple.");
	}
	return 0;
}

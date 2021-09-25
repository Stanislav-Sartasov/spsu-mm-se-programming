#pragma once;
#include <stdio.h>
#include <math.h>
#include "inputAndTools.h"

// If input converted to INT, it returns INT
// else it asks to try again
long int get_int() 
{
	char str[80];
	char ch;

	long int rezult = 0;
	int chek = 1;
	int k = -1;
	int sign = 1;

	printf("> ");

	do
	{
		k++;
		ch = getchar();
		str[k] = ch;
	} while (ch != '\n' && k != 79);

	if (k == 79)
	{
		printf("Number is too big. Try again\n\n");
		return get_int();
	}
	if (k == 0)
	{
		printf("Input something\n\n");
		return get_int();
	}

	for (int i = 0; i < k; i++)
	{
		if (str[0] == '-' && chek)
		{
			sign = -1;
			chek = 0;
			continue;
		}
		if ('0' <= str[i] && str[i] <= '9')
		{
			rezult = rezult * 10 + (int)str[i] - 48;
			continue;
		}

		printf("Input error. Try again\n\n");
		return get_int();
	}

	return rezult * sign;
}


// If input converted to DOUBLE, it returns DOUBLE
// else it asks to try again
// Can read number with '.' and with ','
long double get_double()
{
	char str[80];
	char ch;

	long double rezult = 0;
	int k = -1;
	int newk;
	int sign = 1;
	int chekMinus = 1;
	int chekPoint = 0;

	printf("> ");

	do
	{
		k++;
		ch = getchar();
		str[k] = ch;

	} while (ch != '\n' && k != 79);

	if (k == 79)
	{
		printf("Number is too big. Try again\n\n");
		return get_int();
	}
	if (k == 0)
	{
		printf("Input something\n\n");
		return get_int();
	}

	for (int i = 0; i < k; i++)
	{
		if (str[0] == '-' && chekMinus)
		{
			sign = -1;
			chekMinus = 0;
			continue;
		}
		if ('0' <= str[i] && str[i] <= '9')
		{
			rezult = rezult * 10 + (int)str[i] - 48;
			continue;
		}
		if ((str[i] == '.' || str[i] == ',') && (i != 0) && (i != k - 1))
		{
			chekPoint = 1;
			newk = i + 1;
			break;
		}

		printf("Input error. Try again\n\n");
		return get_double();
	}

	if (chekPoint)
	{
		for (int i = newk; i < k; i++)
		{
			if ('0' <= str[i] && str[i] <= '9')
			{
				rezult += ((int)str[i] - 48) * pow(10, newk - i - 1);
				continue;
			}

			printf("Input error. Try again\n\n");
			return get_double();
		}
	}

	return rezult * sign;
}

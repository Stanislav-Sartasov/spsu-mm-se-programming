#pragma once;
#include <stdio.h>
#include <math.h>
#include "input_and_tools.h"

// if input converted to INT, it returns INT
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

	printf("\n");
	return rezult * sign;
}


// if input converted to DOUBLE, it returns DOUBLE
// else it asks to try again
// can read number with '.' and with ','
long double get_double()
{
	char str[80];
	char ch;

	long double rezult = 0;
	int k = -1;
	int newk;
	int sign = 1;
	int chek_minus = 1;
	int chek_point = 0;

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
		if (str[0] == '-' && chek_minus)
		{
			sign = -1;
			chek_minus = 0;
			continue;
		}
		if ('0' <= str[i] && str[i] <= '9')
		{
			rezult = rezult * 10 + (int)str[i] - 48;
			continue;
		}
		if ((str[i] == '.' || str[i] == ',') && (i != 0) && (i != k - 1))
		{
			chek_point = 1;
			newk = i + 1;
			break;
		}

		printf("Input error. Try again\n\n");
		return get_double();
	}

	if (chek_point)
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

	printf("\n");
	return rezult * sign;
}


// qick sort
void sort_of_array(int* arr, int size)
{
	// pointers to first and last elements
	int first = 0;
	int last = size - 1;

	// middle element
	int middle = arr[size / 2];

	// division of array and swap elements
	do
	{
		while (arr[first] < middle)
		{
			first++;
		}
		while (arr[last] > middle)
		{
			last--;
		}

		if (first <= last)
		{
			int c = arr[last];
			arr[last] = arr[first];
			arr[first] = c;

			first++;
			last--;
		}
	} while (first <= last);

	// recurrent calls for remaining halves
	if (last > 0)
	{
		sort_of_array(arr, last + 1);
	}
	if (first < size)
	{
		sort_of_array(&arr[first], size - first);
	}
}
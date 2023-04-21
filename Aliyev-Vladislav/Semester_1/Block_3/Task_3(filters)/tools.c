#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include "tools.h"

int sort(int* a)
{
	int tmp;
	for (int i = 1; i < 9; i++)
	{
		for (int j = 1; j < 9; j++)
		{
			if (a[j] > a[j - 1])
			{
				tmp = a[j];
				a[j] = a[j - 1];
				a[j - 1] = tmp;
			}
		}
	}
	return a[4];
}

int value_test(int value, char* filter)
{
	if (value != 4)
	{
		printf("You have to submit three arguments to the input.\n");
		return 0;
	}
	if ((strcmp(filter, "gauss") != 0) && (strcmp(filter, "median") != 0) && (strcmp(filter, "sobelX") != 0)
		&& (strcmp(filter, "sobelY") != 0) && (strcmp(filter, "gray") != 0))
	{
		printf("You have entered the wrong filter name. Available filters: gauss, median, sobelX, sobelY, gray.\n");
		return 0;
	}
	return 1;
}
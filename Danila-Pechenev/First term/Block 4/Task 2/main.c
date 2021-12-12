#include <stdlib.h>
#include <stdio.h>
#include "mymem.h"


int main()
{
	printf("Running tests of my_malloc, my_free and my_realloc functions:\n");

	init();

	int* first_array = my_malloc(1000 * sizeof(int));

	for (int i = 0; i < 1000; i++)  // Filling the array
	{
		first_array[i] = i * 2;
	}

	for (int i = 0; i < 1000; i++)  // Testing whether we can access the array and get the correct data
	{
		if (first_array[i] != i * 2)
		{
			printf("Test 1 failed.\n");
			break;
		}
		if (i == 1000 - 1)
		{
			printf("Test 1 passed.\n");
		}
	}

	long long* second_array = my_malloc(1000 * sizeof(long long));

	for (long long i = 0; i < 1000; i++)  // Filling a small array
	{
		second_array[i] = i * i;
	}

	second_array = my_realloc(second_array, 1000000 * sizeof(long long));

	for (long long i = 1000; i < 1000000; i++)  // Filling a large array
	{
		second_array[i] = i * i;
	}

	for (long long i = 0; i < 1000000; i++)  // Testing whether we can access the array and get the correct data
	{
		if (second_array[i] != i * i)
		{
			printf("Test 2 failed.\n");
			break;
		}
		if (i == 1000000 - 1)
		{
			printf("Test 2 passed.\n");
		}
	}

	my_free(first_array);
	my_free(second_array);

	int** third_array = my_malloc(25 * sizeof(int*));
	for (int i = 0; i < 25; i++)
	{
		third_array[i] = my_malloc(25 * sizeof(int));
	}

	for (int i = 0; i < 25; i++)  // Filling a two-dimensional array
	{
		for (int j = 0; j < 25; j++)
		{
			third_array[i][j] = (i + j + 10) * 10;
		}
	}

	for (int i = 0; i < 25; i++)  // Testing whether we can access the array and get the correct data
	{
		for (int j = 0; j < 25; j++)
		{
			if (third_array[i][j] != (i + j + 10) * 10)
			{
				printf("Test 3 failed.\n");
				break;
			}
			if (i == 25 - 1 && j == 25 - 1)
			{
				printf("Test 3 passed.\n");
			}
		}
	}
	printf("\n");

	for (int i = 0; i < 25; i++)  // Printing an array
	{
		for (int j = 0; j < 25; j++)
		{
			printf("%d ", third_array[i][j]);
		}
		printf("\n");
	}

	for (int i = 0; i < 25; i++)
	{
		my_free(third_array[i]);
	}
	my_free(third_array);

	destroy();
}
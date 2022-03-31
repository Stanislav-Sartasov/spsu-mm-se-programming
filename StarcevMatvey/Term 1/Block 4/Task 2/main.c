#define _CRT_NO_SECURE_WARNINGS
#include <stdio.h>
#include "manager.h"
#include "input_and_tools.h"

int main()
{
	printf("Realization of built-in memory management functions\n\n");
	init();

	// malloc
	int size;
	do
	{
		printf("Enter the size of the array\n");
		size = get_int();
	} while (size < 1);

	int* arr = (int*)my_malloc(size * sizeof(int));
	if (!(arr))
	{
		printf("Out of memory");
		return 0;
	}
	else
	{
		printf("Memory allocated\n\n");
	}

	for (int i = 0; i < size; i++)
	{
		printf("Input your number number %d in array\n", i + 1);
		arr[i] = get_int();
	}
	printf("Your numbers:\n");
	for (int i = 0; i < size; i++)
	{
		printf("%d ", arr[i]);
	}


	// realloc
	printf("\n\n");
	do
	{
		printf("Enter new size of your array\n");
		size = get_int();
	} while (size < 1);

	arr = (int*)my_realloc(arr, size);
	if (!(arr))
	{
		printf("Out of memory");
		return 0;
	}
	else
	{
		printf("Memory reallocated\n\n");
		printf("Now array can contains %d elements\n\n", size);
	}


	// calloc
	printf("Also array can be declared with zeros:\n");
	arr = (int*)my_calloc(size * sizeof(int));
	for (int i = 0; i < size; i++)
	{
		printf("%d ", arr[i]);
	}


	// free
	printf("\n\nFreeing up allocated memory\n");
	my_free(arr);
	printf("Freeing was successful\n");

	free_init();
	return 0;
}
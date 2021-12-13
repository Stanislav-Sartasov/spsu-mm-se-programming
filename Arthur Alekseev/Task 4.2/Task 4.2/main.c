#include <stdio.h>
#include "memory_manager.h"

int main()
{
	printf("This program tests my malloc, realloc and free\n");
	printf("Size of memory is 256 bytes, it is 64 integers\n");

	// Some testing code
	// Init global objects for memory manager
	init();

	printf("\n\nAllocating an integer array with 400 elements\n");
	int* array1 = (int*)my_malloc(sizeof(int) * 400);
	if (array1 == NULL)
		printf("Allocation failed (Not enough space)");
	else
		printf("Allocated");

	printf("\n\nAllocating an integer array with 40 elements\n");
	array1 = (int*)my_malloc(sizeof(int) * 40);
	// Filling an array with -1
	for (int i = 0; i < 40; i++)
		array1[i] = -1;
	for (int i = 0; i < 40; i++)
		printf("%d ", array1[i]);


	printf("\n\nAllocating an integer array with 15 elements\n");
	int* array2 = (int*)my_malloc(sizeof(int) * 15);
	// Filling an array with -2
	for (int i = 0; i < 15; i++)
		array2[i] = -2;
	for (int i = 0; i < 15; i++)
		printf("%d ", array2[i]);


	printf("\n\nFreeing the first array");
	my_free(array1);


	printf("\n\nAllocating an integer array with 20 element\n");
	int* array_to_realloc = (int*)my_malloc(sizeof(int) * 20);
	// Filling an array with -3
	for (int i = 0; i < 20; i++)
		array1[i] = -3;
	for (int i = 0; i < 20; i++)
		printf("%d ", array_to_realloc[i]);


	printf("\n\nReallocating the array1\n");
	my_realloc(array_to_realloc, sizeof(int) * 32);
	// Filling an array with -4
	for (int i = 0; i < 32; i++)
		array1[i] = -4;
	for (int i = 0; i < 32; i++)
		printf("%d ", array_to_realloc[i]);


	printf("\n\nAllocating an integer array with 5 elements\n");
	int* array3 = (int*)my_malloc(sizeof(int) * 5);
	// Filling an array with -5
	for (int i = 0; i < 5; i++)
		array3[i] = -5;
	for (int i = 0; i < 5; i++)
		printf("%d ", array3[i]);

	printf("\n\nThe demo is over");
	// Dispose global objects for memory manager
	dispose();
	return 0;
}
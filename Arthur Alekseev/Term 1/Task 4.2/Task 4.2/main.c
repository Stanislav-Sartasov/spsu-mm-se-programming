#include <stdio.h>
#include "memory_manager.h"

int main()
{
	printf("This program tests my malloc, realloc and free\n");
	printf("Size of memory is 256 bytes, it is 64 integers\n");
	
	init();
	printf("Memory begins from %d\n", memory_begin);

	printf("\n\nAllocating an integer array with 400 elements\n");
	int* array1 = (int*)my_malloc(sizeof(int) * 400);
	if (array1 == NULL)
		printf("Allocation failed (Not enough space)");
	else
		printf("Allocated");

	array1 = (int*)my_malloc(sizeof(int) * 40);
	printf("\n\nAllocating an integer array with 40 elements (to %d)\n", array1);
	// Filling an array with -1
	for (int i = 0; i < 40; i++)
		array1[i] = -1;
	for (int i = 0; i < 40; i++)
		printf("%d ", array1[i]);
		
	
	int* array2 = (int*)my_malloc(sizeof(int) * 15);
	printf("\n\nAllocating an integer array with 15 elements (to %d)\n", array2);
	// Filling an array with -2
	for (int i = 0; i < 15; i++)
		array2[i] = -2;
	for (int i = 0; i < 15; i++)
		printf("%d ", array2[i]);

	printf("\n\nFreeing the first array");
	my_free(array1);


	int* array_to_realloc = (int*)my_malloc(sizeof(int) * 20);
	printf("\n\nAllocating an integer array with 20 elements (to %d)\n", array_to_realloc);
	// Filling an array with -3
	for (int i = 0; i < 20; i++)
		array1[i] = -3;
	for (int i = 0; i < 20; i++)
		printf("%d ", array_to_realloc[i]);

	printf("\n\nReallocating the array (to %d)\n", array_to_realloc);
	my_realloc(array_to_realloc, sizeof(int) * 32);
	// Filling an array with -4
	for (int i = 0; i < 32; i++)
		array1[i] = -4;
	for (int i = 0; i < 32; i++)
		printf("%d ", array_to_realloc[i]);

	int* array3 = (int*)my_malloc(sizeof(int) * 5);
	printf("\n\nAllocating an integer array with 5 elements (to %d)\n", array3);
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
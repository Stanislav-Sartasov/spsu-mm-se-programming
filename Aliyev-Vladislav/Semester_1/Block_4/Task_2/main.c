#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "mymem.h"

int main()
{
	init();

	printf("This program demonstrates my realization of memory allocation functions (malloc, realloc and free).\n\n");

	printf("Allocating the first memory block:\n");

	int* a = my_malloc(5 * sizeof(int));
	for (int i = 0; i < 5; i++)
	{
		a[i] = 5;
		printf("%d ", a[i]);
	}

	printf("\n\nAllocating the second memory block:\n");

	int* b = my_malloc(4 * sizeof(int));
	for (int i = 0; i < 4; i++)
	{
		b[i] = 4;
		printf("%d ", b[i]);
	}

	printf("\n\nAllocating the third memory block:\n");

	int* c = my_malloc(10 * sizeof(int));
	for (int i = 0; i < 10; i++)
	{
		c[i] = 10;
		printf("%d ", c[i]);
	}

	printf("\n\nFreeing all.\n\n");

	my_free(a);
	my_free(b);
	my_free(c);

	printf("Adding another block:\n");

	a = my_malloc(20 * sizeof(int));
	for (int i = 0; i < 20; i++)
	{
		a[i] = 123;
		printf("%d ", a[i]);
	}

	printf("\n\nReallocating memory:\n");

	a = my_realloc(a, 10 * sizeof(int));
	for (int i = 0; i < 10; i++)
	{
		a[i] = 234;
		printf("%d ", a[i]);
	}
	printf("\n");

	printf("\nReallocating it again:\n");

	a = my_realloc(a, 20 * sizeof(int));
	for (int i = 0; i < 20; i++)
	{
		a[i] = 345;
		printf("%d ", a[i]);
	}

	printf("\n\nFreeing up allocated memory.");

	my_free(a);

	end();

	return 0;
}
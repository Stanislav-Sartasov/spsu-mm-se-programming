#include <stdio.h>

#include "mmanager/mmanager.h"

int main()
{
	init();

	printf("This program demonstrates my realization of memory allocation functions (malloc, realloc and free).\n\n");
	printf("O - the header\n# - the data\n\n");

	visualize();

	printf("Allocating the first memory block:\n");
	int* a = (int*)my_malloc(2 * sizeof(int));
	visualize();

	printf("Allocating the second memory block:\n");
	double* b = (double*)my_malloc(2 * sizeof(double));
	visualize();

	printf("Freeing the first memory block:\n");
	my_free(a);
	visualize();

	printf("Adding another block:\n");
	int* c = (int*)my_malloc(1 * sizeof(int));
	visualize();

	printf("Writing 123 to it.\n");
	c[0] = 123;

	printf("Reallocating it:\n");
	c = (int*)my_realloc(c, 2 * sizeof(int));
	visualize();

	printf("Writing 228 to it.\n");
	c[1] = 228;

	printf("Reallocating it one more time:\n");
	c = (int*)my_realloc(c, 3 * sizeof(int));
	visualize();

	printf("Reading the values: %d %d.\n\n", c[0], c[1]);

	printf("Trying to allocate too much memory:\n");
	void* d = my_malloc(42);
	if (d == NULL)
		printf("Not enough memory!\n");
	visualize();

	printf("Freeing all:\n");
	my_free(b);
	my_free(c);
	visualize();

	printf("Done!\n");

	end();

	return 0;
}
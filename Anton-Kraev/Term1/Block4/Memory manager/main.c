#include <stdio.h>
#include "manager.h"

void test();

int main()
{
	init();
	printf("Running tests of my memory manager(my_malloc, my_free, my_realloc):\n\n");
	test();
	free_memory();
	return 0;
}

void test()
{
	printf("Allocate 4000 bytes\n");
	char* first = (char*)my_malloc(sizeof(char) * 4000);
	if (!first)
		printf("Failed to allocate %u bytes of memory\n", sizeof(char) * 4000);
	else
		printf("%u bytes have been successfully allocated\n", sizeof(char) * 4000);
	
	int* second = (int*)my_malloc(sizeof(int) * 20);
	if (!second)
		printf("\nFailed to allocate %u bytes of memory\n", sizeof(int) * 20);
	else
		printf("\n%u bytes have been successfully allocated, " 
			"now fill the array with numbers and output it:\n", sizeof(int) * 20);
	for (int i = 0; i < 20; i++)
	{
		second[i] = i;
		printf("%d ", second[i]);
	}

	printf("\n\nNow let's free up the memory allocated for the first array "
		"and allocate additional memory for the second array\n");
	my_free(first);	
	second = my_realloc(second, sizeof(int) * 100);
	if (!second)
		printf("Failed to reallocate %u bytes\n", sizeof(int) * 100);
	else
		printf("Additional memory has been successfully allocated, "
			"now add new values to the array and output the entire array:\n");
	for (int i = 20; i < 100; i++)
		second[i] = i;
	for (int i = 0; i < 100; i++)
		printf("%d ", second[i]);

	printf("\n\nAnd free up the allocated memory\n");
	my_free(second);
}
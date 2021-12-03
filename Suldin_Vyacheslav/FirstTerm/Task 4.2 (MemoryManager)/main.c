#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include "my_mem_man.h"

int main()
{
	printf("Memory manager. Available functions: my_malloc, reaaloc, free. More in header.\n");

	init();

	int first_size = 3, second_size = 5;

	char* test_first = (char*)my_malloc(first_size * (sizeof(char)));

	if (test_first != NULL)
		printf("allocation success, %d is yours on %p\n", first_size, test_first);
	else
	{
		printf("seems like out of memory!");
		return -1;
	}

	for (int i = 0; i < first_size; i++)
	{
		test_first[i] = i+1;
		printf("allocated: >>>%d\n", test_first[i]);
	}

	test_first = (char*)my_realloc(test_first, second_size);

	if (test_first != NULL)
		printf("reallocation success, %d is yours on %p\n", second_size, test_first);
	else
	{
		printf("seems like out of memory!");
		return;
	}
	for (int i = 0; i < second_size; i++)
		printf("reallocated: >>>%d\n", test_first[i]);

	my_free(test_first);
	printf("test is free now\n");

	char* test_second = (char*)my_malloc(first_size);
	printf("pointer to your new memory>>>%p", test_second);

	my_free(test_second);

	init_free();
	return 0;
}
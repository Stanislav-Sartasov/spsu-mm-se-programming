#include "manager_functions.h"
#include <stdlib.h>

void malloc_test()
{
	printf("Demonstration of malloc realization, allocate memory for 10 elements, and demonstrate free_function realization.\n");
	int* test_arr = malloc_realization(10 * sizeof(int));
	for (int i = 0; i < 10; i++)
	{
		test_arr[i] = rand();
	}

	for (int i = 0; i < 10; i++)
	{
		printf("%d ", test_arr[i]);
	}
	printf("\n");

	free_realization(test_arr);
}

void realloc_test()
{
	printf("Demonstration of realloc realization. At first - allocate memory for 10 elements.\n");
	int* test_arr = malloc_realization(10 * sizeof(int));
	for (int i = 0; i < 10; i++)
	{
		test_arr[i] = rand();
	}

	for (int i = 0; i < 10; i++)
	{
		printf("%d ", test_arr[i]);
	}
	printf("\n");

	printf("Use realloc and change the size by 5 elements.\n");

	test_arr = realloc_realization(test_arr, 5 * sizeof(int));

	for (int i = 0; i < 5; ++i)
	{
		printf("%d ", test_arr[i]);
	}
	printf("\n");

	printf("The next element after applying realloc may not be the one that was in its place before.\n"
		"This is next element->");
	printf("%d ", test_arr[6]);
	printf("\n");

	printf("Use realloc and change the size by 20 elements.\n");

	test_arr = realloc_realization(test_arr, 20 * sizeof(int));

	for (int i = 0; i < 15; i++)
	{
		test_arr[i + 5] = rand();
	}

	for (int i = 0; i < 20; i++)
	{
		printf("%d ", test_arr[i]);
	}
	printf("\n");

	free_realization(test_arr);
}

int main()
{
	printf("This programm show realisation MemoryManager(malloc, realloc, free)\n\n");

	malloc_test();
	realloc_test();

	free_init_space();
	return 0;
}
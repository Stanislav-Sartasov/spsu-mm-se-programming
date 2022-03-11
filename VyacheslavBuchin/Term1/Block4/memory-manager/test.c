//
// Created by Вячеслав Бучин on 17.12.2021.
//

#include <stdio.h>

#include "test.h"
#include "memory/my_memory.h"

int alloc_test()
{
	int* a = my_malloc(7 * sizeof(int));
	if (a == NULL)
		return 1;
	my_free(a);
	return 0;
}

int free_test()
{
	int* a = my_malloc(1000 * sizeof(int));
	my_free(a);
	a = my_malloc(1000 * sizeof(int));
	if (a == NULL)
		return 1;
	my_free(a);
	return 0;
}

int realloc_no_rebase_test()
{
	int* a1 = my_malloc(10 * sizeof(int));
	int* a2 = my_malloc(10 * sizeof(int));
	int* a3 = my_malloc(10 * sizeof(int));

	int* old_a2 = a2;
	a2 = my_realloc(a2, 5 * sizeof(int));

	if (a2 != old_a2)
	{
		my_free(a1);
		my_free(old_a2);
		my_free(a3);
		return 1;
	}

	my_free(a1);
	my_free(a2);
	my_free(a3);
	return 0;
}

int realloc_rebase_test()
{
	int* a1 = my_malloc(10 * sizeof(int));
	int* a2 = my_malloc(10 * sizeof(int));
	int* a3 = my_malloc(10 * sizeof(int));

	int* old_a2 = a2;
	a2 = my_realloc(a2, 15 * sizeof(int));

	if (a2 == old_a2)
	{
		my_free(a1);
		my_free(old_a2);
		my_free(a3);
		return 1;
	}

	my_free(a1);
	my_free(a2);
	my_free(a3);
	return 0;
}

int alloc_between_blocks_test()
{
	int* a1 = my_malloc(10 * sizeof(int));
	int* a2 = my_malloc(10 * sizeof(int));
	int* a3 = my_malloc(10 * sizeof(int));

	my_free(a2);

	a2 = my_malloc(5 * sizeof(int));
	int result = a2 == NULL || !(a1 < a2 && a2 < a3);

	my_free(a1);
	if (a2 != NULL)
		my_free(a2);
	my_free(a3);
	return result;
}

void run_test(const char* name, int (*test)())
{
	printf("The \'%s\' is running...\n", name);
	if (test())
		printf("\tTest \'%s\' failed\n", name);
	else
		printf("\tTest \'%s\' passed\n", name);
	printf("\n");
}

void run_tests()
{
	init();

	run_test("allocation", alloc_test);
	run_test("free", free_test);
	run_test("realloc with no rebase", realloc_no_rebase_test);
	run_test("realloc with rebase", realloc_rebase_test);
	run_test("allocation between two blocks", alloc_between_blocks_test);

	give_back();
}
#include <stdio.h>
#include "memory_manager.h"

int check_arr(void *arr, size_t size)
{
	if (arr != NULL)
	{
		printf("Allocated %d bytes successfully\n", size);
		return 1;
	}
	printf("Out of memory!\n");
	return 0;
}

int main()
{
	printf("This is a realization of a memory manager.\n");
	init();
	int *a_arr = (int*) my_malloc(10 * sizeof(int));

	if (!check_arr(a_arr, 10 * sizeof(int)))
		return 1;

	for (int i = 0; i < 10; i++)
		a_arr[i] = i;

	printf("a_arr = ");
	for (int i = 0; i < 10; i++)
		printf("%d ", a_arr[i]);
	printf("\n");


	int *b_arr = (int*) my_malloc(100 * sizeof(int));
	if (!check_arr(b_arr, 100 * sizeof(int)))
		return 1;

	for (int i = 0; i < 100; i++)
		b_arr[i] = i ^ (i - (1 << i));

	printf("b_arr = ");
	for (int i = 0; i < 100; i++)
		printf("%d ", b_arr[i]);
	printf("\n");

	b_arr = my_realloc(a_arr, 900);
	if (!check_arr(b_arr, 900 * sizeof(int)))
		return 1;

	my_free(b_arr);
	printf("Freed successfully\n");

	b_arr = my_malloc(2034 * sizeof(int));
	if (!check_arr(b_arr, 2034 * sizeof(int)))
		return 1;
	my_free(b_arr);
	my_free(b_arr);
	free_memory();
	return 0;
}
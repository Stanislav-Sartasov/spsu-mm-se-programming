#include <stdio.h>
#include "memory_manager.h"

int main()
{
	init();
	int *arr = (int*) my_malloc(10 * sizeof(int));
	for (int i = 0; i < 10; i++)
		arr[i] = i;
	for (int i = 0; i < 10; i++)
		printf("%d ", arr[i]);
	printf("\n");

	int *brr = (int*) my_malloc((10 * sizeof(int)));

	for (int i = 0; i < 10; i++)
		brr[i] = -i;
	for (int i = 0; i < 10; i++)
		printf("%d ", brr[i]);
	my_free(arr);
	my_free(brr);
	free_memory();
	return 0;
}
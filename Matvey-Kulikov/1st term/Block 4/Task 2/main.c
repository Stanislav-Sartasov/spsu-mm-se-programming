#include "memory_manager.h"
#include <stdio.h>

unsigned int get_number()
{
	printf(">>> ");
	unsigned int num;
	char end;
	int read_result = scanf("%u%c", &num, &end);
	if ((read_result == 2) && (end == '\n'))
	{
		return num;
	}
	else
	{
		printf("Please enter correct number\n");
	}

	while (end != '\n')
	{
		scanf("%c", &end);
	}

	return get_number();
}

int main()
{
	init();
	printf("This program demonstrates memory manager work.\n\n");
	printf("Enter amount of integers for new integer array (non-negative):\n");
	unsigned int len = get_number();
	printf("Allocating space for array of %u integers...\n", len);
	uint32_t* arr_one = (uint32_t*)my_malloc(len*sizeof(int));
	while (!arr_one)
	{
		printf("Unable to allocate such space! Enter new amount:\n");
		len = get_number();
		printf("Allocating space for array of %u integers...\n", len);
		arr_one = (uint32_t*)my_malloc(len*sizeof(int));
	}
	printf("Enter %d integers:\n", len);
	for (int i = 0; i < len; i++)
	{
		arr_one[i] = get_number();
	}
	printf("Your array is: [ ");

	for (int i = 0; i < len; i++)
	{
		printf("%d ", arr_one[i]);
	}
	printf("]\n");
	print_blocks();

	printf("Now enter the size you want your array to be reallocated to (in bytes):\n");
	unsigned int size = get_number();
	void* arr_one_reallocated = my_realloc(arr_one, size);

	if (!arr_one_reallocated)
	{
		printf("Reallocation failed (not enough space)\n");
	}
	print_blocks();

	printf("\nNow freeing space for this array\n");
	my_free(arr_one);
	print_blocks();

	free_heap();

	return 0;
}
#include <stdio.h>
#include "manager.h"

int main()
{
	printf("Realizations of memory manager\n");
	init_memory();

	int flag = 0, correctly_scan;
	printf("Do you want initialize your array by zeros?(1 - yes / 0 - no)");
	do
	{
		correctly_scan = scanf("%d", &flag);
		if (1 != correctly_scan || flag != 0 && flag != 1)
		{
			printf("It's not one or zero\n");
		}
	}
	while (1 != correctly_scan || flag != 0 && flag != 1);

	printf("Enter count of integers in array:\n");
	int size;
	do
	{
		correctly_scan = scanf("%d", &size);
		if (1 != correctly_scan || size <= 0)
		{
			printf("It's not positive integer\n");
		}
	}
	while (1 != correctly_scan || size <= 0);
	int *arr;
	if (flag)
	{
		arr = (int *) my_calloc(size * sizeof(int));
		printf("Your array is:\n");
		for (int i = 0; i < size; i++)
			printf("%d, ", arr[i]);
		printf("isn't?\n");
	}
	else
		arr = (int*)my_malloc(size * sizeof(int));
	if (arr == NULL)
	{
		printf("Out of memory\n");
		return 0;
	}
	else
	{
		printf("Memory allocated\n");
	}

	printf("Enter integers in array:\n");
	for (int i = 0; i < size; i++)
	{
		int num;
		do
		{
			correctly_scan = scanf("%d", &num);
			if (1 != correctly_scan)
			{
				printf("It's not integer\n");
			}
		}
		while (1 != correctly_scan);
		arr[i] = num;
	}
	printf("Your array is:\n");
	for (int i = 0; i < size; i++)
		printf("%d, ", arr[i]);
	printf("isn't?\n");

	int old_size = size;
	printf("Enter new size of array:\n");
	do
	{
		correctly_scan = scanf("%d", &size);
		if (1 != correctly_scan || size <= 0)
		{
			printf("It's not positive integer\n");
		}
	}
	while (1 != correctly_scan || size <= 0);
	arr = my_realloc(arr, size * sizeof(int));
	if (arr == NULL)
	{
		printf("Out of memory\n");
		return 0;
	}
	else
	{
		printf("Memory allocated\n");
	}
	if (size > old_size)
	{
		printf("Enter the following integer:\n");
		for (int i = old_size; i < size; i++)
		{
			int num;
			do
			{
				correctly_scan = scanf("%d", &num);
				if (1 != correctly_scan)
				{
					printf("It's not integer\n");
				}
			}
			while (1 != correctly_scan);
			arr[i] = num;
		}
	}

	printf("Your array is:\n");
	for (int i = 0; i < size; i++)
		printf("%d, ", arr[i]);
	printf("isn't?\n");

	my_free(arr);
	printf("Your array was freeing\n");
	free_memory();
	return 0;
}
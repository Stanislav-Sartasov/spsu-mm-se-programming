#define BUF_SIZE 104888320
#define ALLOCATION 100

#include <stdio.h>
#include <stdlib.h>

char* memory;
int allocated_number = 0;
int heap_address = 0;

typedef struct malloc_info
{
	int address;
	int size;
}malloc_info_t;

malloc_info_t data_info[ALLOCATION] = { 0 };

void init()
{
	memory = (char*)malloc(sizeof(char) * BUF_SIZE);
	memset(memory, 0, BUF_SIZE);
}

void* my_malloc(size_t size)
{
	int index = 0, init_gap = 0, heap_index = 0;
	_Bool flag = 0, init_flag = 0;
	void* address = NULL;
	malloc_info_t temp_info = { 0 };

	if (allocated_number >= ALLOCATION)
	{
		return NULL;
	}

	for (; index < allocated_number; index++)
	{
		if (data_info[index + 1].address != 0)
		{
			init_gap = data_info[0].address - heap_address;
			if (init_gap >= size)
			{
				init_flag = 1;
				break;
			}
			else
			{
				flag = 1;
				break;
			}
		}
	}

	if (flag)
	{
		heap_index = ((data_info[index].address + data_info[index].size) - heap_address);
		for (int j = ALLOCATION - 1; j > index + 1; j--)
			memcpy(&data_info[j], &data_info[j - 1], sizeof(malloc_info_t));
	}
	else if (init_flag)
	{
		heap_index = 0;
		for (int j = ALLOCATION - 1; j > index + 1; j--)
			memcpy(&data_info[j], &data_info[j - 1], sizeof(malloc_info_t));
	}
	else
	{
		if (allocated_number != 0)
			heap_index = ((data_info[index - 1].address + data_info[index - 1].size) - heap_address);
		else
			heap_index = 0;
	}

	address = &memory[heap_index];
	data_info[index].address = heap_address + heap_index;
	data_info[index].size = size;
	allocated_number += 1;

	return address;
}

void my_free(void* ptr)
{
	int copy_data = 0;

	for (int i = 0; i < allocated_number; i++)
	{
		if (ptr == data_info[i].address)
		{
			copy_data = 1;
			allocated_number -= 1;
			break;
		}
	}

	if (copy_data == 1)
	{
		if (allocated_number == ALLOCATION - 1)
		{
			data_info[allocated_number].size = 0;
			data_info[allocated_number].address = 0;
		}
		else
			memcpy(&data_info[allocated_number], &data_info[allocated_number + 1], sizeof(malloc_info_t));
	}
}

void* my_realloc(void* ptr, size_t size)
{
	int old_size = data_info->size;
	void* new_ptr = my_malloc(size);
	my_free(ptr);

	for (int i = 0; i < old_size; i++)
		((char*)new_ptr)[i] = ((char*)ptr)[i];
	return new_ptr;
}

void init_free()
{
	free(memory);
}

int main()
{
	printf("The program implements a memory manager.\n\n");
	init();
	printf("Allocating memory and filling it with integers(0-9):\n");

	int* array = my_malloc(sizeof(int) * 10);
	for (int i = 0; i < 10; i++)
	{
		array[i] = i;
		printf("%d ", array[i]);
	}
	printf("\n");

	printf("Memory allocation, filling and deallocation were successful!\n\n");
	my_free(array);

	printf("Allocating memory and filling it with integers, followed by changing the allocated memory size:\n");

	int* new_array = my_malloc(sizeof(int) * 15);
	printf("Old size(15): ");
	for (int i = 0; i < 15; i++)
	{
		new_array[i] = i;
		printf("%d ", new_array[i]);
	}
	printf("\n");

	new_array = my_realloc(new_array, sizeof(int) * 25);
	printf("New size(25): ");
	for (int i = 0; i < 25; i++)
	{
		new_array[i] = i;
		printf("%d ", new_array[i]);
	}
	printf("\n");

	my_free(new_array);
	printf("Memory reallocation and deallocation were successful!\n");

	init_free();
	return 0;
}
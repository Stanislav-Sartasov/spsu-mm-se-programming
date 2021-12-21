#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include "memory_managment.h"

char* memory = NULL;
memory_block* first_block = NULL;

void unit()
{
	memory = (char*)malloc(DEFAULT_MEMORY_SIZE);
}

void free_unit()
{
	free(memory);
}

void unite_empety_blocks(memory_block* curr_block)
{
	if (curr_block == NULL) 
	{
		return;
	}
	else if (curr_block == first_block && first_block->next == NULL && first_block->is_used == 0)
	{
		first_block = NULL;
		return;
	}

	memory_block* before_unused = NULL; // this variable is needed to handle a situation in which unused blocks are at the end

	while (curr_block->next != NULL && curr_block->is_used != 0)
	{
		if (curr_block->next->is_used == 0)
		{
			before_unused = curr_block;
		}

		curr_block = curr_block->next;
	}

	if (curr_block->next == NULL)
	{
		return;
	}

	// find out how many empty block we have, on this step we know that we have at least one empty block 

	memory_block* start_block = curr_block;
	memory_block* end_block = curr_block;
	int empety_blocks = 1;
	curr_block = curr_block->next;

	while (curr_block->is_used == 0 && curr_block->next != NULL)
	{
		empety_blocks += 1;
		end_block = curr_block;
		curr_block = curr_block->next;
	}

	if (empety_blocks > 1 || curr_block->next == NULL)
	{
		if (curr_block->next == NULL && start_block == first_block)
		{
			first_block = NULL;
			return;
		}
		else if (curr_block->next == NULL && curr_block->is_used == 0)
		{
			before_unused->next = NULL;
		}
		else // handle a situation in which empty blocks are in the middle
		{
			start_block->size = (int)curr_block - (int)start_block - HEADER_SIZE;
			start_block->next = curr_block;
		}
	}

	unite_empety_blocks(curr_block); // searching for empty blocks again in the remaining blocks
}

void my_free(void* pointer)
{
	*(((char*)pointer) - OFFSET_TO_ISUSED_FROM_USER_DATA) = 0;
}

void* my_malloc(int size)
{
	unite_empety_blocks(first_block);
	size = (size + 3) / 4 * 4; // makes size a multiple of 4

	if (first_block == NULL) // if we have no blocks yet
	{
		first_block = (memory_block*)memory;
		first_block->next = NULL;
		first_block->size = size;
		first_block->is_used = 1;
		return first_block->data;
	}

	memory_block* curr_block = first_block;

	while (curr_block->next != NULL) // searching for NULL or emepty block with appropriate memory size
	{
		if ((char*)curr_block + curr_block->size + 2 * HEADER_SIZE + size > memory + DEFAULT_MEMORY_SIZE)
			return NULL;

		if (curr_block->is_used == 0 && curr_block->size > size) // try to find space between empty blocks
		{
			int remaining_memory = curr_block->size - size;

			/*
				if remaining memory > 16 (THE_SMALLEST_SIZE_OF_ALLOCATED_MEMORY) we create an extra block with user data size at least 4 bytes
				in other situations we leave this memory as fragmentation
			*/

			if (remaining_memory > THE_SMALLEST_SIZE_OF_ALLOCATED_MEMORY)
			{
				memory_block* temp = curr_block->next;
				curr_block->next = (char*)curr_block + HEADER_SIZE + curr_block->size;
				curr_block->next->size = curr_block->size - size;
				curr_block->next->is_used = 0;
				curr_block->next->next = temp;
			}

			curr_block->size = size;
			curr_block->is_used = 1;
			return curr_block->data;
		}
		curr_block = curr_block->next;
	}

	if ((curr_block->next == NULL) && ((char*)curr_block + curr_block->size + 2 * HEADER_SIZE + size < memory + DEFAULT_MEMORY_SIZE))
	{
		curr_block->next = (char*)curr_block + HEADER_SIZE + curr_block->size;
		curr_block->next->next = NULL;
		curr_block->next->is_used = 1;
		curr_block->next->size = size;
		return curr_block->next->data;
	}
}

void* my_calloc(int size)
{
	char* pointer = (char*)my_malloc(size); 
	for (int i = 0; i < size; i++)
		pointer[i] = 0;

	return (void*)pointer;
}

void* my_realloc(void* pointer, int size)
{
	memory_block* curr_block = ((memory_block*)(((char*)pointer - OFFSET_TO_STRUCT_ADRESS_FROM_USER_DATA)));

	if (curr_block->size >= size)
	{
		if (curr_block->next == NULL)
		{
			curr_block->size = size;
		}
		else
		{
			int remaining_memory = curr_block->size - size;

			if (remaining_memory > THE_SMALLEST_SIZE_OF_ALLOCATED_MEMORY)
			{
				memory_block* temp = curr_block->next;
				curr_block->next = (char*)curr_block + HEADER_SIZE + curr_block->size;
				curr_block->next->size = curr_block->size - size;
				curr_block->next->is_used = 0;
				curr_block->next->next = temp;
			}

			curr_block->size = size;
			curr_block->is_used = 1;
		}
	}
	else
	{
		char* result = (char*)my_malloc(size);

		if (pointer == NULL)
		{
			printf("There is not enough memory to realloc memory for you\n");
		}
		else
		{
			curr_block->is_used = 0;

			for (int i = 0; i < curr_block->size; i++)
			{
				result[i] = ((char*)pointer)[i];
			}
			return result;
		}
	}
	return pointer;
}

void show_memory_situation()
{
	if (first_block == NULL)
	{
		printf("Memory information: all memory is free\n");
		return;
	}

	memory_block* curr_block = first_block;
	printf("Memory situation:\n");
	int available_memory = DEFAULT_MEMORY_SIZE;

	while (curr_block != NULL)
	{

		if (curr_block->next != NULL)
		{
			int difference = (int)curr_block->next - (int)curr_block - HEADER_SIZE - curr_block->size;
			available_memory -= difference + HEADER_SIZE + curr_block->size;
			if (!difference)
			{
				printf("|Used - %d|H: 12|D: %d|--", curr_block->is_used, curr_block->size);
			}
			else
			{
				printf("|Used - %d|H: 12|D: %d|F: %d|--", curr_block->is_used, curr_block->size, difference);
			}
		}
		else
		{
			available_memory -= HEADER_SIZE + curr_block->size;
			printf("|Used - %d|H: 12|D: %d|", curr_block->is_used, curr_block->size);
		}
		curr_block = curr_block->next;
	}

	printf("\nMemory size: %d\nAvailable memory size: %d\n\n", DEFAULT_MEMORY_SIZE, available_memory);
}
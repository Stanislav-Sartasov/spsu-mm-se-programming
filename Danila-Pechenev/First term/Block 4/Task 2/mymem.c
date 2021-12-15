#include <stdbool.h>
#include <stdlib.h>
#include <stdint.h>
#include <stdio.h>
#define MEMORY_SIZE 536870912
#define SIZE_FOR_FOUR 134217728  // 536870912 / 4


uint32_t* memory;
void init()
{
	memory = malloc(MEMORY_SIZE);
	memory[0] = MEMORY_SIZE;
	memory[SIZE_FOR_FOUR - 1] = MEMORY_SIZE;
}


void destroy()
{
	free(memory);
}


void* my_malloc(size_t size)
{
	uint32_t current_block_ptr = 0;
	uint32_t new_size = (((size + 8) % 8 == 0) ?
		(size + 8) :
		(((size + 8) / 8 + 1) * 8));
	while (current_block_ptr < SIZE_FOR_FOUR)
	{
		uint32_t header = memory[current_block_ptr];
		if (header % 2 == 0 && header >= new_size)
		{
			if (header > new_size)
			{
				memory[current_block_ptr + new_size / 4] = header - new_size;
				memory[current_block_ptr + header / 4 - 1] = header - new_size;
			}
			memory[current_block_ptr] = new_size + 1;
			memory[current_block_ptr + new_size / 4 - 1] = new_size + 1;
			return (void*)(&memory[current_block_ptr + 1]);
		}
		else
		{
			current_block_ptr += ((header % 2 == 0) ?
				header :
				(header - 1)) / 4;
		}
	}
	return NULL;
}


void merge_free_blocks(uint32_t header, bool left)
{
	if (left)
	{
		if (header > 0 && memory[header - 1] % 2 == 0)
		{
			memory[header - memory[header - 1] / 4] = memory[header - 1] + memory[header];
			memory[header + memory[header] / 4 - 1] = memory[header - 1] + memory[header];
			merge_free_blocks(header - memory[header - 1] / 4, true);
		}
	}
	if (!left)
	{
		header += memory[header] / 4;
		if (header < SIZE_FOR_FOUR && memory[header] % 2 == 0)
		{
			merge_free_blocks(header, false);
		}
	}
}


void my_free(void* ptr)
{
	uint32_t header = (uint32_t)((uint32_t*)ptr - memory) - 1;
	memory[header] = memory[header] - 1;
	memory[header + memory[header] / 4 - 1] = memory[header];
	merge_free_blocks(header, true);
}


void* my_realloc(void* ptr, size_t size)
{
	uint32_t header = (uint32_t)((uint32_t*)ptr - memory) - 1;
	uint32_t new_size = (((size + 8) % 8 == 0) ?
		(size + 8) :
		(((size + 8) / 8 + 1) * 8));
	if (new_size < memory[header] - 1)
	{
		memory[header + new_size / 4] = memory[header] - new_size;
		memory[header + (memory[header] - 1) / 4 - 1] = memory[header] - new_size;
		memory[header] = new_size + 1;
		memory[header + new_size / 4 - 1] = new_size + 1;
		my_free((void*)(&memory[header + new_size / 4 + 1]));
		return ptr;
	}
	else if (new_size == memory[header] - 1)
	{
		return ptr;
	}
	else
	{
		if (memory[header + memory[header] / 4] % 2 == 0 &&
			new_size <= memory[header] - 1 + memory[header + memory[header] / 4])
		{
			if (new_size < memory[header] - 1 + memory[header + memory[header] / 4])
			{
				memory[header] = memory[header] - 1;
				memory[header + new_size / 4] = memory[header] + memory[header + memory[header] / 4] - new_size;
				memory[header + memory[header] / 4 + memory[header + memory[header] / 4] / 4 - 1] = memory[header + new_size / 4];
			}
			memory[header] = new_size + 1;
			memory[header + new_size / 4 - 1] = new_size + 1;
			return ptr;
		}
		else
		{
			uint32_t* new_ptr = my_malloc(size);
			if (new_ptr == NULL)
			{
				return NULL;
			}
			uint32_t last_real_size = (memory[header] - 1) / 4 - 2;
			for (uint32_t i = 0; i < last_real_size; i++)
			{
				new_ptr[i] = memory[header + 1 + i];
			}
			my_free((void*)(&memory[header + 1]));
			return (void*)new_ptr;
		}
	}
}
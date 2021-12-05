#include "memory_manager.h"

void *memory = NULL;

void init()
{
	free_memory();
	memory = malloc(INITIAL_SIZE);

	mm_block *block_info = memory;
	block_info->is_free = 1;
	block_info->size = INITIAL_SIZE;
}

void free_memory()
{
	free(memory);
	memory = NULL;
}

void *my_malloc(size_t size)
{
	void *current = memory;
	mm_block *current_block_info = current;
	while (!current_block_info->is_free && current_block_info->size < size)
	{
		if (current > memory + INITIAL_SIZE)
		{
			return NULL;
		}

		current += size + sizeof(mm_block);
		current_block_info = current;
	}

	mm_block *next_block_info = current + sizeof(mm_block) + size;
	next_block_info->is_free = 1;
	next_block_info->size = current_block_info->size - size - sizeof(mm_block);

	current_block_info->is_free = 0;
	current_block_info->size = size;
	return current + sizeof(mm_block);
}

void my_free(void *ptr)
{
	mm_block *block_info = ptr - sizeof(mm_block);
	block_info->is_free = 1;
	mm_block *next_block_info = ptr + block_info->size;

	if (next_block_info->is_free)
		block_info->size = block_info->size + next_block_info->size + sizeof(mm_block);
}
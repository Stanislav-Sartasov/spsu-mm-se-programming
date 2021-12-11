#include "memory_manager.h"

void *memory = NULL;
const size_t INFO_SIZE = sizeof(mm_block);

void init()
{
	free_memory();
	memory = malloc(INITIAL_SIZE);

	mm_block *block = memory;
	block->is_free = 1;
	block->size = INITIAL_SIZE;
}

void free_memory()
{
	free(memory);
	memory = NULL;
}

void *my_malloc(size_t size)
{
	void *current = memory;
	mm_block *current_block = current;
	while (!current_block->is_free && current_block->size < size)
	{
		if (current > memory + INITIAL_SIZE)
		{
			return NULL;
		}

		current += current_block->size + INFO_SIZE;
		current_block = current;
	}

	mm_block *next_block = current + INFO_SIZE + size;
	if (size != current_block->size)
	{
		if (INFO_SIZE > current_block->size - size)
		{
			current_block->size = size;
			current_block->is_free = 0;
			return current + INFO_SIZE;
		}
		next_block->is_free = 1;
		next_block->size = current_block->size - size - INFO_SIZE;
	}

	current_block->is_free = 0;
	current_block->size = size;
	return current + INFO_SIZE;
}

void my_free(void *ptr)
{
	mm_block *block_info = ptr - INFO_SIZE;
	block_info->is_free = 1;
	mm_block *next_block = ptr + block_info->size;

	if (next_block->is_free)
		block_info->size = block_info->size + next_block->size + INFO_SIZE;
}

void *my_realloc(void *ptr, size_t size)
{
	mm_block *block = ptr - INFO_SIZE;
	mm_block *next_block = ptr + block->size;

	if (next_block->is_free && next_block->size + block->size >= size)
	{
		block->size = size;
		return ptr;
	}

	next_block = NULL;
	void *current = memory;
	while (current < memory + INITIAL_SIZE)
	{
		mm_block *current_block = current;
		next_block = current + INFO_SIZE + current_block->size;
		if (!current_block->is_free)
		{
			current = next_block;
			continue;
		}
		size_t available_size = current_block->size + block->size * (next_block == block);
		if (available_size >= size)
		{
			memcpy(current + INFO_SIZE, ptr, block->size);
			current_block->size = size;
			current_block->is_free = 0;
			block->is_free = 1;

			if (size != available_size)
			{
				if (INFO_SIZE > available_size - size)
				{
					current_block->size = available_size;
					return current + INFO_SIZE;
				}
				next_block = current + INFO_SIZE + current_block->size;
				next_block->is_free = 1;
				next_block->size = available_size - size - INFO_SIZE;
			}
			return current + INFO_SIZE;
		}
		current = next_block;
	}
	return NULL;
}
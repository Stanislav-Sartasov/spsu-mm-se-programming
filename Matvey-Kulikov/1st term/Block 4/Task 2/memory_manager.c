#include "memory_manager.h"
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

void* my_heap = NULL;

void init()
{
	my_heap = malloc(MY_HEAP_SIZE);
	uint32_t size = MY_HEAP_SIZE - 2 * HEADER_SIZE;

	memory_block* first_block = my_heap;
	first_block->size = size;
	first_block->occupied - 0;

	memory_block* first_block_end = my_heap + first_block->size + HEADER_SIZE;
	first_block_end->size = size;
	first_block_end->occupied = 0;
}

void free_heap()
{
	free(my_heap);
}

void print_blocks()
{
	printf("Current memory state (blocks in format [size, occupied flag]):\n");
	void* pointer = my_heap;
	while (pointer < my_heap + MY_HEAP_SIZE)
	{
		memory_block* block = pointer;
		printf("[%u, %d] ", block->size, block->occupied);
		pointer += 2 * HEADER_SIZE + ((memory_block*)pointer)->size;
	}
	printf("\n\n");
}

void split(void* pointer, uint32_t size)
{
	memory_block* next_block = pointer + 2 * HEADER_SIZE + size;
	uint32_t next_size = ((memory_block*)pointer)->size - size - 2 * HEADER_SIZE;
	memory_block* next_block_end = pointer + size + 3 * HEADER_SIZE + next_size;
	next_block->size = next_size;
	next_block->occupied = 0;
	next_block_end->size = next_size;
	next_block_end->occupied = 0;
}

void* my_malloc(uint32_t size)
{
	if (!size)
	{
		return NULL;
	}
	void* pointer = my_heap;
	memory_block* block = pointer;
	uint32_t block_size = block->size;
	while (block->occupied || block->size < size)
	{
		if (pointer >= my_heap + MY_HEAP_SIZE)
		{
			return NULL;
		}
		pointer += 2 * HEADER_SIZE + ((memory_block*)pointer)->size;
		block = pointer;
	}

	memory_block* block_end = pointer + HEADER_SIZE + block->size;
	if (block->size > size + 2 * HEADER_SIZE)
	{
		split(pointer, size);
		block_end = pointer + HEADER_SIZE + size;
		block_size = size;
	}
	block->occupied = 1;
	block->size = block_size;
	block_end->occupied = 1;
	block_end->size = block_size;

	return pointer + HEADER_SIZE;

}

void* my_free(void* pointer)
{
	memory_block* block = pointer - HEADER_SIZE;
	if (!(block->occupied))
	{
		return (void*)block;
	}

	memory_block* end = pointer + block->size;
	memory_block* prev_block = (pointer - 2 * HEADER_SIZE < my_heap) ? block : (memory_block*)(pointer - 2 * HEADER_SIZE);
	memory_block* next_block = (pointer + block->size + HEADER_SIZE >= my_heap + MY_HEAP_SIZE) ? block : (memory_block*)(pointer + block->size + HEADER_SIZE);

	if (!(next_block->occupied))
	{
		end = (memory_block*)(pointer + block->size + 2 * HEADER_SIZE + next_block->size);
	}
	if (!(prev_block->occupied))
	{
		block = pointer - 3 * HEADER_SIZE - prev_block->size;
	}
	uint32_t new_size = (void*)end - (void*)block - HEADER_SIZE;
	block->size = new_size;
	block->occupied = 0;
	end->size = new_size;
	end->occupied = 0;

	return (void*)block;
}

void* my_realloc(void* pointer, uint32_t size)
{
	memory_block* block = pointer - HEADER_SIZE;
	uint32_t block_size = block->size;

	if (!size)
	{
		return my_free(pointer);
	}

	if (size + 2 * HEADER_SIZE < block->size)
	{
		memory_block* block_end = pointer + size;
		split(pointer - HEADER_SIZE, size);
		block->size = size;
		block_end->size = size;
		block_end->occupied = 1;

		return pointer;
	}
	else if (size > block->size)
	{
		void* merged_block_pointer = my_free(pointer);
		memory_block* merged_block = (memory_block*)merged_block_pointer;
		if (size <= merged_block->size)
		{
			memory_block* merged_block_end = merged_block_pointer + HEADER_SIZE + size;
			block_size = merged_block->size;
			if (size + 2 * HEADER_SIZE < merged_block->size)
			{
				split(merged_block_pointer, size);
				block_size = size;
			}
			memcpy(merged_block_pointer + HEADER_SIZE, pointer, size);
			merged_block->size = block_size;
			merged_block->occupied = 1;
			merged_block_end->size = block_size;
			merged_block_end->occupied = 1;

			return merged_block_pointer;
		}
		else
		{

			void* new_block_pointer = my_malloc(size);
			if (new_block_pointer)
			{
				memcpy(new_block_pointer, pointer, size);
				return new_block_pointer + HEADER_SIZE;
			}
			memory_block* block_end = pointer + block->size;
			block->occupied = 1;
			block->size = block_size;
			block_end->occupied = 1;
			block_end->size = block_size;
			return NULL;
		}
	}
	// if size is equal or decreasing is useless (size difference is <= two headers' size)
	return pointer;
}

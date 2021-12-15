#include "memory_manager.h"
#include <stdlib.h>


void init()
{
	// Allocating global memory
	memory_begin = (byte*)malloc(MEM_SIZE);

	// Fill memory with zeros
	memset(memory_begin, 0, MEM_SIZE);
}

void dispose()
{
	// Free global memory
	free(memory_begin);
}

void* my_malloc(size_t size)
{
	// Variable "current" is current block
	struct memory_block* current;
	// Reading the first element
	current = memory_begin;

	while (current->next_block_offset < MEM_SIZE && current->next_block_offset >= 0)
	{
		// End of sequence (next_block points to 0)
		if (current->next_block_offset == 0)
		{
			// Check for space
			if (MEM_SIZE - (int)(current - memory_begin) > size)
			{
				// Setting the next_block_offset to a new block
				current->next_block_offset = (int)current - (int)memory_begin + (int)size + sizeof(*current);

				// Setting is_used variable to true
				current->in_use = 1;

				return current + 1;
			}
			// Not enough space
			else
				return NULL;
		}

		// If block is not used (is_used is set to false)
		if (current->in_use == 0)
		{
			// Enough space for new block insertion
			if (current->next_block_offset - (int)current >= size + 2 * sizeof(*current))
			{
				// Creating new block
				struct memory_block* new_memory_block = memory_begin + size + sizeof(*current);

				// Binding new block's next_block to the old next_block
				new_memory_block->next_block_offset = current->next_block_offset;

				// Setting new block as unused
				new_memory_block->in_use = 0;

				// Binding next_block to the beginning of new block
				current->next_block_offset = (int)new_memory_block - (int)memory_begin;

				// Setting block as in use
				current->in_use = 1;

				return current + 1;
			}

			// Not enough space for new block, but enough space for just reallocation of block
			if (current->next_block_offset - (int)current >= size + sizeof(*current))
			{
				// Setting block as in use
				current->in_use = 1;

				return current + 1;
			}
		}
		// Switching to next memory block
		current = (memory_begin + current->next_block_offset);
	}

	return NULL;
}

void merge()
{
	// Merge free blocks
	struct memory_block* current = memory_begin;
	while (current->next_block_offset != 0)
	{
		// Check if the next block is free and this block is free
		if (current->in_use == 0 && ((struct memory_block*)(memory_begin + current->next_block_offset))->in_use == 0)
		{
			// Merging next block into first one (changing the current pointer to the next block)
			current->next_block_offset = ((struct memory_block*)(memory_begin + current->next_block_offset))->next_block_offset;
		}

		// End of sequence
		if (current->next_block_offset == 0)
			return;

		// Switching to next block
		current = memory_begin + current->next_block_offset;
	}
}

void my_free(void* ptr)
{
	// If pointer is NULL, my_free does not work
	if (ptr == NULL)
		return;
	// Set block as if it is not in use
	*((int*)ptr - 1) = 0;

	// Merging blocks 
	merge();

	// Merging merged blocks (if are left)
	merge();
}

void* my_realloc(void* ptr, size_t size)
{
	// Determining the previous size
	int block_size = (int)ptr - (int)((int*)ptr - 2);

	// My free does not corrupt information or changes accesability, so data is there, but accessable for use
	my_free(ptr);
	void* result = my_malloc(size);
	// If my_malloc worked correctly elements are just all copied

	if (result != NULL)
		for (int i = 0; i < block_size; i++)
			((byte*)result)[i] = ((byte*)ptr)[i];

	return result;
}

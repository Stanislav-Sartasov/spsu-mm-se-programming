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
	// Variable "current" is offset from memory beginning to current block
	int current = 0;
	while (current < MEM_SIZE && current >= 0)
	{
		// End of sequence (next_block points to 0)
		if (*(current + memory_begin) == 0)
		{
			// Check for space
			if (MEM_SIZE - current - 8 > size + 8)
			{
				// Setting the next_block to a new block
				*(int*)(current + memory_begin) = (int)((current + size) + 8);
				// Setting is_used variable to true
				*(current + memory_begin + 4) = 1;

				return current + memory_begin + 8;
			}
			// Not enough space
			else
				return NULL;
		}

		// If block is not used (is_used is set to false)
		if (*(current + memory_begin + 4) == 0)
		{
			// Enough space for new block insertion
			if (MEM_SIZE - current - 8 > size + 16)
			{
				// Creating new block
				// Binding new block's next_block to the old next_block
				*(int*)(memory_begin + (current + size) + 8) = *(int*)(current + memory_begin);

				// Setting new block as unused
				*(int*)(memory_begin + (current + size) + 12) = 0;

				// Binding next_block to the beginning of new block
				*(int*)(current + memory_begin) = (int)((current + size) + 8);

				// Setting block as in use
				*(current + memory_begin + 4) = 1;


				return current + memory_begin + 8;
			}

			// Not enough space for new block, but enough space for just reallocation of block
			if (MEM_SIZE - current - 8 > size + 8)
			{
				// Setting block as in use
				*(current + memory_begin + 4) = 1;


				return current + memory_begin + 8;
			}
		}
		// Switching to next memory block
		current = *(int*)(current + memory_begin);
	}

	return NULL;
}

void my_free(void* ptr)
{
	// If pointer is NULL, my_free does not work
	if (ptr == NULL)
		return;
	// Set block as if it is not in use
	*((int*)ptr - 1) = 0;

	// Merge free blocks
	int current = 0;
	while (current < MEM_SIZE && current >= 0)
	{

		// Check if the next block is free and this block is free
		if (*(current + memory_begin + 4) == 0 && *(memory_begin + *(int*)(current + memory_begin) + 4) == 0 && *(current + memory_begin) != 0)
		{
			// Merging next block into first one
			*(current + memory_begin) = *(int*)(memory_begin + *(int*)(current + memory_begin));

			return;
		}

		// End of sequence
		if (*(current + memory_begin) == 0)
			return;

		// Switching to next block
		current = *(int*)(current + memory_begin);
	}
}

void* my_realloc(void* ptr, size_t size)
{
	// Determining the previous size
	int block_size = (int)ptr - (int)((int*)ptr - 2);

	// My free does not corrupt information or changes accesability, so data is there, but accessable for use
	my_free(ptr);
	void* result = my_malloc(size);
	// If my_malloc worked correctly elements are just all copied
	return result;
	if (result != NULL)
		for (int i = 0; i < block_size; i++)
			((byte*)result)[i] = ((byte*)ptr)[i];

	return result;
}

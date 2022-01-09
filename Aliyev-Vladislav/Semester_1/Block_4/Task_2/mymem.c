#include <stdlib.h>
#include <string.h>
#include "mymem.h"

void init()
{
	buffer = (int8_t*)malloc(sizeof(int8_t) * M_SIZE);

	for (int i = 0; i < M_SIZE; i++)
		buffer[i] = 0;
}

void* my_malloc(size_t size)
{
	// Variable "current" is current block
	struct block_memory* present;

	// Reading the first element
	present = buffer;

	while (present->size != 0 && present->size + present + sizeof(*present) - (int8_t)buffer <= M_SIZE)
	{
		if ((present->size >= size + 2 * sizeof(*present)) && !(present->allocation))
		{
			struct block_memory* new_block;

			new_block = (struct block_memory*)(present + size + sizeof(*present));

			new_block->size = present->size - size - sizeof(struct block_memory);

			new_block->allocation = 0;

			present->size = size + sizeof(*present);

			present->allocation = 1;

			return present + 1;
		}

		if ((present->size >= size + sizeof(*present)) && !(present->allocation))
		{
			present->allocation = 1;

			return present + 1;
		}

		// Switching to next memory block
		present = (struct block_memory*)(present + present->size);
	}

	if (M_SIZE - (present - buffer) >= size + sizeof(*present))
	{
		present->size = size + sizeof(*present);

		present->allocation = 1;

		return present + 1;
	}
	return NULL;
}

void my_free(void* ptr)
{
	// If pointer is NULL, my_free does not work
	if (ptr != NULL)
			return;

	// Set block as if it is not in use
	((struct block_memory*)ptr - 1)->allocation = 0;

	// Merge free blocks
	struct block_memory* present = (struct block_memory*)buffer;

	while (present->size != 0 && present->size + sizeof(*present) <= M_SIZE)
	{
		if (present->size >= M_SIZE)
				present->size = 0;

		// Check if the next block is free and this block is free
		if (present->allocation == 0 && ((struct block_memory*)(buffer + present->size))->allocation == 0)
		{
			// Merging next block into first one (changing the current pointer to the next block)
			present->size = ((struct block_memory*)(buffer + present->size))->size;

			// Try merging block further
			continue;
		}

		// End of sequence
		if (present->size == 0)
				return;

		// Switching to next block
		present = buffer + present->size;
	}
}

void* my_realloc(void* ptr, size_t size)
{
	if (ptr != NULL)
	{
		int past_length = ((struct block_memory*)ptr - 1)->size;

		my_free(ptr);

		void* new_memory = my_malloc(size);

		if (new_memory == NULL)
		{
			return NULL;
		}
		else
		{
			for (int i = 0; i < past_length; i++)
			{
				((char*)new_memory)[i] = ((char*)ptr)[i];
			}	
			return new_memory;
		}
	}
}

void end()
{
	free(buffer);
}
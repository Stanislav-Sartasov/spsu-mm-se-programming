#include <stdlib.h>
#include <string.h>
#include "mymem.h"

void init()
{
	buffer = (char*)malloc(sizeof(char) * M_SIZE);

	for (int i = 0; i < M_SIZE; i++)
		buffer[i] = 0;
}

void* my_malloc(size_t size)
{
	struct block_memory* present;

	present = (struct block_memory*)buffer;

	while (present->size != 0 && present->size + (int)present + sizeof(struct block_memory) - (int)buffer <= M_SIZE)
	{
		if ((present->size >= size + 2 * sizeof(struct block_memory)) && !(present->allocation))
		{
			struct block_memory* new_block;

			new_block = (struct block_memory*)((int)present + size + sizeof(struct block_memory));

			new_block->size = present->size - size - sizeof(struct block_memory);

			new_block->allocation = 0;

			present->size = size + sizeof(struct block_memory);

			present->allocation = 1;

			return present + 1;
		}

		if ((present->size >= size + sizeof(struct block_memory)) && !(present->allocation))
		{
			present->allocation = 1;

			return present + 1;
		}

		present = (struct block_memory*)((int)present + (int)present->size);
	}

	if (M_SIZE - ((int)present - (int)buffer) >= size + sizeof(struct block_memory))
	{
		present->size = size + sizeof(struct block_memory);

		present->allocation = 1;

		return present + 1;
	}
	return NULL;
}

void my_free(void* ptr)
{
	if (ptr != NULL)
	{
		((struct block_memory*)ptr - 1)->allocation = 0;

		struct block_memory* present;

		present = (struct block_memory*)buffer;

		while (present->size != 0)
		{
			if (present->size + (int)present + sizeof(struct block_memory) - (int)buffer >= M_SIZE)
			{
				present->size = 0;
			}

			if ((present->allocation == 0) && (((struct block_memory*)((int)present + (int)present->size))->allocation == 0))
			{
				present->size += ((struct block_memory*)((int)present + (int)present->size))->size;

				if (((struct block_memory*)((int)present + (int)present->size))->size == 0)
				{
					present->size = 0;
				continue;
				}
			}
			present = (struct block_memory*)((int)present + (int)present->size);
		}
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
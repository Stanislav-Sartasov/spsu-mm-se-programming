#include <stdlib.h>
#include <string.h>
#include "mymem.h"

void* buffer;
block_memory* blocks;

void init()
{
	buffer = malloc(M_SIZE);
}


void memory_initialization(block_memory* block, block_memory* next, size_t size)
{
	block->next = next;

	block->size = size;

	block->allocation = (byte*)block + INFO;
}


void* my_malloc(size_t size)
{
	if (size <= 0)
		return NULL;

	if (!blocks)
	{
		if (M_SIZE < size + INFO)
			return NULL;

		blocks = buffer;

		memory_initialization(blocks, NULL, size);

		return blocks->allocation;
	}

	if ((byte*)blocks - (byte*)buffer >= size + INFO)
	{
		memory_initialization(buffer, blocks, size);

		blocks = buffer;

		return blocks->allocation;
	}

	block_memory* present = blocks;

	while (present->next)
	{
		block_memory* new_block  = (block_memory*)((byte*)present->allocation + present->size);

		if ((byte*)present->next - (byte*)new_block  >= size + INFO)
		{
			memory_initialization(new_block , present->next, size);

			present->next = new_block ;

			return new_block ->allocation;
		}
		present = present->next;
	}

	block_memory* last = (block_memory*)((byte*)present->allocation + INFO);

	if ((byte*)last - (byte*)buffer > M_SIZE - size - INFO)
		return NULL;

	memory_initialization(last, NULL, size);

	present->next = last;

	return last->allocation;
}


void my_free(void* ptr)
{
	if (!ptr)
		return;

	block_memory** present = &blocks;

	if ((*present)->allocation == ptr)
	{
		blocks = (*present)->next;

		return;
	}

	while ((*present)->next)
	{
		if ((*present)->next->allocation == ptr)
		{
			(*present)->next = (*present)->next->next;

			return;
		}
		present = &(*present)->next;
	}
}


void* my_realloc(void* ptr, size_t size)
{
	if (!ptr)
		return NULL;

	block_memory* present = blocks;

	while (present->next && present->allocation != ptr)
		present = present->next;

	if (present->allocation != ptr)
		return NULL;

	if ((byte*)present->next - (byte*)present->allocation >= size)
	{
		present->size = size;

		return ptr;
	}

	block_memory* new_memory = my_malloc(size);

	for (int i = 0; i < present->size; i++)
		*((byte*)new_memory->allocation + i) = *((byte*)present->allocation + i);

	my_free(present->allocation);

	return new_memory->allocation;
}


void end()
{
	free(buffer);
}
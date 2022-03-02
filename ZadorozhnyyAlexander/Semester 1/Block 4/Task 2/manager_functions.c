#include "manager_functions.h"

memory_block* first_block = NULL;

void init_free_space()
{
	manager_free_space = (char*)malloc(SIZE_OF_SPACE * sizeof(char));
}

void init_first_block()
{
	init_free_space();
	first_block = (memory_block*)manager_free_space;
	first_block->block_size = SIZE_OF_SPACE - sizeof(memory_block);
	first_block->is_empty = 1;
	first_block->next_block = NULL;
	first_block->prev_block = NULL;
	first_block->data = (char*)first_block + sizeof(memory_block);
}

void add_new_memory_block(memory_block* curr, unsigned int size)
{
	memory_block* new_block = (memory_block*)(curr->data + size);

	new_block = (memory_block*)(curr->data + size);
	new_block->block_size = curr->block_size - (size + sizeof(memory_block));
	new_block->is_empty = 1;
	new_block->next_block = curr->next_block;
	new_block->prev_block = curr;
	new_block->data = (char*)new_block + sizeof(memory_block);

	curr->next_block = new_block;
}

void* malloc_realization(unsigned int size)
{
	if (!first_block)
		init_first_block();

	size = size % 4 == 0 ? size : size - size % 4 + 4;
	memory_block* curr = (memory_block*)manager_free_space;
	memory_block* prev = NULL;
	while (curr && !(curr->is_empty) && size > curr->block_size)
	{
		prev = curr;
		curr = curr->next_block;
	}

	if (curr)
	{
		curr->is_empty = 0;
		if (curr->block_size - size >= sizeof(memory_block))
			add_new_memory_block(curr, size);
		curr->block_size = size;
		return curr->data;
	}
	return NULL;
}

void* realloc_realization(void* ptr, unsigned int size)
{
	memory_block* new_block = (memory_block*)((char*)ptr - sizeof(memory_block));;
	memory_block* curr = new_block;
	while (curr->next_block)
	{
		curr = curr->next_block->is_empty ? curr->next_block : curr;
	}

	unsigned int needed_space = curr->next_block != NULL ? ((char*)curr->next_block - new_block->data) : manager_free_space + SIZE_OF_SPACE - new_block->data;
	
	if (needed_space < size)
	{
		char* res = (char*)malloc_realization(size);
		if (!res)
			return NULL;
		for (unsigned int i = 0; i < new_block->block_size; i++)
		{
			res[i] = ((char*)ptr)[i];
		}
		new_block->is_empty = 0;
		return res;
	}
	else
	{
		new_block->block_size = needed_space;
		new_block->next_block = curr->next_block;
		if (needed_space - size >= sizeof(memory_block))
			add_new_memory_block(new_block, size);

		new_block->block_size = size;
		return new_block->data;
	}
}

void free_realization(void* ptr)
{
	memory_block* block = (memory_block*)((char*)ptr - sizeof(memory_block));
	block->is_empty = 0;
	memory_block* prev_blocks = block;
	memory_block* next_blocks = block;

	while (prev_blocks->prev_block)
	{
		memory_block* temp = prev_blocks;
		if ((prev_blocks->prev_block->is_empty ? prev_blocks->prev_block : prev_blocks) == temp)
			break;
	}

	while (next_blocks->next_block)
	{
		memory_block* temp = next_blocks;
		if ((next_blocks = next_blocks->next_block->is_empty ? next_blocks->next_block : next_blocks) == temp)
			break;
	}

	unsigned int new_size;
	if (!next_blocks->next_block)
		new_size = manager_free_space + SIZE_OF_SPACE - prev_blocks->data;
	else
	{
		new_size = (char*)next_blocks->next_block - prev_blocks->data;
		next_blocks->next_block->prev_block = prev_blocks;
	}

	prev_blocks->block_size = new_size;
	prev_blocks->next_block = next_blocks->next_block;
}

void free_init_space()
{
	if (manager_free_space)
		free(manager_free_space);
}
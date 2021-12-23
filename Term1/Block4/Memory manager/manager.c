#include "manager.h"
#include <stdlib.h>
#include <string.h>

void* memory;
mem_block* blocks;

void init()
{
	memory = malloc(MEM_SIZE);
}

void free_memory()
{
	free(memory);
}

void init_mem_block(mem_block* block, mem_block* next, size_t size)
{
	block->next = next;
	block->size = size;
	block->data = (byte*)block + BLOCK_INFO;
}

void* my_malloc(size_t size)
{
	if (size <= 0)
		return NULL;

	if (!blocks)
	{
		if (MEM_SIZE < size + BLOCK_INFO)
			return NULL;
		blocks = memory;
		init_mem_block(blocks, NULL, size);
		return blocks->data;
	}

	if ((byte*)blocks - (byte*)memory >= size + BLOCK_INFO)
	{
		init_mem_block(memory, blocks, size);
		blocks = memory;
		return blocks->data;
	}

	mem_block* current = blocks;
	while (current->next)
	{
		mem_block* next_free = (mem_block*)((byte*)current->data + current->size);
		if ((byte*)current->next - (byte*)next_free >= size + BLOCK_INFO)
		{
			init_mem_block(next_free, current->next, size);
			current->next = next_free;
			return next_free->data;
		}
		current = current->next;
	}

	mem_block* last = (mem_block*)((byte*)current->data + BLOCK_INFO);
	if ((byte*)last - (byte*)memory > MEM_SIZE - size - BLOCK_INFO)
		return NULL;

	init_mem_block(last, NULL, size);
	current->next = last;
	return last->data;
}

void my_free(void* ptr)
{
	if (!ptr)
		return;

	mem_block** current = &blocks;
	if ((*current)->data == ptr)
	{
		blocks = (*current)->next;
		return;
	}

	while ((*current)->next)
	{
		if ((*current)->next->data == ptr)
		{
			(*current)->next = (*current)->next->next;
			return;
		}
		current = &(*current)->next;
	}
}

void* my_realloc(void* ptr, size_t size)
{
	if (!ptr)
		return NULL;

	mem_block* current = blocks;
	while (current->next && current->data != ptr)
		current = current->next;
	
	if (current->data != ptr)
		return NULL;

	if ((byte*)current->next - (byte*)current->data >= size)
	{
		current->size = size;
		return ptr;
	}

	mem_block* new = my_malloc(size);
	for (uint32_t i = 0; i < current->size; i++)
		*((byte*)new->data + i) = *((byte*)current->data + i);
	
	my_free(current->data);
	return new->data;
}
//
// Created by Вячеслав Бучин on 16.12.2021.
//

#include <memory.h>

#include "my_memory.h"
#include "memory_block.h"

#define PREALLOCATED_BYTES 4096

void* memory;
memory_t* blocks;

void init()
{
	memory = malloc(PREALLOCATED_BYTES);
}

void give_back()
{
	free(memory);
}

void* my_malloc(size_t size)
{
	size_t needed_size = size + sizeof(memory_t);
	if (blocks == NULL)
	{
		if (needed_size > PREALLOCATED_BYTES) {
			return NULL;
		}
		blocks = memory;
		init_block(blocks, NULL, size);
		return blocks[0].data;
	}

	if ((byte_t*)blocks - (byte_t*)memory >= needed_size)
	{
		init_block(memory, blocks, size);
		blocks = memory;
		return blocks[0].data;
	}

	memory_t* block = blocks;
	while (block->next != NULL)
	{
		memory_t* new_block = (memory_t*) ((byte_t*) block->data + block->size);
		if ((byte_t*)block->next - (byte_t*)new_block >= needed_size)
		{
			init_block(new_block, block->next, size);
			block->next = new_block;
			return new_block->data;
		}
		block = block->next;
	}
	memory_t* new_block = block->data + block->size;
	if (PREALLOCATED_BYTES - ((byte_t*)new_block - (byte_t*)memory) < needed_size)
		return NULL;

	init_block(new_block, NULL, size);
	block->next = new_block;
	return new_block->data;
}

void my_free(void* ptr)
{
	memory_t** block = &blocks;
	if ((*block)->data == ptr)
	{
		blocks = (*block)->next;
		return;
	}
	while ((*block)->next != NULL)
	{
		if ((*block)->next->data == ptr)
		{
			(*block)->next = (*block)->next->next;
			return;
		}
		block = &(*block)->next;
	}
	*block = NULL;
}

void* my_realloc(void* ptr, size_t size)
{
	if (ptr == NULL)
		return my_malloc(size);

	memory_t* block = blocks;
	while (block->data != ptr)
		block = block->next;

	if (size <= (byte_t*)block->next - (byte_t*)block->data) {
		block->size = size;
		return ptr;
	}

	memory_t* new_block = my_malloc(size);
	if (new_block == NULL)
		return NULL;
	new_block = (memory_t*)((byte_t*)new_block - sizeof(memory_t));
	memcpy(new_block->data, block->data, size < block->size ? size : block->size);
	my_free(block->data);
	return new_block->data;
}
#include "manager.h"

void init()
{
	heap = (int8_t*)malloc(sizeof(int8_t) * HEAP_SIZE);
	allocs = heap;
	allocs->next = NULL;
	allocs->point = heap+BLOCK_SIZE;
	allocs->free = true;
	allocs->size = HEAP_SIZE - BLOCK_SIZE;
	occupied_blocks = 0;
}

size_t align(size_t size)
{
	return !(size % 4) ? size : (((size - 1) >> 2) << 2) + 4;
}

void* my_malloc(size_t size)
{
	size = align(size);
	if (size == 0)
	{
		return NULL;
	}
	alloc_blocks* current = allocs;
	while (current->next && ( current->size < size + BLOCK_SIZE || !current->free))
	{
		current = current->next;
	}
	if (current->free && current->size >= size + BLOCK_SIZE)
	{
		alloc_blocks* new_block = current->point + size;
		new_block->size = current->size - size - BLOCK_SIZE;
		new_block->next = current->next;
		new_block->free = true;
		new_block->point = current->point + BLOCK_SIZE + size;
		current->size = size;
		current->next = new_block;
		current->free = false;
		occupied_blocks++;
	}
	else
	{
		return NULL;
	}
	return (void*)current->point;
}

void offset_blocks(alloc_blocks* left, alloc_blocks* right)
{
	left->size = right->size + left->size + BLOCK_SIZE;
	left->next = right->next;
	right = NULL;
}
alloc_blocks* find_prev_block(void* ptr)
{
	alloc_blocks* block = allocs;
	alloc_blocks* prev = NULL;
	while (block->next && block->point != ptr)
	{
		prev = block;
		block = block->next;
	}
	return prev;
}

void my_free(void* ptr)
{
	alloc_blocks* prev = find_prev_block(ptr);
	alloc_blocks* current = !prev ? allocs : prev->next;
	if (current->point != ptr || current->free)
	{
		return;
	}
	occupied_blocks--;
	current->free = true;
	if (current->next && current->next->free)
	{
		offset_blocks(current, current->next);
	}
	if (prev && prev->free)
	{
		offset_blocks(prev, current);
		prev->free = true;
	}
}

void* my_realloc(void* ptr, size_t size)
{
	alloc_blocks* prev = find_prev_block(ptr);
	alloc_blocks* current = !prev ? allocs : prev->next;
	if (!current || current->point != ptr)
	{
		return NULL;
	}
	size = align(size);
	if (size == 0)
	{
		my_free(ptr);
		return NULL;
	}
	size_t min_size = size < current->size ? size : current->size;
	int8_t* save_data = (int8_t*)my_malloc(min_size);
	for (size_t i = 0; i < min_size; i++)
	{
		save_data[i] = *((int8_t*)ptr + i);
	}
	my_free(ptr);
	int8_t* result_point = (int8_t*)my_malloc(size);
	for (size_t i = 0; i < min_size; i++)
	{
		result_point[i] = *((int8_t*)save_data + i);
	}
	my_free(save_data);
	return (void*)result_point;
}

void init_free()
{
	free(heap);
}
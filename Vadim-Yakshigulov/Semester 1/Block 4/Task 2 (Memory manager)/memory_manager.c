#include "memory_manager.h"

void init()
{
	heap = (byte *) malloc(HEAP_SIZE);
	initial_block = (allocated_block_t *) heap;
	initial_block->ptr = heap + BLOCK_SIZE;
	initial_block->size = HEAP_SIZE - BLOCK_SIZE;
	initial_block->is_free = true;
	initial_block->next_block = NULL;
	initial_block->prev_block = NULL;
}

void destroy()
{
	free(heap);
}

void create_next_block_with_offset(allocated_block_t *parent_block, size_t offset)
{
	allocated_block_t *next_block = parent_block->ptr + offset + 1;
	next_block->ptr = next_block + BLOCK_SIZE;
	next_block->size = parent_block->size - BLOCK_SIZE - offset;
	next_block->is_free = true;
	next_block->next_block = parent_block->next_block;
	next_block->prev_block = parent_block;

	parent_block->size = offset;
	parent_block->next_block = next_block;
}

void unite_free_blocks_around(allocated_block_t *parent_block)
{
	allocated_block_t *start_block = parent_block;

	while (start_block->prev_block != NULL && start_block->prev_block->is_free)
		start_block = start_block->prev_block;

	allocated_block_t *current_block = start_block;
	size_t total_size = 0;

	while (current_block != NULL && current_block->is_free)
	{
		total_size += current_block->size + BLOCK_SIZE;
		current_block = current_block->next_block;
	}

	total_size -= BLOCK_SIZE;
	start_block->size = total_size;
	start_block->next_block = current_block;
}

allocated_block_t *find_block_by_ptr(void *ptr)
{
	allocated_block_t *current_block = initial_block;
	while (current_block != NULL && current_block->ptr != ptr)
		current_block = current_block->next_block;

	return current_block;
}

allocated_block_t *find_free_block_by_size(size_t size)
{
	allocated_block_t *current_block = initial_block;
	while (current_block != NULL && (current_block->size < size || !current_block->is_free))
		current_block = current_block->next_block;

	return current_block;
}

void *my_malloc(size_t size)
{
	allocated_block_t *current_block = find_free_block_by_size(size);
	if (current_block == NULL)
		return NULL;

	if (current_block->size <= size + BLOCK_SIZE)
	{
		current_block->is_free = false;
		return current_block->ptr;
	}

	current_block = find_free_block_by_size(size + BLOCK_SIZE);
	current_block->is_free = false;
	create_next_block_with_offset(current_block, size);
	return current_block->ptr;
}

void my_free(void *ptr)
{
	allocated_block_t *current_block = find_block_by_ptr(ptr);
	if (current_block == NULL)
		return;

	current_block->is_free = true;
	unite_free_blocks_around(current_block);
}

void *my_memcpy(void *destination, const void *source, size_t n)
{
	byte *ptr_destination = (byte *) destination;
	byte *ptr_source = (byte *) source;
	for (int i = 0; i < n; i++, ptr_destination[i] = ptr_source[i]);
	return destination;
}


void *my_realloc(void *ptr, size_t size)
{

	allocated_block_t *current_block = find_block_by_ptr(ptr);
	if (current_block == NULL || current_block->is_free)
		return NULL;

	size_t stored_data_size = current_block->size;
	my_free(ptr);
	void* new_ptr = my_malloc(size);
	my_memcpy(new_ptr, ptr, stored_data_size);
	return new_ptr;
}
#include <stdio.h>
#include <stdlib.h>
#include "my_mem_man.h"

void init()
{
	heap = (char*)calloc(HEAP_SIZE, sizeof(char));

	alloc_list.count = 0;
	alloc_list.units = (unit*)malloc(UNIT_LIST_SIZE * sizeof(unit));

	free_list.count = 1;
	free_list.units = (unit*)malloc(UNIT_LIST_SIZE * sizeof(unit));
	free_list.units->ptr = heap;
	free_list.units->size = HEAP_SIZE;

	tmp_list.count = 0;
	tmp_list.units = (unit*)malloc(UNIT_LIST_SIZE * sizeof(unit));
}

void init_free()
{
	free(heap);
	free(alloc_list.units);
	free(tmp_list.units);
	free(free_list.units);
}

void list_insert(metadata_list* list, void* start, size_t size)
{
	if (list->count > UNIT_LIST_SIZE)
	{
		printf("System: List is out of space, to much allocatins\n");
		return;
	}

	list->units[list->count].ptr = (char*)start;
	list->units[list->count].size = size;

	for (int i = list->count; i > 0 && list->units[i - 1].ptr > list->units[i].ptr; i--)
	{
		unit tmp = list->units[i];
		list->units[i] = list->units[i - 1];
		list->units[i - 1] = tmp;
	}

	list->count += 1;
}

void list_merge(metadata_list* first_list, metadata_list* second_list)
{
	first_list->count = 0;

	for (size_t i = 0; i < second_list->count;++i)
	{
		unit chunk = second_list->units[i];

		if (first_list->count > 0)
		{
			unit* top_chunk = &first_list->units[first_list->count - 1];

			if (top_chunk->ptr + top_chunk->size == chunk.ptr)
				top_chunk->size += chunk.size;
			else
				list_insert(first_list, chunk.ptr, chunk.size);
		}
		else
			list_insert(first_list, chunk.ptr, chunk.size);
	}
}

int list_find(metadata_list* list, void* ptr)
{
	for (size_t i = 0; i < list->count; i++)
		if (list->units[i].ptr == ptr)
			return (int)i;

	return -1;
}

void list_remove(metadata_list* list, size_t i)
{
	if (i >= list->count)
	{
		printf("System: object is not in list\n");
		return;
	}

	for (size_t s = i; s < list->count; ++s)
		list->units[s] = list->units[s + 1];

	list->count -= 1;
}

void* my_malloc(size_t size)
{
	if (size > 0)
	{
		list_merge(&tmp_list, &free_list);

		int max_count = tmp_list.count > free_list.count ? tmp_list.count : free_list.count;
		free_list.count = tmp_list.count;

		for (int i = 0; i < max_count; i++)
			free_list.units[i] = tmp_list.units[i];

		for (int i = 0; i < free_list.count; i++)
		{
			unit chunk = free_list.units[i];

			if (chunk.size >= size)
			{
				list_remove(&free_list, i);

				size_t available_units_size = chunk.size - size;
				list_insert(&alloc_list, chunk.ptr, size);

				if (available_units_size > 0)
					list_insert(&free_list, chunk.ptr + size, available_units_size);

				return chunk.ptr;
			}
		}
	}
	else
		return NULL;
}

void collector(int i)
{
	char* start = alloc_list.units[i].ptr;
	int size = alloc_list.units[i].size;

	for (int k = 0; k < size; k++)
		*(start + k) = NULL;

	return;
}

void my_free(void* ptr)
{
	if (ptr != NULL)
	{
		int pos = list_find(&alloc_list, ptr);

		if (pos < 0 || ptr != alloc_list.units[pos].ptr)
		{
			printf("System: can't free memory\n");
			return;
		}

		list_insert(&free_list, alloc_list.units[pos].ptr, alloc_list.units[pos].size);
		collector(pos);
		list_remove(&alloc_list, (size_t)pos);
	}
}

void* my_realloc(void* ptr, size_t size)
{
	size_t previous_size = alloc_list.units[(size_t)list_find(&alloc_list, ptr)].size;
	size_t min_size = previous_size > size ? size : previous_size;

	if (previous_size <= 0 || size == 0)
		return NULL;
 
	char* phase_ptr = (char*)my_malloc(previous_size + size - min_size);

	char* tmp = (char*)my_malloc(min_size);
	for (int i = 0; i < min_size; i++)
		tmp[i] = *((char*)ptr + i);

	my_free(ptr);
	
	if (phase_ptr != NULL)
		my_free(phase_ptr);

	char* result_ptr = (char*)my_malloc(size);

	for (int i = 0; i < min_size; i++)
		*(result_ptr + i) = tmp[i];

	my_free(tmp);
	
	return (void*)result_ptr;
}
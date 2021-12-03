#pragma once

/*Description: Memory manager emulator.
my_malloc: returns a void pointer to the allocated space, or NULL if there is insufficient memory available.
free: freeing memory by the pointer, making its available again
realloc: returns a void pointer to the reallocated space of new size, or NULL if it imposible or new size == 0
Structures:
metadata_list: using to realtime manage the pointers, information about them are consisting in units.
UNIT_LIST_SIZE = max number of allocations in Single time.
*/

#define HEAP_SIZE 1000000
#define UNIT_LIST_SIZE 1000

typedef struct
{
	size_t size;
	char* ptr;
} unit;

typedef struct
{
	size_t count;
	unit* units;
} metadata_list;

metadata_list alloc_list;
metadata_list free_list;
metadata_list tmp_list;

char* heap;

void* my_malloc(size_t size);

void* my_realloc(void* ptr, size_t size);

void my_free(void* ptr);

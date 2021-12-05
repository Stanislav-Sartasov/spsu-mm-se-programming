#ifndef TASK_2_MEMORY_MANAGER_H
#define TASK_2_MEMORY_MANAGER_H

#include <stdlib.h>

#define INITIAL_SIZE 1000

typedef struct memory_manager_block
{
	int is_free;
	size_t size;
} mm_block;

void init();

void free_memory();

void *my_malloc(size_t size);

void *my_calloc(size_t nmemb, size_t size);

void *my_realloc(void *ptr, size_t size);

void my_free(void *ptr);

#endif //TASK_2_MEMORY_MANAGER_H

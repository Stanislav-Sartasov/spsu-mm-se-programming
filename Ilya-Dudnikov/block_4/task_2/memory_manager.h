#ifndef TASK_2_MEMORY_MANAGER_H
#define TASK_2_MEMORY_MANAGER_H

#include <stdlib.h>
#include <string.h>

#define INITIAL_SIZE 8192

#pragma pack(1)
#pragma push
typedef struct memory_manager_block
{
	size_t size;
	char is_free;
} mm_block;
#pragma pop

void init();

void free_memory();

void *my_malloc(size_t size);

void *my_realloc(void *ptr, size_t size);

void my_free(void *ptr);

#endif //TASK_2_MEMORY_MANAGER_H

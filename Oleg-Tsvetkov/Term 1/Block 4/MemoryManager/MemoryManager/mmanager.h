#ifndef MEMORYMANAGER_MMANAGER_H
#define MEMORYMANAGER_MMANAGER_H

#include <stdlib.h>
#include "mem_block.h"

#define SPACE_BYTES_SIZE 1024

struct memory_manager
{
	void *space;
	memory_block *start_block;

};

typedef struct memory_manager memory_manager;

void init();

void *my_malloc(size_t size);

void *my_realloc(void *data, size_t size);

void my_free(void *data);

void free_init_space();


#endif //MEMORYMANAGER_MMANAGER_H

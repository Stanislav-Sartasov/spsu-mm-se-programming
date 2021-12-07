#ifndef MEMORYMANAGER_MEM_BLOCK_H
#define MEMORYMANAGER_MEM_BLOCK_H

#include <stdbool.h>

struct memory_block
{
	size_t size;
	struct memory_block *previous_block;
	struct memory_block *next_block;
	void *data;
};

typedef struct memory_block memory_block;

#endif //MEMORYMANAGER_MEM_BLOCK_H

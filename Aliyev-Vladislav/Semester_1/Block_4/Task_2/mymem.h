#ifndef MYMEM_H
#define MYMEM_H

#include <stdint.h>

#define M_SIZE 256
#define INFO sizeof(block_memory)

typedef unsigned char byte;

typedef struct block_memory
{
	size_t size;
	void* allocation;
	struct block_memory* next; // pointer to next reserved block
}block_memory;

void init();

void* my_malloc(size_t size);

void my_free(void* ptr);

void* my_realloc(void* ptr, size_t size);

void end();

#endif
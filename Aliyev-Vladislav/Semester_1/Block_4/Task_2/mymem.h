#ifndef MYMEM_H
#define MYMEM_H

#include <stdlib.h>
#include <stdint.h>

#define M_SIZE 100

struct block_memory
{
	int size;
	char allocation;
};

int8_t* buffer;

void init();

void* my_malloc(size_t size);

void my_free(void* ptr);

void* my_realloc(void* ptr, size_t size);

void end();

#endif
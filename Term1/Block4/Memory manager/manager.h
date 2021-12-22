#pragma once

#include <stdint.h>

#define MEM_SIZE 4096
#define BLOCK_INFO sizeof(mem_block)

typedef unsigned char byte;

#pragma pack(push, 1)
typedef struct mem_block
{
	size_t size;
	void* data; 
	struct mem_block* next; // pointer to next reserved block
}mem_block;
#pragma pack(pop)

void init();
void free_memory();

void* my_malloc(size_t size);
void my_free(void* ptr);
void* my_realloc(void* ptr, size_t size);
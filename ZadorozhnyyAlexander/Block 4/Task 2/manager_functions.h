#pragma once
#include <stdio.h>

#define SIZE_OF_SPACE 1024

typedef struct mm_block
{
	size_t block_size;
	char is_empty;
	struct mm_block* next_block;
	struct mm_block* prev_block;
	char* data;
} memory_block;

char* manager_free_space;

void* malloc_realization(unsigned int size);

void* realloc_realization(void* ptr, unsigned int size);

void free_realization(void* ptr);

void free_init_space();


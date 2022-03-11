#ifndef MMANAGER_H
#define MMANAGER_H

#pragma once

#include <stdlib.h>

#define byte unsigned char
#define M_SIZE 100

// memory block type
typedef struct memory_block
{
	struct memory_block* next;
	size_t size;
} mblock_t;

// memory manager type
typedef struct memory_manager
{
	mblock_t* first;
	byte* data;
} mmanager_t;

// global memory manager
mmanager_t mmanager;

// init global variables
void init();

// free global variables
void end();

// my malloc
void* my_malloc(size_t size);

// my realloc
void* my_realloc(void* ptr, size_t size);

// my free
void my_free(void* ptr);

// visualize memory heap
void visualize();

#endif // MMANAGER_H
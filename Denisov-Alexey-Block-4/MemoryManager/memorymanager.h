#include <stdio.h>
#include <stdlib.h>

#ifndef MEMORY_MANAGER
#define MEMORY_MANAGER

typedef unsigned char BYTE;
#define MEMORY_SIZE 100

BYTE* memory;

struct block
{
	int isOccupied;
	int size;
};
typedef struct block block;

void init();

void deinit();

void* myMalloc(size_t size);

void myFree(void* pointer);

void* myRealloc(void* pointer, size_t size);

#endif // MEMORY_MANAGER
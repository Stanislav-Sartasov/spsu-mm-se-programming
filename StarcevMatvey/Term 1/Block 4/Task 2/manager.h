#pragma once
#define SIZE_OF_SPACE 256
#define SIZE_OF_BLOCK 12

struct memory_block
{
	int size;
	int flag;
	struct memory_block* next;
	char* data[1];
};
typedef struct memory_block block;

char* space;

void* my_malloc(int size);

void* my_calloc(int size);

void* my_realloc(void* pointer, int size);

void my_free(void* pointer);

void init();

void free_init();
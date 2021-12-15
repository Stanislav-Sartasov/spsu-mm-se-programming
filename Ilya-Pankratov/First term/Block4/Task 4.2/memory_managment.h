#pragma once

#define DEFAULT_MEMORY_SIZE 256
#define HEADER_SIZE 12

typedef struct memory_block
{
	// header of the memory block 

	int size;
	int is_used;
	struct memory_block* next;

	// allocated memory for user data

	char* data[1];

}memory_block;

void unit();

void free_unit();

void unite_empety_blocks(memory_block* curr_block);

void my_free(void* pointer);

void* my_malloc(int size);

void* my_calloc(int size);

void* my_realloc(void* pointer, int size); // returns a pointer to user data on error

void show_memory_situation();
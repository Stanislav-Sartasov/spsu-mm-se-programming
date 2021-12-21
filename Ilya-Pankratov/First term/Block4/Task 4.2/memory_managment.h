#pragma once

#define DEFAULT_MEMORY_SIZE 256
#define HEADER_SIZE 12
#define THE_SMALLEST_SIZE_OF_ALLOCATED_MEMORY 16
#define OFFSET_TO_ISUSED_FROM_USER_DATA 8
#define OFFSET_TO_STRUCT_ADRESS_FROM_USER_DATA 12

/*
	Explanation for defines 

	The struct looks like |size - 4 bytes|is_used - 4 bytes|next - 4 bytes|beginning of user data|

	That's why the smallest size of memory to create a memory block is 4 16 bytes, 12 bytes for header and 4 bytes 
	is the smallest size of allocated memory by user.

	User always gives us the address of the beginning of user data so to change the element is_used we need to make the 8 bytes offset.

	The same situation with offset to the struct. We get the address of element size by 12 bytes offset, but adress of struct and adress of 
	struct are the same. That's why we may convert int* to the memory_block*.
*/

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

void my_free(void* pointer);

void* my_malloc(int size);

void* my_calloc(int size);

void* my_realloc(void* pointer, int size); // returns a pointer to user data on error

void show_memory_situation();
#ifndef MY_MM
#define MY_MM

#define byte char

// Size of memory for memory manager (in bytes)
#define MEM_SIZE 256
#include <stdio.h>

/*
* Memory has the following structure:
* Next block stride: 4 bytes
* In use: 4 byte
* Actual data: a lot of bytes
*/

// Global variables and constants
// Beginning of array
byte* memory_begin;

// Functions
// Creates global storages
void init();

// Frees global storages
void dispose();

// Allocates memory in global array
void* my_malloc(size_t size);
// Allocates new memory in global array and copies elements there
void* my_realloc(void* ptr, size_t size);
// Frees the block
void my_free(void* ptr);

#endif
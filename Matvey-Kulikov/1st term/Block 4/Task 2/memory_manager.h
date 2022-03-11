#include <stdint.h>
#define MY_HEAP_SIZE 1024
#define HEADER_SIZE sizeof(memory_block)
#pragma pack(1)

struct memory_block
{
	uint32_t size;
	uint8_t occupied;
};

typedef struct memory_block memory_block;

void init();

void free_heap();

void print_blocks();

void split(void* pointer, uint32_t size);

void* my_malloc(uint32_t size);

void* my_free(void* pointer);

void* my_realloc(void* pointer, uint32_t size);

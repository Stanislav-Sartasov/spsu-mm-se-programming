#include <stdbool.h>
#include <stdlib.h>

#define HEAP_SIZE 655360
#define BLOCK_SIZE sizeof(allocated_block_t)

typedef char byte;
typedef struct allocated_block
{
	size_t size;
	struct allocated_block* next_block;
	struct allocated_block* prev_block;
	void* ptr;
	bool is_free;
} allocated_block_t;

byte* heap;
allocated_block_t* initial_block;

void* my_malloc(size_t size);
void *my_memcpy(void *destination, const void *source, size_t n);
void my_free(void* ptr);
void* my_realloc(void* ptr, size_t size);
void init();
void destroy();
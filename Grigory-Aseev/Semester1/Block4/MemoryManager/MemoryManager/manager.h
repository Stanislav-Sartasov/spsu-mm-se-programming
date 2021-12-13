#pragma once
#include <stdlib.h>
#include <stdint.h>
#include <stdbool.h>

#define BLOCK_SIZE sizeof(alloc_blocks)
#define HEAP_SIZE 65536

typedef struct alloc_blocks
{
    size_t size;
    struct alloc_blocks* next;
    bool free;
    int8_t* point;
}alloc_blocks;

int8_t* heap;
alloc_blocks *allocs;
uint32_t occupied_blocks;
void init();
void* my_malloc(size_t size);
void my_free(void* ptr);
void init_free();
void* my_realloc(void* ptr, size_t size);
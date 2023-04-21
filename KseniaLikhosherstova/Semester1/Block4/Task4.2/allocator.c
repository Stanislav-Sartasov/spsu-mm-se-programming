#include "allocator.h"

#include <assert.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>


struct
{
    union
    {
        struct block* block;
        int8_t* bytes;
    };
} alloc = { 0 };



void init()
{
    assert(MEMORY_SIZE >= 2 * BLOCK_SIZE && "Too little memory!");
    alloc.bytes = malloc(MEMORY_SIZE);
    if (alloc.bytes == NULL)
    {
        fprintf(stderr, "Can't allocate %lu bytes on heap!\n", MEMORY_SIZE);
        exit(1);
    }

    alloc.block->size = MEMORY_SIZE - BLOCK_SIZE;
    alloc.block->used = false;
    alloc.block->prev = NULL;
    alloc.block->next = NULL;
    alloc.block->data = alloc.bytes + BLOCK_SIZE;
}


void deinit()
{
    free(alloc.bytes);
    alloc.bytes = NULL;
}

size_t allignment(size_t n)
{
    size_t word = sizeof(intptr_t);
    return n % word == 0 ? 0 : word - (n % word);
}


struct block* split_block(struct block* block, size_t size)
{
    struct block* next = (struct block*)(block->data + size);
    next->size = block->size - size - BLOCK_SIZE;
    next->used = false;
    next->prev = block;
    next->next = block->next;
    if (block->next)
    {
        block->next->prev = next;
    }
    next->data = ((int8_t*)next) + BLOCK_SIZE;

    block->size = size;
    block->used = true;
    block->next = next;

    return block;
}


struct block* find_free_block(size_t size)
{
    struct block* curr = alloc.block;
    while (curr && (curr->used || curr->size < size))
    {
        curr = curr->next;
    }

    if (curr->size - size >= 2 * BLOCK_SIZE)
    {
        return split_block(curr, size);
    }

    return curr;
}


void* my_malloc(size_t size)
{
    if (size == 0)
    {
        return NULL;
    }

    size = size + allignment(size);

    struct block* curr = find_free_block(size);
    if (curr == NULL)
    {
        return NULL;
    }

    curr->used = true;
    return curr->data;
}


struct block* merge_blocks(struct block* a, struct block* b)
{
    a->size += b->size + BLOCK_SIZE;
    a->next = b->next;
    if (b->next)
    {
        b->next->prev = a;
    }
    return a;
}


struct block* get_block(void* ptr)
{
    return (struct block*)((int8_t*)ptr - BLOCK_SIZE);
}


void my_free(void* ptr)
{
    if (ptr == NULL)
    {
        return;
    }

    struct block* block = get_block(ptr);
    block->used = false;

    while (block->prev && !block->prev->used)
    {
        block = merge_blocks(block->prev, block);
    }

    while (block->next && !block->next->used)
    {
        block = merge_blocks(block, block->next);
    }
}

void* my_realloc(void* ptr, size_t size)
{
    size = size + allignment(size);

    struct block* block = get_block(ptr);
    if (block->size >= size)
    {
        return block->data;
    }

    struct block* free = find_free_block(size);
    if (free == NULL)
    {
        return NULL;
    }

    free->used = true;
    memcpy(free->data, ptr, size);

    my_free(ptr);

    return free->data;
}
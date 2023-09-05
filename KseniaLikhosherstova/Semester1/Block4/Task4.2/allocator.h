#pragma once

#include <stddef.h>
#include <assert.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define BLOCK_SIZE sizeof(struct block)
#define MEMORY_SIZE 256


struct block
{
    size_t size;
    bool used;
    struct block* prev;
    struct block* next;
    int8_t* data;
};



void* my_malloc(size_t size);
void my_free(void* ptr);
void* my_realloc(void* ptr, size_t size);

void init();
void deinit();
